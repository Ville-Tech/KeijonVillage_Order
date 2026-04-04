using System;
using System.Drawing;
using System.Windows.Forms;

namespace VillageNewbies_Projekti.Views
{
    public partial class LaskutView : UserControl
    {
        public LaskutView()
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
            // Main title (already in Designer)
            label1.Text = "Laskut";

            // Create menu labels
            CreateMenuLabel("➕ Luo uusi lasku", new Point(50, 120), out lblNewInvoice);
            CreateMenuLabel("📄 Näytä kaikki laskut", new Point(50, 170), out lblShowAll);
            CreateMenuLabel("🔍 Hae laskuja", new Point(50, 220), out lblSearch);
            CreateMenuLabel("⚙ Asetukset", new Point(50, 270), out lblSettings);
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

            // Add to control
            Controls.Add(label);
        }

        // -------------------------------
        //  EVENT HOOKS
        // -------------------------------
        private void HookEvents()
        {
            AddHoverEffect(lblNewInvoice);
            AddHoverEffect(lblShowAll);
            AddHoverEffect(lblSearch);
            AddHoverEffect(lblSettings);

            lblNewInvoice.Click += (s, e) => MessageBox.Show("Uuden laskun luonti avataan.");
            lblShowAll.Click += (s, e) => MessageBox.Show("Näytetään kaikki laskut.");
            lblSearch.Click += (s, e) => MessageBox.Show("Laskujen haku avataan.");
            lblSettings.Click += (s, e) => MessageBox.Show("Asetukset avataan.");
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
        private Label lblNewInvoice;
        private Label lblShowAll;
        private Label lblSearch;
        private Label lblSettings;
    }
}
