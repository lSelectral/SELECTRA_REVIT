namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Windows.Forms;
    using Autodesk.Revit.UI;
    #endregion

    /// <summary>
    /// Rotate Element Form
    /// </summary>
    public partial class Rotating_Form : Form
    {
        private RotateMultiExternalEvent m_Handler;
        private ExternalEvent m_ExEvent;

        /// <summary>
        /// Rotating form constructor
        /// </summary>
        /// <param name="exEvent">External event which implemented when raise calling</param>
        /// <param name="handler">Revit API Externl Event Handler</param>
        public Rotating_Form(ExternalEvent exEvent, RotateMultiExternalEvent handler)
        {
            InitializeComponent();
            m_Handler = handler;
            m_ExEvent = exEvent;
        }

        /// <summary>
        /// Process while loading the form
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event which will happen when button clicked</param>
        private void Rotating_Form_Load(object sender, EventArgs e)
        {

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
                this.btnClose.Enabled = true;
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

        /// <summary>
        ///   A private helper method to make a request
        ///   and put the dialog to sleep at the same time.
        /// </summary>
        /// <remarks>
        ///   It is expected that the process which executes the request 
        ///   (the Idling helper in this particular case) will also
        ///   wake the dialog up after finishing the execution.
        /// </remarks>
        ///
        private void MakeRequest()
        {
            m_ExEvent.Raise();
            DozeOff();
        }

        #endregion

        /// <summary>
        /// OK Button Click Event
        /// </summary>
        /// <param name="sender">Sender Objecet</param>
        /// <param name="e">Event which will happen when button clicked</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox_X.Text, out double q);
            double.TryParse(textBox_Y.Text, out double q2);
            double.TryParse(textBox_Z.Text, out double q3);
            App.angleX = q;
            App.angleY = q2;
            App.angleZ = q3;

            DialogResult = DialogResult.OK;
            MakeRequest();
        }
        /// <summary>
        /// Close Button Click Event
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event which will happen when button clicked</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox_X_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_X.ResetText();
        }

        private void textBox_Y_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Y.ResetText();
        }

        private void textBox_Z_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Z.ResetText();
        }
    }
}
