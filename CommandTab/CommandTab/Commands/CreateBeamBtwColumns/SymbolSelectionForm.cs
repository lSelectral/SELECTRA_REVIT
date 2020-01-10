using Autodesk.Revit.UI;
using System;
using System.Windows.Forms;
using Document = Autodesk.Revit.DB.Document;
using Control = System.Windows.Forms.Control;
using Form = System.Windows.Forms.Form;
using System.Data;

namespace CommandTab
{
    /// <summary>
    /// Beam Symbol Type Selection Form
    /// </summary>
    public partial class SymbolSelectionForm : Form
    {
        private CreateBeamExternalEventHandler m_Handler;
        private ExternalEvent m_ExEvent;
        private Document doc;

        /// <summary>
        /// Symbol Selection Form Class Instance and Initializer
        /// </summary>
        /// <param name="exEvent">External event which implemented when raise calling</param>
        /// <param name="handler">Revit API Externl Event Handler</param>
        /// <param name="m_doc">Active Revit Document</param>
        public SymbolSelectionForm(ExternalEvent exEvent, CreateBeamExternalEventHandler handler, Document m_doc)
        {
            InitializeComponent();
            m_Handler = handler;
            m_ExEvent = exEvent;
            doc = m_doc;
        }

        private void SymbolSelectionForm_Load(object sender, EventArgs e)
        {
            this.beamComboBox.DataSource = App.beamMaps;
            this.beamComboBox.DisplayMember = "SymbolName";
            this.beamComboBox.ValueMember = "ElementType";


            this.connectionComboBox.DataSource = App.structuralConnectionMap;
            this.connectionComboBox.DisplayMember = "ConnectionName";
            this.connectionComboBox.ValueMember = "ConnectionType";

            connectionComboBox.Enabled = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            App.beamTypeSelection = beamComboBox.SelectedValue;
            App.connectionTypeSelection = connectionComboBox.SelectedValue;

            DialogResult = DialogResult.OK;
            m_ExEvent.Raise();
            DozeOff();
        }

        #region Modeless Form Methods

        /// <summary>
        /// Form closed event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // we own both the event and the handler
            // we should dispose it before we are closed
            m_ExEvent.Dispose();
            m_ExEvent = null;
            m_Handler = null;

            // do not forget to call the base class
            base.OnFormClosed(e);
        }

        /// <summary>
        ///   Control enabler / disabler 
        /// </summary>
        ///
        private void EnableCommands(bool status)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = status;
            }
            if (!status)
            {
                this.cancelButton.Enabled = true;
            }
        }

        private void EnableStructuralConnection(bool state)
        {
            if (state)
            {
                this.connectionComboBox.Enabled = true;
            }
            if (!state)
            {
                this.connectionComboBox.Enabled = false;
            }
        }

        /// <summary>
        ///   DozeOff -> disable all controls (but the Exit button)
        /// </summary>
        /// 
        private void DozeOff()
        {
            EnableCommands(false);
        }

        /// <summary>
        ///   WakeUp -> enable all controls
        /// </summary>
        /// 
        public void WakeUp()
        {
            EnableCommands(true);
        }

        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckBoxConenction_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxConenction.Checked)
                EnableStructuralConnection(true);
            if (!CheckBoxConenction.Checked)
                EnableStructuralConnection(false);
        }
    }
}
