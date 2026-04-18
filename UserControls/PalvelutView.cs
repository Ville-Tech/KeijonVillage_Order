using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;
using System.Collections.Generic;

namespace VillageNewbies_Projekti.Views
{
    public partial class PalvelutView : UserControl
    {
        private readonly PalveluService _palveluService = new PalveluService();
        private readonly AlueService _alueService = new AlueService();

        private DataGridView dgvPalvelut = new DataGridView();
        private TextBox txtHaku = new TextBox();
        private ComboBox cmbAlue = new ComboBox();
        private TextBox txtNimi = new TextBox();
        private TextBox txtKuvaus = new TextBox();
        private TextBox txtHinta = new TextBox();
        private TextBox txtAlv = new TextBox();
        private Button btnLisaa = new Button();
        private Button btnMuokkaa = new Button();
        private Button btnPoista = new Button();
        private Button btnTyhjenna = new Button();

        public PalvelutView()
        {
            InitializeComponent();
            RakennaUI();
            dgvPalvelut.DataBindingComplete += DgvPalvelut_DataBindingComplete;
            dgvPalvelut.SelectionChanged += DgvPalvelut_SelectionChanged;
        }

        private void RakennaUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            var ylaPanel = new Panel { Dock = DockStyle.Top, Height = 112 };
            var lblTitle = new Label
            {
                Text = "Palveluiden hallinta",
                Font = new System.Drawing.Font("Segoe UI", 20.25f),
                Location = new System.Drawing.Point(60, 40),
                AutoSize = true
            };
            ylaPanel.Controls.Add(lblTitle);

            var keskiPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(18, 15, 18, 15) };
            var panelVasen = new Panel { Dock = DockStyle.Left, Width = 350 };

            var lblHaku = new Label
            {
                Text = "Hae palvelua:",
                Font = new System.Drawing.Font("Segoe UI", 16.2f),
                Location = new System.Drawing.Point(43, 13),
                AutoSize = true
            };

            txtHaku.Location = new System.Drawing.Point(43, 64);
            txtHaku.Width = 261;
            txtHaku.TextChanged += (s, e) => LataaPalvelut(txtHaku.Text);

            dgvPalvelut.Location = new System.Drawing.Point(43, 105);
            dgvPalvelut.Size = new System.Drawing.Size(285, 350);
            dgvPalvelut.BackgroundColor = System.Drawing.Color.White;
            dgvPalvelut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPalvelut.MultiSelect = false;
            dgvPalvelut.ReadOnly = true;
            dgvPalvelut.AllowUserToAddRows = false;
            dgvPalvelut.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            panelVasen.Controls.AddRange(new Control[] { lblHaku, txtHaku, dgvPalvelut });

            var panelOikea = new Panel { Dock = DockStyle.Fill };
            int y = 44;

            void LisaaKentta(string otsikko, Control ctrl, int leveys = 280)
            {
                var lbl = new Label { Text = otsikko, Location = new System.Drawing.Point(43, y), AutoSize = true };
                ctrl.Location = new System.Drawing.Point(43, y + 20);
                if (ctrl is TextBox tb) tb.Width = leveys;
                if (ctrl is ComboBox cb) cb.Width = leveys;
                panelOikea.Controls.Add(lbl);
                panelOikea.Controls.Add(ctrl);
                y += 60;
            }

            LisaaKentta("Valitse alue", cmbAlue);
            cmbAlue.DropDownStyle = ComboBoxStyle.DropDownList;
            LisaaKentta("Palvelun nimi", txtNimi);
            txtKuvaus.Multiline = true; txtKuvaus.Height = 60;
            LisaaKentta("Kuvaus", txtKuvaus); y += 25;
            LisaaKentta("Hinta (€)", txtHinta);
            LisaaKentta("ALV (%)", txtAlv);

            void AsetaNappi(Button btn, string teksti, int x)
            {
                btn.Text = teksti; btn.Location = new System.Drawing.Point(x, y + 10);
                btn.Size = new System.Drawing.Size(190, 40); btn.FlatStyle = FlatStyle.Popup;
                panelOikea.Controls.Add(btn);
            }

            AsetaNappi(btnLisaa, "Lisää", 43);
            AsetaNappi(btnMuokkaa, "Muokkaa", 239);
            AsetaNappi(btnPoista, "Poista", 435);
            y += 55;
            AsetaNappi(btnTyhjenna, "Tyhjennä", 43);

            btnLisaa.Click += btnLisaa_Click;
            btnMuokkaa.Click += btnMuokkaa_Click;
            btnPoista.Click += btnPoista_Click;
            btnTyhjenna.Click += (s, e) => TyhjennaLomake();

            keskiPanel.Controls.AddRange(new Control[] { panelOikea, panelVasen });
            this.Controls.AddRange(new Control[] { keskiPanel, ylaPanel });

