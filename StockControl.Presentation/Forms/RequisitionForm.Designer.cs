namespace StockControl.Presentation.Forms
{
    partial class RequisitionForm
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
            lblDate = new System.Windows.Forms.Label();
            lblEmployee = new System.Windows.Forms.Label();
            lblTotalCost = new System.Windows.Forms.Label();
            lblTotalValue = new System.Windows.Forms.Label();
            dtpDate = new System.Windows.Forms.DateTimePicker();
            txtEmployee = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnAddItem = new System.Windows.Forms.Button();
            btnRemoveItem = new System.Windows.Forms.Button();
            dgvItems = new System.Windows.Forms.DataGridView();
            dgvRequisitions = new System.Windows.Forms.DataGridView();

            SuspendLayout();

            // lblDate
            lblDate.Text = "Date:";
            lblDate.Location = new System.Drawing.Point(12, 20);
            lblDate.Size = new System.Drawing.Size(100, 23);

            // dtpDate
            dtpDate.Location = new System.Drawing.Point(120, 17);
            dtpDate.Size = new System.Drawing.Size(200, 23);
            dtpDate.Name = "dtpDate";
            dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // lblEmployee
            lblEmployee.Text = "Responsible:";
            lblEmployee.Location = new System.Drawing.Point(12, 55);
            lblEmployee.Size = new System.Drawing.Size(100, 23);

            // txtEmployee
            txtEmployee.Location = new System.Drawing.Point(120, 52);
            txtEmployee.Size = new System.Drawing.Size(300, 23);
            txtEmployee.Name = "txtEmployee";

            // dgvItems (items of current requisition)
            dgvItems.Location = new System.Drawing.Point(12, 90);
            dgvItems.Size = new System.Drawing.Size(760, 150);
            dgvItems.Name = "dgvItems";
            dgvItems.AllowUserToAddRows = false;
            dgvItems.ReadOnly = true;
            dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // lblTotalCost
            lblTotalCost.Text = "Total Cost:";
            lblTotalCost.Location = new System.Drawing.Point(12, 252);
            lblTotalCost.Size = new System.Drawing.Size(80, 23);

            // lblTotalValue
            lblTotalValue.Text = "R$ 0.00";
            lblTotalValue.Location = new System.Drawing.Point(100, 252);
            lblTotalValue.Size = new System.Drawing.Size(150, 23);
            lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            lblTotalValue.Name = "lblTotalValue";

            // btnAddItem
            btnAddItem.Text = "Add Item";
            btnAddItem.Location = new System.Drawing.Point(12, 285);
            btnAddItem.Size = new System.Drawing.Size(100, 30);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Click += new System.EventHandler(btnAddItem_Click);

            // btnRemoveItem
            btnRemoveItem.Text = "Remove Item";
            btnRemoveItem.Location = new System.Drawing.Point(122, 285);
            btnRemoveItem.Size = new System.Drawing.Size(110, 30);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Click += new System.EventHandler(btnRemoveItem_Click);

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

            // dgvRequisitions (list of all requisitions)
            dgvRequisitions.Location = new System.Drawing.Point(12, 370);
            dgvRequisitions.Size = new System.Drawing.Size(760, 180);
            dgvRequisitions.Name = "dgvRequisitions";
            dgvRequisitions.AllowUserToAddRows = false;
            dgvRequisitions.ReadOnly = true;
            dgvRequisitions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRequisitions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvRequisitions_CellClick);

            // Form
            ClientSize = new System.Drawing.Size(800, 580);
            Text = "Requisitions";
            Name = "RequisitionForm";
            Load += new System.EventHandler(RequisitionForm_Load);

            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(lblEmployee);
            Controls.Add(txtEmployee);
            Controls.Add(dgvItems);
            Controls.Add(lblTotalCost);
            Controls.Add(lblTotalValue);
            Controls.Add(btnAddItem);
            Controls.Add(btnRemoveItem);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgvRequisitions);

            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridView dgvRequisitions;
    }
}