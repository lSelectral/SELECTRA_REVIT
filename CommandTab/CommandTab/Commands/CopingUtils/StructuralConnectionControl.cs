namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;
    #endregion

    /// <summary>
    /// Structural Connection Cope Distance Form
    /// </summary>
    public partial class StructuralConnectionControl : System.Windows.Forms.Form
    {
        /// <summary>
        /// Instance and initializer of this class
        /// </summary>
        public StructuralConnectionControl()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StructuralConnectionControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets the cope distance from text box input
        /// </summary>
        /// <returns></returns>
        public double GetCopeDistance()
        {
            double.TryParse(textBoxCopeDistance.Text, out double q);

            return GeometryHelper.FeetToMilimeter(q);
        }
    }
}
