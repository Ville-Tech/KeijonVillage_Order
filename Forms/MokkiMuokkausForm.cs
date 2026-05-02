using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Forms
{
    public class MokkiMuokkausForm : Form
    {
        public Mokki Mokki { get; private set; }
        public bool Poistettu { get; private set; } = false;

        private ComboBox cmbAlue;
        private TextBox txtNimi, txtKatuosoite, txtPostinro,
                         txtHinta, txtHenkilomaara, txtKuvaus, txtVarustelu;
        private Button btnTallenna, btnPoista, btnPeruuta;

        public MokkiMuokkausForm(Mokki mokki, AlueService alueService)
        {
            Mokki = mokki;

            this.Text = $"Muokkaa mökkiä — {mokki.Mokkinimi}";
            this.Size = new System.Drawing.Size(480, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            int y = 20;

            void LisaaKentta(string otsikko, Control ctrl, int extraH = 0)
            {
                var lbl = new Label
                {
                    Text = otsikko,
                    Location = new System.Drawing.Point(20, y),
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 9f)
                };
                ctrl.Location = new System.Drawing.Point(20, y + 20);
                if (ctrl is TextBox tb) tb.Width = 420;
                if (ctrl is ComboBox cb) cb.Width = 420;
                this.Controls.Add(lbl);
                this.Controls.Add(ctrl);
                y += 55 + extraH;
            }

            cmbAlue = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            LisaaKentta("Alue *", cmbAlue);

            txtNimi = new TextBox { MaxLength = 45 };
            LisaaKentta("Mökin nimi *", txtNimi);

            txtKatuosoite = new TextBox { MaxLength = 45 };
            LisaaKentta("Katuosoite", txtKatuosoite);

            txtPostinro = new TextBox { MaxLength = 5 };
            LisaaKentta("Postinumero *", txtPostinro);

            txtHinta = new TextBox { MaxLength = 8 };
            LisaaKentta("Hinta (€/yö) *", txtHinta);

            txtHenkilomaara = new TextBox { MaxLength = 3 };
            LisaaKentta("Henkilömäärä", txtHenkilomaara);

            txtKuvaus = new TextBox
            {
                Multiline = true,
                Height = 80,
                Width = 420,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 150
            };
            LisaaKentta("Kuvaus", txtKuvaus, extraH: 55);

            txtVarustelu = new TextBox { MaxLength = 100 };
            LisaaKentta("Varustelu", txtVarustelu);

            // Napit
            btnTallenna = new Button
            {
                Text = "💾 Tallenna",
                Location = new System.Drawing.Point(20, y + 10),
                Size = new System.Drawing.Size(180, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.FromArgb(200, 235, 215)
            };
            btnTallenna.Click += BtnTallenna_Click;

            btnPoista = new Button
            {
                Text = "🗑 Poista mökki",
                Location = new System.Drawing.Point(210, y + 10),
                Size = new System.Drawing.Size(120, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.FromArgb(255, 200, 200)
            };
            btnPoista.Click += BtnPoista_Click;

            btnPeruuta = new Button
            {
                Text = "Peruuta",
                Location = new System.Drawing.Point(340, y + 10),
                Size = new System.Drawing.Size(100, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.White,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnTallenna);
            this.Controls.Add(btnPoista);
            this.Controls.Add(btnPeruuta);

            // Täytä alue-dropdown
            try
            {
                var alueet = alueService.HaeAlueet();
                cmbAlue.DataSource = alueet;
                cmbAlue.DisplayMember = "Nimi";
                cmbAlue.ValueMember = "Alue_ID";
                cmbAlue.SelectedValue = mokki.Alue_ID;
            }
            catch { }

            // Täytä kentät mökin tiedoilla
            txtNimi.Text = mokki.Mokkinimi ?? "";
            txtKatuosoite.Text = mokki.Katuosoite ?? "";
            txtPostinro.Text = mokki.Postinro ?? "";
            txtHinta.Text = mokki.Hinta.ToString("0.00");
            txtHenkilomaara.Text = mokki.Henkilomaara.ToString();
            txtKuvaus.Text = mokki.Kuvaus ?? "";
            txtVarustelu.Text = mokki.Varustelu ?? "";

            // Tab-järjestys
            cmbAlue.TabIndex = 0;
            txtNimi.TabIndex = 1;
            txtKatuosoite.TabIndex = 2;
            txtPostinro.TabIndex = 3;
            txtHinta.TabIndex = 4;
            txtHenkilomaara.TabIndex = 5;
            txtKuvaus.TabIndex = 6;
            txtVarustelu.TabIndex = 7;
            btnTallenna.TabIndex = 8;
            btnPoista.TabIndex = 9;
            btnPeruuta.TabIndex = 10;
        }

        private void BtnTallenna_Click(object sender, EventArgs e)
        {
            // Validointi
            if (cmbAlue.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtNimi.Text) ||
                string.IsNullOrWhiteSpace(txtPostinro.Text) ||
                string.IsNullOrWhiteSpace(txtHinta.Text))
            {
                MessageBox.Show("Täytä pakolliset kentät: Alue, Nimi, Postinumero, Hinta.",
                    "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPostinro.Text.Length != 5 || !txtPostinro.Text.All(char.IsDigit))
            {
                MessageBox.Show("Postinumeron on oltava tasan 5 numeroa.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostinro.Focus();
                return;
            }

            if (!double.TryParse(txtHinta.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double hinta) || hinta < 0)
            {
                MessageBox.Show("Hinta ei ole kelvollinen positiivinen luku.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHinta.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtHenkilomaara.Text) &&
                (!int.TryParse(txtHenkilomaara.Text, out int hlomCheck) || hlomCheck < 0))
            {
                MessageBox.Show("Henkilömäärän täytyy olla positiivinen kokonaisluku.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHenkilomaara.Focus();
                return;
            }

            int.TryParse(txtHenkilomaara.Text, out int hlom);

            Mokki = new Mokki
            {
                Mokki_ID = Mokki.Mokki_ID,
                Alue_ID = (int)cmbAlue.SelectedValue,
                Postinro = txtPostinro.Text.Trim(),
                Mokkinimi = txtNimi.Text.Trim(),
                Katuosoite = txtKatuosoite.Text.Trim(),
                Hinta = hinta,
                Henkilomaara = hlom,
                Kuvaus = txtKuvaus.Text.Trim(),
                Varustelu = txtVarustelu.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnPoista_Click(object sender, EventArgs e)
        {
            var vastaus = MessageBox.Show(
                $"Poistetaanko mökki \"{Mokki.Mokkinimi}\"?\nTämä poistaa myös mökin varaukset!",
                "Vahvista poisto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (vastaus == DialogResult.Yes)
            {
                Poistettu = true;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}