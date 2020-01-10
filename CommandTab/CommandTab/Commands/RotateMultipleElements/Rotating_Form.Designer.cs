using System;

namespace CommandTab
{
    partial class Rotating_Form
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
            this.label_X = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.label_Y = new System.Windows.Forms.Label();
            this.label_Z = new System.Windows.Forms.Label();
            this.textBox_Z = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_X
            // 
            this.label_X.AutoSize = true;
            this.label_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_X.ForeColor = System.Drawing.Color.Red;
            this.label_X.Location = new System.Drawing.Point(29, 30);
            this.label_X.Name = "label_X";
            this.label_X.Size = new System.Drawing.Size(42, 13);
            this.label_X.TabIndex = 0;
            this.label_X.Text = "X Axis";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 33);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textBox_X
            // 
            this.textBox_X.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_X.Location = new System.Drawing.Point(150, 23);
            this.textBox_X.MaximumSize = new System.Drawing.Size(100, 20);
            this.textBox_X.MaxLength = 9;
            this.textBox_X.MinimumSize = new System.Drawing.Size(100, 20);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(100, 20);
            this.textBox_X.TabIndex = 3;
            this.textBox_X.TabStop = false;
            this.textBox_X.WordWrap = false;
            this.textBox_X.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_X_MouseClick);
            // 
            // label_Y
            // 
            this.label_Y.AutoSize = true;
            this.label_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_Y.ForeColor = System.Drawing.Color.Red;
            this.label_Y.Location = new System.Drawing.Point(29, 80);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(42, 13);
            this.label_Y.TabIndex = 3;
            this.label_Y.Text = "Y Axis";
            this.label_Y.Click += new System.EventHandler(this.label2_Click);
            // 
            // label_Z
            // 
            this.label_Z.AutoSize = true;
            this.label_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_Z.ForeColor = System.Drawing.Color.Red;
            this.label_Z.Location = new System.Drawing.Point(29, 128);
            this.label_Z.Name = "label_Z";
            this.label_Z.Size = new System.Drawing.Size(42, 13);
            this.label_Z.TabIndex = 4;
            this.label_Z.Text = "Z Axis";
            // 
            // textBox_Z
            // 
            this.textBox_Z.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_Z.Location = new System.Drawing.Point(150, 121);
            this.textBox_Z.Name = "textBox_Z";
            this.textBox_Z.Size = new System.Drawing.Size(100, 20);
            this.textBox_Z.TabIndex = 5;
            this.textBox_Z.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Z_MouseClick);
            // 
            // textBox_Y
            // 
            this.textBox_Y.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_Y.Location = new System.Drawing.Point(150, 73);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(100, 20);
            this.textBox_Y.TabIndex = 6;
            this.textBox_Y.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Y_MouseClick);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(150, 176);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 33);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Rotating_Form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(276, 242);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_Z);
            this.Controls.Add(this.label_Z);
            this.Controls.Add(this.label_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label_X);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rotating_Form";
            this.Opacity = 0.89D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rotate Elements";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Rotating_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.TextBox textBox_Z;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.Button btnClose;
    }
}