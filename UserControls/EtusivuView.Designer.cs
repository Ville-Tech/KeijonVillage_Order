namespace VillageNewbies_Projekti.Views
{
    partial class EtusivuView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel = new Panel();
            btnLuoLasku = new Button();
            btnUusiAsiakas = new Button();
            btnUusiVaraus = new Button();
            panelLaskut = new Panel();
            lblLaskut = new Label();
            label3 = new Label();
            panelLoppuu = new Panel();
            lblLoppuu = new Label();
            label2 = new Label();
            panelAlkaa = new Panel();
            panel1 = new Panel();
            lblAlkaa = new Label();
            label1 = new Label();
            lblTervetuloa = new Label();
            panel.SuspendLayout();
            panelLaskut.SuspendLayout();
            panelLoppuu.SuspendLayout();
            panelAlkaa.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(btnLuoLasku);
            panel.Controls.Add(btnUusiAsiakas);
            panel.Controls.Add(btnUusiVaraus);
            panel.Controls.Add(panelLaskut);
            panel.Controls.Add(panelLoppuu);
            panel.Controls.Add(panelAlkaa);
            panel.Location = new Point(0, 228);
            panel.Name = "panel";
            panel.Size = new Size(1064, 453);
            panel.TabIndex = 0;
            // 
            // btnLuoLasku
            // 
            btnLuoLasku.BackColor = Color.White;
            btnLuoLasku.FlatAppearance.BorderSize = 0;
            btnLuoLasku.FlatStyle = FlatStyle.Popup;
            btnLuoLasku.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLuoLasku.ForeColor = Color.Black;
            btnLuoLasku.Location = new Point(460, 300);
            btnLuoLasku.Name = "btnLuoLasku";
            btnLuoLasku.Size = new Size(180, 45);
            btnLuoLasku.TabIndex = 2;
            btnLuoLasku.Text = "Luo lasku";
            btnLuoLasku.UseVisualStyleBackColor = false;
            btnLuoLasku.Click += btnLuoLasku_Click_1;
            // 
            // btnUusiAsiakas
            // 
            btnUusiAsiakas.BackColor = Color.White;
            btnUusiAsiakas.FlatAppearance.BorderSize = 0;
            btnUusiAsiakas.FlatStyle = FlatStyle.Popup;
            btnUusiAsiakas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUusiAsiakas.ForeColor = Color.Black;
            btnUusiAsiakas.Location = new Point(260, 300);
            btnUusiAsiakas.Name = "btnUusiAsiakas";
            btnUusiAsiakas.Size = new Size(180, 45);
            btnUusiAsiakas.TabIndex = 3;
            btnUusiAsiakas.Text = "Uusi asiakas";
            btnUusiAsiakas.UseVisualStyleBackColor = false;
            btnUusiAsiakas.Click += btnUusiAsiakas_Click_1;
            // 
            // btnUusiVaraus
            // 
            btnUusiVaraus.BackColor = Color.White;
            btnUusiVaraus.FlatStyle = FlatStyle.Popup;
            btnUusiVaraus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUusiVaraus.ForeColor = Color.Black;
            btnUusiVaraus.Location = new Point(60, 300);
            btnUusiVaraus.Name = "btnUusiVaraus";
            btnUusiVaraus.Size = new Size(180, 45);
            btnUusiVaraus.TabIndex = 3;
            btnUusiVaraus.Text = "Uusi varaus";
            btnUusiVaraus.UseVisualStyleBackColor = false;
            btnUusiVaraus.Click += btnUusiVaraus_Click_1;
            // 
            // panelLaskut
            // 
            panelLaskut.BackColor = Color.White;
            panelLaskut.BorderStyle = BorderStyle.Fixed3D;
            panelLaskut.Controls.Add(lblLaskut);
            panelLaskut.Controls.Add(label3);
            panelLaskut.Location = new Point(680, 120);
            panelLaskut.Name = "panelLaskut";
            panelLaskut.Padding = new Padding(15);
            panelLaskut.Size = new Size(280, 120);
            panelLaskut.TabIndex = 3;
            // 
            // lblLaskut
            // 
            lblLaskut.AutoSize = true;
            lblLaskut.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLaskut.Location = new Point(35, 55);
            lblLaskut.Name = "lblLaskut";
            lblLaskut.Size = new Size(29, 40);
            lblLaskut.TabIndex = 3;
            lblLaskut.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(35, 20);
            label3.Name = "label3";
            label3.Size = new Size(185, 21);
            label3.TabIndex = 2;
            label3.Text = "Maksamattomat laskut";
            // 
            // panelLoppuu
            // 
            panelLoppuu.BackColor = Color.White;
            panelLoppuu.BorderStyle = BorderStyle.Fixed3D;
            panelLoppuu.Controls.Add(lblLoppuu);
            panelLoppuu.Controls.Add(label2);
            panelLoppuu.Location = new Point(360, 120);
            panelLoppuu.Name = "panelLoppuu";
            panelLoppuu.Padding = new Padding(15);
            panelLoppuu.Size = new Size(280, 120);
            panelLoppuu.TabIndex = 2;
            // 
            // lblLoppuu
            // 
            lblLoppuu.AutoSize = true;
            lblLoppuu.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLoppuu.Location = new Point(35, 55);
            lblLoppuu.Name = "lblLoppuu";
            lblLoppuu.Size = new Size(29, 40);
            lblLoppuu.TabIndex = 2;
            lblLoppuu.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(35, 20);
            label2.Name = "label2";
            label2.Size = new Size(124, 21);
            label2.TabIndex = 1;
            label2.Text = "Tänään loppuu";
            // 
            // panelAlkaa
            // 
            panelAlkaa.BackColor = Color.White;
            panelAlkaa.BorderStyle = BorderStyle.Fixed3D;
            panelAlkaa.Controls.Add(panel1);
            panelAlkaa.Controls.Add(lblAlkaa);
            panelAlkaa.Controls.Add(label1);
            panelAlkaa.Location = new Point(40, 120);
            panelAlkaa.Name = "panelAlkaa";
            panelAlkaa.Padding = new Padding(15);
            panelAlkaa.Size = new Size(280, 120);
            panelAlkaa.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Location = new Point(-2, 194);
            panel1.Name = "panel1";
            panel1.Size = new Size(999, 63);
            panel1.TabIndex = 4;
            // 
            // lblAlkaa
            // 
            lblAlkaa.AutoSize = true;
            lblAlkaa.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAlkaa.Location = new Point(35, 55);
            lblAlkaa.Name = "lblAlkaa";
            lblAlkaa.Size = new Size(29, 40);
            lblAlkaa.TabIndex = 1;
            lblAlkaa.Text = "-";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(35, 20);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 0;
            label1.Text = "Tänään alkaa";
            // 
            // lblTervetuloa
            // 
            lblTervetuloa.AutoSize = true;
            lblTervetuloa.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTervetuloa.Location = new Point(60, 40);
            lblTervetuloa.Name = "lblTervetuloa";
            lblTervetuloa.Size = new Size(173, 37);
            lblTervetuloa.TabIndex = 1;
            lblTervetuloa.Text = "TERVETULOA";
            // 
            // EtusivuView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            Controls.Add(lblTervetuloa);
            Controls.Add(panel);
            Name = "EtusivuView";
            Size = new Size(1064, 681);
            panel.ResumeLayout(false);
            panelLaskut.ResumeLayout(false);
            panelLaskut.PerformLayout();
            panelLoppuu.ResumeLayout(false);
            panelLoppuu.PerformLayout();
            panelAlkaa.ResumeLayout(false);
            panelAlkaa.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel;
        private Panel panelAlkaa;
        private Panel panelLaskut;
        private Panel panelLoppuu;
        private Label lblTervetuloa;
        private Label lblLaskut;
        private Label label3;
        private Label lblLoppuu;
        private Label label2;
        private Label lblAlkaa;
        private Label label1;
        private Button btnUusiAsiakas;
        private Button btnUusiVaraus;
        private Button btnLuoLasku;
        private Panel panel1;
    }
}
