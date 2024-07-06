namespace Automation
{
    partial class TimeFormatWindow
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
            this.US_Time = new System.Windows.Forms.RadioButton();
            this.EuropeanTime = new System.Windows.Forms.RadioButton();
            this.AsianTime = new System.Windows.Forms.RadioButton();
            this.Set_Button = new System.Windows.Forms.Button();
            this.Close_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.Close_Button);
            this.kryptonPanel.Controls.Add(this.Set_Button);
            this.kryptonPanel.Controls.Add(this.AsianTime);
            this.kryptonPanel.Controls.Add(this.EuropeanTime);
            this.kryptonPanel.Controls.Add(this.US_Time);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(246, 209);
            this.kryptonPanel.TabIndex = 0;
            // 
            // US_Time
            // 
            this.US_Time.AutoSize = true;
            this.US_Time.BackColor = System.Drawing.Color.Transparent;
            this.US_Time.Location = new System.Drawing.Point(27, 26);
            this.US_Time.Name = "US_Time";
            this.US_Time.Size = new System.Drawing.Size(136, 17);
            this.US_Time.TabIndex = 0;
            this.US_Time.TabStop = true;
            this.US_Time.Text = "Set To US Time Format";
            this.US_Time.UseVisualStyleBackColor = false;
            // 
            // EuropeanTime
            // 
            this.EuropeanTime.AutoSize = true;
            this.EuropeanTime.BackColor = System.Drawing.Color.Transparent;
            this.EuropeanTime.Location = new System.Drawing.Point(27, 67);
            this.EuropeanTime.Name = "EuropeanTime";
            this.EuropeanTime.Size = new System.Drawing.Size(167, 17);
            this.EuropeanTime.TabIndex = 1;
            this.EuropeanTime.TabStop = true;
            this.EuropeanTime.Text = "Set To European Time Format";
            this.EuropeanTime.UseVisualStyleBackColor = false;
            // 
            // AsianTime
            // 
            this.AsianTime.AutoSize = true;
            this.AsianTime.BackColor = System.Drawing.Color.Transparent;
            this.AsianTime.Location = new System.Drawing.Point(27, 109);
            this.AsianTime.Name = "AsianTime";
            this.AsianTime.Size = new System.Drawing.Size(147, 17);
            this.AsianTime.TabIndex = 2;
            this.AsianTime.TabStop = true;
            this.AsianTime.Text = "Set To Asian Time Format";
            this.AsianTime.UseVisualStyleBackColor = false;
            // 
            // Set_Button
            // 
            this.Set_Button.Location = new System.Drawing.Point(27, 167);
            this.Set_Button.Name = "Set_Button";
            this.Set_Button.Size = new System.Drawing.Size(75, 23);
            this.Set_Button.TabIndex = 3;
            this.Set_Button.Text = "Set Format";
            this.Set_Button.UseVisualStyleBackColor = true;
            this.Set_Button.Click += new System.EventHandler(this.Set_Button_Click);
            // 
            // Close_Button
            // 
            this.Close_Button.Location = new System.Drawing.Point(142, 167);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(75, 23);
            this.Close_Button.TabIndex = 4;
            this.Close_Button.Text = "Close";
            this.Close_Button.UseVisualStyleBackColor = true;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // TimeFormatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 209);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "TimeFormatWindow";
            this.Text = "TimeFormatWindow";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.Button Set_Button;
        private System.Windows.Forms.RadioButton AsianTime;
        private System.Windows.Forms.RadioButton EuropeanTime;
        private System.Windows.Forms.RadioButton US_Time;
    }
}

