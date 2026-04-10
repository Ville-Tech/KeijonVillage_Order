namespace VillageNewbies_Projekti.Views
{
    partial class VarauksetView
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
            dgvVaraukset = new DataGridView();
            txtHaku = new TextBox();
            label1 = new Label();
            panelOikea = new Panel();
            dtpLoppu = new DateTimePicker();
            dtpAlku = new DateTimePicker();
            cmbMokki = new ComboBox();
            cmbAsiakas = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnTyhjenna = new Button();
            btnPoista = new Button();
            btnMuokkaa = new Button();
            btnLisaa = new Button();
            lblAsiakas = new Label();
            ylaPanel.SuspendLayout();
            keskiPanel.SuspendLayout();
            panelVasen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVaraukset).BeginInit();
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
            ylaPanel.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F);
            lblTitle.Location = new Point(60, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(131, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Varaukset";
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
            keskiPanel.TabIndex = 4;
            // 
            // panelVasen
            // 
            panelVasen.Controls.Add(dgvVaraukset);
            panelVasen.Controls.Add(txtHaku);
            panelVasen.Controls.Add(label1);
            panelVasen.Dock = DockStyle.Left;
            panelVasen.Location = new Point(18, 15);
            panelVasen.Margin = new Padding(3, 2, 3, 2);
            panelVasen.Name = "panelVasen";
            panelVasen.Size = new Size(350, 539);
            panelVasen.TabIndex = 3;
            // 
            // dgvVaraukset
            // 
            dgvVaraukset.BackgroundColor = Color.White;
            dgvVaraukset.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVaraukset.Location = new Point(43, 105);
            dgvVaraukset.Name = "dgvVaraukset";
            dgvVaraukset.Size = new Size(300, 350);
            dgvVaraukset.TabIndex = 3;
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
            label1.Size = new Size(143, 30);
            label1.TabIndex = 1;
            label1.Text = "Hae varausta:";
            // 
            // panelOikea
            // 
            panelOikea.Controls.Add(dtpLoppu);
            panelOikea.Controls.Add(dtpAlku);
            panelOikea.Controls.Add(cmbMokki);
            panelOikea.Controls.Add(cmbAsiakas);
            panelOikea.Controls.Add(label4);
            panelOikea.Controls.Add(label3);
            panelOikea.Controls.Add(label2);
            panelOikea.Controls.Add(btnTyhjenna);
            panelOikea.Controls.Add(btnPoista);
            panelOikea.Controls.Add(btnMuokkaa);
            panelOikea.Controls.Add(btnLisaa);
            panelOikea.Controls.Add(lblAsiakas);
            panelOikea.Dock = DockStyle.Right;
            panelOikea.Location = new Point(389, 15);
            panelOikea.Margin = new Padding(18, 0, 0, 0);
            panelOikea.Name = "panelOikea";
            panelOikea.Size = new Size(657, 539);
            panelOikea.TabIndex = 2;
            // 
            // dtpLoppu
            // 
            dtpLoppu.Location = new Point(345, 125);
            dtpLoppu.Name = "dtpLoppu";
            dtpLoppu.Size = new Size(280, 23);
            dtpLoppu.TabIndex = 22;
            // 
            // dtpAlku
            // 
            dtpAlku.Location = new Point(43, 124);
            dtpAlku.Name = "dtpAlku";
            dtpAlku.Size = new Size(280, 23);
            dtpAlku.TabIndex = 20;
            // 
            // cmbMokki
            // 
            cmbMokki.FormattingEnabled = true;
            cmbMokki.Location = new Point(345, 64);
            cmbMokki.Name = "cmbMokki";
            cmbMokki.Size = new Size(280, 23);
            cmbMokki.TabIndex = 19;
            // 
            // cmbAsiakas
            // 
            cmbAsiakas.FormattingEnabled = true;
            cmbAsiakas.Location = new Point(43, 64);
            cmbAsiakas.Name = "cmbAsiakas";
            cmbAsiakas.Size = new Size(280, 23);
            cmbAsiakas.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(345, 105);
            label4.Name = "label4";
            label4.Size = new Size(71, 17);
            label4.TabIndex = 15;
            label4.Text = "Lähtöpäivä";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(43, 104);
            label3.Name = "label3";
            label3.Size = new Size(95, 17);
            label3.TabIndex = 14;
            label3.Text = "Saapumispäivä";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(345, 44);
            label2.Name = "label2";
            label2.Size = new Size(83, 17);
            label2.TabIndex = 13;
            label2.Text = "Valitse mökki";
            // 
            // btnTyhjenna
            // 
            btnTyhjenna.BackColor = Color.White;
            btnTyhjenna.FlatStyle = FlatStyle.Popup;
            btnTyhjenna.Location = new Point(43, 230);
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
            btnPoista.Location = new Point(435, 184);
            btnPoista.Name = "btnPoista";
            btnPoista.Size = new Size(190, 40);
            btnPoista.TabIndex = 6;
            btnPoista.Text = "Poista";
            btnPoista.UseVisualStyleBackColor = false;
            btnPoista.Click += btnPoista_Click;
            // 
            // btnMuokkaa
            // 
            btnMuokkaa.BackColor = Color.White;
            btnMuokkaa.FlatStyle = FlatStyle.Popup;
            btnMuokkaa.Location = new Point(239, 184);
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
            btnLisaa.Location = new Point(43, 184);
            btnLisaa.Name = "btnLisaa";
            btnLisaa.Size = new Size(190, 40);
            btnLisaa.TabIndex = 4;
            btnLisaa.Text = "Lisää";
            btnLisaa.UseVisualStyleBackColor = false;
            btnLisaa.Click += btnLisaa_Click;
            // 
            // lblAsiakas
            // 
            lblAsiakas.AutoSize = true;
            lblAsiakas.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAsiakas.Location = new Point(43, 44);
            lblAsiakas.Name = "lblAsiakas";
            lblAsiakas.Size = new Size(91, 17);
            lblAsiakas.TabIndex = 2;
            lblAsiakas.Text = "Valitse asiakas";
            // 
            // VarauksetView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(keskiPanel);
            Controls.Add(ylaPanel);
            Name = "VarauksetView";
            Size = new Size(1064, 681);
            ylaPanel.ResumeLayout(false);
            ylaPanel.PerformLayout();
            keskiPanel.ResumeLayout(false);
            panelVasen.ResumeLayout(false);
            panelVasen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVaraukset).EndInit();
            panelOikea.ResumeLayout(false);
            panelOikea.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ylaPanel;
        private Label lblTitle;
        private Panel keskiPanel;
        private Panel panelVasen;
        private DataGridView dgvVaraukset;
        private TextBox txtHaku;
        private Label label1;
        private Panel panelOikea;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtSukunimi;
        private Button btnTyhjenna;
        private Button btnPoista;
        private Button btnMuokkaa;
        private Button btnLisaa;
        private TextBox txtEtunimi;
        private Label lblAsiakas;
        private ComboBox cmbMokki;
        private ComboBox cmbAsiakas;
        private DateTimePicker dtpLoppu;
        private DateTimePicker dtpAlku;
    }
}
