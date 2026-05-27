using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class RequisitionForm : Form
    {
        private readonly RequisitionService _service;
        private readonly SimpleProductService _simpleProductService;
        private readonly CompositeProductService _compositeProductService;

        private List<RequisitionItem> _currentItems;
        private int _selectedRequisitionId = 0;

        public RequisitionForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            var reqRepo = new RequisitionRepository();
            var simpleRepo = new SimpleProductRepository();
            var compositeRepo = new CompositeProductRepository();

            _service = new RequisitionService(reqRepo);
            _simpleProductService = new SimpleProductService(simpleRepo);
            _compositeProductService = new CompositeProductService(compositeRepo);
            _currentItems = new List<RequisitionItem>();
        }

        private void RequisitionForm_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
            LoadRequisitions();
        }

        private void LoadRequisitions()
        {
            try
            {
                var requisitions = _service.GetAllRequisitions();
                dgvRequisitions.DataSource = null;
                dgvRequisitions.DataSource = requisitions;

                if (dgvRequisitions.Columns.Count > 0)
                {
                    dgvRequisitions.Columns["Id"].HeaderText = "ID";
                    dgvRequisitions.Columns["WithdrawalDate"].HeaderText = "Date";
                    dgvRequisitions.Columns["ResponsibleEmployee"].HeaderText = "Employee";

                    if (dgvRequisitions.Columns["Items"] != null)
                        dgvRequisitions.Columns["Items"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading requisitions: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshItemsGrid()
        {
            var display = _currentItems.ConvertAll(item => new
            {
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                UnitCost = item.Product.GetCostPrice(),
                Subtotal = item.Product.GetCostPrice() * item.Quantity
            });

            dgvItems.DataSource = null;
            dgvItems.DataSource = display;

            if (dgvItems.Columns.Count > 0)
            {
                dgvItems.Columns["ProductName"].HeaderText = "Product";
                dgvItems.Columns["Quantity"].HeaderText = "Qty";
                dgvItems.Columns["UnitCost"].HeaderText = "Unit Cost";
                dgvItems.Columns["Subtotal"].HeaderText = "Subtotal";
            }

            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            decimal total = 0;
            foreach (var item in _currentItems)
                total += item.Product.GetCostPrice() * item.Quantity;

            lblTotalValue.Text = total.ToString("C");
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddRequisitionItemDialog(
                _simpleProductService,
                _compositeProductService))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var existing = _currentItems.Find(
                        i => i.Product.Id == dialog.SelectedItem.Product.Id);

                    if (existing != null)
                        existing.Quantity += dialog.SelectedItem.Quantity;
                    else
                        _currentItems.Add(dialog.SelectedItem);

                    RefreshItemsGrid();
                }
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;

            _currentItems.RemoveAt(dgvItems.CurrentRow.Index);
            RefreshItemsGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var requisition = new Requisition
                {
                    Id = _selectedRequisitionId,
                    WithdrawalDate = dtpDate.Value.Date,
                    ResponsibleEmployee = txtEmployee.Text.Trim(),
                    Items = _currentItems
                };

                if (_selectedRequisitionId == 0)
                    _service.CreateRequisition(requisition);
                else
                    _service.UpdateRequisition(requisition);

                MessageBox.Show("Requisition saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadRequisitions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving requisition: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRequisitionId == 0)
            {
                MessageBox.Show("Select a requisition to delete.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this requisition?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _service.DeleteRequisition(_selectedRequisitionId);
                    MessageBox.Show("Requisition deleted successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadRequisitions();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting requisition: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvRequisitions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRequisitions.Rows[e.RowIndex];
            _selectedRequisitionId = (int)row.Cells["Id"].Value;

            var requisition = _service.GetRequisitionById(_selectedRequisitionId);
            dtpDate.Value = (DateTime)row.Cells["WithdrawalDate"].Value;
            txtEmployee.Text = row.Cells["ResponsibleEmployee"].Value.ToString();
            _currentItems = requisition.Items;

            RefreshItemsGrid();
        }

        private void ClearFields()
        {
            _selectedRequisitionId = 0;
            dtpDate.Value = DateTime.Today;
            txtEmployee.Text = string.Empty;
            _currentItems = new List<RequisitionItem>();
            RefreshItemsGrid();
            dgvRequisitions.ClearSelection();
        }
    }
}