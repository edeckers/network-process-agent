using ElyDeckers.NetworkWatcher.UI;
namespace ElyDeckers.NetworkWatcher
{
    partial class MainForm
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
            this.lstNetworkInterfaces = new System.Windows.Forms.ListBox();
            this.btnAddNetworkInterfaceRule = new System.Windows.Forms.Button();
            this.txtProcessName = new ElyDeckers.NetworkWatcher.UI.PlaceholderTextBox() { PlaceholderText = "Process to be killed when interface comes up" };
            this.lstNetworkInterfaceRules = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstNetworkInterfaces
            // 
            this.lstNetworkInterfaces.FormattingEnabled = true;
            this.lstNetworkInterfaces.Location = new System.Drawing.Point(12, 12);
            this.lstNetworkInterfaces.Name = "lstNetworkInterfaces";
            this.lstNetworkInterfaces.Size = new System.Drawing.Size(447, 108);
            this.lstNetworkInterfaces.TabIndex = 0;
            // 
            // btnAddNetworkInterfaceRule
            // 
            this.btnAddNetworkInterfaceRule.Location = new System.Drawing.Point(384, 127);
            this.btnAddNetworkInterfaceRule.Name = "btnAddNetworkInterfaceRule";
            this.btnAddNetworkInterfaceRule.Size = new System.Drawing.Size(75, 23);
            this.btnAddNetworkInterfaceRule.TabIndex = 1;
            this.btnAddNetworkInterfaceRule.Text = "Add rule";
            this.btnAddNetworkInterfaceRule.UseVisualStyleBackColor = true;
            this.btnAddNetworkInterfaceRule.Click += new System.EventHandler(this.btnAddNetworkInterfaceRule_Click);
            // 
            // txtProcessName
            // 
            this.txtProcessName.Location = new System.Drawing.Point(12, 127);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.PlaceholderText = null;
            this.txtProcessName.Size = new System.Drawing.Size(366, 20);
            this.txtProcessName.TabIndex = 2;
            this.txtProcessName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessName_KeyDown);
            // 
            // lstNetworkInterfaceRules
            // 
            this.lstNetworkInterfaceRules.FormattingEnabled = true;
            this.lstNetworkInterfaceRules.Location = new System.Drawing.Point(12, 153);
            this.lstNetworkInterfaceRules.Name = "lstNetworkInterfaceRules";
            this.lstNetworkInterfaceRules.Size = new System.Drawing.Size(447, 95);
            this.lstNetworkInterfaceRules.TabIndex = 3;
            this.lstNetworkInterfaceRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstNetworkInterfaceRules_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 257);
            this.Controls.Add(this.lstNetworkInterfaceRules);
            this.Controls.Add(this.txtProcessName);
            this.Controls.Add(this.btnAddNetworkInterfaceRule);
            this.Controls.Add(this.lstNetworkInterfaces);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NIC Watcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstNetworkInterfaces;
        private System.Windows.Forms.Button btnAddNetworkInterfaceRule;
        private System.Windows.Forms.ListBox lstNetworkInterfaceRules;
        private PlaceholderTextBox txtProcessName;
    }
}

