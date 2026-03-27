using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VillageNewbies_Projekti.Views
{
    public partial class MokitView : UserControl
    {
        // ── Data model ──────────────────────────────────────────────────────────
        private class Mokki
        {
            public string  Name           { get; set; } = "";
            public string  Location       { get; set; } = "";
            public string  Description    { get; set; } = "";
            public int     Guests         { get; set; }
            public int     Bedrooms       { get; set; }
            public decimal PricePerNight  { get; set; }
            public bool    Available      { get; set; }
            public Color   AccentColor    { get; set; }
        }

        // ── Sample data ──────────────────────────────────────────────────────────
        private readonly List<Mokki> _mokit = new List<Mokki>
        {
            new Mokki { Name="Koivuranta",   Location="Kuopio",    Guests=6,  Bedrooms=3, PricePerNight=149, Available=true,  AccentColor=Color.FromArgb(52,168,130),  Description="Lakeside log cabin with private sauna and rowing boat. Breathtaking sunset views." },
            new Mokki { Name="Honkala",      Location="Tampere",   Guests=4,  Bedrooms=2, PricePerNight=99,  Available=true,  AccentColor=Color.FromArgb(70,130,180),  Description="Cosy pine cottage in the forest, just 200 m from the swimming beach." },
            new Mokki { Name="Peräkorpi",    Location="Rovaniemi", Guests=8,  Bedrooms=4, PricePerNight=219, Available=false, AccentColor=Color.FromArgb(180,90,60),   Description="Arctic wilderness cabin. Northern lights visible in season. Snowmobile access." },
            new Mokki { Name="Rantasauna",   Location="Savonlinna",Guests=5,  Bedrooms=2, PricePerNight=125, Available=true,  AccentColor=Color.FromArgb(160,100,200), Description="Classic smoke sauna right on the lake shore. Rowing boat and fishing equipment included." },
            new Mokki { Name="Vuorimökki",   Location="Koli",      Guests=10, Bedrooms=5, PricePerNight=299, Available=true,  AccentColor=Color.FromArgb(210,150,40),  Description="Grand hilltop retreat with panoramic national park views and outdoor hot tub." },
            new Mokki { Name="Järvenranta",  Location="Jyväskylä", Guests=4,  Bedrooms=2, PricePerNight=89,  Available=false, AccentColor=Color.FromArgb(80,160,100),  Description="Modern lakefront cottage, fully equipped kitchen, fibre internet, pet friendly." },
        };

        // ── Layout constants ─────────────────────────────────────────────────────
        private const int CARD_W      = 320;
        private const int CARD_H      = 220;
        private const int CARD_GAP    = 20;
        private const int PANEL_PAD   = 28;
        private const int HEADER_H    = 70;
        private const int FILTER_H    = 48;

        // ── Colors & fonts ───────────────────────────────────────────────────────
        private static readonly Color BG       = Color.FromArgb(245, 246, 250);
        private static readonly Color SURFACE  = Color.White;
        private static readonly Color TEXT_PRI = Color.FromArgb(30,  30,  50);
        private static readonly Color TEXT_SEC = Color.FromArgb(110, 110, 140);
        private static readonly Color AVAIL_OK = Color.FromArgb(34,  197, 94);
        private static readonly Color AVAIL_NO = Color.FromArgb(239, 68,  68);

        private readonly Font _fontTitle    = new Font("Segoe UI", 13f, FontStyle.Bold,   GraphicsUnit.Point);
        private readonly Font _fontSub      = new Font("Segoe UI", 9f,  FontStyle.Regular,GraphicsUnit.Point);
        private readonly Font _fontSmall    = new Font("Segoe UI", 8f,  FontStyle.Regular,GraphicsUnit.Point);
        private readonly Font _fontBadge    = new Font("Segoe UI", 8f,  FontStyle.Bold,   GraphicsUnit.Point);
        private readonly Font _fontPrice    = new Font("Segoe UI", 14f, FontStyle.Bold,   GraphicsUnit.Point);
        private readonly Font _fontHeader   = new Font("Segoe UI", 18f, FontStyle.Bold,   GraphicsUnit.Point);

        // ── State ────────────────────────────────────────────────────────────────
        private bool   _showOnlyAvailable = false;
        private int    _hoverIndex        = -1;
        private Panel    _cardPanel    = null!;
        private CheckBox _filterCheck  = null!;
        private Label    _lblCount     = null!;

        // ─────────────────────────────────────────────────────────────────────────
        public MokitView()
        {
            InitializeComponent();
            BuildUI();
        }

        private void MokitView_Load(object sender, EventArgs e)
        {
            RenderCards();
        }

        // ── Build static chrome (header + filter bar + scroll panel) ─────────────
        private void BuildUI()
        {
            this.BackColor  = BG;
            this.AutoScroll = false;
            this.Resize    += (s, e) => RenderCards();

            // ── Header ────────────────────────────────────────────────────────────
            var header = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = HEADER_H,
                BackColor = Color.FromArgb(30, 30, 50),
                Padding   = new Padding(PANEL_PAD, 0, PANEL_PAD, 0),
            };
            var lblTitle = new Label
            {
                Text      = "🏡  Mökit – Vuokrakohteet",
                Font      = _fontHeader,
                ForeColor = Color.White,
                AutoSize  = false,
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            header.Controls.Add(lblTitle);

            // ── Filter bar ────────────────────────────────────────────────────────
            var filterBar = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = FILTER_H,
                BackColor = Color.FromArgb(255,255,255),
                Padding   = new Padding(PANEL_PAD, 0, PANEL_PAD, 0),
            };
            _filterCheck = new CheckBox
            {
                Text      = "Näytä vain vapaat kohteet",
                Font      = _fontSub,
                ForeColor = TEXT_PRI,
                AutoSize  = true,
                Checked   = false,
                Location  = new Point(PANEL_PAD, 14),
                Cursor    = Cursors.Hand,
            };
            _filterCheck.CheckedChanged += (s, e) =>
            {
                _showOnlyAvailable = _filterCheck.Checked;
                RenderCards();
            };
            _lblCount = new Label
            {
                AutoSize  = false,
                Width     = 200,
                Height    = FILTER_H,
                TextAlign = ContentAlignment.MiddleRight,
                Font      = _fontSmall,
                ForeColor = TEXT_SEC,
            };
            filterBar.Controls.Add(_filterCheck);
            filterBar.Controls.Add(_lblCount);
            filterBar.Resize += (s, e) =>
            {
                _lblCount.Location = new Point(filterBar.Width - 220, 0);
                _lblCount.Width    = 200;
            };

            // ── Scrollable card area ───────────────────────────────────────────────
            _cardPanel = new Panel
            {
                Dock        = DockStyle.Fill,
                BackColor   = BG,
                AutoScroll  = true,
                Padding     = new Padding(PANEL_PAD),
            };
            _cardPanel.MouseMove  += CardPanel_MouseMove;
            _cardPanel.MouseLeave += (s, e) => { _hoverIndex = -1; _cardPanel.Invalidate(); };
            _cardPanel.Paint      += CardPanel_Paint;

            this.Controls.Add(_cardPanel);
            this.Controls.Add(filterBar);
            this.Controls.Add(header);
        }

        // ── Layout & paint cards ─────────────────────────────────────────────────
        private List<(Mokki m, Rectangle r)> _layout = new List<(Mokki, Rectangle)>();

        private void RenderCards()
        {
            _layout.Clear();
            var list = _showOnlyAvailable
                ? _mokit.FindAll(m => m.Available)
                : _mokit;

            int cols       = Math.Max(1, (_cardPanel.ClientSize.Width - PANEL_PAD) / (CARD_W + CARD_GAP));
            int totalWidth = cols * CARD_W + (cols - 1) * CARD_GAP;
            int startX     = (_cardPanel.ClientSize.Width - totalWidth) / 2;

            for (int i = 0; i < list.Count; i++)
            {
                int col = i % cols;
                int row = i / cols;
                int x   = startX + col * (CARD_W + CARD_GAP);
                int y   = PANEL_PAD + row * (CARD_H + CARD_GAP);
                _layout.Add((list[i], new Rectangle(x, y, CARD_W, CARD_H)));
            }

            int rows          = (list.Count + cols - 1) / cols;
            _cardPanel.AutoScrollMinSize = new Size(0, PANEL_PAD + rows * (CARD_H + CARD_GAP) + PANEL_PAD);
            _lblCount.Text    = $"{list.Count} kohdetta";
            _cardPanel.Invalidate();
        }

        private void CardPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Account for scroll offset
            g.TranslateTransform(0, -_cardPanel.AutoScrollPosition.Y * -1 + _cardPanel.AutoScrollPosition.Y);

            for (int i = 0; i < _layout.Count; i++)
            {
                var (m, r) = _layout[i];
                DrawCard(g, m, r, i == _hoverIndex);
            }
        }

        private void DrawCard(Graphics g, Mokki m, Rectangle r, bool hovered)
        {
            int shadow = hovered ? 8 : 3;
            int lift   = hovered ? -3 : 0;
            var rc     = new Rectangle(r.X, r.Y + lift, r.Width, r.Height);

            // Shadow
            using (var path = RoundedRect(new Rectangle(rc.X + 2, rc.Y + shadow, rc.Width - 2, rc.Height - 2), 12))
            using (var brush = new SolidBrush(Color.FromArgb(hovered ? 40 : 20, 0, 0, 0)))
                g.FillPath(brush, path);

            // Card background
            using (var path  = RoundedRect(rc, 12))
            using (var brush = new SolidBrush(SURFACE))
                g.FillPath(brush, path);

            // Accent stripe (left edge)
            using (var stripePath = RoundedRect(new Rectangle(rc.X, rc.Y, 6, rc.Height), 3))
            using (var brush = new SolidBrush(m.AccentColor))
                g.FillPath(brush, stripePath);

            // Header band
            int bandH = 62;
            using (var bandPath = RoundedRect(new Rectangle(rc.X, rc.Y, rc.Width, bandH), 12))
            using (var brush = new LinearGradientBrush(
                new Point(rc.X, rc.Y), new Point(rc.X, rc.Y + bandH),
                Color.FromArgb(230, m.AccentColor), Color.FromArgb(30, m.AccentColor)))
                g.FillPath(brush, bandPath);

            // Name
            var nameRect = new Rectangle(rc.X + 18, rc.Y + 10, rc.Width - 100, 28);
            using (var b = new SolidBrush(TEXT_PRI))
                g.DrawString(m.Name, _fontTitle, b, nameRect, StringFormat.GenericDefault);

            // Location
            var locRect = new Rectangle(rc.X + 18, rc.Y + 36, rc.Width - 28, 20);
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString($"📍 {m.Location}", _fontSmall, b, locRect);

            // Availability badge
            var badgeColor = m.Available ? AVAIL_OK : AVAIL_NO;
            var badgeText  = m.Available ? "Vapaa" : "Varattu";
            var badgeRect  = new Rectangle(rc.Right - 82, rc.Y + 12, 70, 22);
            using (var br = new SolidBrush(badgeColor))
                g.FillRectangle(br, RoundedRect(badgeRect, 11).GetBounds());
            using (var b = new SolidBrush(Color.White))
                g.DrawString(badgeText, _fontBadge, b, badgeRect,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            // Description
            var descRect = new Rectangle(rc.X + 18, rc.Y + 70, rc.Width - 30, 52);
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(m.Description, _fontSmall, b, descRect,
                    new StringFormat { Trimming = StringTrimming.EllipsisWord });

            // Separator
            using (var pen = new Pen(Color.FromArgb(230, 230, 240)))
                g.DrawLine(pen, rc.X + 14, rc.Y + 130, rc.Right - 14, rc.Y + 130);

            // Icons row
            int iconY = rc.Y + 140;
            DrawIconStat(g, $"👥 {m.Guests} hlö",  rc.X + 18,   iconY);
            DrawIconStat(g, $"🛏 {m.Bedrooms} mh",  rc.X + 108,  iconY);

            // Price
            var priceStr  = $"€{m.PricePerNight:0}";
            var nightStr  = " / yö";
            var priceRect = new Rectangle(rc.X + 18, rc.Y + 168, rc.Width - 30, 28);
            using (var b = new SolidBrush(m.AccentColor))
                g.DrawString(priceStr, _fontPrice, b, priceRect);
            var priceW = (int)g.MeasureString(priceStr, _fontPrice).Width;
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(nightStr, _fontSmall, b, new Rectangle(rc.X + 18 + priceW - 4, rc.Y + 178, 60, 20));

            // Card border
            using (var pen = new Pen(hovered ? m.AccentColor : Color.FromArgb(220, 220, 235), hovered ? 2f : 1f))
            using (var path = RoundedRect(rc, 12))
                g.DrawPath(pen, path);
        }

        private void DrawIconStat(Graphics g, string text, int x, int y)
        {
            using (var b = new SolidBrush(TEXT_SEC))
                g.DrawString(text, _fontSub, b, new Point(x, y));
        }

        // ── Mouse hover detection ─────────────────────────────────────────────────
        private void CardPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            // Adjust for scroll
            var pt = new Point(e.X, e.Y - _cardPanel.AutoScrollPosition.Y);
            int prev = _hoverIndex;
            _hoverIndex = -1;
            for (int i = 0; i < _layout.Count; i++)
            {
                if (_layout[i].r.Contains(pt)) { _hoverIndex = i; break; }
            }
            if (_hoverIndex != prev) _cardPanel.Invalidate();
            _cardPanel.Cursor = _hoverIndex >= 0 ? Cursors.Hand : Cursors.Default;
        }

        // ── Utility: rounded rectangle path ─────────────────────────────────────
        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X,            r.Y,            d, d, 180, 90);
            path.AddArc(r.Right - d,    r.Y,            d, d, 270, 90);
            path.AddArc(r.Right - d,    r.Bottom - d,   d, d, 0,   90);
            path.AddArc(r.X,            r.Bottom - d,   d, d, 90,  90);
            path.CloseFigure();
            return path;
        }

    }
}