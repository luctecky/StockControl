namespace StockControl.Presentation.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblStartDate = new System.Windows.Forms.Label();
            lblEndDate = new System.Windows.Forms.Label();
            lblReportType = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();
            lblTotalValue = new System.Windows.Forms.Label();
            dtpStartDate = new System.Windows.Forms.DateTimePicker();
            dtpEndDate = new System.Windows.Forms.DateTimePicker();
            cmbReportType = new System.Windows.Forms.ComboBox();
            btnGenerate = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            dgvReport = new System.Windows.Forms.DataGridView();

            SuspendLayout();

            // lblReportType
            lblReportType.Text = "Report:";
            lblReportType.Location = new System.Drawing.Point(12, 20);
            lblReportType.Size = new System.Drawing.Size(80, 23);

            // cmbReportType
            cmbReportType.Location = new System.Drawing.Point(100, 17);
            cmbReportType.Size = new System.Drawing.Size(250, 23);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbReportType.Items.AddRange(new object[]
            {
                "Requisitions Report",
                "Stock Output Report"
            });

            // lblStartDate
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new System.Drawing.Point(12, 55);
            lblStartDate.Size = new System.Drawing.Size(80, 23);

            // dtpStartDate
            dtpStartDate.Location = new System.Drawing.Point(100, 52);
            dtpStartDate.Size = new System.Drawing.Size(150, 23);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // lblEndDate
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new System.Drawing.Point(265, 55);
            lblEndDate.Size = new System.Drawing.Size(70, 23);

            // dtpEndDate
            dtpEndDate.Location = new System.Drawing.Point(340, 52);
            dtpEndDate.Size = new System.Drawing.Size(150, 23);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // btnGenerate
            btnGenerate.Text = "Generate";
            btnGenerate.Location = new System.Drawing.Point(100, 90);
            btnGenerate.Size = new System.Drawing.Size(100, 30);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Click += new System.EventHandler(btnGenerate_Click);

            // btnClear
            btnClear.Text = "Clear";
            btnClear.Location = new System.Drawing.Point(210, 90);
            btnClear.Size = new System.Drawing.Size(100, 30);
            btnClear.Name = "btnClear";
            btnClear.Click += new System.EventHandler(btnClear_Click);

            // dgvReport
            dgvReport.Location = new System.Drawing.Point(12, 135);
            dgvReport.Size = new System.Drawing.Size(760, 320);
            dgvReport.Name = "dgvReport";
            dgvReport.AllowUserToAddRows = false;
            dgvReport.ReadOnly = true;
            dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // lblTotal
            lblTotal.Text = "TOTAL:";
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblTotal.Location = new System.Drawing.Point(12, 465);
            lblTotal.Size = new System.Drawing.Size(70, 23);

            // lblTotalValue
            lblTotalValue.Text = "";
            lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            lblTotalValue.Location = new System.Drawing.Point(90, 465);
            lblTotalValue.Size = new System.Drawing.Size(400, 23);
            lblTotalValue.Name = "lblTotalValue";

            // Form
            ClientSize = new System.Drawing.Size(800, 510);
            Text = "Reports";
            Name = "ReportForm";
            Load += new System.EventHandler(ReportForm_Load);

            Controls.Add(lblReportType);
            Controls.Add(cmbReportType);
            Controls.Add(lblStartDate);
            Controls.Add(dtpStartDate);
            Controls.Add(lblEndDate);
            Controls.Add(dtpEndDate);
            Controls.Add(btnGenerate);
            Controls.Add(btnClear);
            Controls.Add(dgvReport);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalValue);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}