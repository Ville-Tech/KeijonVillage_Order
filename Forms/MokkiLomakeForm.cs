using System;
using System.Linq;
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
            this.Size = new System.Drawing.Size(480, 680);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            int y = 20;
            int tabIdx = 0;

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
                ctrl.TabIndex = tabIdx++;
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
                MaxLength = 150
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
                BackColor = System.Drawing.Color.FromArgb(200, 235, 215),
                DialogResult = DialogResult.None,
                TabIndex = tabIdx++
            };
            btnOk.Click += BtnOk_Click;

            btnPeruuta = new Button
            {
                Text = "Peruuta",
                Location = new System.Drawing.Point(220, y + 10),
                Size = new System.Drawing.Size(190, 38),
                FlatStyle = FlatStyle.Popup,
                BackColor = System.Drawing.Color.White,
                DialogResult = DialogResult.Cancel,
                TabIndex = tabIdx++
            };

            this.Controls.Add(btnOk);
            this.Controls.Add(btnPeruuta);
            this.AcceptButton = btnOk;
            this.CancelButton = btnPeruuta;

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
            // Pakolliset kentät
            if (cmbAlue.SelectedValue == null)
            {
                MessageBox.Show("Valitse alue.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAlue.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txtNimi.Text))
            {
                MessageBox.Show("Mökin nimi on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNimi.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txtPostinro.Text))
            {
                MessageBox.Show("Postinumero on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostinro.Focus(); return;
            }
            if (txtPostinro.Text.Length != 5 || !txtPostinro.Text.All(char.IsDigit))
            {
                MessageBox.Show("Postinumeron on oltava tasan 5 numeroa.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPostinro.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txtHinta.Text))
            {
                MessageBox.Show("Hinta on pakollinen.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHinta.Focus(); return;
            }
            if (!double.TryParse(txtHinta.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double hinta) || hinta < 0)
            {
                MessageBox.Show("Hinta ei ole kelvollinen positiivinen luku.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHinta.Focus(); return;
            }
            if (!string.IsNullOrWhiteSpace(txtHenkilomaara.Text) &&
                (!int.TryParse(txtHenkilomaara.Text, out int hlomCheck) || hlomCheck < 0))
            {
                MessageBox.Show("Henkilömäärän täytyy olla positiivinen kokonaisluku.", "Huomio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHenkilomaara.Focus(); return;
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