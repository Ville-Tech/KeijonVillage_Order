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
            label1 = new Label();
            listBoxVasen = new ListBox();
            panelOikea = new Panel();
            lblTyhjatKpl = new Label();
            lblAlueitaKpl = new Label();
            lblAktiivisetKpl = new Label();
            label4 = new Label();
            alaPanel = new Panel();
            lblNimi = new Label();
            label2 = new Label();
            lblValittu = new Label();
            ylaPanel.SuspendLayout();
            keskiPanel.SuspendLayout();
            panelVasen.SuspendLayout();
            panelOikea.SuspendLayout();
            alaPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ylaPanel
            // 
            ylaPanel.Controls.Add(lblTitle);
            ylaPanel.Dock = DockStyle.Top;
            ylaPanel.Location = new Point(0, 0);
            ylaPanel.Name = "ylaPanel";
            ylaPanel.Size = new Size(1216, 150);
            ylaPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F);
            lblTitle.Location = new Point(69, 53);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(117, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Alueet";
            // 
            // keskiPanel
            // 
            keskiPanel.Controls.Add(panelVasen);
            keskiPanel.Controls.Add(panelOikea);
            keskiPanel.Location = new Point(0, 149);
            keskiPanel.Name = "keskiPanel";
            keskiPanel.Padding = new Padding(20);
            keskiPanel.Size = new Size(1216, 552);
            keskiPanel.TabIndex = 2;
            // 
            // panelVasen
            // 
            panelVasen.Controls.Add(label1);
            panelVasen.Controls.Add(listBoxVasen);
            panelVasen.Dock = DockStyle.Left;
            panelVasen.Location = new Point(20, 20);
            panelVasen.Name = "panelVasen";
            panelVasen.Size = new Size(400, 512);
            panelVasen.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F);
            label1.Location = new Point(49, 17);
            label1.Name = "label1";
            label1.Size = new Size(122, 38);
            label1.TabIndex = 1;
            label1.Text = "Aluelista";
            // 
            // listBoxVasen
            // 
            listBoxVasen.Font = new Font("Segoe UI", 16.2F);
            listBoxVasen.FormattingEnabled = true;
            listBoxVasen.ItemHeight = 37;
            listBoxVasen.Location = new Point(49, 85);
            listBoxVasen.Name = "listBoxVasen";
            listBoxVasen.Size = new Size(327, 374);
            listBoxVasen.TabIndex = 0;
            listBoxVasen.SelectedIndexChanged += listBoxVasen_SelectedIndexChanged;
            // 
            // panelOikea
            // 
            panelOikea.Controls.Add(lblTyhjatKpl);
            panelOikea.Controls.Add(lblAlueitaKpl);
            panelOikea.Controls.Add(lblAktiivisetKpl);
            panelOikea.Controls.Add(label4);
            panelOikea.Dock = DockStyle.Right;
            panelOikea.Location = new Point(796, 20);
            panelOikea.Margin = new Padding(20, 0, 0, 0);
            panelOikea.Name = "panelOikea";
            panelOikea.Size = new Size(400, 512);
            panelOikea.TabIndex = 2;
            // 
            // lblTyhjatKpl
            // 
            lblTyhjatKpl.AutoSize = true;
            lblTyhjatKpl.Font = new Font("Segoe UI", 16.2F);
            lblTyhjatKpl.Location = new Point(49, 195);
            lblTyhjatKpl.Name = "lblTyhjatKpl";
            lblTyhjatKpl.Size = new Size(160, 38);
            lblTyhjatKpl.TabIndex = 5;
            lblTyhjatKpl.Text = "Tyhjät: - kpl";
            // 
            // lblAlueitaKpl
            // 
            lblAlueitaKpl.AutoSize = true;
            lblAlueitaKpl.Font = new Font("Segoe UI", 16.2F);
            lblAlueitaKpl.Location = new Point(49, 85);
            lblAlueitaKpl.Name = "lblAlueitaKpl";
            lblAlueitaKpl.Size = new Size(173, 38);
            lblAlueitaKpl.TabIndex = 4;
            lblAlueitaKpl.Text = "Alueita: - kpl";
            // 
            // lblAktiivisetKpl
            // 
            lblAktiivisetKpl.AutoSize = true;
            lblAktiivisetKpl.Font = new Font("Segoe UI", 16.2F);
            lblAktiivisetKpl.Location = new Point(49, 140);
            lblAktiivisetKpl.Name = "lblAktiivisetKpl";
            lblAktiivisetKpl.Size = new Size(198, 38);
            lblAktiivisetKpl.TabIndex = 3;
            lblAktiivisetKpl.Text = "Aktiiviset: - kpl";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16.2F);
            label4.Location = new Point(49, 17);
            label4.Name = "label4";
            label4.Size = new Size(106, 38);
            label4.TabIndex = 2;
            label4.Text = "Tilastot";
            // 
            // alaPanel
            // 
            alaPanel.Controls.Add(lblNimi);
            alaPanel.Controls.Add(label2);
            alaPanel.Controls.Add(lblValittu);
            alaPanel.Dock = DockStyle.Bottom;
            alaPanel.Location = new Point(0, 701);
            alaPanel.Name = "alaPanel";
            alaPanel.Size = new Size(1216, 207);
            alaPanel.TabIndex = 3;
            // 
            // lblNimi
            // 
            lblNimi.AutoSize = true;
            lblNimi.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNimi.Location = new Point(197, 80);
            lblNimi.Name = "lblNimi";
            lblNimi.Size = new Size(28, 38);
            lblNimi.TabIndex = 5;
            lblNimi.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(69, 80);
            label2.Name = "label2";
            label2.Size = new Size(82, 38);
            label2.TabIndex = 2;
            label2.Text = "Nimi:";
            // 
            // lblValittu
            // 
            lblValittu.AutoSize = true;
            lblValittu.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValittu.Location = new Point(69, 22);
            lblValittu.Name = "lblValittu";
            lblValittu.Size = new Size(160, 38);
            lblValittu.TabIndex = 1;
            lblValittu.Text = "Valittu alue:";
            // 
            // AlueetView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(alaPanel);
            Controls.Add(keskiPanel);
            Controls.Add(ylaPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AlueetView";
            Size = new Size(1216, 908);
            ylaPanel.ResumeLayout(false);
            ylaPanel.PerformLayout();
            keskiPanel.ResumeLayout(false);
            panelVasen.ResumeLayout(false);
            panelVasen.PerformLayout();
            panelOikea.ResumeLayout(false);
            panelOikea.PerformLayout();
            alaPanel.ResumeLayout(false);
            alaPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ylaPanel;
        private Panel keskiPanel;
        private Panel alaPanel;
        private Label lblTitle;
        private ListBox listBoxVasen;
        private Label lblNimi;
        private Label label2;
        private Label lblValittu;
        private Panel panelOikea;
        private Label label1;
        private Panel panelVasen;
        private Label lblTyhjatKpl;
        private Label lblAlueitaKpl;
        private Label lblAktiivisetKpl;
        private Label label4;
    }
}
