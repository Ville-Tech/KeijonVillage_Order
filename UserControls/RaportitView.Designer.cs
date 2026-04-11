namespace VillageNewbies_Projekti.Views
{
    partial class RaportitView
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblRaportti;
        private System.Windows.Forms.ComboBox cmbRaportti;
        private System.Windows.Forms.Label lblAikavali;
        private System.Windows.Forms.DateTimePicker dtpAlku;
        private System.Windows.Forms.DateTimePicker dtpLoppu;
        private System.Windows.Forms.Button btnHae;
        private System.Windows.Forms.Button btnVie;
        private System.Windows.Forms.DataGridView dgvRaportti;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRaportti = new System.Windows.Forms.Label();
            this.cmbRaportti = new System.Windows.Forms.ComboBox();
            this.lblAikavali = new System.Windows.Forms.Label();
            this.dtpAlku = new System.Windows.Forms.DateTimePicker();
            this.dtpLoppu = new System.Windows.Forms.DateTimePicker();
            this.btnHae = new System.Windows.Forms.Button();
            this.btnVie = new System.Windows.Forms.Button();
            this.dgvRaportti = new System.Windows.Forms.DataGridView();

            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaportti)).BeginInit();
            this.SuspendLayout();

            // TABLE LAYOUT
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;

            // LABEL RAPORTTI
            this.lblRaportti.Text = "Raporttityyppi:";
            this.lblRaportti.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRaportti.Dock = System.Windows.Forms.DockStyle.Fill;

            // COMBOBOX RAPORTTI
            this.cmbRaportti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRaportti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // LABEL AIKAVÄLI
            this.lblAikavali.Text = "Aikaväli:";
            this.lblAikavali.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAikavali.Dock = System.Windows.Forms.DockStyle.Fill;

            // DATEPICKER ALKU
            this.dtpAlku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpAlku.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // DATEPICKER LOPPU
            this.dtpLoppu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpLoppu.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // BUTTON HAE
            this.btnHae.Text = "Hae";
            this.btnHae.Dock = System.Windows.Forms.DockStyle.Fill;

            // BUTTON VIE
            this.btnVie.Text = "Vie CSV";
            this.btnVie.Dock = System.Windows.Forms.DockStyle.Fill;

            // DATAGRIDVIEW
            this.dgvRaportti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRaportti.ReadOnly = true;
            this.dgvRaportti.AllowUserToAddRows = false;
            this.dgvRaportti.AllowUserToDeleteRows = false;
            this.dgvRaportti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // ADD CONTROLS TO TABLE
            this.tableLayoutPanel1.Controls.Add(this.lblRaportti, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbRaportti, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAikavali, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpAlku, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpLoppu, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnHae, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnVie, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvRaportti, 0, 1);

            this.tableLayoutPanel1.SetColumnSpan(this.dgvRaportti, 5);

            // USERCONTROL
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RaportitView";
            this.Size = new System.Drawing.Size(900, 600);

            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaportti)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
