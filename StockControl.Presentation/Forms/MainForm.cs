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
            OpenForm(new RequisitionForm());
        }

        private void mnuReqReport_Click(object sender, EventArgs e)
        {
            var form = new ReportForm();
            // Pre-select Requisitions Report
            OpenForm(form);
        }

        private void mnuStockReport_Click(object sender, EventArgs e)
        {
            OpenForm(new ReportForm());
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