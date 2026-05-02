using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Views
{
    public partial class LaskutView : UserControl
    {
        private readonly LaskuService _laskuService = new();
        private readonly VarausService _varausService = new();
        private readonly AsiakasService _asiakasService = new();
        private readonly MokkiService _mokkiService = new();

        private Panel pnlSidebar, pnlWorkArea;
        private DataGridView dgvMain;
        private Label lblOtsikko;
        private Button btnToiminto, btnPdf;

        // Muistetaan mikä näkymä on aktiivinen PDF:ää varten
        private bool _onLaskuNakyma = false;

        public LaskutView()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Sivupalkki
            pnlSidebar = new Panel
            {
                Width = 250,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Työskentelyalue
            pnlWorkArea = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30) };

            lblOtsikko = new Label
            {
                Text = "Laskutus",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(30, 20),
                AutoSize = true
            };

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
                BorderStyle = BorderStyle.None,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 246, 250)
                }
            };

            // Pää-toimintopainike (Luo lasku / Merkitse maksetuksi)
            btnToiminto = new Button
            {
                Location = new Point(30, 500),
                Size = new Size(220, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Visible = false
            };

            // PDF-painike
            btnPdf = new Button
            {
                Text = "📄 Tulosta PDF",
                Location = new Point(260, 500),
                Size = new Size(160, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                Visible = false
            };
            btnPdf.Click += BtnPdf_Click;

            AddMenuBtn("📄 Kaikki laskut", 80, (s, e) => NaytaKaikkiLaskut());
            AddMenuBtn("➕ Luo uusi lasku", 140, (s, e) => NaytaLaskuttamattomat());

            pnlWorkArea.Controls.AddRange(new Control[] { lblOtsikko, dgvMain, btnToiminto, btnPdf });
            this.Controls.AddRange(new Control[] { pnlWorkArea, pnlSidebar });
        }

        private void AddMenuBtn(string t, int y, EventHandler h)
        {
            var b = new Button
            {
                Text = t,
                Location = new Point(0, y),
                Width = 250,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Segoe UI", 11)
            };
            b.Click += h;
            pnlSidebar.Controls.Add(b);
        }

        // ── Näkymät ───────────────────────────────────────────────────────────

        private void NaytaKaikkiLaskut()
        {
            _onLaskuNakyma = true;
            lblOtsikko.Text = "Kaikki laskut (ALV 24 %)";
            var laskut = _laskuService.HaeLaskut();

            dgvMain.DataSource = laskut.Select(l => new
            {
                ID = l.Lasku_ID,
                Varaus = l.Varaus_ID,
                Veroton = l.Summa.ToString("N2") + " €",
                ALV_24 = l.Alv.ToString("N2") + " €",
                Yhteensa = (l.Summa + l.Alv).ToString("N2") + " €",
                Tila = l.Maksettu == 1 ? "✅ Maksettu" : "⏳ Avoin"
            }).ToList();

            btnToiminto.Text = "MERKITSE MAKSETUKSI";
            btnToiminto.BackColor = Color.FromArgb(200, 230, 255);
            btnToiminto.Visible = true;
            btnPdf.Visible = true;

            btnToiminto.Click -= BtnLuo_Click;
            btnToiminto.Click -= BtnMaksu_Click;
            btnToiminto.Click += BtnMaksu_Click;
        }

        private void NaytaLaskuttamattomat()
        {
            _onLaskuNakyma = false;
            lblOtsikko.Text = "Laskuttamattomat varaukset";

            var laskut = _laskuService.HaeLaskut();
            var varaukset = _varausService.HaeVaraukset()
                .Where(v => !laskut.Any(l => l.Varaus_ID == v.Varaus_ID))
                .ToList();
            var asiakkaat = _asiakasService.HaeAsiakkaat();
            var mokit = _mokkiService.HaeMokit();

            dgvMain.DataSource = varaukset.Select(v =>
            {
                var m = mokit.FirstOrDefault(x => x.Mokki_ID == v.Mokki_ID);
                int paivat = v.Varattu_Loppupvm.HasValue && v.Varattu_Alkupvm.HasValue
                    ? Math.Max(1, (v.Varattu_Loppupvm.Value - v.Varattu_Alkupvm.Value).Days)
                    : 1;
                double arvio = Math.Round((m?.Hinta ?? 0) * paivat * 1.24, 2);
                return new
                {
                    ID = v.Varaus_ID,
                    Asiakas = asiakkaat.Find(a => a.Asiakas_ID == v.Asiakas_ID)?.KokoNimi ?? "N/A",
                    Mokki = m?.Mokkinimi ?? "-",
                    Alkaa = v.Varattu_Alkupvm?.ToString("dd.MM.yyyy"),
                    Loppuu = v.Varattu_Loppupvm?.ToString("dd.MM.yyyy"),
                    Paivat = paivat,
                    Arvio = arvio.ToString("N2") + " €"
                };
            }).ToList();

            btnToiminto.Text = "LUO LASKU VALITULLE";
            btnToiminto.BackColor = Color.FromArgb(192, 255, 192);
            btnToiminto.Visible = true;
            btnPdf.Visible = false;   // ei PDF:ää ennen laskun luontia

            btnToiminto.Click -= BtnLuo_Click;
            btnToiminto.Click -= BtnMaksu_Click;
            btnToiminto.Click += BtnLuo_Click;
        }

        // ── Toiminnot ─────────────────────────────────────────────────────────

        private void BtnLuo_Click(object s, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvMain.CurrentRow.Cells[0].Value);
            try
            {
                _laskuService.LuoLaskuVarauksesta(id);
                MessageBox.Show("Lasku luotu onnistuneesti!", "Onnistui",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NaytaKaikkiLaskut();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMaksu_Click(object s, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvMain.CurrentRow.Cells[0].Value);
            _laskuService.MerkitseMaksetuksi(id);
            NaytaKaikkiLaskut();
        }

        // ── PDF-tulostus ──────────────────────────────────────────────────────

        private void BtnPdf_Click(object s, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
            {
                MessageBox.Show("Valitse ensin lasku listasta.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int laskuId = Convert.ToInt32(dgvMain.CurrentRow.Cells[0].Value);
            int varausId = Convert.ToInt32(dgvMain.CurrentRow.Cells[1].Value);

            // Hae tiedot PDF:ää varten
            try
            {
                var lasku = _laskuService.HaeLaskut().FirstOrDefault(l => l.Lasku_ID == laskuId);
                var varaus = _varausService.HaeVaraukset().FirstOrDefault(v => v.Varaus_ID == varausId);
                var asiakas = varaus != null
                    ? _asiakasService.HaeAsiakasId(varaus.Asiakas_ID)
                    : null;
                var mokki = varaus != null
                    ? _mokkiService.HaeMokit().FirstOrDefault(m => m.Mokki_ID == varaus.Mokki_ID)
                    : null;

                if (lasku == null || varaus == null)
                {
                    MessageBox.Show("Laskun tietoja ei löydy.", "Virhe",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var sfd = new SaveFileDialog
                {
                    Title = "Tallenna lasku",
                    Filter = "HTML-tiedosto (*.html)|*.html",
                    FileName = $"Lasku_{laskuId}_{DateTime.Today:yyyyMMdd}.html"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                string html = RakennaLaskuHtml(lasku, varaus, asiakas, mokki);
                File.WriteAllText(sfd.FileName, html, Encoding.UTF8);

                var tulos = MessageBox.Show(
                    "Lasku tallennettu.\nAvataanko selaimessa? (Tulosta PDF:ksi Ctrl+P)",
                    "Tallennettu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (tulos == DialogResult.Yes)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe PDF-viennissä:\n{ex.Message}", "Virhe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string RakennaLaskuHtml(Lasku lasku, Varaus varaus, Asiakas? asiakas, Mokki? mokki)
        {
            int paivat = varaus.Varattu_Loppupvm.HasValue && varaus.Varattu_Alkupvm.HasValue
                ? Math.Max(1, (varaus.Varattu_Loppupvm.Value - varaus.Varattu_Alkupvm.Value).Days)
                : 1;

            double yhteensa = lasku.Summa + lasku.Alv;
            string tila = lasku.Maksettu == 1 ? "MAKSETTU" : "AVOIN";
            string tilaVari = lasku.Maksettu == 1 ? "#22c55e" : "#ef4444";

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html lang='fi'><head><meta charset='UTF-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine(@"
body { font-family: Segoe UI, Arial, sans-serif; margin: 0; padding: 40px; color: #1e1e32; background: #fff; }
.header { display: flex; justify-content: space-between; align-items: flex-start;
          border-bottom: 3px solid #1e1e32; padding-bottom: 20px; margin-bottom: 30px; }
.company { font-size: 28px; font-weight: bold; color: #1e1e32; }
.company small { display: block; font-size: 13px; font-weight: normal; color: #666; margin-top: 4px; }
.invoice-title { font-size: 22px; font-weight: bold; text-align: right; }
.invoice-title .num { color: #34a882; }
.badge { display: inline-block; padding: 4px 14px; border-radius: 20px; font-weight: bold;
         font-size: 13px; margin-top: 6px; }
.section { margin-bottom: 24px; }
.section h3 { font-size: 12px; text-transform: uppercase; letter-spacing: 1px;
              color: #999; margin-bottom: 8px; border-bottom: 1px solid #eee; padding-bottom: 4px; }
.info-grid { display: grid; grid-template-columns: 1fr 1fr; gap: 8px 40px; }
.info-row { font-size: 14px; }
.info-row span { color: #666; font-size: 12px; display: block; }
table { width: 100%; border-collapse: collapse; margin-top: 8px; font-size: 14px; }
th { background: #1e1e32; color: #fff; padding: 10px 12px; text-align: left; }
td { padding: 9px 12px; border-bottom: 1px solid #eee; }
tr:last-child td { border-bottom: none; }
.totals { margin-top: 20px; display: flex; justify-content: flex-end; }
.totals table { width: 300px; }
.totals td { padding: 6px 12px; }
.totals .grand { font-weight: bold; font-size: 16px; background: #f5f6fa; }
.footer { margin-top: 40px; font-size: 12px; color: #999; text-align: center; border-top: 1px solid #eee; padding-top: 16px; }
@media print { body { padding: 20px; } }
");
            sb.AppendLine("</style></head><body>");

            // Ylätunniste
            sb.AppendLine("<div class='header'>");
            sb.AppendLine($@"<div class='company'>Village Newbies
<small>Mökkivuokrauspalvelu<br>info@villagenewbies.fi</small></div>");
            sb.AppendLine($@"<div class='invoice-title'>LASKU
<div class='num'>#{lasku.Lasku_ID}</div>
<div class='badge' style='background:{tilaVari};color:#fff'>{tila}</div>
<div style='font-size:13px;font-weight:normal;margin-top:6px'>
  Päivämäärä: {DateTime.Today:dd.MM.yyyy}</div>
</div>");
            sb.AppendLine("</div>");

            // Asiakas & varaus
            sb.AppendLine("<div class='info-grid section'>");
            sb.AppendLine($@"
<div class='info-row'><span>Asiakas</span>{asiakas?.KokoNimi ?? "–"}</div>
<div class='info-row'><span>Varaus ID</span>#{varaus.Varaus_ID}</div>
<div class='info-row'><span>Osoite</span>{asiakas?.Lahiosoite ?? "–"}</div>
<div class='info-row'><span>Mökki</span>{mokki?.Mokkinimi ?? "–"}</div>
<div class='info-row'><span>Sähköposti</span>{asiakas?.Sahkoposti ?? "–"}</div>
<div class='info-row'><span>Ajanjakso</span>
  {varaus.Varattu_Alkupvm?.ToString("dd.MM.yyyy") ?? "–"} – {varaus.Varattu_Loppupvm?.ToString("dd.MM.yyyy") ?? "–"}
  ({paivat} vrk)</div>
");
            sb.AppendLine("</div>");

            // Erittelytaulukko
            sb.AppendLine("<div class='section'><h3>Erittely</h3>");
            sb.AppendLine("<table><thead><tr><th>Kuvaus</th><th>Määrä</th><th>á-hinta</th><th>Yhteensä</th></tr></thead><tbody>");
            double mokkiHinta = (mokki?.Hinta ?? 0) * paivat;
            sb.AppendLine($"<tr><td>Majoitus – {mokki?.Mokkinimi ?? "–"}</td><td>{paivat} vrk</td><td>{mokki?.Hinta:N2} €</td><td>{mokkiHinta:N2} €</td></tr>");

            // Lisäpalvelut
            double palveluYht = lasku.Summa - mokkiHinta;
            if (palveluYht > 0.01)
                sb.AppendLine($"<tr><td>Lisäpalvelut yhteensä</td><td>–</td><td>–</td><td>{palveluYht:N2} €</td></tr>");

            sb.AppendLine("</tbody></table></div>");

            // Summat
            sb.AppendLine(@"<div class='totals'><table>
<tr><td>Veroton summa</td><td style='text-align:right'>" + lasku.Summa.ToString("N2") + @" €</td></tr>
<tr><td>ALV 24 %</td><td style='text-align:right'>" + lasku.Alv.ToString("N2") + @" €</td></tr>
<tr class='grand'><td><strong>Yhteensä</strong></td><td style='text-align:right'><strong>" + yhteensa.ToString("N2") + @" €</strong></td></tr>
</table></div>");

            sb.AppendLine($"<div class='footer'>Village Newbies · Lasku #{lasku.Lasku_ID} · Tulostettu {DateTime.Now:dd.MM.yyyy HH:mm}</div>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}