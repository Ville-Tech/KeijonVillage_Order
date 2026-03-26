namespace VillageNewbies_Projekti
{
    partial class MainFormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            label1 = new Label();
            panelLeft = new Panel();
            btnRaportit = new Button();
            btnLaskut = new Button();
            btnVaraukset = new Button();
            btnAsiakkaat = new Button();
            btnPalvelut = new Button();
            btnMokit = new Button();
            btnAlueet = new Button();
            btnEtusivu = new Button();
            panelFill = new Panel();
            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = SystemColors.ButtonFace;
            panelTop.Controls.Add(label1);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1284, 70);
            panelTop.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 15);
            label1.Name = "label1";
            label1.Size = new Size(470, 40);
            label1.TabIndex = 0;
            label1.Text = "Village Newbies - Varausjärjestelmä";
            // 
            // panelLeft
            // 
            panelLeft.AccessibleRole = AccessibleRole.None;
            panelLeft.BorderStyle = BorderStyle.Fixed3D;
            panelLeft.Controls.Add(btnRaportit);
            panelLeft.Controls.Add(btnLaskut);
            panelLeft.Controls.Add(btnVaraukset);
            panelLeft.Controls.Add(btnAsiakkaat);
            panelLeft.Controls.Add(btnPalvelut);
            panelLeft.Controls.Add(btnMokit);
            panelLeft.Controls.Add(btnAlueet);
            panelLeft.Controls.Add(btnEtusivu);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(220, 680);
            panelLeft.TabIndex = 1;
            // 
            // btnRaportit
            // 
            btnRaportit.Dock = DockStyle.Top;
            btnRaportit.FlatStyle = FlatStyle.Popup;
            btnRaportit.Font = new Font("Segoe UI", 12F);
            btnRaportit.Location = new Point(0, 595);
            btnRaportit.Name = "btnRaportit";
            btnRaportit.Size = new Size(216, 85);
            btnRaportit.TabIndex = 7;
            btnRaportit.Text = "Raportit";
            btnRaportit.UseVisualStyleBackColor = true;
            btnRaportit.Click += btnRaportit_Click;
            // 
            // btnLaskut
            // 
            btnLaskut.Dock = DockStyle.Top;
            btnLaskut.FlatStyle = FlatStyle.Popup;
            btnLaskut.Font = new Font("Segoe UI", 12F);
            btnLaskut.Location = new Point(0, 510);
            btnLaskut.Name = "btnLaskut";
            btnLaskut.Size = new Size(216, 85);
            btnLaskut.TabIndex = 6;
            btnLaskut.Text = "Laskut";
            btnLaskut.UseVisualStyleBackColor = true;
            btnLaskut.Click += btnLaskut_Click;
            // 
            // btnVaraukset
            // 
            btnVaraukset.Dock = DockStyle.Top;
            btnVaraukset.FlatStyle = FlatStyle.Popup;
            btnVaraukset.Font = new Font("Segoe UI", 12F);
            btnVaraukset.Location = new Point(0, 425);
            btnVaraukset.Name = "btnVaraukset";
            btnVaraukset.Size = new Size(216, 85);
            btnVaraukset.TabIndex = 5;
            btnVaraukset.Text = "Varaukset";
            btnVaraukset.UseVisualStyleBackColor = true;
            btnVaraukset.Click += btnVaraukset_Click;
            // 
            // btnAsiakkaat
            // 
            btnAsiakkaat.Dock = DockStyle.Top;
            btnAsiakkaat.FlatStyle = FlatStyle.Popup;
            btnAsiakkaat.Font = new Font("Segoe UI", 12F);
            btnAsiakkaat.Location = new Point(0, 340);
            btnAsiakkaat.Name = "btnAsiakkaat";
            btnAsiakkaat.Size = new Size(216, 85);
            btnAsiakkaat.TabIndex = 4;
            btnAsiakkaat.Text = "Asiakkaat";
            btnAsiakkaat.UseVisualStyleBackColor = true;
            btnAsiakkaat.Click += btnAsiakkaat_Click;
            // 
            // btnPalvelut
            // 
            btnPalvelut.Dock = DockStyle.Top;
            btnPalvelut.FlatStyle = FlatStyle.Popup;
            btnPalvelut.Font = new Font("Segoe UI", 12F);
            btnPalvelut.Location = new Point(0, 255);
            btnPalvelut.Name = "btnPalvelut";
            btnPalvelut.Size = new Size(216, 85);
            btnPalvelut.TabIndex = 3;
            btnPalvelut.Text = "Palvelut";
            btnPalvelut.UseVisualStyleBackColor = true;
            btnPalvelut.Click += btnPalvelut_Click;
            // 
            // btnMokit
            // 
            btnMokit.Dock = DockStyle.Top;
            btnMokit.FlatStyle = FlatStyle.Popup;
            btnMokit.Font = new Font("Segoe UI", 12F);
            btnMokit.Location = new Point(0, 170);
            btnMokit.Name = "btnMokit";
            btnMokit.Size = new Size(216, 85);
            btnMokit.TabIndex = 2;
            btnMokit.Text = "Mökit";
            btnMokit.UseVisualStyleBackColor = true;
            btnMokit.Click += btnMokit_Click;
            // 
            // btnAlueet
            // 
            btnAlueet.Dock = DockStyle.Top;
            btnAlueet.FlatStyle = FlatStyle.Popup;
            btnAlueet.Font = new Font("Segoe UI", 12F);
            btnAlueet.Location = new Point(0, 85);
            btnAlueet.Name = "btnAlueet";
            btnAlueet.Size = new Size(216, 85);
            btnAlueet.TabIndex = 1;
            btnAlueet.Text = "Alueet";
            btnAlueet.UseVisualStyleBackColor = true;
            btnAlueet.Click += btnAlueet_Click;
            // 
            // btnEtusivu
            // 
            btnEtusivu.Dock = DockStyle.Top;
            btnEtusivu.FlatStyle = FlatStyle.Popup;
            btnEtusivu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEtusivu.Location = new Point(0, 0);
            btnEtusivu.Name = "btnEtusivu";
            btnEtusivu.Size = new Size(216, 85);
            btnEtusivu.TabIndex = 0;
            btnEtusivu.Text = "Etusivu";
            btnEtusivu.UseVisualStyleBackColor = true;
            btnEtusivu.Click += btnEtusivu_Click;
            // 
            // panelFill
            // 
            panelFill.BorderStyle = BorderStyle.Fixed3D;
            panelFill.Dock = DockStyle.Fill;
            panelFill.Location = new Point(220, 70);
            panelFill.Name = "panelFill";
            panelFill.Size = new Size(1064, 680);
            panelFill.TabIndex = 2;
            // 
            // MainFormView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1284, 750);
            Controls.Add(panelFill);
            Controls.Add(panelLeft);
            Controls.Add(panelTop);
            MaximumSize = new Size(1300, 789);
            MinimumSize = new Size(1100, 789);
            Name = "MainFormView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainFormView";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Panel panelLeft;
        private Panel panelFill;
        private Button btnRaportit;
        private Button btnLaskut;
        private Button btnVaraukset;
        private Button btnAsiakkaat;
        private Button btnPalvelut;
        private Button btnMokit;
        private Button btnAlueet;
        private Button btnEtusivu;
        private Label label1;
    }
}