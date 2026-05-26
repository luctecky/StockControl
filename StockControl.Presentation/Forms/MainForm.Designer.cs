namespace StockControl.Presentation.Forms
{
    partial class MainForm
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
            menuStrip = new MenuStrip();
            mnuProducts = new ToolStripMenuItem();
            mnuSimple = new ToolStripMenuItem();
            mnuComposite = new ToolStripMenuItem();
            mnuRequisitions = new ToolStripMenuItem();
            mnuManage = new ToolStripMenuItem();
            mnuReports = new ToolStripMenuItem();
            mnuReqReport = new ToolStripMenuItem();
            mnuStockReport = new ToolStripMenuItem();
            lblWelcome = new Label();
            lblVersion = new Label();
            exitToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuProducts, mnuRequisitions, mnuReports });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            // 
            // mnuProducts
            // 
            mnuProducts.DropDownItems.AddRange(new ToolStripItem[] { mnuSimple, mnuComposite, exitToolStripMenuItem });
            mnuProducts.Name = "mnuProducts";
            mnuProducts.Size = new Size(66, 20);
            mnuProducts.Text = "Products";
            // 
            // mnuSimple
            // 
            mnuSimple.Name = "mnuSimple";
            mnuSimple.Size = new Size(182, 22);
            mnuSimple.Text = "Simple Products";
            mnuSimple.Click += mnuSimple_Click;
            // 
            // mnuComposite
            // 
            mnuComposite.Name = "mnuComposite";
            mnuComposite.Size = new Size(182, 22);
            mnuComposite.Text = "Composite Products";
            mnuComposite.Click += mnuComposite_Click;
            // 
            // mnuRequisitions
            // 
            mnuRequisitions.DropDownItems.AddRange(new ToolStripItem[] { mnuManage });
            mnuRequisitions.Name = "mnuRequisitions";
            mnuRequisitions.Size = new Size(83, 20);
            mnuRequisitions.Text = "Requisitions";
            // 
            // mnuManage
            // 
            mnuManage.Name = "mnuManage";
            mnuManage.Size = new Size(184, 22);
            mnuManage.Text = "Manage Requisitions";
            mnuManage.Click += mnuManage_Click;
            // 
            // mnuReports
            // 
            mnuReports.DropDownItems.AddRange(new ToolStripItem[] { mnuReqReport, mnuStockReport });
            mnuReports.Name = "mnuReports";
            mnuReports.Size = new Size(59, 20);
            mnuReports.Text = "Reports";
            // 
            // mnuReqReport
            // 
            mnuReqReport.Name = "mnuReqReport";
            mnuReqReport.Size = new Size(182, 22);
            mnuReqReport.Text = "Requisitions Report";
            mnuReqReport.Click += mnuReqReport_Click;
            // 
            // mnuStockReport
            // 
            mnuStockReport.Name = "mnuStockReport";
            mnuStockReport.Size = new Size(182, 22);
            mnuStockReport.Text = "Stock Output Report";
            mnuStockReport.Click += mnuStockReport_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.Location = new Point(0, 100);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(800, 60);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Stock Control System - " + DateTime.Now.ToString("MMMM yyyy");
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 10F);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Location = new Point(0, 170);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(800, 30);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "v1.0.0 — Learning Project";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(182, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(menuStrip);
            Controls.Add(lblWelcome);
            Controls.Add(lblVersion);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "Stock Control";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuProducts;
        private System.Windows.Forms.ToolStripMenuItem mnuSimple;
        private System.Windows.Forms.ToolStripMenuItem mnuComposite;
        private System.Windows.Forms.ToolStripMenuItem mnuRequisitions;
        private System.Windows.Forms.ToolStripMenuItem mnuManage;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        private System.Windows.Forms.ToolStripMenuItem mnuReqReport;
        private System.Windows.Forms.ToolStripMenuItem mnuStockReport;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblVersion;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}