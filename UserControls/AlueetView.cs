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
        private readonly MokkiService _mokkiService = new MokkiService();

        public AlueetView()
        {
            InitializeComponent();
        }

        private void AlueetView_Load(object sender, EventArgs e)
        {
            LataaAlueet();
        }

        private void LataaAlueet()
        {
            try
            {
                var alueet = _alueService.HaeAlueet();

                // Täytä listbox
                listBoxVasen.DisplayMember = "Nimi";
                listBoxVasen.ValueMember = "Alue_ID";
                listBoxVasen.DataSource = alueet;

                // Tilastot
                var mokit = _mokkiService.HaeMokit();
                int yhteensa = alueet.Count;
                int aktiiviset = 0;
                foreach (var a in alueet)
                {
                    if (mokit.Exists(m => m.Alue_ID == a.Alue_ID))
                        aktiiviset++;
                }
                int tyhjai = yhteensa - aktiiviset;

                lblAlueitaKpl.Text = $"Alueita:      {yhteensa} kpl";
                lblAktiivisetKpl.Text = $"Aktiiviset:   {aktiiviset} kpl";
                lblTyhjatKpl.Text = $"Tyhjät:       {tyhjai} kpl";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tietojen haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxVasen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxVasen.SelectedItem is Alue valittu)
            {
                lblNimi.Text = valittu.Nimi ?? "-";
            }
        }
    }
}