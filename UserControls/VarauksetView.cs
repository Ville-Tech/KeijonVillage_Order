using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class VarauksetView : UserControl
    {
        private readonly VarausService _varausService = new();
        private readonly AsiakasService _asiakasService = new();
        private readonly MokkiService _mokkiService = new();
        private readonly PalveluService _palveluService = new();
        private readonly VarauksenPalvelutService _vpService = new();
        private readonly AlueService _alueService = new();

        public VarauksetView()
        {
            InitializeComponent();
            MaaritaGridit();
            LataaDropdownit();

            txtHaku.TextChanged += txtHaku_TextChanged;
            btnLisaaPalvelu.Click += BtnLisaaPalvelu_Click;
            btnPoistaPalvelu.Click += BtnPoistaPalvelu_Click;

            dtpAlku.Value = DateTime.Today;
            dtpLoppu.Value = DateTime.Today.AddDays(1);

            LataaVaraukset();
        }

        // ── Grid-asetukset ────────────────────────────────────────────────────
        private void MaaritaGridit()
        {
            KonfiguroiGrid(dgvVaraukset);
            dgvVaraukset.Columns.Clear();
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Varaus_ID", HeaderText = "ID", Width = 40, Name = "Varaus_ID" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Asiakas", HeaderText = "Asiakas", Name = "Asiakas" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Alue", HeaderText = "Alue", Name = "Alue" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mokki", HeaderText = "Mökki", Name = "Mokki" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Alkaa", HeaderText = "Alkaa", Name = "Alkaa" });
            dgvVaraukset.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Loppuu", HeaderText = "Loppuu", Name = "Loppuu" });

            KonfiguroiGrid(dgvVarauksenPalvelut);
            dgvVarauksenPalvelut.Columns.Clear();
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Palvelu_ID", HeaderText = "ID", Width = 40, Name = "Palvelu_ID" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nimi", HeaderText = "Palvelu", Name = "Nimi" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Lkm", HeaderText = "Lkm", Width = 50, Name = "Lkm" });
            dgvVarauksenPalvelut.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Yhteensa", HeaderText = "Yhteensä", Name = "Yhteensa" });

            dgvVaraukset.SelectionChanged += DgvVaraukset_SelectionChanged;
        }

        private static void KonfiguroiGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(245, 246, 250)
            };
        }

        // ── Data ──────────────────────────────────────────────────────────────
        private void LataaVaraukset(string hakusana = "")
        {
            try
            {
                var varaukset = _varausService.HaeVaraukset();
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                var mokit = _mokkiService.HaeMokit();
                var alueet = _alueService.HaeAlueet();

                var nakyma = varaukset.Select(v =>
                {
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
                    nakyma = nakyma.Where(x =>
                        x.Asiakas.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                        x.Alue.Contains(hakusana, StringComparison.OrdinalIgnoreCase) ||
                        x.Mokki.Contains(hakusana, StringComparison.OrdinalIgnoreCase)).ToList();

                dgvVaraukset.SelectionChanged -= DgvVaraukset_SelectionChanged;
                dgvVaraukset.DataSource = null;
                dgvVaraukset.DataSource = nakyma;
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

            dgvVarauksenPalvelut.DataSource = vp.Select(v =>
            {
                var p = palvelut.FirstOrDefault(x => x.Palvelu_ID == v.Palvelu_ID);
                return new VarauksenPalveluNakyma
                {
                    Palvelu_ID = v.Palvelu_ID,
                    Nimi = p?.Nimi ?? "-",
                    Lkm = v.Lkm,
                    Yhteensa = $"{(p?.Hinta ?? 0) * v.Lkm:N2} €"
                };
            }).ToList();
        }

        // ── Nappien toiminnot ─────────────────────────────────────────────────
        private void btnLisaa_Click(object sender, EventArgs e)
        {
            var v = KeraaVarausTiedot();
            if (v == null) return;

            try
            {
                _varausService.LisaaVaraus(v);
                LataaVaraukset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow?.Cells[0].Value is not int id)
            {
                MessageBox.Show("Valitse ensin varaus listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var v = KeraaVarausTiedot();
            if (v == null) return;
            v.Varaus_ID = id;

            try
            {
                _varausService.PaivitaVaraus(v);
                LataaVaraukset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPoista_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow?.Cells[0].Value is not int id)
            {
                MessageBox.Show("Valitse ensin varaus listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Poistetaanko varaus? Myös mahdollinen lasku poistetaan.",
                "Vahvista", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _varausService.PoistaVaraus(id);
                    dgvVarauksenPalvelut.DataSource = null;
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
            cmbAsiakas.SelectedIndex = cmbMokki.SelectedIndex = -1;
            dtpAlku.Value = DateTime.Today;
            dtpLoppu.Value = DateTime.Today.AddDays(1);
            dgvVarauksenPalvelut.DataSource = null;
            dgvVaraukset.ClearSelection();
        }

        private void BtnLisaaPalvelu_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null)
            {
                MessageBox.Show("Valitse ensin varaus.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPalvelu.SelectedValue == null)
            {
                MessageBox.Show("Valitse palvelu.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLkm.Text, out int lkm) || lkm <= 0)
            {
                MessageBox.Show("Lukumäärän on oltava positiivinen kokonaisluku.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLkm.Focus(); return;
            }

            int varausId = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            try
            {
                _vpService.LisaaVarauksenPalvelu(new Varauksen_Palvelut
                {
                    Varaus_ID = varausId,
                    Palvelu_ID = (int)cmbPalvelu.SelectedValue,
                    Lkm = lkm
                });
                LataaVarauksenPalvelut(varausId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe palvelun lisäyksessä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPoistaPalvelu_Click(object sender, EventArgs e)
        {
            if (dgvVaraukset.CurrentRow == null || dgvVarauksenPalvelut.CurrentRow == null) return;

            int varausId = (int)dgvVaraukset.CurrentRow.Cells[0].Value;
            int palveluId = (int)dgvVarauksenPalvelut.CurrentRow.Cells[0].Value;

            try
            {
                _vpService.PoistaVarauksenPalvelu(varausId, palveluId);
                LataaVarauksenPalvelut(varausId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe palvelun poistossa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Apumetodit ────────────────────────────────────────────────────────
        private Varaus? KeraaVarausTiedot()
        {
            if (cmbAsiakas.SelectedValue == null)
            {
                MessageBox.Show("Valitse asiakas.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAsiakas.Focus(); return null;
            }
            if (cmbMokki.SelectedValue == null)
            {
                MessageBox.Show("Valitse mökki.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMokki.Focus(); return null;
            }
            if (dtpAlku.Value.Date >= dtpLoppu.Value.Date)
            {
                MessageBox.Show("Loppupäivän on oltava alkupäivän jälkeen.", "Huomio",
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
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe dropdownien latauksessa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtHaku_TextChanged(object sender, EventArgs e) => LataaVaraukset(txtHaku.Text);
    }

    public class VarausNakyma
    {
        public int Varaus_ID { get; set; }
        public string Asiakas { get; set; } = "";
        public string Alue { get; set; } = "";
        public string Mokki { get; set; } = "";
        public string Alkaa { get; set; } = "";
        public string Loppuu { get; set; } = "";
    }

    public class VarauksenPalveluNakyma
    {
        public int Palvelu_ID { get; set; }
        public string Nimi { get; set; } = "";
        public int Lkm { get; set; }
        public string Yhteensa { get; set; } = "";
    }
}