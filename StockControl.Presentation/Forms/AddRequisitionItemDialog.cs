using StockControl.Application.Services;
using StockControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class AddRequisitionItemDialog : Form
    {
        private readonly SimpleProductService _simpleProductService;
        private readonly CompositeProductService _compositeProductService;

        // All products (simple + composite) for the combo
        private List<Product> _allProducts;

        public RequisitionItem SelectedItem { get; private set; }

        public AddRequisitionItemDialog(
            SimpleProductService simpleProductService,
            CompositeProductService compositeProductService)
        {
            InitializeComponent();
            _simpleProductService = simpleProductService;
            _compositeProductService = compositeProductService;
        }

        private void AddRequisitionItemDialog_Load(object sender, EventArgs e)
        {
            _allProducts = new List<Product>();

            // Add all simple products
            foreach (var p in _simpleProductService.GetAllProducts())
                _allProducts.Add(p);

            // Add all composite products
            foreach (var p in _compositeProductService.GetAllProducts())
                _allProducts.Add(p);

            // Sort by name
            _allProducts.Sort((a, b) => string.Compare(a.Name, b.Name));

            cmbProducts.DisplayMember = "Name";
            cmbProducts.DataSource = _allProducts;
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

            SelectedItem = new RequisitionItem
            {
                Product = (Product)cmbProducts.SelectedItem,
                Quantity = quantity
            };
        }
    }
}