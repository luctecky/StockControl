using System;
using System.Windows.Forms;

namespace StockControl.Presentation.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mnuSimple_Click(object sender, EventArgs e)
        {
            OpenForm(new SimpleProductForm());
        }

        private void mnuComposite_Click(object sender, EventArgs e)
        {
            OpenForm(new CompositeProductForm());
        }

        private void mnuManage_Click(object sender, EventArgs e)
        {
            // Coming in Phase 7
            MessageBox.Show("Requisitions — Coming soon!",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuReqReport_Click(object sender, EventArgs e)
        {
            // Coming in Phase 8
            MessageBox.Show("Requisitions Report — Coming soon!",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuStockReport_Click(object sender, EventArgs e)
        {
            // Coming in Phase 8
            MessageBox.Show("Stock Output Report — Coming soon!",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Opens a form as a dialog, keeping MainForm as the parent
        private void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Close();
            }
        }
    }
}