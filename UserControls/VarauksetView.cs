using System;
using System.Linq;
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

        public VarauksetView()
        {
            InitializeComponent();
            MaaritaGrid();
            txtHaku.TextChanged += txtHaku_TextChanged;
            LataaDropdownit();
            LataaVaraukset();
        }

        private void MaaritaGrid()
        {
            dgvVaraukset.AutoGenerateColumns = false;
            dgvVaraukset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVaraukset.MultiSelect = false;
            dgvVaraukset.ReadOnly = true;
            dgvVaraukset.AllowUserToAddRows = false;
            dgvVaraukset.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVaraukset.Columns.Clear();

            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Varaus_ID",
                HeaderText = "ID",
                Width = 40
            });

            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Asiakas",
                HeaderText = "Asiakas"
            });

            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Mokki",
                HeaderText = "Mökki"
            });

            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Alkaa",
                HeaderText = "Alkaa"
            });

            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Loppuu",
                HeaderText = "Loppuu"
            });
        }

        private void LataaDropdownit()
        {
            cmbAsiakas.DataSource = _asiakasService.HaeAsiakkaat();
            cmbAsiakas.DisplayMember = "KokoNimi";
            cmbAsiakas.ValueMember = "Asiakas_ID";
            cmbAsiakas.SelectedIndex = -1;

            cmbMokki.DataSource = _mokkiService.HaeMokit();
            cmbMokki.DisplayMember = "Mokkinimi";
            cmbMokki.ValueMember = "Mokki_ID";
            cmbMokki.SelectedIndex = -1;
        }

        private void LataaVaraukset(string hakusana = "")
        {
            var varaukset = _varausService.HaeVaraukset();
            var asiakkaat = _asiakasService.HaeAsiakkaat();
            var mokit = _mokkiService.HaeMokit();

            var nakyma = varaukset.Select(v => new
            {
                v.Varaus_ID,
                Asiakas = asiakkaat.FirstOrDefault(a => a.Asiakas_ID == v.Asiakas_ID)?.KokoNimi ?? "-",
                Mokki = mokit.FirstOrDefault(m => m.Mokki_ID == v.Mokki_ID)?.Mokkinimi ?? "-",
                Alkaa = v.Varattu_Alkupvm?.ToString("dd.MM.yyyy") ?? "-",
                Loppuu = v.Varattu_Loppupvm?.ToString("dd.MM.yyyy") ?? "-"
            }).ToList();

            if (!string.IsNullOrWhiteSpace(hakusana))
            {
                nakyma = nakyma.Where(n =>
                    n.Asiakas.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                    n.Mokki.Contains(hakusana, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            dgvVaraukset.DataSource = nakyma;
        }

        private void txtHaku_TextChanged(object sender, EventArgs e)
        {
            LataaVaraukset(txtHaku.Text);
        }

        private Varaus? LomakkeestaMokki()
        {
            if (cmbAsiakas.SelectedValue == null || cmbMokki.SelectedValue == null)
            {
                MessageBox.Show("Valitse asiakas ja mökki.");
                return null;
            }

            if (dtpLoppu.Value.Date <= dtpAlku.Value.Date)
            {
                MessageBox.Show("Lähtöpäivän pitää olla saapumispäivän jälkeen.");
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
