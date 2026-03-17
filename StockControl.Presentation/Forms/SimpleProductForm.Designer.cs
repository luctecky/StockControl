namespace StockControl.Presentation.Forms;

partial class SimpleProductForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblName = new Label();
        lblCostPrice = new Label();
        lblSalePrice = new Label();
        txtName = new TextBox();
        txtCostPrice = new TextBox();
        txtSalePrice = new TextBox();
        btnSave = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        dgvProducts = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        SuspendLayout();
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(12, 9);
        lblName.Name = "lblName";
        lblName.Size = new Size(42, 15);
        lblName.TabIndex = 0;
        lblName.Text = "Name:";
        // 
        // lblCostPrice
        // 
        lblCostPrice.AutoSize = true;
        lblCostPrice.Location = new Point(12, 47);
        lblCostPrice.Name = "lblCostPrice";
        lblCostPrice.Size = new Size(63, 15);
        lblCostPrice.TabIndex = 1;
        lblCostPrice.Text = "Cost Price:";
        // 
        // lblSalePrice
        // 
        lblSalePrice.AutoSize = true;
        lblSalePrice.Location = new Point(149, 47);
        lblSalePrice.Name = "lblSalePrice";
        lblSalePrice.Size = new Size(60, 15);
        lblSalePrice.TabIndex = 2;
        lblSalePrice.Text = "Sale Price:";
        // 
        // txtName
        // 
        txtName.Location = new Point(60, 6);
        txtName.Name = "txtName";
        txtName.Size = new Size(217, 23);
        txtName.TabIndex = 3;
        // 
        // txtCostPrice
        // 
        txtCostPrice.Location = new Point(72, 44);
        txtCostPrice.Name = "txtCostPrice";
        txtCostPrice.Size = new Size(71, 23);
        txtCostPrice.TabIndex = 4;
        // 
        // txtSalePrice
        // 
        txtSalePrice.Location = new Point(206, 44);
        txtSalePrice.Name = "txtSalePrice";
        txtSalePrice.Size = new Size(71, 23);
        txtSalePrice.TabIndex = 5;
        // 
        // btnSave
        // 
        btnSave.Location = new Point(206, 114);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(75, 23);
        btnSave.TabIndex = 6;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        // 
        // btnDelete
        // 
        btnDelete.Location = new Point(12, 114);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(75, 23);
        btnDelete.TabIndex = 7;
        btnDelete.Text = "Delete";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnClear
        // 
        btnClear.Location = new Point(104, 73);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(75, 23);
        btnClear.TabIndex = 8;
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        // 
        // dgvProducts
        // 
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProducts.Dock = DockStyle.Bottom;
        dgvProducts.Location = new Point(0, 143);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(282, 176);
        dgvProducts.TabIndex = 9;
        dgvProducts.CellClick += dgvProducts_CellClick;
        // 
        // SimpleProductForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(282, 319);
        Controls.Add(dgvProducts);
        Controls.Add(btnClear);
        Controls.Add(btnDelete);
        Controls.Add(btnSave);
        Controls.Add(txtSalePrice);
        Controls.Add(txtCostPrice);
        Controls.Add(txtName);
        Controls.Add(lblSalePrice);
        Controls.Add(lblCostPrice);
        Controls.Add(lblName);
        Name = "SimpleProductForm";
        Text = "Cadastrar produto simples";
        Load += SimpleProductForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblName;
    private Label lblCostPrice;
    private Label lblSalePrice;
    private TextBox txtName;
    private TextBox txtCostPrice;
    private TextBox txtSalePrice;
    private Button btnSave;
    private Button btnDelete;
    private Button btnClear;
    private DataGridView dgvProducts;
}
