using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class RaportitView : UserControl
    {
        // ── Palvelut ─────────────────────────────────────────────────────────────
        private readonly VarausService _varausService = new();
        private readonly MokkiService _mokkiService = new();
        private readonly AsiakasService _asiakasService = new();
        private readonly AlueService _alueService = new();
        private readonly PalveluService _palveluService = new();
        private readonly VarauksenPalvelutService _vpService = new();
        private readonly LaskuService _laskuService = new();

        // ── UI ───────────────────────────────────────────────────────────────────
        private TabControl _tabs = null!;
        private TabPage _tabMajoitus = null!;
        private TabPage _tabPalvelut = null!;

        // Majoitusraportti
        private DateTimePicker _majAlku = null!, _majLoppu = null!;
        private ComboBox _majAlue = null!;
        private DataGridView _majGrid = null!;
        private Label _majYhteensa = null!;
        private Button _majHae = null!, _majPdf = null!;

        // Palveluraportti
        private DateTimePicker _palAlku = null!, _palLoppu = null!;
        private ComboBox _palAlue = null!;
        private DataGridView _palGrid = null!;
        private Label _palYhteensa = null!;
        private Button _palHae = null!, _palPdf = null!;

        // ── Värit ────────────────────────────────────────────────────────────────
        private static readonly Color HDR = Color.FromArgb(30, 30, 50);
        private static readonly Color ACC = Color.FromArgb(52, 168, 130);
        private static readonly Font F_HDR = new("Segoe UI", 18f, FontStyle.Bold);
        private static readonly Font F_LBL = new("Segoe UI", 9f);
        private static readonly Font F_BTN = new("Segoe UI", 9f, FontStyle.Bold);

        public RaportitView()
        {
            InitializeComponent();
            RakennaUI();
        }

        // ═════════════════════════════════════════════════════════════════════════
        // UI-rakenne
        // ═════════════════════════════════════════════════════════════════════════

        private void RakennaUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            // Otsikkorivi
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = HDR
            };
            header.Controls.Add(new Label
            {
                Text = "📊  Raportit",
                Font = F_HDR,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(24, 0, 0, 0)
            });

            // TabControl
            _tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f),
                Padding = new Point(16, 6)
            };

            _tabMajoitus = new TabPage("🏡  Majoittumiset");
            _tabPalvelut = new TabPage("🛎  Lisäpalvelut");

            _tabs.TabPages.Add(_tabMajoitus);
            _tabs.TabPages.Add(_tabPalvelut);

            RakennaMajoitusTab();
            RakennaPalveluTab();

            this.Controls.Add(_tabs);
            this.Controls.Add(header);

            LataaAlueDropdownit();
        }

        // ── Majoitusraportti-välilehti ────────────────────────────────────────
        private void RakennaMajoitusTab()
        {
            _tabMajoitus.BackColor = Color.WhiteSmoke;
            _tabMajoitus.Padding = new Padding(12);

            var filterPanel = new Panel { Dock = DockStyle.Top, Height = 56 };

            filterPanel.Controls.Add(MakeLbl("Aikaväli:", 0));
            _majAlku = MakeDtp(110); filterPanel.Controls.Add(_majAlku);
            filterPanel.Controls.Add(MakeLbl("–", 300));
            _majLoppu = MakeDtp(318); filterPanel.Controls.Add(_majLoppu);
            filterPanel.Controls.Add(MakeLbl("Alue:", 510));
            _majAlue = MakeAlueCombo(570); filterPanel.Controls.Add(_majAlue);

            _majHae = MakeBtn("🔍 Hae", 800, ACC);
            _majHae.Click += (s, e) => HaeMajoitukset();
            filterPanel.Controls.Add(_majHae);

            _majPdf = MakeBtn("📄 PDF", 910, Color.FromArgb(70, 130, 180));
            _majPdf.Click += (s, e) => TulostaLaskuPdf();
            filterPanel.Controls.Add(_majPdf);

            _majGrid = MakeGrid();
            _majGrid.Columns.AddRange(
                Col("Varaus ID", 70),
                Col("Asiakas", 160),
                Col("Mökki", 150),
                Col("Alue", 120),
                Col("Alkaa", 95),
                Col("Loppuu", 95),
                Col("Päiviä", 60),
                Col("Hinta (veroton)", 120),
                Col("ALV 24 %", 90),
                Col("Yhteensä", 100)
            );

            _majYhteensa = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 32,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = HDR,
                Padding = new Padding(0, 0, 12, 0)
            };

            _tabMajoitus.Controls.Add(_majGrid);
            _tabMajoitus.Controls.Add(_majYhteensa);
            _tabMajoitus.Controls.Add(filterPanel);
        }

        // ── Palveluraportti-välilehti ─────────────────────────────────────────
        private void RakennaPalveluTab()
        {
            _tabPalvelut.BackColor = Color.WhiteSmoke;
            _tabPalvelut.Padding = new Padding(12);

            var filterPanel = new Panel { Dock = DockStyle.Top, Height = 56 };

            filterPanel.Controls.Add(MakeLbl("Aikaväli:", 0));
            _palAlku = MakeDtp(110); filterPanel.Controls.Add(_palAlku);
            filterPanel.Controls.Add(MakeLbl("–", 300));
            _palLoppu = MakeDtp(318); filterPanel.Controls.Add(_palLoppu);
            filterPanel.Controls.Add(MakeLbl("Alue:", 510));
            _palAlue = MakeAlueCombo(570); filterPanel.Controls.Add(_palAlue);

            _palHae = MakeBtn("🔍 Hae", 800, ACC);
            _palHae.Click += (s, e) => HaePalvelut();
            filterPanel.Controls.Add(_palHae);

            _palPdf = MakeBtn("📄 PDF", 910, Color.FromArgb(70, 130, 180));
            _palPdf.Click += (s, e) => TulostaLaskuPdf();
            filterPanel.Controls.Add(_palPdf);

            _palGrid = MakeGrid();
            _palGrid.Columns.AddRange(
                Col("Varaus ID", 70),
                Col("Asiakas", 160),
                Col("Palvelu", 160),
                Col("Alue", 120),
                Col("Varauksen alkupvm", 130),
                Col("Lkm", 60),
                Col("á-hinta", 90),
                Col("Yhteensä", 100)
            );

            _palYhteensa = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 32,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = HDR,
                Padding = new Padding(0, 0, 12, 0)
            };

            _tabPalvelut.Controls.Add(_palGrid);
            _tabPalvelut.Controls.Add(_palYhteensa);
            _tabPalvelut.Controls.Add(filterPanel);
        }

        // ═════════════════════════════════════════════════════════════════════════
        // Datan haku
        // ═════════════════════════════════════════════════════════════════════════

        private void LataaAlueDropdownit()
        {
            try
            {
                var alueet = _alueService.HaeAlueet();
                var kaikki = new Alue { Alue_ID = 0, Nimi = "(Kaikki alueet)" };
                alueet.Insert(0, kaikki);

                foreach (var cb in new[] { _majAlue, _palAlue })
                {
                    cb.DataSource = new List<Alue>(alueet);
                    cb.DisplayMember = "Nimi";
                    cb.ValueMember = "Alue_ID";
                    cb.SelectedIndex = 0;
                }

                // Oletusaikaväli: kuluva kuukausi
                var nyt = DateTime.Today;
                foreach (var d in new[] { _majAlku, _palAlku })
                    d.Value = new DateTime(nyt.Year, nyt.Month, 1);
                foreach (var d in new[] { _majLoppu, _palLoppu })
                    d.Value = new DateTime(nyt.Year, nyt.Month, DateTime.DaysInMonth(nyt.Year, nyt.Month));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe aluelistan haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HaeMajoitukset()
        {
            try
            {
                DateTime alku = _majAlku.Value.Date;
                DateTime loppu = _majLoppu.Value.Date.AddDays(1).AddSeconds(-1);
                int alueId = (int)_majAlue.SelectedValue!;

                if (alku > loppu)
                {
                    MessageBox.Show("Alkupäivä ei voi olla loppupäivän jälkeen.", "Huomio",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var varaukset = _varausService.HaeVaraukset()
                    .Where(v => v.Varattu_Alkupvm.HasValue && v.Varattu_Loppupvm.HasValue
                             && v.Varattu_Alkupvm.Value <= loppu
                             && v.Varattu_Loppupvm.Value >= alku)
                    .ToList();

                var mokit = _mokkiService.HaeMokit();
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                var alueet = _alueService.HaeAlueet();

                if (alueId != 0)
                    varaukset = varaukset
                        .Where(v => mokit.FirstOrDefault(m => m.Mokki_ID == v.Mokki_ID)?.Alue_ID == alueId)
                        .ToList();

                _majGrid.Rows.Clear();
                double kokonaissumma = 0;

                foreach (var v in varaukset)
                {
                    var mokki = mokit.FirstOrDefault(m => m.Mokki_ID == v.Mokki_ID);
                    var alue = alueet.FirstOrDefault(a => a.Alue_ID == mokki?.Alue_ID);
                    var asiakas = asiakkaat.FirstOrDefault(a => a.Asiakas_ID == v.Asiakas_ID);

                    int paivat = Math.Max(1,
                        (v.Varattu_Loppupvm!.Value - v.Varattu_Alkupvm!.Value).Days);

                    double veroton = (mokki?.Hinta ?? 0) * paivat;
                    double alv24 = Math.Round(veroton * 0.24, 2);
                    double yhteensa = Math.Round(veroton + alv24, 2);
                    kokonaissumma += yhteensa;

                    _majGrid.Rows.Add(
                        v.Varaus_ID,
                        asiakas?.KokoNimi ?? "-",
                        mokki?.Mokkinimi ?? "-",
                        alue?.Nimi ?? "-",
                        v.Varattu_Alkupvm.Value.ToString("dd.MM.yyyy"),
                        v.Varattu_Loppupvm.Value.ToString("dd.MM.yyyy"),
                        paivat,
                        veroton.ToString("N2") + " €",
                        alv24.ToString("N2") + " €",
                        yhteensa.ToString("N2") + " €"
                    );
                }

                _majYhteensa.Text = $"Varauksia: {varaukset.Count}    |    " +
                                    $"Kokonaistulo (sis. ALV): {kokonaissumma:N2} €";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe raportin haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HaePalvelut()
        {
            try
            {
                DateTime alku = _palAlku.Value.Date;
                DateTime loppu = _palLoppu.Value.Date.AddDays(1).AddSeconds(-1);
                int alueId = (int)_palAlue.SelectedValue!;

                if (alku > loppu)
                {
                    MessageBox.Show("Alkupäivä ei voi olla loppupäivän jälkeen.", "Huomio",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var varaukset = _varausService.HaeVaraukset()
                    .Where(v => v.Varattu_Alkupvm.HasValue
                             && v.Varattu_Alkupvm.Value >= alku
                             && v.Varattu_Alkupvm.Value <= loppu)
                    .ToList();

                var mokit = _mokkiService.HaeMokit();
                var asiakkaat = _asiakasService.HaeAsiakkaat();
                var palvelut = _palveluService.HaePalvelut();
                var alueet = _alueService.HaeAlueet();

                if (alueId != 0)
                    varaukset = varaukset
                        .Where(v => mokit.FirstOrDefault(m => m.Mokki_ID == v.Mokki_ID)?.Alue_ID == alueId)
                        .ToList();

                _palGrid.Rows.Clear();
                double kokonaissumma = 0;

                foreach (var v in varaukset)
                {
                    var vp = _vpService.HaeVarauksenPalvelut(v.Varaus_ID);
                    if (!vp.Any()) continue;

                    var asiakas = asiakkaat.FirstOrDefault(a => a.Asiakas_ID == v.Asiakas_ID);
                    var mokki = mokit.FirstOrDefault(m => m.Mokki_ID == v.Mokki_ID);
                    var alue = alueet.FirstOrDefault(a => a.Alue_ID == mokki?.Alue_ID);

                    foreach (var item in vp)
                    {
                        var palvelu = palvelut.FirstOrDefault(p => p.Palvelu_ID == item.Palvelu_ID);
                        double rivi = Math.Round((palvelu?.Hinta ?? 0) * item.Lkm, 2);
                        kokonaissumma += rivi;

                        _palGrid.Rows.Add(
                            v.Varaus_ID,
                            asiakas?.KokoNimi ?? "-",
                            palvelu?.Nimi ?? "-",
                            alue?.Nimi ?? "-",
                            v.Varattu_Alkupvm?.ToString("dd.MM.yyyy") ?? "-",
                            item.Lkm,
                            (palvelu?.Hinta ?? 0).ToString("N2") + " €",
                            rivi.ToString("N2") + " €"
                        );
                    }
                }

                _palYhteensa.Text = $"Rivejä: {_palGrid.Rows.Count}    |    " +
                                    $"Palvelutulot yhteensä: {kokonaissumma:N2} €";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe palveluraportin haussa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════════
        // PDF-tulostus (tekstipohjainen, ei ulkoisia kirjastoja)
        // ═════════════════════════════════════════════════════════════════════════

        private void TulostaLaskuPdf()
        {
            // Käytetään PrintDocument → tulostus PrintPreviewDialog kautta
            // → käyttäjä voi "tulostaa" PDF-kirjoittimelle (Windows PDF Printer)
            bool onMajoitus = _tabs.SelectedTab == _tabMajoitus;
            DataGridView grid = onMajoitus ? _majGrid : _palGrid;
            string otsikko = onMajoitus ? "Majoittumisraportti" : "Lisäpalveluraportti";
            string yhteensa = onMajoitus ? _majYhteensa.Text : _palYhteensa.Text;

            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("Ei raporttidataa. Hae ensin raportti.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tallenna yksinkertaisena HTML-tiedostona → avaa selaimessa → Ctrl+P → PDF
            var sfd = new SaveFileDialog
            {
                Title = "Tallenna raportti",
                Filter = "HTML-tiedosto (*.html)|*.html",
                FileName = $"{otsikko}_{DateTime.Today:yyyyMMdd}.html"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("<!DOCTYPE html><html lang='fi'><head><meta charset='UTF-8'>");
                sb.AppendLine("<style>");
                sb.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;margin:32px;color:#1e1e32}");
                sb.AppendLine("h1{color:#1e1e32;border-bottom:3px solid #34a882;padding-bottom:8px}");
                sb.AppendLine(".meta{color:#666;font-size:13px;margin-bottom:20px}");
                sb.AppendLine("table{width:100%;border-collapse:collapse;font-size:13px}");
                sb.AppendLine("th{background:#1e1e32;color:#fff;padding:8px 10px;text-align:left}");
                sb.AppendLine("td{padding:7px 10px;border-bottom:1px solid #ddd}");
                sb.AppendLine("tr:nth-child(even) td{background:#f5f6fa}");
                sb.AppendLine(".footer{margin-top:20px;font-weight:bold;font-size:14px;color:#1e1e32;text-align:right}");
                sb.AppendLine("@media print{body{margin:16px}}");
                sb.AppendLine("</style></head><body>");
                sb.AppendLine($"<h1>Village Newbies – {otsikko}</h1>");
                sb.AppendLine($"<div class='meta'>Tulostettu: {DateTime.Now:dd.MM.yyyy HH:mm}</div>");

                sb.AppendLine("<table><thead><tr>");
                foreach (DataGridViewColumn col in grid.Columns)
                    sb.AppendLine($"<th>{col.HeaderText}</th>");
                sb.AppendLine("</tr></thead><tbody>");

                foreach (DataGridViewRow row in grid.Rows)
                {
                    sb.AppendLine("<tr>");
                    foreach (DataGridViewCell cell in row.Cells)
                        sb.AppendLine($"<td>{cell.Value}</td>");
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</tbody></table>");
                sb.AppendLine($"<div class='footer'>{yhteensa}</div>");
                sb.AppendLine("</body></html>");

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);

                var result = MessageBox.Show(
                    $"Raportti tallennettu.\nAvataanko tiedosto selaimessa? (Voit tulostaa PDF:ksi Ctrl+P)",
                    "Tallennettu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tallennuksessa:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════════
        // UI-apumetodit
        // ═════════════════════════════════════════════════════════════════════════

        private static Label MakeLbl(string text, int x) => new()
        {
            Text = text,
            AutoSize = true,
            Location = new Point(x, 18),
            Font = F_LBL
        };

        private static DateTimePicker MakeDtp(int x) => new()
        {
            Location = new Point(x, 14),
            Width = 175,
            Format = DateTimePickerFormat.Short,
            TabStop = true
        };

        private static ComboBox MakeAlueCombo(int x) => new()
        {
            Location = new Point(x, 14),
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList,
            TabStop = true
        };

        private static Button MakeBtn(string text, int x, Color back) => new()
        {
            Text = text,
            Location = new Point(x, 12),
            Size = new Size(100, 30),
            FlatStyle = FlatStyle.Flat,
            BackColor = back,
            ForeColor = Color.White,
            Font = F_BTN,
            Cursor = Cursors.Hand,
            TabStop = true
        };

        private static DataGridView MakeGrid() => new()
        {
            Dock = DockStyle.Fill,
            BackgroundColor = Color.White,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BorderStyle = BorderStyle.None,
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 250)
            }
        };

        private static DataGridViewTextBoxColumn Col(string header, int width) => new()
        {
            HeaderText = header,
            MinimumWidth = width,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        };
    }
}