using System;
using System.Linq;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class AsiakkaatView : UserControl
    {
        private readonly AsiakasService _asiakasService = new();

        public AsiakkaatView()
        {
            InitializeComponent();
            MaaritaGrid();
            dgvAsiakkaat.DataBindingComplete += DgvAsiakkaat_DataBindingComplete;
            txtHaku.TextChanged += txtHaku_TextChanged;
            MaaritaTabJarjestys();
            LataaAsiakkaat();
        }

        private void MaaritaGrid()
        {
            dgvAsiakkaat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsiakkaat.MultiSelect = false;
            dgvAsiakkaat.ReadOnly = true;
            dgvAsiakkaat.AllowUserToAddRows = false;
            dgvAsiakkaat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAsiakkaat.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(245, 246, 250)
            };
        }

        private void MaaritaTabJarjestys()
        {
            txtHaku.TabIndex = 0;
            txtEtunimi.TabIndex = 1;
            txtSukunimi.TabIndex = 2;
            txtLahiosoite.TabIndex = 3;
            txtPostinro.TabIndex = 4;
            txtEmail.TabIndex = 5;
            txtPuhelin.TabIndex = 6;
        }

        private void LataaAsiakkaat(string hakusana = "")
        {
            try
            {
                var asiakkaat = _asiakasService.HaeAsiakkaat();

                if (!string.IsNullOrWhiteSpace(hakusana))
                    asiakkaat = asiakkaat.FindAll(a =>
                        a != null && (
                        (!string.IsNullOrEmpty(a.Sukunimi) && a.Sukunimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(a.Etunimi) && a.Etunimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(a.Sahkoposti) && a.Sahkoposti.Contains(hakusana, StringComparison.OrdinalIgnoreCase))));

                dgvAsiakkaat.SelectionChanged -= DgvAsiakkaat_SelectionChanged;
                dgvAsiakkaat.DataSource = null;
                dgvAsiakkaat.DataSource = asiakkaat;
                dgvAsiakkaat.SelectionChanged += DgvAsiakkaat_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tietojen haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAsiakkaat_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] naytettavat = { "Asiakas_ID", "Etunimi", "Sukunimi", "Lahiosoite", "Postinro", "Sahkoposti", "Puhelin" };
            string[] otsikot = { "ID", "Etunimi", "Sukunimi", "Osoite", "Postinro", "Sähköposti", "Puhelin" };

            foreach (DataGridViewColumn col in dgvAsiakkaat.Columns)
                col.Visible = false;

            for (int i = 0; i < naytettavat.Length; i++)
            {
                if (dgvAsiakkaat.Columns[naytettavat[i]] is DataGridViewColumn c)
                {
                    c.Visible = true;
                    c.HeaderText = otsikot[i];
                    c.DisplayIndex = i;
                    if (naytettavat[i] == "Asiakas_ID") c.Width = 40;
                }
            }
        }

        private void DgvAsiakkaat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAsiakkaat.CurrentRow?.DataBoundItem is Asiakas a)
            {
                txtEtunimi.Text = a.Etunimi ?? "";
                txtSukunimi.Text = a.Sukunimi ?? "";
                txtLahiosoite.Text = a.Lahiosoite ?? "";
                txtPostinro.Text = a.Postinro ?? "";
                txtEmail.Text = a.Sahkoposti ?? "";
                txtPuhelin.Text = a.Puhelin ?? "";
            }
        }

        private void txtHaku_TextChanged(object sender, EventArgs e) => LataaAsiakkaat(txtHaku.Text);

        private Asiakas? LomakkeestaMokki()
        {
            if (string.IsNullOrWhiteSpace(txtEtunimi.Text))
            {
                MessageBox.Show("Etunimi on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEtunimi.Focus(); return null;
            }
            if (string.IsNullOrWhiteSpace(txtSukunimi.Text))
            {
                MessageBox.Show("Sukunimi on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSukunimi.Focus(); return null;
            }
            if (string.IsNullOrWhiteSpace(txtPostinro.Text))
            {
                MessageBox.Show("Postinumero on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostinro.Focus(); return null;
            }
            if (txtPostinro.Text.Length != 5 || !txtPostinro.Text.All(char.IsDigit))
            {
                MessageBox.Show("Postinumeron on oltava tasan 5 numeroa.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostinro.Focus(); return null;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains('@'))
            {
                MessageBox.Show("Sähköpostiosoite ei ole kelvollinen.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus(); return null;
            }

            return new Asiakas
            {
                Etunimi = txtEtunimi.Text.Trim(),
                Sukunimi = txtSukunimi.Text.Trim(),
                Lahiosoite = txtLahiosoite.Text.Trim(),
                Postinro = txtPostinro.Text.Trim(),
                Sahkoposti = txtEmail.Text.Trim(),
                Puhelin = txtPuhelin.Text.Trim()
            };
        }

        private void btnLisaa_Click(object sender, EventArgs e)
        {
            var a = LomakkeestaMokki();
            if (a == null) return;
            try
            {
                _asiakasService.LisaaAsiakas(a);
                TyhjennaLomake();
                LataaAsiakkaat();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe lisäyksessä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvAsiakkaat.CurrentRow?.DataBoundItem is not Asiakas valittu)
            {
                MessageBox.Show("Valitse ensin asiakas listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var a = LomakkeestaMokki();
            if (a == null) return;
            a.Asiakas_ID = valittu.Asiakas_ID;
            try
            {
                _asiakasService.PaivitaAsiakas(a);
                LataaAsiakkaat();
                MessageBox.Show("Asiakas päivitetty!", "Onnistui",
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
            if (dgvAsiakkaat.CurrentRow?.DataBoundItem is not Asiakas valittu)
            {
                MessageBox.Show("Valitse ensin asiakas listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Poistetaanko asiakas \"{valittu.KokoNimi}\"?",
                "Vahvista poisto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _asiakasService.PoistaAsiakas(valittu.Asiakas_ID);
                    TyhjennaLomake();
                    LataaAsiakkaat();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Virhe poistossa:\n{ex.Message}\n\nAsiakkaalla saattaa olla varauksia.",
                        "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTyhjenna_Click(object sender, EventArgs e) => TyhjennaLomake();

        private void TyhjennaLomake()
        {
            txtEtunimi.Text = txtSukunimi.Text = txtLahiosoite.Text =
                txtPostinro.Text = txtEmail.Text = txtPuhelin.Text = "";
            dgvAsiakkaat.ClearSelection();
        }
    }
}