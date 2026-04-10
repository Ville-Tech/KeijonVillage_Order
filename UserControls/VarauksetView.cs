using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class VarauksetView : UserControl
    {
        private readonly VarausService _varausService = new VarausService();
        private readonly AsiakasService _asiakasService = new AsiakasService();
        private readonly MokkiService _mokkiService = new MokkiService();
        private readonly AlueService _alueService = new AlueService();

        public VarauksetView()
        {
            InitializeComponent();
            MaaritaGrid();
            LataaDropdownit();
            LataaVaraukset();

            txtHaku.TextChanged += txtHaku_TextChanged;
            dgvVaraukset.SelectionChanged -= DgvVaraukset_SelectionChanged;
            dgvVaraukset.SelectionChanged += DgvVaraukset_SelectionChanged;
        }

        private void MaaritaGrid()
        {
            dgvVaraukset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVaraukset.MultiSelect = false;
            dgvVaraukset.ReadOnly = true;
            dgvVaraukset.AllowUserToAddRows = false;
            dgvVaraukset.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LataaDropdownit()
        {
            try
            {
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                cmbAsiakas.DataSource = asiakkaat;
                cmbAsiakas.DisplayMember = "KokoNimi";
                cmbAsiakas.ValueMember = "Asiakas_ID";
                cmbAsiakas.SelectedIndex = -1;
            }
            catch { }

            try
            {
                var mokit = _mokkiService.HaeMokit();
                cmbMokki.DataSource = mokit;
                cmbMokki.DisplayMember = "Mokkinimi";
                cmbMokki.ValueMember = "Mokki_ID";
                cmbMokki.SelectedIndex = -1;
            }
            catch { }
        }

        private void LataaVaraukset(string hakusana = "")
        {
            try
            {
                var varaukset = _varausService.HaeVaraukset();
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                var mokit = _mokkiService.HaeMokit();

                var nakyma = varaukset.ConvertAll(v => new
                {
                    v.Varaus_ID,
                    Asiakas = asiakkaat.Find(a => a.Asiakas_ID == v.Asiakas_ID)
                        ?.Sukunimi ?? "-",
                    Mokki = mokit.Find(m => m.Mokki_ID == v.Mokki_ID)
                        ?.Mokkinimi ?? "-",
                    Alkaa = v.Varattu_Alkupvm?.ToString("dd.MM.yyyy") ?? "-",
                    Loppuu = v.Varattu_Loppupvm?.ToString("dd.MM.yyyy") ?? "-"
                });

                if (!string.IsNullOrWhiteSpace(hakusana))
                    nakyma = nakyma.FindAll(n =>
                        n.Asiakas.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                        n.Mokki.Contains(hakusana, StringComparison.OrdinalIgnoreCase));

                dgvVaraukset.SelectionChanged -= DgvVaraukset_SelectionChanged;
                dgvVaraukset.DataSource = null;
                dgvVaraukset.DataSource = nakyma;

                if (dgvVaraukset.Columns.Count > 0 && nakyma.Count > 0)
                {
                    dgvVaraukset.Columns[0].HeaderText = "ID";
                    dgvVaraukset.Columns[0].Width = 40;
                    dgvVaraukset.Columns[1].HeaderText = "Asiakas";
                    dgvVaraukset.Columns[2].HeaderText = "Mökki";
                    dgvVaraukset.Columns[3].HeaderText = "Alkaa";
                    dgvVaraukset.Columns[4].HeaderText = "Loppuu";
                }

                dgvVaraukset.SelectionChanged += DgvVaraukset_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe varausten haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvVaraukset_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null) return;

            int id = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            var varaukset = _varausService.HaeVaraukset();
            var v = varaukset.Find(x => x.Varaus_ID == id);
            if (v == null) return;

            cmbAsiakas.SelectedValue = v.Asiakas_ID;
            cmbMokki.SelectedValue = v.Mokki_ID;

            if (v.Varattu_Alkupvm.HasValue)
                dtpAlku.Value = v.Varattu_Alkupvm.Value;
            if (v.Varattu_Loppupvm.HasValue)
                dtpLoppu.Value = v.Varattu_Loppupvm.Value;
        }

        private void txtHaku_TextChanged(object sender, EventArgs e)
        {
            LataaVaraukset(txtHaku.Text);
        }

        private Varaus? LomakkeestaMokki()
        {
            if (cmbAsiakas.SelectedValue == null || cmbMokki.SelectedValue == null)
            {
                MessageBox.Show("Valitse asiakas ja mökki.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (dtpLoppu.Value.Date <= dtpAlku.Value.Date)
            {
                MessageBox.Show("Lähtöpäivän pitää olla saapumispäivän jälkeen.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return new Varaus
            {
                Asiakas_ID = (int)cmbAsiakas.SelectedValue,
                Mokki_ID = (int)cmbMokki.SelectedValue,
                Varattu_Pvm = DateTime.Now,
                Varattu_Alkupvm = dtpAlku.Value.Date,
                Varattu_Loppupvm = dtpLoppu.Value.Date
            };
        }

        private void btnLisaa_Click(object sender, EventArgs e)
        {
            var v = LomakkeestaMokki();
            if (v == null) return;

            // Tarkista ettei mökki ole jo varattuna
            if (!_varausService.OnkoVapaa(v.Mokki_ID, v.Varattu_Alkupvm!.Value, v.Varattu_Loppupvm!.Value))
            {
                MessageBox.Show("Mökki on jo varattuna valitulle ajanjaksolle!", "Varattu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _varausService.LisaaVaraus(v);
                TyhjennaLomake();
                LataaVaraukset();
                MessageBox.Show("Varaus lisätty!", "Onnistui",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe lisäyksessä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null)
            {
                MessageBox.Show("Valitse ensin varaus listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var v = LomakkeestaMokki();
            if (v == null) return;
            v.Varaus_ID = (int)dgvVaraukset.CurrentRow.Cells[0].Value;

            try
            {
                _varausService.PaivitaVaraus(v);
                LataaVaraukset();
                MessageBox.Show("Varaus päivitetty!", "Onnistui",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe päivityksessä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPoista_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null)
            {
                MessageBox.Show("Valitse ensin varaus listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            string asiakas = dgvVaraukset.CurrentRow.Cells[1].Value?.ToString() ?? "?";

            var vastaus = MessageBox.Show(
                $"Poistetaanko asiakkaan \"{asiakas}\" varaus?",
                "Vahvista poisto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (vastaus == DialogResult.Yes)
            {
                try
                {
                    _varausService.PoistaVaraus(id);
                    TyhjennaLomake();
                    LataaVaraukset();
                    MessageBox.Show("Varaus poistettu.", "Onnistui",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Virhe poistossa:\n{ex.Message}", "Virhe",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTyhjenna_Click(object sender, EventArgs e)
        {
            TyhjennaLomake();
        }

        private void TyhjennaLomake()
        {
            cmbAsiakas.SelectedIndex = -1;
            cmbMokki.SelectedIndex = -1;
            dtpAlku.Value = DateTime.Today;
            dtpLoppu.Value = DateTime.Today.AddDays(1);
            dgvVaraukset.ClearSelection();
        }
    }
}