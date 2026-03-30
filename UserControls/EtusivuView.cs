using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class EtusivuView : UserControl
    {
        private VarausService varausService = new VarausService();
        private LaskuService laskuService = new LaskuService();

        public EtusivuView()
        {
            InitializeComponent();
            PaivitaTilastot();
        }

        private void PaivitaTilastot()
        {
            try
            {
                var tanaan = DateTime.Today;

                // Haetaan kaikki varaukset
                var varaukset = varausService.HaeVaraukset();

                // Tänään alkavat varaukset
                int alkaa = varaukset.Count(v => v.Varattu_Alkupvm?.Date == tanaan);
                // Tänään loppuvat varaukset
                int loppuu = varaukset.Count(v => v.Varattu_Loppupvm?.Date == tanaan);

                // Maksamattomat laskut
                var laskut = laskuService.HaeLaskut();
                int maksamatta = laskut.Count(l => l.Maksettu == 0);

                // Päivitetään labelit — muuta nimet vastaamaan omiasi Designer:ssa
                lblAlkaa.Text = $"{alkaa} kpl";
                lblLoppuu.Text = $"{loppuu} kpl";
                lblLaskut.Text = $"{maksamatta} kpl";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tietojen haussa: {ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Pikanapit — muuta nimet vastaamaan omiasi Designer:ssa
        private void btnUusiVaraus_Click(object sender, EventArgs e)
        {
            // Navigoi VarauksetView:iin — haetaan MainForm ylös
            var main = this.FindForm() as MainFormView;
            main?.ShowView(new VarauksetView());
        }

        private void btnUusiAsiakas_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainFormView;
            main?.ShowView(new AsiakkaatView());
        }

        private void btnLuoLasku_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainFormView;
            main?.ShowView(new LaskutView());
        }

        private void btnUusiVaraus_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUusiAsiakas_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLuoLasku_Click_1(object sender, EventArgs e)
        {

        }
    }
}