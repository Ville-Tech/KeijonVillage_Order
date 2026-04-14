using System;
using System.Windows.Forms;
using VillageNewbies_Projekti.Models;
using VillageNewbies_Projekti.Services;

namespace VillageNewbies_Projekti.Forms
{
    public class MokkiLomakeForm : Form
    {
        public Mokki Mokki { get; private set; } = new Mokki();

        private ComboBox cmbAlue;
        private TextBox txtNimi, txtKatuosoite, txtPostinro,
                         txtHinta, txtHenkilomaara, txtKuvaus, txtVarustelu;
        private Button btnOk, btnPeruuta;

        public MokkiLomakeForm(AlueService alueService)
        {
            this.Text = "Lisää mökki";
            this.Size = new System.Drawing.Size(480, 660);
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

            txtNimi = new TextBox { PlaceholderText = "Mökin nimi", MaxLength = 45 };
            LisaaKentta("Mökin nimi *", txtNimi);

            txtKatuosoite = new TextBox { PlaceholderText = "Katuosoite", MaxLength = 45 };
            LisaaKentta("Katuosoite", txtKatuosoite);

            txtPostinro = new TextBox { PlaceholderText = "12345", MaxLength = 5 };
            LisaaKentta("Postinumero *", txtPostinro);

            txtHinta = new TextBox { PlaceholderText = "0.00", MaxLength = 8 };
            LisaaKentta("Hinta (€/yö) *", txtHinta);

            txtHenkilomaara = new TextBox { PlaceholderText = "0", MaxLength = 3 };
            LisaaKentta("Henkilömäärä", txtHenkilomaara);

            txtKuvaus = new TextBox
            {
                PlaceholderText = "Kuvaus",
                Multiline = true,
                Height = 80,
                Width = 420,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 150 // tietokannan VARCHAR(150) mukaan
            };
            LisaaKentta("Kuvaus", txtKuvaus, extraH: 55);

            txtVarustelu = new TextBox { PlaceholderText = "Esim. sauna, WiFi", MaxLength = 100 };
            LisaaKentta("Varustelu", txtVarustelu);

            btnOk = new Button
            {
                Text = "Lisää",
                Location = new System.Drawing.Point(20, y + 10),
                Size = new System.Drawing.Size(190, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.White,
                DialogResult = DialogResult.None
            };
            btnOk.Click += BtnOk_Click;

            btnPeruuta = new Button
            {
                Text = "Peruuta",
                Location = new System.Drawing.Point(220, y + 10),
                Size = new System.Drawing.Size(190, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.White,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnOk);
            this.Controls.Add(btnPeruuta);

            try
            {
                var alueet = alueService.HaeAlueet();
                cmbAlue.DataSource = alueet;
                cmbAlue.DisplayMember = "Nimi";
                cmbAlue.ValueMember = "Alue_ID";
                cmbAlue.SelectedIndex = -1;
            }
            catch { }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (cmbAlue.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtNimi.Text) ||
                string.IsNullOrWhiteSpace(txtPostinro.Text) ||
                string.IsNullOrWhiteSpace(txtHinta.Text))
            {
                MessageBox.Show("Täytä pakolliset kentät: Alue, Nimi, Postinumero, Hinta.",
                    "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtHinta.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double hinta))
            {
                MessageBox.Show("Hinta ei ole kelvollinen luku.", "Huomio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtHenkilomaara.Text, out int hlom);

            Mokki = new Mokki
            {
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
    }
}