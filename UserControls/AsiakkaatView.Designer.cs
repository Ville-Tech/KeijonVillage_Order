namespace VillageNewbies_Projekti.Views
{
    partial class AsiakkaatView
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
            ylaPanel = new Panel();
            lblTitle = new Label();
            keskiPanel = new Panel();
            panelVasen = new Panel();
            dgvAsiakkaat = new DataGridView();
            txtHaku = new TextBox();
            label1 = new Label();
            panelOikea = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtPuhelin = new TextBox();
            txtEmail = new TextBox();
            txtPostinro = new TextBox();
            txtLahiosoite = new TextBox();
            txtSukunimi = new TextBox();
            btnTyhjenna = new Button();
            btnPoista = new Button();
            btnMuokkaa = new Button();
            btnLisaa = new Button();
            txtEtunimi = new TextBox();
            lblNimi = new Label();
            ylaPanel.SuspendLayout();
            keskiPanel.SuspendLayout();
            panelVasen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAsiakkaat).BeginInit();
            panelOikea.SuspendLayout();
            SuspendLayout();
            // 
            // ylaPanel
            // 
            ylaPanel.Controls.Add(lblTitle);
            ylaPanel.Dock = DockStyle.Top;
            ylaPanel.Location = new Point(0, 0);
            ylaPanel.Margin = new Padding(3, 2, 3, 2);
            ylaPanel.Name = "ylaPanel";
            ylaPanel.Size = new Size(1064, 112);
            ylaPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F);
            lblTitle.Location = new Point(60, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(129, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Asiakkaat";
            // 
            // keskiPanel
            // 
            keskiPanel.Controls.Add(panelVasen);
            keskiPanel.Controls.Add(panelOikea);
            keskiPanel.Dock = DockStyle.Fill;
            keskiPanel.Location = new Point(0, 112);
            keskiPanel.Margin = new Padding(3, 2, 3, 2);
            keskiPanel.Name = "keskiPanel";
            keskiPanel.Padding = new Padding(18, 15, 18, 15);
            keskiPanel.Size = new Size(1064, 569);
            keskiPanel.TabIndex = 3;
            // 
            // panelVasen
            // 
            panelVasen.Controls.Add(dgvAsiakkaat);
            panelVasen.Controls.Add(txtHaku);
            panelVasen.Controls.Add(label1);
            panelVasen.Dock = DockStyle.Left;
            panelVasen.Location = new Point(18, 15);
            panelVasen.Margin = new Padding(3, 2, 3, 2);
            panelVasen.Name = "panelVasen";
            panelVasen.Size = new Size(350, 539);
            panelVasen.TabIndex = 3;
            // 
            // dgvAsiakkaat
            // 
            dgvAsiakkaat.BackgroundColor = Color.White;
            dgvAsiakkaat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAsiakkaat.Location = new Point(43, 105);
            dgvAsiakkaat.Name = "dgvAsiakkaat";
            dgvAsiakkaat.Size = new Size(300, 350);
            dgvAsiakkaat.TabIndex = 3;
            // 
            // txtHaku
            // 
            txtHaku.BackColor = Color.White;
            txtHaku.Location = new Point(43, 64);
            txtHaku.Name = "txtHaku";
            txtHaku.Size = new Size(300, 23);
            txtHaku.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F);
            label1.Location = new Point(43, 13);
            label1.Name = "label1";
            label1.Size = new Size(148, 30);
            label1.TabIndex = 1;
            label1.Text = "Hae asiakasta:";
            // 
            // panelOikea
            // 
            panelOikea.Controls.Add(label6);
            panelOikea.Controls.Add(label5);
            panelOikea.Controls.Add(label4);
            panelOikea.Controls.Add(label3);
            panelOikea.Controls.Add(label2);
            panelOikea.Controls.Add(txtPuhelin);
            panelOikea.Controls.Add(txtEmail);
            panelOikea.Controls.Add(txtPostinro);
            panelOikea.Controls.Add(txtLahiosoite);
            panelOikea.Controls.Add(txtSukunimi);
            panelOikea.Controls.Add(btnTyhjenna);
            panelOikea.Controls.Add(btnPoista);
            panelOikea.Controls.Add(btnMuokkaa);
            panelOikea.Controls.Add(btnLisaa);
            panelOikea.Controls.Add(txtEtunimi);
            panelOikea.Controls.Add(lblNimi);
            panelOikea.Dock = DockStyle.Right;
            panelOikea.Location = new Point(389, 15);
            panelOikea.Margin = new Padding(18, 0, 0, 0);
            panelOikea.Name = "panelOikea";
            panelOikea.Size = new Size(657, 539);
            panelOikea.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(345, 164);
            label6.Name = "label6";
            label6.Size = new Size(94, 17);
            label6.TabIndex = 17;
            label6.Text = "Puhelinnumero";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(43, 164);
            label5.Name = "label5";
            label5.Size = new Size(72, 17);
            label5.TabIndex = 16;
            label5.Text = "Sähköposti";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(345, 105);
            label4.Name = "label4";
            label4.Size = new Size(81, 17);
            label4.TabIndex = 15;
            label4.Text = "Postinumero";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(43, 104);
            label3.Name = "label3";
            label3.Size = new Size(67, 17);
            label3.TabIndex = 14;
            label3.Text = "Lähiosoite";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(345, 44);
            label2.Name = "label2";
            label2.Size = new Size(59, 17);
            label2.TabIndex = 13;
            label2.Text = "Sukunimi";
            // 
            // txtPuhelin
            // 
            txtPuhelin.BackColor = Color.White;
            txtPuhelin.Location = new Point(345, 184);
            txtPuhelin.Name = "txtPuhelin";
            txtPuhelin.Size = new Size(280, 23);
            txtPuhelin.TabIndex = 12;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.Location = new Point(43, 184);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(280, 23);
            txtEmail.TabIndex = 11;
            // 
            // txtPostinro
            // 
            txtPostinro.BackColor = Color.White;
            txtPostinro.Location = new Point(345, 124);
            txtPostinro.Name = "txtPostinro";
            txtPostinro.Size = new Size(280, 23);
            txtPostinro.TabIndex = 10;
            // 
            // txtLahiosoite
            // 
            txtLahiosoite.BackColor = Color.White;
            txtLahiosoite.Location = new Point(43, 124);
            txtLahiosoite.Name = "txtLahiosoite";
            txtLahiosoite.Size = new Size(280, 23);
            txtLahiosoite.TabIndex = 9;
            // 
            // txtSukunimi
            // 
            txtSukunimi.BackColor = Color.White;
            txtSukunimi.Location = new Point(345, 64);
            txtSukunimi.Name = "txtSukunimi";
            txtSukunimi.Size = new Size(280, 23);
            txtSukunimi.TabIndex = 8;
            // 
            // btnTyhjenna
            // 
            btnTyhjenna.BackColor = Color.White;
            btnTyhjenna.FlatStyle = FlatStyle.Popup;
            btnTyhjenna.Location = new Point(43, 290);
            btnTyhjenna.Name = "btnTyhjenna";
            btnTyhjenna.Size = new Size(190, 40);
            btnTyhjenna.TabIndex = 7;
            btnTyhjenna.Text = "Tyhjennä";
            btnTyhjenna.UseVisualStyleBackColor = false;
            btnTyhjenna.Click += btnTyhjenna_Click;
            // 
            // btnPoista
            // 
            btnPoista.BackColor = Color.White;
            btnPoista.FlatStyle = FlatStyle.Popup;
            btnPoista.Location = new Point(435, 244);
            btnPoista.Name = "btnPoista";
            btnPoista.Size = new Size(190, 40);
            btnPoista.TabIndex = 6;
            btnPoista.Text = "Poista";
            btnPoista.UseVisualStyleBackColor = false;
            btnPoista.Click += btnPoista_Click;
            // 
            // btnMuokkaa
            // 
            btnMuokkaa.AccessibleDescription = "btnMuokkaa_Click";
            btnMuokkaa.BackColor = Color.White;
            btnMuokkaa.FlatStyle = FlatStyle.Popup;
            btnMuokkaa.Location = new Point(239, 244);
            btnMuokkaa.Name = "btnMuokkaa";
            btnMuokkaa.Size = new Size(190, 40);
            btnMuokkaa.TabIndex = 5;
            btnMuokkaa.Text = "Muokkaa";
            btnMuokkaa.UseVisualStyleBackColor = false;
            btnMuokkaa.Click += btnMuokkaa_Click;
            // 
            // btnLisaa
            // 
            btnLisaa.BackColor = Color.White;
            btnLisaa.FlatStyle = FlatStyle.Popup;
            btnLisaa.Location = new Point(43, 244);
            btnLisaa.Name = "btnLisaa";
            btnLisaa.Size = new Size(190, 40);
            btnLisaa.TabIndex = 4;
            btnLisaa.Text = "Lisää";
            btnLisaa.UseVisualStyleBackColor = false;
            btnLisaa.Click += btnLisaa_Click;
            // 
            // txtEtunimi
            // 
            txtEtunimi.BackColor = Color.White;
            txtEtunimi.Location = new Point(43, 64);
            txtEtunimi.Name = "txtEtunimi";
            txtEtunimi.Size = new Size(280, 23);
            txtEtunimi.TabIndex = 3;
            // 
            // lblNimi
            // 
            lblNimi.AutoSize = true;
            lblNimi.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNimi.Location = new Point(43, 44);
            lblNimi.Name = "lblNimi";
            lblNimi.Size = new Size(50, 17);
            lblNimi.TabIndex = 2;
            lblNimi.Text = "Etunimi";
            // 
            // AsiakkaatView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(keskiPanel);
            Controls.Add(ylaPanel);
            Name = "AsiakkaatView";
            Size = new Size(1064, 681);
            ylaPanel.ResumeLayout(false);
            ylaPanel.PerformLayout();
            keskiPanel.ResumeLayout(false);
            panelVasen.ResumeLayout(false);
            panelVasen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAsiakkaat).EndInit();
            panelOikea.ResumeLayout(false);
            panelOikea.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ylaPanel;
        private Label lblTitle;
        private Panel keskiPanel;
        private Panel panelVasen;
        private DataGridView dgvAsiakkaat;
        private TextBox txtHaku;
        private Label label1;
        private Panel panelOikea;
        private TextBox txtPuhelin;
        private TextBox txtEmail;
        private TextBox txtPostinro;
        private TextBox txtLahiosoite;
        private TextBox txtSukunimi;
        private Button btnTyhjenna;
        private Button btnPoista;
        private Button btnMuokkaa;
        private Button btnLisaa;
        private TextBox txtEtunimi;
        private Label lblNimi;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
