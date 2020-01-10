namespace CommandTab
{
    using System;
    using System.Windows.Forms;
    using Autodesk.Revit.UI;

    /// <summary>
    /// Door Control Modeless Form
    /// </summary>
    public partial class ModelessForm : Form
    {
        private RequestHandler m_Handler;
        private ExternalEvent m_ExEvent;

        /// <summary>
        /// Door Control Modeless Form Instance and Initializer
        /// </summary>
        /// <param name="exEvent">External Event for modeless form implemantion</param>
        /// <param name="handler">External Event Handler Class</param>
        public ModelessForm(ExternalEvent exEvent, RequestHandler handler)
        {
            InitializeComponent();
            m_Handler = handler;
            m_ExEvent = exEvent;
        }

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
                this.btnExit.Enabled = true;
            }
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
        private void MakeRequest(RequestId request)
        {
            m_Handler.Request.Make(request);
            m_ExEvent.Raise();
            DozeOff();
        }


        /// <summary>
        ///   DozeOff -> disable all controls (but the Exit button)
        /// </summary>
        /// 
        private void DozeOff() => EnableCommands(false);


        /// <summary>
        ///   WakeUp -> enable all controls
        /// </summary>
        /// 
        public void WakeUp()
        {
            EnableCommands(true);
        }

        private void ModelessForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnFlipLeftRight_Click(object sender, EventArgs e) => MakeRequest(RequestId.FlipLeftRight);

        private void BtnFlipUpDown_Click(object sender, EventArgs e) => MakeRequest(RequestId.FlipInOut);

        private void BtnFlipLeft_Click(object sender, EventArgs e) => MakeRequest(RequestId.MakeLeft);

        private void BtnFlipUp_Click(object sender, EventArgs e) => MakeRequest(RequestId.TurnOut);

        private void BtnFlipRight_Click(object sender, EventArgs e) => MakeRequest(RequestId.MakeRight);

        private void BtnFlipDown_Click(object sender, EventArgs e) => MakeRequest(RequestId.TurnIn);

        private void BtnRotate_Click(object sender, EventArgs e) => MakeRequest(RequestId.Rotate);

        private void BtnDeleted_Click(object sender, EventArgs e) => MakeRequest(RequestId.Delete);

        private void BtnExit_Click(object sender, EventArgs e) => Close();
    }
}
