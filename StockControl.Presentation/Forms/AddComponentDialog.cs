using StockControl.Application.Services;
using StockControl.Domain.Entities;
using System;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class AddComponentDialog : Form
    {
        private readonly SimpleProductService _simpleProductService;

        // The component selected by the user — accessible from CompositeProductForm
        public ProductComponent SelectedComponent { get; private set; }

        public AddComponentDialog(SimpleProductService simpleProductService)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            _simpleProductService = simpleProductService;
        }

        private void AddComponentDialog_Load(object sender, EventArgs e)
        {
            // Load all simple products into the combo box
            var products = _simpleProductService.GetAllProducts();
            cmbProducts.DisplayMember = "Name";
            cmbProducts.ValueMember = "Id";
            cmbProducts.DataSource = products;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var selectedProduct = (SimpleProduct)cmbProducts.SelectedItem;

            SelectedComponent = new ProductComponent
            {
                SimpleProduct = selectedProduct,
                Quantity = quantity
            };
        }
    }
}