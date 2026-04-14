using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class AlueetView : UserControl
    {
        private readonly AlueService _alueService = new AlueService();

        public AlueetView()
        {
            InitializeComponent();
            MaaritaGrid();
            dgvAlueet.DataBindingComplete += DgvAlueet_DataBindingComplete;
            txtHaku.TextChanged += txtHaku_TextChanged;
            LataaAlueet();
        }

        private void MaaritaGrid()
        {
            dgvAlueet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlueet.MultiSelect = false;
            dgvAlueet.ReadOnly = true;
            dgvAlueet.AllowUserToAddRows = false;
            dgvAlueet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LataaAlueet(string hakusana = "")
        {
            try
            {
                var alueet = _alueService.HaeAlueet() ?? new List<Alue>();

                if (!string.IsNullOrWhiteSpace(hakusana))
                {
                    alueet = alueet.FindAll(a =>
                        a != null &&
                        !string.IsNullOrEmpty(a.Nimi) &&
                        a.Nimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase));
                }

                dgvAlueet.SelectionChanged -= DgvAlueet_SelectionChanged;

                dgvAlueet.DataSource = null;
                dgvAlueet.DataSource = alueet;

                dgvAlueet.SelectionChanged += DgvAlueet_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tietojen haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAlueet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvAlueet.Columns.Count > 0)
            {
                dgvAlueet.Columns[0].HeaderText = "ID";
                dgvAlueet.Columns[0].Width = 50;
            }

            if (dgvAlueet.Columns.Count > 1)
            {
                dgvAlueet.Columns[1].HeaderText = "Alueen nimi";
            }
        }

        private void DgvAlueet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlueet.CurrentRow == null) return;

            if (dgvAlueet.CurrentRow.DataBoundItem is Alue valittu)
                txtNimi.Text = valittu.Nimi ?? "";
        }

        private void txtHaku_TextChanged(object sender, EventArgs e)
        {
            LataaAlueet(txtHaku.Text);
        }

        private void btnLisaa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNimi.Text))
            {
                MessageBox.Show("Syötä alueen nimi.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _alueService.LisaaAlue(new Alue { Nimi = txtNimi.Text.Trim() });
                TyhjennaLomake();
                LataaAlueet();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe lisäyksessä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvAlueet.CurrentRow?.DataBoundItem is not Alue valittu)
            {
                MessageBox.Show("Valitse ensin alue listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNimi.Text))
            {
                MessageBox.Show("Nimi ei voi olla tyhjä.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                valittu.Nimi = txtNimi.Text.Trim();
                _alueService.PaivitaAlue(valittu);

                LataaAlueet();

                MessageBox.Show("Alue päivitetty!", "Onnistui",
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
            if (dgvAlueet.CurrentRow?.DataBoundItem is not Alue valittu)
            {
                MessageBox.Show("Valitse ensin alue listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var vastaus = MessageBox.Show(
                $"Poistetaanko alue \"{valittu.Nimi}\"?\nTämä voi poistaa myös alueen mökit!",
                "Vahvista poisto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (vastaus == DialogResult.Yes)
            {
                try
                {
                    _alueService.PoistaAlue(valittu.Alue_ID);

                    TyhjennaLomake();
                    LataaAlueet();
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
            txtNimi.Text = "";
            dgvAlueet.ClearSelection();
        }
    }
}