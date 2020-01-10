namespace CommandTab
{
    partial class SymbolSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.beamComboBox = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.beamLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionComboBox = new System.Windows.Forms.ComboBox();
            this.CheckBoxConenction = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(218, 177);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(85, 30);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // beamComboBox
            // 
            this.beamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.beamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.beamComboBox.Location = new System.Drawing.Point(36, 44);
            this.beamComboBox.MaxDropDownItems = 12;
            this.beamComboBox.Name = "beamComboBox";
            this.beamComboBox.Size = new System.Drawing.Size(303, 21);
            this.beamComboBox.TabIndex = 10;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(63, 177);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 29);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "&OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // beamLabel
            // 
            this.beamLabel.Location = new System.Drawing.Point(33, 18);
            this.beamLabel.Name = "beamLabel";
            this.beamLabel.Size = new System.Drawing.Size(120, 23);
            this.beamLabel.TabIndex = 13;
            this.beamLabel.Text = "Type of Beams:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(33, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Structural Connection Types:";
            // 
            // connectionComboBox
            // 
            this.connectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectionComboBox.Location = new System.Drawing.Point(36, 122);
            this.connectionComboBox.MaxDropDownItems = 12;
            this.connectionComboBox.Name = "connectionComboBox";
            this.connectionComboBox.Size = new System.Drawing.Size(303, 21);
            this.connectionComboBox.TabIndex = 14;
            // 
            // CheckBoxConenction
            // 
            this.CheckBoxConenction.AutoSize = true;
            this.CheckBoxConenction.Location = new System.Drawing.Point(205, 95);
            this.CheckBoxConenction.Name = "CheckBoxConenction";
            this.CheckBoxConenction.Size = new System.Drawing.Size(134, 17);
            this.CheckBoxConenction.TabIndex = 17;
            this.CheckBoxConenction.Text = "Structural Connection?";
            this.CheckBoxConenction.UseVisualStyleBackColor = true;
            this.CheckBoxConenction.CheckedChanged += new System.EventHandler(this.CheckBoxConenction_CheckedChanged);
            // 
            // SymbolSelectionForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(367, 234);
            this.Controls.Add(this.CheckBoxConenction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectionComboBox);
            this.Controls.Add(this.beamLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.beamComboBox);
            this.Controls.Add(this.OKButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SymbolSelectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beam Type Selection Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SymbolSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox beamComboBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label beamLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox connectionComboBox;
        private System.Windows.Forms.CheckBox CheckBoxConenction;
    }
}