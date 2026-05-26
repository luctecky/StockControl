namespace StockControl.Presentation.Forms
{
    partial class CompositeProductForm
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
            lblName = new System.Windows.Forms.Label();
            lblSalePrice = new System.Windows.Forms.Label();
            lblCostPrice = new System.Windows.Forms.Label();
            lblCostPriceValue = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            txtSalePrice = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnAddComponent = new System.Windows.Forms.Button();
            btnRemoveComponent = new System.Windows.Forms.Button();
            dgvProducts = new System.Windows.Forms.DataGridView();
            dgvComponents = new System.Windows.Forms.DataGridView();

            SuspendLayout();

            // lblName
            lblName.Text = "Name:";
            lblName.Location = new System.Drawing.Point(12, 20);
            lblName.Size = new System.Drawing.Size(80, 23);

            // txtName
            txtName.Location = new System.Drawing.Point(100, 17);
            txtName.Size = new System.Drawing.Size(300, 23);
            txtName.Name = "txtName";

            // lblSalePrice
            lblSalePrice.Text = "Sale Price:";
            lblSalePrice.Location = new System.Drawing.Point(12, 55);
            lblSalePrice.Size = new System.Drawing.Size(80, 23);

            // txtSalePrice
            txtSalePrice.Location = new System.Drawing.Point(100, 52);
            txtSalePrice.Size = new System.Drawing.Size(150, 23);
            txtSalePrice.Name = "txtSalePrice";

            // lblCostPrice
            lblCostPrice.Text = "Calculated Cost:";
            lblCostPrice.Location = new System.Drawing.Point(12, 90);
            lblCostPrice.Size = new System.Drawing.Size(110, 23);

            // lblCostPriceValue
            lblCostPriceValue.Text = "R$ 0.00";
            lblCostPriceValue.Location = new System.Drawing.Point(130, 90);
            lblCostPriceValue.Size = new System.Drawing.Size(120, 23);
            lblCostPriceValue.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            lblCostPriceValue.Name = "lblCostPriceValue";

            // dgvComponents (lista de componentes do produto composto)
            dgvComponents.Location = new System.Drawing.Point(12, 125);
            dgvComponents.Size = new System.Drawing.Size(500, 150);
            dgvComponents.Name = "dgvComponents";
            dgvComponents.AllowUserToAddRows = false;
            dgvComponents.ReadOnly = true;
            dgvComponents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // btnAddComponent
            btnAddComponent.Text = "Add Component";
            btnAddComponent.Location = new System.Drawing.Point(12, 285);
            btnAddComponent.Size = new System.Drawing.Size(130, 30);
            btnAddComponent.Name = "btnAddComponent";
            btnAddComponent.Click += new System.EventHandler(btnAddComponent_Click);

            // btnRemoveComponent
            btnRemoveComponent.Text = "Remove Component";
            btnRemoveComponent.Location = new System.Drawing.Point(152, 285);
            btnRemoveComponent.Size = new System.Drawing.Size(150, 30);
            btnRemoveComponent.Name = "btnRemoveComponent";
            btnRemoveComponent.Click += new System.EventHandler(btnRemoveComponent_Click);

            // btnSave
            btnSave.Text = "Save";
            btnSave.Location = new System.Drawing.Point(12, 325);
            btnSave.Size = new System.Drawing.Size(90, 30);
            btnSave.Name = "btnSave";
            btnSave.Click += new System.EventHandler(btnSave_Click);

            // btnDelete
            btnDelete.Text = "Delete";
            btnDelete.Location = new System.Drawing.Point(112, 325);
            btnDelete.Size = new System.Drawing.Size(90, 30);
            btnDelete.Name = "btnDelete";
            btnDelete.Click += new System.EventHandler(btnDelete_Click);

            // btnClear
            btnClear.Text = "Clear";
            btnClear.Location = new System.Drawing.Point(212, 325);
            btnClear.Size = new System.Drawing.Size(90, 30);
            btnClear.Name = "btnClear";
            btnClear.Click += new System.EventHandler(btnClear_Click);

            // dgvProducts (lista de todos os produtos compostos)
            dgvProducts.Location = new System.Drawing.Point(12, 370);
            dgvProducts.Size = new System.Drawing.Size(760, 200);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProducts_CellClick);

            // Form
            ClientSize = new System.Drawing.Size(800, 600);
            Text = "Composite Products";
            Name = "CompositeProductForm";
            Load += new System.EventHandler(CompositeProductForm_Load);

            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblSalePrice);
            Controls.Add(txtSalePrice);
            Controls.Add(lblCostPrice);
            Controls.Add(lblCostPriceValue);
            Controls.Add(dgvComponents);
            Controls.Add(btnAddComponent);
            Controls.Add(btnRemoveComponent);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgvProducts);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSalePrice;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.Label lblCostPriceValue;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSalePrice;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddComponent;
        private System.Windows.Forms.Button btnRemoveComponent;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridView dgvComponents;
    }
}