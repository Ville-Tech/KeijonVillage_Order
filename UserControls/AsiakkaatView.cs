using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class AsiakkaatView : UserControl
    {
        private readonly AsiakasService _asiakasService = new AsiakasService();

        public AsiakkaatView()
        {
            InitializeComponent();
            MaaritaGrid();
            dgvAsiakkaat.DataBindingComplete += DgvAsiakkaat_DataBindingComplete;
            LataaAsiakkaat();
        }

        private void MaaritaGrid()
        {
            dgvAsiakkaat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsiakkaat.MultiSelect = false;
            dgvAsiakkaat.ReadOnly = true;
            dgvAsiakkaat.AllowUserToAddRows = false;
            dgvAsiakkaat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvAsiakkaat.SelectionChanged += DgvAsiakkaat_SelectionChanged;
        }

        private void LataaAsiakkaat(string hakusana = "")
        {
            try
            {
                var asiakkaat = _asiakasService.HaeAsiakkaat();

                if (!string.IsNullOrWhiteSpace(hakusana))
                    asiakkaat = asiakkaat.FindAll(a =>
                        a != null &&
                        (
                            (!string.IsNullOrEmpty(a.Sukunimi) && a.Sukunimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(a.Etunimi) && a.Etunimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(a.Sahkoposti) && a.Sahkoposti.Contains(hakusana, StringComparison.OrdinalIgnoreCase))
                        ));

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
            if (dgvAsiakkaat.Columns.Count > 0)
            {
                dgvAsiakkaat.Columns[0].HeaderText = "ID";
                dgvAsiakkaat.Columns[0].Width = 40;
            }

            if (dgvAsiakkaat.Columns.Count > 1)
                dgvAsiakkaat.Columns[1].HeaderText = "Etunimi";

            if (dgvAsiakkaat.Columns.Count > 2)
                dgvAsiakkaat.Columns[2].HeaderText = "Sukunimi";

            if (dgvAsiakkaat.Columns.Count > 3)
                dgvAsiakkaat.Columns[3].HeaderText = "Osoite";

            if (dgvAsiakkaat.Columns.Count > 4)
                dgvAsiakkaat.Columns[4].HeaderText = "Postinro";

            if (dgvAsiakkaat.Columns.Count > 5)
                dgvAsiakkaat.Columns[5].HeaderText = "Sähköposti";

            if (dgvAsiakkaat.Columns.Count > 6)
                dgvAsiakkaat.Columns[6].HeaderText = "Puhelin";
        }

        private void DgvAsiakkaat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAsiakkaat.CurrentRow == null) return;

            if (dgvAsiakkaat.CurrentRow.DataBoundItem is Asiakas a)
            {
                txtEtunimi.Text = a.Etunimi ?? "";
                txtSukunimi.Text = a.Sukunimi ?? "";
                txtLahiosoite.Text = a.Lahiosoite ?? "";
                txtPostinro.Text = a.Postinro ?? "";
                txtEmail.Text = a.Sahkoposti ?? "";
                txtPuhelin.Text = a.Puhelin ?? "";
            }
        }

        private void txtHaku_TextChanged(object sender, EventArgs e)
        {
            LataaAsiakkaat(txtHaku.Text);
        }

        private Asiakas? LomakkeestaMokki()
        {
            if (string.IsNullOrWhiteSpace(txtEtunimi.Text) ||
                string.IsNullOrWhiteSpace(txtSukunimi.Text) ||
                string.IsNullOrWhiteSpace(txtPostinro.Text))
            {
                MessageBox.Show("Täytä pakolliset kentät: Etunimi, Sukunimi, Postinumero.",
                    "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
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

                MessageBox.Show("Asiakas lisätty!", "Onnistui",
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

            var vastaus = MessageBox.Show(
                $"Poistetaanko asiakas \"{valittu.Etunimi} {valittu.Sukunimi}\"?",
                "Vahvista poisto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (vastaus == DialogResult.Yes)
            {
                try
                {
                    _asiakasService.PoistaAsiakas(valittu.Asiakas_ID);

                    TyhjennaLomake();
                    LataaAsiakkaat();

                    MessageBox.Show("Asiakas poistettu.", "Onnistui",
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
            txtEtunimi.Text = "";
            txtSukunimi.Text = "";
            txtLahiosoite.Text = "";
            txtPostinro.Text = "";
            txtEmail.Text = "";
            txtPuhelin.Text = "";
            dgvAsiakkaat.ClearSelection();
        }
    }
}