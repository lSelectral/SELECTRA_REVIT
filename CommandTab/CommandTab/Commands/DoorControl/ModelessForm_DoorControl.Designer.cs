namespace CommandTab
{
    partial class ModelessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelessForm));
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnFlipRight = new System.Windows.Forms.Button();
            this.btnFlipLeft = new System.Windows.Forms.Button();
            this.btnFlipLeftRight = new System.Windows.Forms.Button();
            this.btnFlipDown = new System.Windows.Forms.Button();
            this.btnFlipUp = new System.Windows.Forms.Button();
            this.btnFlipUpDown = new System.Windows.Forms.Button();
            this.btnDeleted = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(35, 118);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(89, 24);
            this.btnRotate.TabIndex = 25;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.BtnRotate_Click);
            // 
            // btnFlipRight
            // 
            this.btnFlipRight.Location = new System.Drawing.Point(35, 88);
            this.btnFlipRight.Name = "btnFlipRight";
            this.btnFlipRight.Size = new System.Drawing.Size(89, 24);
            this.btnFlipRight.TabIndex = 23;
            this.btnFlipRight.Text = "Right";
            this.btnFlipRight.UseVisualStyleBackColor = true;
            this.btnFlipRight.Click += new System.EventHandler(this.BtnFlipRight_Click);
            // 
            // btnFlipLeft
            // 
            this.btnFlipLeft.Location = new System.Drawing.Point(35, 58);
            this.btnFlipLeft.Name = "btnFlipLeft";
            this.btnFlipLeft.Size = new System.Drawing.Size(89, 24);
            this.btnFlipLeft.TabIndex = 21;
            this.btnFlipLeft.Text = "Left";
            this.btnFlipLeft.UseVisualStyleBackColor = true;
            this.btnFlipLeft.Click += new System.EventHandler(this.BtnFlipLeft_Click);
            // 
            // btnFlipLeftRight
            // 
            this.btnFlipLeftRight.Location = new System.Drawing.Point(35, 28);
            this.btnFlipLeftRight.Name = "btnFlipLeftRight";
            this.btnFlipLeftRight.Size = new System.Drawing.Size(89, 24);
            this.btnFlipLeftRight.TabIndex = 19;
            this.btnFlipLeftRight.Text = "Left / Right";
            this.btnFlipLeftRight.UseVisualStyleBackColor = true;
            this.btnFlipLeftRight.Click += new System.EventHandler(this.BtnFlipLeftRight_Click);
            // 
            // btnFlipDown
            // 
            this.btnFlipDown.Location = new System.Drawing.Point(138, 88);
            this.btnFlipDown.Name = "btnFlipDown";
            this.btnFlipDown.Size = new System.Drawing.Size(89, 24);
            this.btnFlipDown.TabIndex = 24;
            this.btnFlipDown.Text = "In";
            this.btnFlipDown.UseVisualStyleBackColor = true;
            this.btnFlipDown.Click += new System.EventHandler(this.BtnFlipDown_Click);
            // 
            // btnFlipUp
            // 
            this.btnFlipUp.Location = new System.Drawing.Point(138, 58);
            this.btnFlipUp.Name = "btnFlipUp";
            this.btnFlipUp.Size = new System.Drawing.Size(89, 24);
            this.btnFlipUp.TabIndex = 22;
            this.btnFlipUp.Text = "Out";
            this.btnFlipUp.UseVisualStyleBackColor = true;
            this.btnFlipUp.Click += new System.EventHandler(this.BtnFlipUp_Click);
            // 
            // btnFlipUpDown
            // 
            this.btnFlipUpDown.Location = new System.Drawing.Point(138, 28);
            this.btnFlipUpDown.Name = "btnFlipUpDown";
            this.btnFlipUpDown.Size = new System.Drawing.Size(89, 24);
            this.btnFlipUpDown.TabIndex = 20;
            this.btnFlipUpDown.Text = "In / Out";
            this.btnFlipUpDown.UseVisualStyleBackColor = true;
            this.btnFlipUpDown.Click += new System.EventHandler(this.BtnFlipUpDown_Click);
            // 
            // btnDeleted
            // 
            this.btnDeleted.Location = new System.Drawing.Point(138, 118);
            this.btnDeleted.Name = "btnDeleted";
            this.btnDeleted.Size = new System.Drawing.Size(89, 24);
            this.btnDeleted.TabIndex = 26;
            this.btnDeleted.Text = "Delete";
            this.btnDeleted.UseVisualStyleBackColor = true;
            this.btnDeleted.Click += new System.EventHandler(this.BtnDeleted_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(89, 162);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 23);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // ModelessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(270, 238);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnFlipRight);
            this.Controls.Add(this.btnFlipLeft);
            this.Controls.Add(this.btnFlipLeftRight);
            this.Controls.Add(this.btnFlipDown);
            this.Controls.Add(this.btnFlipUp);
            this.Controls.Add(this.btnFlipUpDown);
            this.Controls.Add(this.btnDeleted);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelessForm";
            this.Opacity = 0.75D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Door Control";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ModelessForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnFlipRight;
        private System.Windows.Forms.Button btnFlipLeft;
        private System.Windows.Forms.Button btnFlipLeftRight;
        private System.Windows.Forms.Button btnFlipDown;
        private System.Windows.Forms.Button btnFlipUp;
        private System.Windows.Forms.Button btnFlipUpDown;
        private System.Windows.Forms.Button btnDeleted;
        private System.Windows.Forms.Button btnExit;
    }
}