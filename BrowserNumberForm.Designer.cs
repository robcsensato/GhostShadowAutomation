namespace Automation
{
    partial class Browser_NumberForm
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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.Browser_NumberTextBox = new System.Windows.Forms.TextBox();
            this.Cancel_Button = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.OK_Button = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.Cancel_Button)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.OK_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.Browser_NumberTextBox);
            this.kryptonPanel.Controls.Add(this.Cancel_Button);
            this.kryptonPanel.Controls.Add(this.OK_Button);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(240, 74);
            this.kryptonPanel.TabIndex = 0;
            // 
            // Browser_NumberTextBox
            // 
            this.Browser_NumberTextBox.Location = new System.Drawing.Point(88, 12);
            this.Browser_NumberTextBox.Name = "Browser_NumberTextBox";
            this.Browser_NumberTextBox.Size = new System.Drawing.Size(63, 20);
            this.Browser_NumberTextBox.TabIndex = 6;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(161, 38);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(67, 25);
            this.Cancel_Button.TabIndex = 4;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Values.ExtraText = "";
            this.Cancel_Button.Values.Image = null;
            this.Cancel_Button.Values.ImageStates.ImageCheckedNormal = null;
            this.Cancel_Button.Values.ImageStates.ImageCheckedPressed = null;
            this.Cancel_Button.Values.ImageStates.ImageCheckedTracking = null;
            this.Cancel_Button.Values.Text = "Cancel";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_Button
            // 
            this.OK_Button.Location = new System.Drawing.Point(12, 38);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(64, 25);
            this.OK_Button.TabIndex = 3;
            this.OK_Button.Text = "OK";
            this.OK_Button.Values.ExtraText = "";
            this.OK_Button.Values.Image = null;
            this.OK_Button.Values.ImageStates.ImageCheckedNormal = null;
            this.OK_Button.Values.ImageStates.ImageCheckedPressed = null;
            this.OK_Button.Values.ImageStates.ImageCheckedTracking = null;
            this.OK_Button.Values.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Browser_NumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 74);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "Browser_NumberForm";
            this.Text = "Enter Browser Number";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.Cancel_Button)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.OK_Button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton OK_Button;
        private ComponentFactory.Krypton.Toolkit.KryptonButton Cancel_Button;
        private System.Windows.Forms.TextBox Browser_NumberTextBox;
    }
}