            LataaAlueDropdown();
            LataaPalvelut();
        }

        // UUSI: Sarakkeiden määrittely vasta kun data on ladattu (kuten Varaukset-näkymässä)
        private void DgvPalvelut_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (dgvPalvelut.Columns.Count >= 5)
                {
                    dgvPalvelut.Columns[0].HeaderText = "ID";
                    dgvPalvelut.Columns[0].Width = 40;
                    dgvPalvelut.Columns[1].HeaderText = "Alue";
                    dgvPalvelut.Columns[2].HeaderText = "Nimi";
                    dgvPalvelut.Columns[3].HeaderText = "Hinta";
                    dgvPalvelut.Columns[4].HeaderText = "ALV";
                }
            }
            catch { }
        }

        private void LataaAlueDropdown()
        {
            try
            {
                cmbAlue.DataSource = _alueService.HaeAlueet();
                cmbAlue.DisplayMember = "Nimi";
                cmbAlue.ValueMember = "Alue_ID";
                cmbAlue.SelectedIndex = -1;
            }
            catch { }
        }

        private void LataaPalvelut(string hakusana = "")
        {
            try
            {
                var palvelut = _palveluService.HaePalvelut();
                var alueet = _alueService.HaeAlueet();

                if (!string.IsNullOrWhiteSpace(hakusana))
                    palvelut = palvelut.FindAll(p => p.Nimi != null && p.Nimi.Contains(hakusana, StringComparison.OrdinalIgnoreCase));

                var nakyma = palvelut.ConvertAll(p => new PalveluNakyma
                {
                    Palvelu_ID = p.Palvelu_ID,
                    Alue = alueet.Find(a => a.Alue_ID == p.Alue_ID)?.Nimi ?? "-",
                    Nimi = p.Nimi,
                    Hinta = $"{p.Hinta:F2} €",
                    Alv = $"{p.Alv:F1} %"
                });

                dgvPalvelut.SelectionChanged -= DgvPalvelut_SelectionChanged;
                dgvPalvelut.DataSource = null;
                dgvPalvelut.DataSource = nakyma;
                dgvPalvelut.SelectionChanged += DgvPalvelut_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe palveluiden haussa: {ex.Message}");
            }
        }

        private void DgvPalvelut_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPalvelut.CurrentRow == null || dgvPalvelut.CurrentRow.Cells[0].Value == null) return;

            try
            {
                int id = (int)dgvPalvelut.CurrentRow.Cells[0].Value;
                var p = _palveluService.HaePalvelut().Find(x => x.Palvelu_ID == id);
                if (p == null) return;

                cmbAlue.SelectedValue = p.Alue_ID;
                txtNimi.Text = p.Nimi ?? "";
                txtKuvaus.Text = p.Kuvaus ?? "";
                txtHinta.Text = p.Hinta.ToString("F2");
                txtAlv.Text = p.Alv.ToString("F1");
            }
            catch { }
        }

        private Palvelu? LomakkeestaPalvelu()
        {
            if (cmbAlue.SelectedValue == null || string.IsNullOrWhiteSpace(txtNimi.Text))
            {
                MessageBox.Show("Valitse alue ja anna nimi.");
                return null;
            }

            double.TryParse(txtHinta.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double hinta);
            double.TryParse(txtAlv.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double alv);

            return new Palvelu
            {
                Alue_ID = (int)cmbAlue.SelectedValue,
                Nimi = txtNimi.Text.Trim(),
                Kuvaus = txtKuvaus.Text.Trim(),
                Hinta = hinta,
                Alv = alv
            };
        }

        private void btnLisaa_Click(object sender, EventArgs e)
        {
            var p = LomakkeestaPalvelu();
            if (p == null) return;
            _palveluService.LisaaPalvelu(p);
            TyhjennaLomake();
            LataaPalvelut();
        }

        private void btnMuokkaa_Click(object sender, EventArgs e)
        {
            if (dgvPalvelut.CurrentRow == null) return;
            var p = LomakkeestaPalvelu();
            if (p == null) return;
            p.Palvelu_ID = (int)dgvPalvelut.CurrentRow.Cells[0].Value;
            _palveluService.PaivitaPalvelu(p);
            LataaPalvelut();
        }

        private void btnPoista_Click(object sender, EventArgs e)
        {
            if (dgvPalvelut.CurrentRow == null) return;
            int id = (int)dgvPalvelut.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Poistetaanko?", "Vahvista", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _palveluService.PoistaPalvelu(id);
                TyhjennaLomake();
                LataaPalvelut();
            }
        }

        private void TyhjennaLomake()
        {
            cmbAlue.SelectedIndex = -1;
            txtNimi.Clear();
            txtKuvaus.Clear();
            txtHinta.Clear();
            txtAlv.Clear();
            dgvPalvelut.ClearSelection();
        }

        public class PalveluNakyma
        {
            public int Palvelu_ID { get; set; }
            public string Alue { get; set; } = "";
            public string? Nimi { get; set; }
            public string Hinta { get; set; } = "";
            public string Alv { get; set; } = "";
        }
    }
}