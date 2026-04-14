using System;
using System.Drawing;
using System.Windows.Forms;

namespace VillageNewbies_Projekti.Views
{
    public partial class PalvelutView : UserControl
    {
        public PalvelutView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetupUI();
            HookEvents();
        }

        // -------------------------------
        //  UI SETUP
        // -------------------------------
        private void SetupUI()
        {

            // Create clickable service labels
            CreateMenuLabel("🏡 Varaa mökki", new Point(50, 120), out lblMokit);
            CreateMenuLabel("🔥 Varaa sauna", new Point(50, 170), out lblSauna);
            CreateMenuLabel("🚤 Vuokraa vene", new Point(50, 220), out lblVene);
            CreateMenuLabel("🎣 Kalastus & aktiviteetit", new Point(50, 270), out lblAktiviteetit);
            CreateMenuLabel("📅 Omat varaukset", new Point(50, 320), out lblVaraukset);
        }

        private void CreateMenuLabel(string text, Point location, out Label label)
        {
            label = new Label();
            label.Text = text;
            label.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            label.ForeColor = Color.DimGray;
            label.Location = location;
            label.AutoSize = true;
            label.Cursor = Cursors.Hand;

            Controls.Add(label);
        }

        // -------------------------------
        //  EVENT HOOKS
        // -------------------------------
        private void HookEvents()
        {
            AddHoverEffect(lblMokit);
            AddHoverEffect(lblSauna);
            AddHoverEffect(lblVene);
            AddHoverEffect(lblAktiviteetit);
            AddHoverEffect(lblVaraukset);
        }

        private void AddHoverEffect(Label label)
        {
            label.MouseEnter += (s, e) =>
            {
                label.ForeColor = Color.Black;
                label.Font = new Font(label.Font, FontStyle.Bold);
            };

            label.MouseLeave += (s, e) =>
            {
                label.ForeColor = Color.DimGray;
                label.Font = new Font(label.Font, FontStyle.Regular);
            };
        }

        // -------------------------------
        //  PRIVATE FIELDS
        // -------------------------------
        private Label lblMokit;
        private Label lblSauna;
        private Label lblVene;
        private Label lblAktiviteetit;
        private Label lblVaraukset;
    }
}
