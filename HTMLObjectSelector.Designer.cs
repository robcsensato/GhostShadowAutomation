namespace Automation
{
    partial class HTMLObjectSelector
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
            this.InnerTextButton = new System.Windows.Forms.RadioButton();
            this.InnerHtmlButton = new System.Windows.Forms.RadioButton();
            this.HREFButton = new System.Windows.Forms.RadioButton();
            this.NameButton = new System.Windows.Forms.RadioButton();
            this.AltButton = new System.Windows.Forms.RadioButton();
            this.tagNameButton = new System.Windows.Forms.RadioButton();
            this.OuterTextButton = new System.Windows.Forms.RadioButton();
            this.UserDefinedButton = new System.Windows.Forms.RadioButton();
            this.OuterHtmlButton = new System.Windows.Forms.RadioButton();
            this.UserDefinedTextBox = new System.Windows.Forms.TextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.OKButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.CancelButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CancelButton)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.CancelButton);
            this.kryptonPanel.Controls.Add(this.OKButton);
            this.kryptonPanel.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel.Controls.Add(this.UserDefinedTextBox);
            this.kryptonPanel.Controls.Add(this.OuterHtmlButton);
            this.kryptonPanel.Controls.Add(this.UserDefinedButton);
            this.kryptonPanel.Controls.Add(this.OuterTextButton);
            this.kryptonPanel.Controls.Add(this.tagNameButton);
            this.kryptonPanel.Controls.Add(this.AltButton);
            this.kryptonPanel.Controls.Add(this.NameButton);
            this.kryptonPanel.Controls.Add(this.HREFButton);
            this.kryptonPanel.Controls.Add(this.InnerHtmlButton);
            this.kryptonPanel.Controls.Add(this.InnerTextButton);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(237, 290);
            this.kryptonPanel.TabIndex = 0;
            // 
            // InnerTextButton
            // 
            this.InnerTextButton.AutoSize = true;
            this.InnerTextButton.BackColor = System.Drawing.Color.Transparent;
            this.InnerTextButton.Location = new System.Drawing.Point(12, 31);
            this.InnerTextButton.Name = "InnerTextButton";
            this.InnerTextButton.Size = new System.Drawing.Size(70, 17);
            this.InnerTextButton.TabIndex = 0;
            this.InnerTextButton.TabStop = true;
            this.InnerTextButton.Text = "InnerText";
            this.InnerTextButton.UseVisualStyleBackColor = false;
            // 
            // InnerHtmlButton
            // 
            this.InnerHtmlButton.AutoSize = true;
            this.InnerHtmlButton.BackColor = System.Drawing.Color.Transparent;
            this.InnerHtmlButton.Location = new System.Drawing.Point(12, 70);
            this.InnerHtmlButton.Name = "InnerHtmlButton";
            this.InnerHtmlButton.Size = new System.Drawing.Size(70, 17);
            this.InnerHtmlButton.TabIndex = 1;
            this.InnerHtmlButton.TabStop = true;
            this.InnerHtmlButton.Text = "InnerHtml";
            this.InnerHtmlButton.UseVisualStyleBackColor = false;
            // 
            // HREFButton
            // 
            this.HREFButton.AutoSize = true;
            this.HREFButton.BackColor = System.Drawing.Color.Transparent;
            this.HREFButton.Location = new System.Drawing.Point(12, 112);
            this.HREFButton.Name = "HREFButton";
            this.HREFButton.Size = new System.Drawing.Size(54, 17);
            this.HREFButton.TabIndex = 2;
            this.HREFButton.TabStop = true;
            this.HREFButton.Text = "HREF";
            this.HREFButton.UseVisualStyleBackColor = false;
            // 
            // NameButton
            // 
            this.NameButton.AutoSize = true;
            this.NameButton.BackColor = System.Drawing.Color.Transparent;
            this.NameButton.Location = new System.Drawing.Point(13, 152);
            this.NameButton.Name = "NameButton";
            this.NameButton.Size = new System.Drawing.Size(53, 17);
            this.NameButton.TabIndex = 3;
            this.NameButton.TabStop = true;
            this.NameButton.Text = "Name";
            this.NameButton.UseVisualStyleBackColor = false;
            // 
            // AltButton
            // 
            this.AltButton.AutoSize = true;
            this.AltButton.BackColor = System.Drawing.Color.Transparent;
            this.AltButton.Location = new System.Drawing.Point(124, 31);
            this.AltButton.Name = "AltButton";
            this.AltButton.Size = new System.Drawing.Size(45, 17);
            this.AltButton.TabIndex = 4;
            this.AltButton.TabStop = true;
            this.AltButton.Text = "ALT";
            this.AltButton.UseVisualStyleBackColor = false;
            // 
            // tagNameButton
            // 
            this.tagNameButton.AutoSize = true;
            this.tagNameButton.BackColor = System.Drawing.Color.Transparent;
            this.tagNameButton.Location = new System.Drawing.Point(124, 70);
            this.tagNameButton.Name = "tagNameButton";
            this.tagNameButton.Size = new System.Drawing.Size(68, 17);
            this.tagNameButton.TabIndex = 5;
            this.tagNameButton.TabStop = true;
            this.tagNameButton.Text = "tagName";
            this.tagNameButton.UseVisualStyleBackColor = false;
            // 
            // OuterTextButton
            // 
            this.OuterTextButton.AutoSize = true;
            this.OuterTextButton.BackColor = System.Drawing.Color.Transparent;
            this.OuterTextButton.Location = new System.Drawing.Point(124, 112);
            this.OuterTextButton.Name = "OuterTextButton";
            this.OuterTextButton.Size = new System.Drawing.Size(72, 17);
            this.OuterTextButton.TabIndex = 6;
            this.OuterTextButton.TabStop = true;
            this.OuterTextButton.Text = "OuterText";
            this.OuterTextButton.UseVisualStyleBackColor = false;
            // 
            // UserDefinedButton
            // 
            this.UserDefinedButton.AutoSize = true;
            this.UserDefinedButton.BackColor = System.Drawing.Color.Transparent;
            this.UserDefinedButton.Location = new System.Drawing.Point(124, 152);
            this.UserDefinedButton.Name = "UserDefinedButton";
            this.UserDefinedButton.Size = new System.Drawing.Size(102, 17);
            this.UserDefinedButton.TabIndex = 7;
            this.UserDefinedButton.TabStop = true;
            this.UserDefinedButton.Text = "USERDEFINED";
            this.UserDefinedButton.UseVisualStyleBackColor = false;
            // 
            // OuterHtmlButton
            // 
            this.OuterHtmlButton.AutoSize = true;
            this.OuterHtmlButton.BackColor = System.Drawing.Color.Transparent;
            this.OuterHtmlButton.Location = new System.Drawing.Point(13, 183);
            this.OuterHtmlButton.Name = "OuterHtmlButton";
            this.OuterHtmlButton.Size = new System.Drawing.Size(72, 17);
            this.OuterHtmlButton.TabIndex = 8;
            this.OuterHtmlButton.TabStop = true;
            this.OuterHtmlButton.Text = "OuterHtml";
            this.OuterHtmlButton.UseVisualStyleBackColor = false;
            // 
            // UserDefinedTextBox
            // 
            this.UserDefinedTextBox.Location = new System.Drawing.Point(126, 206);
            this.UserDefinedTextBox.Name = "UserDefinedTextBox";
            this.UserDefinedTextBox.Size = new System.Drawing.Size(100, 20);
            this.UserDefinedTextBox.TabIndex = 9;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(124, 184);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(102, 16);
            this.kryptonLabel1.TabIndex = 10;
            this.kryptonLabel1.Text = "UserDefinedValue";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "UserDefinedValue";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(12, 253);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(90, 25);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.Values.ExtraText = "";
            this.OKButton.Values.Image = null;
            this.OKButton.Values.ImageStates.ImageCheckedNormal = null;
            this.OKButton.Values.ImageStates.ImageCheckedPressed = null;
            this.OKButton.Values.ImageStates.ImageCheckedTracking = null;
            this.OKButton.Values.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(124, 253);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(90, 25);
            this.CancelButton.TabIndex = 12;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Values.ExtraText = "";
            this.CancelButton.Values.Image = null;
            this.CancelButton.Values.ImageStates.ImageCheckedNormal = null;
            this.CancelButton.Values.ImageStates.ImageCheckedPressed = null;
            this.CancelButton.Values.ImageStates.ImageCheckedTracking = null;
            this.CancelButton.Values.Text = "Cancel";
            // 
            // HTMLObjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(237, 290);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "HTMLObjectSelector";
            this.Text = "HTMLObjectSelector";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CancelButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.RadioButton InnerTextButton;
        private System.Windows.Forms.TextBox UserDefinedTextBox;
        private System.Windows.Forms.RadioButton OuterHtmlButton;
        private System.Windows.Forms.RadioButton UserDefinedButton;
        private System.Windows.Forms.RadioButton OuterTextButton;
        private System.Windows.Forms.RadioButton tagNameButton;
        private System.Windows.Forms.RadioButton AltButton;
        private System.Windows.Forms.RadioButton NameButton;
        private System.Windows.Forms.RadioButton HREFButton;
        private System.Windows.Forms.RadioButton InnerHtmlButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton CancelButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton OKButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}

