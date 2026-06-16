using StockControl.Application.Services;
using StockControl.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class ReportForm : Form
    {
        private readonly ReportService _reportService;

        public ReportForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            var requisitionRepository = new RequisitionRepository();
            _reportService = new ReportService(requisitionRepository);
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // Default to first option and current month period
            cmbReportType.SelectedIndex = 0;
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Today;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a report type.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var startDate = dtpStartDate.Value.Date;
                var endDate = dtpEndDate.Value.Date;

                if (cmbReportType.SelectedIndex == 0)
                    GenerateRequisitionsReport(startDate, endDate);
                else
                    GenerateStockOutputReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateRequisitionsReport(DateTime startDate, DateTime endDate)
        {
            var data = _reportService.GetRequisitionsReport(startDate, endDate).ToList();

            dgvReport.DataSource = null;
            dgvReport.DataSource = data;

            if (dgvReport.Columns.Count > 0)
            {
                dgvReport.Columns["ProductName"].HeaderText = "Product";
                dgvReport.Columns["TotalQuantity"].HeaderText = "Qty Requisitioned";
                dgvReport.Columns["TotalCost"].HeaderText = "Total Cost";
                dgvReport.Columns["TotalSalePrice"].HeaderText = "Total Sale Price";
            }

            // Show totals
            decimal totalCost = data.Sum(r => r.TotalCost);
            decimal totalSalePrice = data.Sum(r => r.TotalSalePrice);

            lblTotalValue.Text = data.Count == 0
                ? "No data found for the selected period."
                : $"Cost: {totalCost:C}   |   Sale Price: {totalSalePrice:C}";
        }

        private void GenerateStockOutputReport(DateTime startDate, DateTime endDate)
        {
            var data = _reportService.GetStockOutputReport(startDate, endDate).ToList();

            dgvReport.DataSource = null;
            dgvReport.DataSource = data;

            if (dgvReport.Columns.Count > 0)
            {
                dgvReport.Columns["ProductName"].HeaderText = "Product";
                dgvReport.Columns["TotalQuantity"].HeaderText = "Qty Withdrawn";
                dgvReport.Columns["TotalCost"].HeaderText = "Total Cost";
            }

            // Show total
            decimal totalCost = data.Sum(r => r.TotalCost);

            lblTotalValue.Text = data.Count == 0
                ? "No data found for the selected period."
                : $"Total Cost: {totalCost:C}";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = null;
            lblTotalValue.Text = string.Empty;
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Today;
            cmbReportType.SelectedIndex = 0;
        }
    }
}