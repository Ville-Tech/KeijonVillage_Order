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
            txtHaku.TextChanged += txtHaku_TextChanged;
            LataaAsiakkaat();
        }

        private void MaaritaGrid()
        {
            dgvAsiakkaat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsiakkaat.MultiSelect = false;
            dgvAsiakkaat.ReadOnly = true;
            dgvAsiakkaat.AllowUserToAddRows = false;
            dgvAsiakkaat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            foreach (DataGridViewColumn col in dgvAsiakkaat.Columns)
                col.Visible = false;

            if (dgvAsiakkaat.Columns["Asiakas_ID"] != null)
            {
                dgvAsiakkaat.Columns["Asiakas_ID"].Visible = true;
                dgvAsiakkaat.Columns["Asiakas_ID"].HeaderText = "ID";
                dgvAsiakkaat.Columns["Asiakas_ID"].Width = 40;
                dgvAsiakkaat.Columns["Asiakas_ID"].DisplayIndex = 0;
            }
            if (dgvAsiakkaat.Columns["Etunimi"] != null)
            {
                dgvAsiakkaat.Columns["Etunimi"].Visible = true;
                dgvAsiakkaat.Columns["Etunimi"].HeaderText = "Etunimi";
                dgvAsiakkaat.Columns["Etunimi"].DisplayIndex = 1;
            }
            if (dgvAsiakkaat.Columns["Sukunimi"] != null)
            {
                dgvAsiakkaat.Columns["Sukunimi"].Visible = true;
                dgvAsiakkaat.Columns["Sukunimi"].HeaderText = "Sukunimi";
                dgvAsiakkaat.Columns["Sukunimi"].DisplayIndex = 2;
            }
            if (dgvAsiakkaat.Columns["Lahiosoite"] != null)
            {
                dgvAsiakkaat.Columns["Lahiosoite"].Visible = true;
                dgvAsiakkaat.Columns["Lahiosoite"].HeaderText = "Osoite";
                dgvAsiakkaat.Columns["Lahiosoite"].DisplayIndex = 3;
            }
            if (dgvAsiakkaat.Columns["Postinro"] != null)
            {
                dgvAsiakkaat.Columns["Postinro"].Visible = true;
                dgvAsiakkaat.Columns["Postinro"].HeaderText = "Postinro";
                dgvAsiakkaat.Columns["Postinro"].DisplayIndex = 4;
            }
            if (dgvAsiakkaat.Columns["Sahkoposti"] != null)
            {
                dgvAsiakkaat.Columns["Sahkoposti"].Visible = true;
                dgvAsiakkaat.Columns["Sahkoposti"].HeaderText = "Sähköposti";
                dgvAsiakkaat.Columns["Sahkoposti"].DisplayIndex = 5;
            }
            if (dgvAsiakkaat.Columns["Puhelin"] != null)
            {
                dgvAsiakkaat.Columns["Puhelin"].Visible = true;
                dgvAsiakkaat.Columns["Puhelin"].HeaderText = "Puhelin";
                dgvAsiakkaat.Columns["Puhelin"].DisplayIndex = 6;
            }
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