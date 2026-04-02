using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class MokitView : UserControl
    {
        // ── Palvelut ─────────────────────────────────────────────────────────────
        private readonly MokkiService _mokkiService = new MokkiService();
        private readonly VarausService _varausService = new VarausService();

        // Värit korteille — kierrätetään jos mökkejä on enemmän kuin värejä
        private static readonly Color[] ACCENT_COLORS =
        {
            Color.FromArgb(52,  168, 130),
            Color.FromArgb(70,  130, 180),
            Color.FromArgb(180, 90,  60),
            Color.FromArgb(160, 100, 200),
            Color.FromArgb(210, 150, 40),
            Color.FromArgb(80,  160, 100),
        };

        // ── Data ─────────────────────────────────────────────────────────────────
        private List<Mokki> _mokit = new List<Mokki>();

        // ── Layout constants ─────────────────────────────────────────────────────
        private const int CARD_W = 320;
        private const int CARD_H = 220;
        private const int CARD_GAP = 20;
        private const int PANEL_PAD = 28;
        private const int HEADER_H = 70;
        private const int FILTER_H = 48;

        // ── Colors & fonts ───────────────────────────────────────────────────────
        private static readonly Color BG = Color.FromArgb(245, 246, 250);
        private static readonly Color SURFACE = Color.White;
        private static readonly Color TEXT_PRI = Color.FromArgb(30, 30, 50);
        private static readonly Color TEXT_SEC = Color.FromArgb(110, 110, 140);
        private static readonly Color AVAIL_OK = Color.FromArgb(34, 197, 94);
        private static readonly Color AVAIL_NO = Color.FromArgb(239, 68, 68);

        private readonly Font _fontTitle = new Font("Segoe UI", 13f, FontStyle.Bold, GraphicsUnit.Point);
        private readonly Font _fontSub = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point);
        private readonly Font _fontSmall = new Font("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point);
        private readonly Font _fontBadge = new Font("Segoe UI", 8f, FontStyle.Bold, GraphicsUnit.Point);
        private readonly Font _fontPrice = new Font("Segoe UI", 14f, FontStyle.Bold, GraphicsUnit.Point);
        private readonly Font _fontHeader = new Font("Segoe UI", 18f, FontStyle.Bold, GraphicsUnit.Point);

        // ── State ────────────────────────────────────────────────────────────────
        private bool _showOnlyAvailable = false;
        private int _hoverIndex = -1;
        private Panel _cardPanel = null!;
        private CheckBox _filterCheck = null!;
        private Label _lblCount = null!;

        // Tallennetaan mille mökille mikä väri — pysyy samana haun jälkeenkin
        private Dictionary<int, Color> _accentMap = new Dictionary<int, Color>();
        // Tallennetaan onko mökki vapaa tänään
        private Dictionary<int, bool> _vapaaMap = new Dictionary<int, bool>();

        // ─────────────────────────────────────────────────────────────────────────
        public MokitView()
        {
            InitializeComponent();
            BuildUI();
        }

        private void MokitView_Load(object sender, EventArgs e)
        {
            LataaMokit();
        }

        // ── Lataa data tietokannasta ──────────────────────────────────────────────
        private void LataaMokit()
        {
            try
            {
                _mokit = _mokkiService.HaeMokit();
                var varaukset = _varausService.HaeVaraukset();
                var tanaan = DateTime.Today;

                _accentMap.Clear();
                _vapaaMap.Clear();

                for (int i = 0; i < _mokit.Count; i++)
                {
                    var m = _mokit[i];
                    // Kierrätä värejä
                    _accentMap[m.Mokki_ID] = ACCENT_COLORS[i % ACCENT_COLORS.Length];
                    // Onko mökki varattu tänään?
                    bool varattu = varaukset.Exists(v =>
                        v.Mokki_ID == m.Mokki_ID &&
                        v.Varattu_Alkupvm <= tanaan &&
                        v.Varattu_Loppupvm >= tanaan);
                    _vapaaMap[m.Mokki_ID] = !varattu;
                }

                RenderCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe mökkien haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Build static chrome ───────────────────────────────────────────────────
        private void BuildUI()
        {
            this.BackColor = BG;
            this.AutoScroll = false;
            this.Resize += (s, e) => RenderCards();

            // Header
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = HEADER_H,
                BackColor = Color.FromArgb(30, 30, 50),
                Padding = new Padding(PANEL_PAD, 0, PANEL_PAD, 0),
            };
            var lblTitle = new Label
            {
                Text = "🏡  Mökit – Vuokrakohteet",
                Font = _fontHeader,
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            header.Controls.Add(lblTitle);

            // Filter bar
            var filterBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = FILTER_H,
                BackColor = Color.White,
                Padding = new Padding(PANEL_PAD, 0, PANEL_PAD, 0),
            };
            _filterCheck = new CheckBox
            {
                Text = "Näytä vain vapaat kohteet",
                Font = _fontSub,
                ForeColor = TEXT_PRI,
                AutoSize = true,
                Checked = false,
                Location = new Point(PANEL_PAD, 14),
                Cursor = Cursors.Hand,
            };
            _filterCheck.CheckedChanged += (s, e) =>
            {
                _showOnlyAvailable = _filterCheck.Checked;
                RenderCards();
            };

            // Päivitä-nappi
            var btnPaivita = new Button
            {
                Text = "🔄 Päivitä",
                Font = _fontSub,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                AutoSize = true,
                Location = new Point(PANEL_PAD + 220, 10),
            };
            btnPaivita.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 220);
            btnPaivita.Click += (s, e) => LataaMokit();

            _lblCount = new Label
            {
                AutoSize = false,
                Width = 200,
                Height = FILTER_H,
                TextAlign = ContentAlignment.MiddleRight,
                Font = _fontSmall,
                ForeColor = TEXT_SEC,
            };
            filterBar.Controls.Add(_filterCheck);
            filterBar.Controls.Add(btnPaivita);
            filterBar.Controls.Add(_lblCount);
            filterBar.Resize += (s, e) =>
            {
                _lblCount.Location = new Point(filterBar.Width - 220, 0);
                _lblCount.Width = 200;
            };

            // Scroll-alue
            _cardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = BG,
                AutoScroll = true,
                Padding = new Padding(PANEL_PAD),
            };
            _cardPanel.MouseMove += CardPanel_MouseMove;
            _cardPanel.MouseLeave += (s, e) => { _hoverIndex = -1; _cardPanel.Invalidate(); };
            _cardPanel.Paint += CardPanel_Paint;

            this.Controls.Add(_cardPanel);
            this.Controls.Add(filterBar);
            this.Controls.Add(header);
        }

        // ── Layout & paint ────────────────────────────────────────────────────────
        private List<(Mokki m, Rectangle r)> _layout = new List<(Mokki, Rectangle)>();

        private void RenderCards()
        {
            _layout.Clear();

            var list = _showOnlyAvailable
                ? _mokit.FindAll(m => _vapaaMap.TryGetValue(m.Mokki_ID, out bool v) && v)
                : _mokit;

            int cols = Math.Max(1, (_cardPanel.ClientSize.Width - PANEL_PAD) / (CARD_W + CARD_GAP));
            int totalWidth = cols * CARD_W + (cols - 1) * CARD_GAP;
            int startX = (_cardPanel.ClientSize.Width - totalWidth) / 2;

            for (int i = 0; i < list.Count; i++)
            {
                int col = i % cols;
                int row = i / cols;
                int x = startX + col * (CARD_W + CARD_GAP);
                int y = PANEL_PAD + row * (CARD_H + CARD_GAP);
                _layout.Add((list[i], new Rectangle(x, y, CARD_W, CARD_H)));
            }

            int rows = (list.Count + cols - 1) / cols;
            _cardPanel.AutoScrollMinSize = new Size(0, PANEL_PAD + rows * (CARD_H + CARD_GAP) + PANEL_PAD);
            _lblCount.Text = $"{list.Count} kohdetta";
            _cardPanel.Invalidate();
        }

        private void CardPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TranslateTransform(0, -_cardPanel.AutoScrollPosition.Y * -1 + _cardPanel.AutoScrollPosition.Y);

            for (int i = 0; i < _layout.Count; i++)
            {
                var (m, r) = _layout[i];
                bool vapaa = _vapaaMap.TryGetValue(m.Mokki_ID, out bool v) && v;
                Color accent = _accentMap.TryGetValue(m.Mokki_ID, out Color c) ? c : Color.Gray;
                DrawCard(g, m, r, i == _hoverIndex, vapaa, accent);
            }
        }

        private void DrawCard(Graphics g, Mokki m, Rectangle r, bool hovered, bool vapaa, Color accent)
        {
            int shadow = hovered ? 8 : 3;
            int lift = hovered ? -3 : 0;
            var rc = new Rectangle(r.X, r.Y + lift, r.Width, r.Height);

            // Shadow
            using (var path = RoundedRect(new Rectangle(rc.X + 2, rc.Y + shadow, rc.Width - 2, rc.Height - 2), 12))
            using (var brush = new SolidBrush(Color.FromArgb(hovered ? 40 : 20, 0, 0, 0)))
                g.FillPath(brush, path);

            // Card background
            using (var path = RoundedRect(rc, 12))
            using (var brush = new SolidBrush(SURFACE))
                g.FillPath(brush, path);

            // Accent stripe
            using (var stripePath = RoundedRect(new Rectangle(rc.X, rc.Y, 6, rc.Height), 3))
            using (var brush = new SolidBrush(accent))
                g.FillPath(brush, stripePath);

            // Header band
            int bandH = 62;
            using (var bandPath = RoundedRect(new Rectangle(rc.X, rc.Y, rc.Width, bandH), 12))
            using (var brush = new LinearGradientBrush(
                new Point(rc.X, rc.Y), new Point(rc.X, rc.Y + bandH),
                Color.FromArgb(230, accent), Color.FromArgb(30, accent)))
                g.FillPath(brush, bandPath);

            // Nimi
            var nameRect = new Rectangle(rc.X + 18, rc.Y + 10, rc.Width - 100, 28);
            using (var b = new SolidBrush(TEXT_PRI))
                g.DrawString(m.Mokkinimi ?? "–", _fontTitle, b, nameRect);

            // Sijainti (postinro, koska ei erillistä location-kenttää)
            var locRect = new Rectangle(rc.X + 18, rc.Y + 36, rc.Width - 28, 20);
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString($"📍 {m.Postinro}", _fontSmall, b, locRect);

            // Saatavuusbadge
            var badgeColor = vapaa ? AVAIL_OK : AVAIL_NO;
            var badgeText = vapaa ? "Vapaa" : "Varattu";
            var badgeRect = new Rectangle(rc.Right - 82, rc.Y + 12, 70, 22);
            using (var br = new SolidBrush(badgeColor))
                g.FillRectangle(br, RoundedRect(badgeRect, 11).GetBounds());
            using (var b = new SolidBrush(Color.White))
                g.DrawString(badgeText, _fontBadge, b, badgeRect,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            // Kuvaus
            var descRect = new Rectangle(rc.X + 18, rc.Y + 70, rc.Width - 30, 52);
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(m.Kuvaus ?? "", _fontSmall, b, descRect,
                    new StringFormat { Trimming = StringTrimming.EllipsisWord });

            // Separator
            using (var pen = new Pen(Color.FromArgb(230, 230, 240)))
                g.DrawLine(pen, rc.X + 14, rc.Y + 130, rc.Right - 14, rc.Y + 130);

            // Ikonirivi
            int iconY = rc.Y + 140;
            DrawIconStat(g, $"👥 {m.Henkilomaara} hlö", rc.X + 18, iconY);

            // Hinta
            var priceStr = $"€{m.Hinta:0}";
            var nightStr = " / yö";
            var priceRect = new Rectangle(rc.X + 18, rc.Y + 168, rc.Width - 30, 28);
            using (var b = new SolidBrush(accent))
                g.DrawString(priceStr, _fontPrice, b, priceRect);
            var priceW = (int)g.MeasureString(priceStr, _fontPrice).Width;
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(nightStr, _fontSmall, b,
                    new Rectangle(rc.X + 18 + priceW - 4, rc.Y + 178, 60, 20));

            // Reunus
            using (var pen = new Pen(hovered ? accent : Color.FromArgb(220, 220, 235), hovered ? 2f : 1f))
            using (var path = RoundedRect(rc, 12))
                g.DrawPath(pen, path);
        }

        private void DrawIconStat(Graphics g, string text, int x, int y)
        {
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(text, _fontSub, b, new Point(x, y));
        }

        // ── Hover ────────────────────────────────────────────────────────────────
        private void CardPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            var pt = new Point(e.X, e.Y - _cardPanel.AutoScrollPosition.Y);
            int prev = _hoverIndex;
            _hoverIndex = -1;
            for (int i = 0; i < _layout.Count; i++)
                if (_layout[i].r.Contains(pt)) { _hoverIndex = i; break; }
            if (_hoverIndex != prev) _cardPanel.Invalidate();
            _cardPanel.Cursor = _hoverIndex >= 0 ? Cursors.Hand : Cursors.Default;
        }

        // ── Rounded rect ─────────────────────────────────────────────────────────
        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}