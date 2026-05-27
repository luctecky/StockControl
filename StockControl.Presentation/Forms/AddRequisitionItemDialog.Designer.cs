namespace StockControl.Presentation.Forms
{
    partial class AddRequisitionItemDialog
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
            lblProduct = new System.Windows.Forms.Label();
            lblQuantity = new System.Windows.Forms.Label();
            cmbProducts = new System.Windows.Forms.ComboBox();
            txtQuantity = new System.Windows.Forms.TextBox();
            btnConfirm = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();

            SuspendLayout();

            lblProduct.Text = "Product:";
            lblProduct.Location = new System.Drawing.Point(12, 20);
            lblProduct.Size = new System.Drawing.Size(70, 23);

            cmbProducts.Location = new System.Drawing.Point(90, 17);
            cmbProducts.Size = new System.Drawing.Size(350, 23);
            cmbProducts.Name = "cmbProducts";
            cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            lblQuantity.Text = "Quantity:";
            lblQuantity.Location = new System.Drawing.Point(12, 55);
            lblQuantity.Size = new System.Drawing.Size(70, 23);

            txtQuantity.Location = new System.Drawing.Point(90, 52);
            txtQuantity.Size = new System.Drawing.Size(100, 23);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Text = "1";

            btnConfirm.Text = "Add";
            btnConfirm.Location = new System.Drawing.Point(90, 90);
            btnConfirm.Size = new System.Drawing.Size(90, 30);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnConfirm.Click += new System.EventHandler(btnConfirm_Click);

            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(190, 90);
            btnCancel.Size = new System.Drawing.Size(90, 30);
            btnCancel.Name = "btnCancel";
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            ClientSize = new System.Drawing.Size(470, 150);
            Text = "Add Item";
            Name = "AddRequisitionItemDialog";
            AcceptButton = btnConfirm;
            CancelButton = btnCancel;
            Load += new System.EventHandler(AddRequisitionItemDialog_Load);

            Controls.Add(lblProduct);
            Controls.Add(cmbProducts);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}