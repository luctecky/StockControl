using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Repositories;

namespace StockControl.Presentation.Forms;

public partial class SimpleProductForm : Form
{
    private readonly SimpleProductServices _services;
    private int _selectedProductId = 0;
    public SimpleProductForm()
    {
        InitializeComponent();

        var repository = new SimpleProductRepository();
        _services = new SimpleProductServices(repository);
    }

    private void SimpleProductForm_Load(object sender, EventArgs e)
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        try
        {
            var products = _services.GetAllProducts();
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = products;

            dgvProducts.Columns["Id"].HeaderText = "ID";
            dgvProducts.Columns["Name"].HeaderText = "Name";
            dgvProducts.Columns["CostPrice"].HeaderText = "Cost Price";
            dgvProducts.Columns["SalePrice"].HeaderText = "Sale Price";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading products: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var product = new SimpleProduct
            {
                Id = _selectedProductId,
                Name = txtName.Text.Trim(),
                CostPrice = decimal.Parse(txtCostPrice.Text),
                SalePrice = decimal.Parse(txtSalePrice.Text)
            };

            if (_selectedProductId == 0)
            {
                _services.CreateProduct(product);
                MessageBox.Show("Product created successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _services.UpdateProduct(product);
                MessageBox.Show("Product updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearFields();
            LoadProducts();
        }

        catch (FormatException)
        {
            MessageBox.Show("Please ensure all fields are filled correctly.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving product: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearFields()
    {
        _selectedProductId = 0;
        txtName.Text =  string.Empty;
        txtCostPrice.Text = string.Empty;
        txtSalePrice.Text = string.Empty;
        dgvProducts.ClearSelection();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedProductId == 0)
        {
            MessageBox.Show("Please select a product to delete.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show("Are you sure you want to delete this product?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
        {
            try
            {
                _services.DeleteProduct(_selectedProductId);
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
        txtCostPrice.Text = row.Cells["CostPrice"].Value.ToString();
        txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();
    }

}
