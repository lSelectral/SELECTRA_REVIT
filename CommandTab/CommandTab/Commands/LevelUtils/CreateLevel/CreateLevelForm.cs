using System;
using System.Windows.Forms;

namespace CommandTab
{
    /// <summary>
    /// Create Multiple Level User Form
    /// </summary>
    public partial class CreateLevelForm : Form
    {
        public CreateLevelForm()
        {
            InitializeComponent();
        }

        private void CreateLevelForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets the level count from text box input
        /// </summary>
        /// <returns></returns>
        public double getLevelCount()
        {
            double.TryParse(textBoxCount.Text, out double q);
            return q;
        }

        /// <summary>
        /// Gets the level offset distance from text box input
        /// </summary>
        /// <returns></returns>
        public double getLevelOffset()
        {
            double.TryParse(textBoxOffset.Text, out double q);
            return q;
        }

        /// <summary>
        /// Gets the check box result for creation of first level
        /// </summary>
        /// <returns></returns>
        public bool getCheckBox()
        {
            if (checkBoxFirstLvl.Checked)
                return true;
            else
                return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
