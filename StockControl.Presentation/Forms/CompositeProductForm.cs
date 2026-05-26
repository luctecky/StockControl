using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class CompositeProductForm : Form
    {
        private readonly CompositeProductService _service;
        private readonly SimpleProductService _simpleProductService;

        // Holds the components being added to the current product
        private List<ProductComponent> _currentComponents;
        private int _selectedProductId = 0;

        public CompositeProductForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            var compositeRepo = new CompositeProductRepository();
            var simpleRepo = new SimpleProductRepository();

            _service = new CompositeProductService(compositeRepo);
            _simpleProductService = new SimpleProductService(simpleRepo);
            _currentComponents = new List<ProductComponent>();
        }

        private void CompositeProductForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var products = _service.GetAllProducts();
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                dgvProducts.Columns["Id"].HeaderText = "ID";
                dgvProducts.Columns["Name"].HeaderText = "Name";
                dgvProducts.Columns["SalePrice"].HeaderText = "Sale Price";

                // Hide the Components column from the grid
                if (dgvProducts.Columns["Components"] != null)
                    dgvProducts.Columns["Components"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshComponentsGrid()
        {
            // Build a display-friendly list for the grid
            var display = _currentComponents.ConvertAll(c => new
            {
                ProductName = c.SimpleProduct.Name,
                Quantity = c.Quantity,
                CostPrice = c.SimpleProduct.CostPrice,
                Subtotal = c.SimpleProduct.CostPrice * c.Quantity
            });

            dgvComponents.DataSource = null;
            dgvComponents.DataSource = display;

            if (dgvComponents.Columns.Count > 0)
            {
                dgvComponents.Columns["ProductName"].HeaderText = "Product";
                dgvComponents.Columns["Quantity"].HeaderText = "Qty";
                dgvComponents.Columns["CostPrice"].HeaderText = "Unit Cost";
                dgvComponents.Columns["Subtotal"].HeaderText = "Subtotal";
            }

            UpdateCalculatedCost();
        }

        private void UpdateCalculatedCost()
        {
            decimal total = 0;
            foreach (var c in _currentComponents)
                total += c.SimpleProduct.CostPrice * c.Quantity;

            lblCostPriceValue.Text = total.ToString("C");
        }

        private void btnAddComponent_Click(object sender, EventArgs e)
        {
            // Open a dialog to select a simple product and quantity
            using (var dialog = new AddComponentDialog(_simpleProductService))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Check if product already exists in the list
                    var existing = _currentComponents.Find(
                        c => c.SimpleProduct.Id == dialog.SelectedComponent.SimpleProduct.Id);

                    if (existing != null)
                    {
                        existing.Quantity += dialog.SelectedComponent.Quantity;
                    }
                    else
                    {
                        _currentComponents.Add(dialog.SelectedComponent);
                    }

                    RefreshComponentsGrid();
                }
            }
        }

        private void btnRemoveComponent_Click(object sender, EventArgs e)
        {
            if (dgvComponents.CurrentRow == null) return;

            int index = dgvComponents.CurrentRow.Index;
            _currentComponents.RemoveAt(index);
            RefreshComponentsGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var product = new CompositeProduct
                {
                    Id = _selectedProductId,
                    Name = txtName.Text.Trim(),
                    SalePrice = decimal.Parse(txtSalePrice.Text),
                    Components = _currentComponents
                };

                if (_selectedProductId == 0)
                    _service.CreateProduct(product);
                else
                    _service.UpdateProduct(product);

                MessageBox.Show("Product saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadProducts();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric value for Sale Price.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Select a product to delete.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _service.DeleteProduct(_selectedProductId);
                    MessageBox.Show("Product deleted successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvProducts.Rows[e.RowIndex];
            _selectedProductId = (int)row.Cells["Id"].Value;
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();

            // Load the components of the selected product
            var product = _service.GetProductById(_selectedProductId);
            _currentComponents = product.Components;
            RefreshComponentsGrid();
        }

        private void ClearFields()
        {
            _selectedProductId = 0;
            txtName.Text = string.Empty;
            txtSalePrice.Text = string.Empty;
            _currentComponents = new List<ProductComponent>();
            RefreshComponentsGrid();
            dgvProducts.ClearSelection();
        }
    }
}