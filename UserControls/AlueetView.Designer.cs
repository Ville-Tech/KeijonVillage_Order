namespace VillageNewbies_Projekti.Views
{
    partial class AlueetView
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
            dgvAlueet = new DataGridView();
            txtHaku = new TextBox();
            label1 = new Label();
            panelOikea = new Panel();
            btnTyhjenna = new Button();
            btnPoista = new Button();
            btnMuokkaa = new Button();
            btnLisaa = new Button();
            txtNimi = new TextBox();
            lblNimi = new Label();
            ylaPanel.SuspendLayout();
            keskiPanel.SuspendLayout();
            panelVasen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlueet).BeginInit();
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
            ylaPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F);
            lblTitle.Location = new Point(60, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(93, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Alueet";
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
            keskiPanel.TabIndex = 2;
            // 
            // panelVasen
            // 
            panelVasen.Controls.Add(dgvAlueet);
            panelVasen.Controls.Add(txtHaku);
            panelVasen.Controls.Add(label1);
            panelVasen.Dock = DockStyle.Left;
            panelVasen.Location = new Point(18, 15);
            panelVasen.Margin = new Padding(3, 2, 3, 2);
            panelVasen.Name = "panelVasen";
            panelVasen.Size = new Size(350, 539);
            panelVasen.TabIndex = 3;
            // 
            // dgvAlueet
            // 
            dgvAlueet.BackgroundColor = Color.White;
            dgvAlueet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlueet.Location = new Point(43, 105);
            dgvAlueet.Name = "dgvAlueet";
            dgvAlueet.Size = new Size(300, 350);
            dgvAlueet.TabIndex = 4;
            // 
            // txtHaku
            // 
            txtHaku.BackColor = Color.White;
            txtHaku.Location = new Point(43, 64);
            txtHaku.Name = "txtHaku";
            txtHaku.Size = new Size(300, 23);
            txtHaku.TabIndex = 2;
            txtHaku.TextChanged += txtHaku_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F);
            label1.Location = new Point(43, 13);
            label1.Name = "label1";
            label1.Size = new Size(126, 30);
            label1.TabIndex = 1;
            label1.Text = "Hae alueita:";
            // 
            // panelOikea
            // 
            panelOikea.Controls.Add(btnTyhjenna);
            panelOikea.Controls.Add(btnPoista);
            panelOikea.Controls.Add(btnMuokkaa);
            panelOikea.Controls.Add(btnLisaa);
            panelOikea.Controls.Add(txtNimi);
            panelOikea.Controls.Add(lblNimi);
            panelOikea.Dock = DockStyle.Right;
            panelOikea.Location = new Point(389, 15);
            panelOikea.Margin = new Padding(18, 0, 0, 0);
            panelOikea.Name = "panelOikea";
            panelOikea.Size = new Size(657, 539);
            panelOikea.TabIndex = 2;
            // 
            // btnTyhjenna
            // 
            btnTyhjenna.BackColor = Color.White;
            btnTyhjenna.FlatStyle = FlatStyle.Popup;
            btnTyhjenna.Location = new Point(43, 151);
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
            btnPoista.Location = new Point(435, 105);
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
            btnMuokkaa.Location = new Point(239, 105);
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
            btnLisaa.Location = new Point(43, 105);
            btnLisaa.Name = "btnLisaa";
            btnLisaa.Size = new Size(190, 40);
            btnLisaa.TabIndex = 4;
            btnLisaa.Text = "Lisää";
            btnLisaa.UseVisualStyleBackColor = false;
            btnLisaa.Click += btnLisaa_Click;
            // 
            // txtNimi
            // 
            txtNimi.BackColor = Color.White;
            txtNimi.Location = new Point(43, 64);
            txtNimi.Name = "txtNimi";
            txtNimi.Size = new Size(522, 23);
            txtNimi.TabIndex = 3;
            // 
            // lblNimi
            // 
            lblNimi.AutoSize = true;
            lblNimi.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNimi.Location = new Point(43, 44);
            lblNimi.Name = "lblNimi";
            lblNimi.Size = new Size(78, 17);
            lblNimi.TabIndex = 2;
            lblNimi.Text = "Alueen nimi:";
            // 
            // AlueetView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(keskiPanel);
            Controls.Add(ylaPanel);
            Name = "AlueetView";
            Size = new Size(1064, 681);
            ylaPanel.ResumeLayout(false);
            ylaPanel.PerformLayout();
            keskiPanel.ResumeLayout(false);
            panelVasen.ResumeLayout(false);
            panelVasen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlueet).EndInit();
            panelOikea.ResumeLayout(false);
            panelOikea.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ylaPanel;
        private Panel keskiPanel;
        private Panel alaPanel;
        private Label lblTitle;
        private Button btnPoista;
        private Button btnMuokkaa;
        private Button btnLisaa;
        private TextBox txtNimi;
        private Button btnTyhjenna;
        private Label label2;
        private Label lblValittu;
        private Panel panelOikea;
        private Label label1;
        private Panel panelVasen;
        private TextBox txtHaku;
        private Label lblNimi;
        private DataGridView dgvAlueet;
    }
}
