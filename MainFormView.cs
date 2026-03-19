using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VillageNewbies_Projekti.Views;

namespace VillageNewbies_Projekti
{
    public partial class MainFormView : Form
    {
        public MainFormView()
        {
            InitializeComponent();
            // Alussa näytetään panelissa etusivu
            ShowView(new EtusivuView());
        }

        // Apumetodi joka vaihtaa näkymän
        private void ShowView(UserControl view)
        {
            // Tyhjennetään panelFill kaikista sen sisällä olevista kontrolleista - Clear();
            panelFill.Controls.Clear();
            // Asetetaan uusi view täyttämään koko paneelin - DockStyle.Fill
            view.Dock = DockStyle.Fill;
            // Lisätään uusi view panelFill:iin - Add(view);
            panelFill.Controls.Add(view);
        }

        // Jokaiselle napille oma tapahtuma
        private void btnEtusivu_Click(object sender, EventArgs e)
        {
            ShowView(new EtusivuView());
        }

        private void btnAlueet_Click(object sender, EventArgs e)
        {
            ShowView(new AlueetView());
        }

        private void btnMokit_Click(object sender, EventArgs e)
        {
            ShowView(new MokitView());
        }

        private void btnPalvelut_Click(object sender, EventArgs e)
        {
            ShowView(new PalvelutView());
        }

        private void btnAsiakkaat_Click(object sender, EventArgs e)
        {
            ShowView(new AsiakkaatView());
        }

        private void btnVaraukset_Click(object sender, EventArgs e)
        {
            ShowView(new VarauksetView());
        }

        private void btnLaskut_Click(object sender, EventArgs e)
        {
            ShowView(new LaskutView());
        }

        private void btnRaportit_Click(object sender, EventArgs e)
        {
            ShowView(new RaportitView());
        }
    }
}
