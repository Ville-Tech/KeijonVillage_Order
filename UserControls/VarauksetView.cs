using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class VarauksetView : UserControl
    {
        // Palvelut
        private readonly VarausService _varausService = new VarausService();
        private readonly AsiakasService _asiakasService = new AsiakasService();
        private readonly MokkiService _mokkiService = new MokkiService();
        private readonly PalveluService _palveluService = new PalveluService();
        private readonly VarauksenPalvelutService _vpService = new VarauksenPalvelutService();
        private readonly AlueService _alueService = new AlueService();

        public VarauksetView()
        {
            InitializeComponent();

            // Alustetaan gridit ja dropdownit
            MaaritaGridit();
            LataaDropdownit();

            // Tapahtumat hakuun ja palveluihin
            txtHaku.TextChanged += txtHaku_TextChanged;
            btnLisaaPalvelu.Click += BtnLisaaPalvelu_Click;
            btnPoistaPalvelu.Click += BtnPoistaPalvelu_Click;

            // Oletuspäivämäärät
            dtpAlku.Value = DateTime.Today;
            dtpLoppu.Value = DateTime.Today.AddDays(1);

            LataaVaraukset();
        }

        private void MaaritaGridit()
        {
            // Varaus-gridin perusasetukset
            dgvVaraukset.AutoGenerateColumns = false;
            dgvVaraukset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVaraukset.ReadOnly = true;
            dgvVaraukset.AllowUserToAddRows = false;
            dgvVaraukset.RowHeadersVisible = false;
            dgvVaraukset.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVaraukset.Columns.Clear();
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Varaus_ID", HeaderText = "ID", Width = 40, Name = "Varaus_ID" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Asiakas", HeaderText = "Asiakas", Name = "Asiakas" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Alue", HeaderText = "Alue", Name = "Alue" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mokki", HeaderText = "Mökki", Name = "Mokki" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Alkaa", HeaderText = "Alkaa", Name = "Alkaa" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Loppuu", HeaderText = "Loppuu", Name = "Loppuu" });

            // Palvelu-gridin perusasetukset
            dgvVarauksenPalvelut.AutoGenerateColumns = false;
            dgvVarauksenPalvelut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVarauksenPalvelut.ReadOnly = true;
            dgvVarauksenPalvelut.AllowUserToAddRows = false;
            dgvVarauksenPalvelut.RowHeadersVisible = false;
            dgvVarauksenPalvelut.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVarauksenPalvelut.Columns.Clear();
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Palvelu_ID", HeaderText = "ID", Width = 40, Name = "Palvelu_ID" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nimi", HeaderText = "Palvelu", Name = "Nimi" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Lkm", HeaderText = "Lkm", Width = 50, Name = "Lkm" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Yhteensa", HeaderText = "Yhteensä", Name = "Yhteensa" });

            dgvVaraukset.SelectionChanged += DgvVaraukset_SelectionChanged;
        }

        private void LataaVaraukset(string hakusana = "")
        {
            try
            {
                var varaukset = _varausService.HaeVaraukset();
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                var mokit = _mokkiService.HaeMokit();
                var alueet = _alueService.HaeAlueet();

                var nakyma = varaukset.Select(v => {
                    var m = mokit.FirstOrDefault(x => x.Mokki_ID == v.Mokki_ID);
                    var a = alueet.FirstOrDefault(x => x.Alue_ID == m?.Alue_ID);
                    return new VarausNakyma
                    {
                        Varaus_ID = v.Varaus_ID,
                        Asiakas = asiakkaat.FirstOrDefault(x => x.Asiakas_ID == v.Asiakas_ID)?.KokoNimi ?? "-",
                        Alue = a?.Nimi ?? "-",
                        Mokki = m?.Mokkinimi ?? "-",
                        Alkaa = v.Varattu_Alkupvm?.ToString("dd.MM.yyyy") ?? "-",
                        Loppuu = v.Varattu_Loppupvm?.ToString("dd.MM.yyyy") ?? "-"
                    };
                }).ToList();

                if (!string.IsNullOrWhiteSpace(hakusana))
                {
                    nakyma = nakyma.Where(x =>
                        x.Asiakas.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                        x.Alue.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                        x.Mokki.Contains(hakusana, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                dgvVaraukset.SelectionChanged -= DgvVaraukset_SelectionChanged;
                dgvVaraukset.DataSource = null;
                dgvVaraukset.DataSource = nakyma;
                dgvVaraukset.SelectionChanged += DgvVaraukset_SelectionChanged;
            }
            catch { }
        }

        private void DgvVaraukset_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow?.Cells[0].Value is int id)
            {
                var v = _varausService.HaeVaraukset().FirstOrDefault(x => x.Varaus_ID == id);
                if (v != null)
                {
                    cmbAsiakas.SelectedValue = v.Asiakas_ID;
                    cmbMokki.SelectedValue = v.Mokki_ID;
                    dtpAlku.Value = v.Varattu_Alkupvm ?? DateTime.Today;
                    dtpLoppu.Value = v.Varattu_Loppupvm ?? DateTime.Today.AddDays(1);

                    LataaVarauksenPalvelut(id);
                }
            }
        }

        private void LataaVarauksenPalvelut(int varausId)
        {
            var vp = _vpService.HaeVarauksenPalvelut(varausId);
            var palvelut = _palveluService.HaePalvelut();

            var nakyma = vp.Select(v => {
                var p = palvelut.FirstOrDefault(x => x.Palvelu_ID == v.Palvelu_ID);
                return new VarauksenPalveluNakyma
                {
                    Palvelu_ID = v.Palvelu_ID,
                    Nimi = p?.Nimi ?? "-",
                    Lkm = v.Lkm,
                    Yhteensa = $"{(p?.Hinta ?? 0) * v.Lkm:N2} €"
                };
            }).ToList();

            dgvVarauksenPalvelut.DataSource = nakyma;
        }

        // --- NAPPIEN TOIMINNOT ---

        private void btnLisaa_Click(object sender, EventArgs e)
        {
            var v = KeraaVarausTiedot();
            if (v == null) return;

            if (!_varausService.OnkoVapaa(v.Mokki_ID, v.Varattu_Alkupvm.Value, v.Varattu_Loppupvm.Value))
            {
                MessageBox.Show("Mokki on jo varattu!");
                return;
            }

            _varausService.LisaaVaraus(v);
            LataaVaraukset();
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow?.Cells[0].Value is int id)
            {
                var v = KeraaVarausTiedot();
                if (v == null) return;
                v.Varaus_ID = id;

                _varausService.PaivitaVaraus(v);
                LataaVaraukset();
            }
        }

        private void btnPoista_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow?.Cells[0].Value is int id)
            {
                if (MessageBox.Show("Poistetaanko varaus?", "Vahvista", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _varausService.PoistaVaraus(id);
                    LataaVaraukset();
                }
            }
        }

        private void btnTyhjenna_Click(object sender, EventArgs e)
        {
            cmbAsiakas.SelectedIndex = cmbMokki.SelectedIndex = -1;
            dtpAlku.Value = DateTime.Today;
            dtpLoppu.Value = DateTime.Today.AddDays(1);
            dgvVarauksenPalvelut.DataSource = null;
        }

        private void BtnLisaaPalvelu_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null || cmbPalvelu.SelectedValue == null) return;
            if (!int.TryParse(txtLkm.Text, out int lkm)) lkm = 1;

            int varausId = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            _vpService.LisaaVarauksenPalvelu(new Varauksen_Palvelut
            {
                Varaus_ID = varausId,
                Palvelu_ID = (int)cmbPalvelu.SelectedValue,
                Lkm = lkm
            });
            LataaVarauksenPalvelut(varausId);
        }

        private void BtnPoistaPalvelu_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null || dgvVarauksenPalvelut.CurrentRow == null) return;
            int varausId = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            int palveluId = (int)dgvVarauksenPalvelut.CurrentRow.Cells[0].Value;

            _vpService.PoistaVarauksenPalvelu(varausId, palveluId);
            LataaVarauksenPalvelut(varausId);
        }

        // --- APUMETODIT ---

        private Varaus KeraaVarausTiedot()
        {
            if (cmbAsiakas.SelectedValue == null || cmbMokki.SelectedValue == null) return null;
            return new Varaus
            {
                Asiakas_ID = (int)cmbAsiakas.SelectedValue,
                Mokki_ID = (int)cmbMokki.SelectedValue,
                Varattu_Pvm = DateTime.Now,
                Varattu_Alkupvm = dtpAlku.Value.Date,
                Varattu_Loppupvm = dtpLoppu.Value.Date
            };
        }

        private void LataaDropdownit()
        {
            try
            {
                cmbAsiakas.DataSource = _asiakasService.HaeAsiakkaat();
                cmbAsiakas.DisplayMember = "KokoNimi";
                cmbAsiakas.ValueMember = "Asiakas_ID";

                cmbMokki.DataSource = _mokkiService.HaeMokit();
                cmbMokki.DisplayMember = "Mokkinimi";
                cmbMokki.ValueMember = "Mokki_ID";

                cmbPalvelu.DataSource = _palveluService.HaePalvelut();
                cmbPalvelu.DisplayMember = "Nimi";
                cmbPalvelu.ValueMember = "Palvelu_ID";

                cmbAsiakas.SelectedIndex = cmbMokki.SelectedIndex = cmbPalvelu.SelectedIndex = -1;
            }
            catch { }
        }

        private void txtHaku_TextChanged(object sender, EventArgs e) => LataaVaraukset(txtHaku.Text);
    }

    // --- NAKYMA-LUOKAT ---

    public class VarausNakyma
    {
        public int Varaus_ID { get; set; }
        public string Asiakas { get; set; }
        public string Alue { get; set; }
        public string Mokki { get; set; }
        public string Alkaa { get; set; }
        public string Loppuu { get; set; }
    }

    public class VarauksenPalveluNakyma
    {
        public int Palvelu_ID { get; set; }
        public string Nimi { get; set; }
        public int Lkm { get; set; }
        public string Yhteensa { get; set; }
    }
}