using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class LaskutView : UserControl
    {
        private readonly LaskuService _laskuService = new LaskuService();
        private readonly VarausService _varausService = new VarausService();
        private readonly AsiakasService _asiakasService = new AsiakasService();

        private Panel pnlSidebar, pnlWorkArea;
        private DataGridView dgvMain;
        private Label lblOtsikko;
        private Button btnToiminto;

        public LaskutView()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            pnlSidebar = new Panel { Width = 250, Dock = DockStyle.Left, BackColor = Color.FromArgb(240, 240, 240) };
            pnlWorkArea = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30) };

            lblOtsikko = new Label { Text = "Laskutus", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(30, 20), AutoSize = true };

            dgvMain = new DataGridView
            {
                Location = new Point(30, 80),
                Size = new Size(850, 400),
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.None
            };

            btnToiminto = new Button { Location = new Point(30, 500), Size = new Size(220, 45), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Visible = false };

            AddMenuBtn("📄 Kaikki laskut", 80, (s, e) => NaytaKaikkiLaskut());
            AddMenuBtn("➕ Luo uusi lasku", 140, (s, e) => NaytaLaskuttamattomat());

            pnlWorkArea.Controls.AddRange(new Control[] { lblOtsikko, dgvMain, btnToiminto });
            this.Controls.AddRange(new Control[] { pnlWorkArea, pnlSidebar });
        }

        private void AddMenuBtn(string t, int y, EventHandler h)
        {
            Button b = new Button { Text = t, Location = new Point(0, y), Width = 250, Height = 50, FlatStyle = FlatStyle.Flat, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(20, 0, 0, 0), Font = new Font("Segoe UI", 11) };
            b.Click += h;
            pnlSidebar.Controls.Add(b);
        }

        private void NaytaKaikkiLaskut()
        {
            lblOtsikko.Text = "Kaikki laskut (ALV 24%)";
            var laskut = _laskuService.HaeLaskut();
            dgvMain.DataSource = laskut.Select(l => new {
                ID = l.Lasku_ID,
                Varaus = l.Varaus_ID,
                Veroton = l.Summa.ToString("N2") + " €",
                ALV_24 = l.Alv.ToString("N2") + " €",
                Yhteensa = (l.Summa + l.Alv).ToString("N2") + " €",
                Tila = l.Maksettu == 1 ? "Maksettu" : "Avoin"
            }).ToList();

            btnToiminto.Text = "MERKITSE MAKSETUKSI";
            btnToiminto.BackColor = Color.FromArgb(200, 230, 255);
            btnToiminto.Visible = true;
            btnToiminto.Click -= BtnLuo_Click; btnToiminto.Click -= BtnMaksu_Click;
            btnToiminto.Click += BtnMaksu_Click;
        }

        private void NaytaLaskuttamattomat()
        {
            lblOtsikko.Text = "Laskuttamattomat varaukset";
            var laskut = _laskuService.HaeLaskut();
            var varaukset = _varausService.HaeVaraukset().Where(v => !laskut.Any(l => l.Varaus_ID == v.Varaus_ID)).ToList();
            var asiakkaat = _asiakasService.HaeAsiakkaat();

            dgvMain.DataSource = varaukset.Select(v => new {
                ID = v.Varaus_ID,
                Asiakas = asiakkaat.Find(a => a.Asiakas_ID == v.Asiakas_ID)?.KokoNimi ?? "N/A",
                Alkaa = v.Varattu_Alkupvm?.ToShortDateString(),
                Loppuu = v.Varattu_Loppupvm?.ToShortDateString()
            }).ToList();

            btnToiminto.Text = "LUO LASKU VALITULLE";
            btnToiminto.BackColor = Color.FromArgb(192, 255, 192);
            btnToiminto.Visible = true;
            btnToiminto.Click -= BtnLuo_Click; btnToiminto.Click -= BtnMaksu_Click;
            btnToiminto.Click += BtnLuo_Click;
        }

        private void BtnLuo_Click(object s, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvMain.CurrentRow.Cells[0].Value);
            try { _laskuService.LuoLaskuVarauksesta(id); MessageBox.Show("Lasku luotu onnistuneesti!"); NaytaKaikkiLaskut(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnMaksu_Click(object s, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvMain.CurrentRow.Cells[0].Value);
            _laskuService.MerkitseMaksetuksi(id);
            NaytaKaikkiLaskut();
        }
    }
}