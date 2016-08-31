using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontValidator
{
    
    public partial class DRMForm : Form
    {
        private UIWizard _uiWizard;

        public DRMForm(UIWizard wizard)
        {
            InitializeComponent();
            _uiWizard = wizard;
        }

        private void DRMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double width;
            if (!double.TryParse(textBox1.Text, out width) || width < 0 || width > 100)
            {
                MessageBox.Show("Not a valid input." + Environment.NewLine +
                    "Please use an integer within 0-100 for input", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            _uiWizard.update_tolerance_drm(width);
        }
    }
}
