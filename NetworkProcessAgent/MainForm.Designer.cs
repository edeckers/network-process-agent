using ElyDeckers.NetworkProcessAgent.UI;
namespace ElyDeckers.NetworkProcessAgent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstNetworkInterfaces = new System.Windows.Forms.ListBox();
            this.btnAddNetworkInterfaceRule = new System.Windows.Forms.Button();
            this.lstNetworkInterfaceRules = new System.Windows.Forms.ListBox();
            this.txtProcessName = new ElyDeckers.NetworkProcessAgent.UI.PlaceholderTextBox() { PlaceholderText = "Name of the process to be killed when the selected interface comes online" };
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
            // lstNetworkInterfaceRules
            // 
            this.lstNetworkInterfaceRules.FormattingEnabled = true;
            this.lstNetworkInterfaceRules.Location = new System.Drawing.Point(12, 153);
            this.lstNetworkInterfaceRules.Name = "lstNetworkInterfaceRules";
            this.lstNetworkInterfaceRules.Size = new System.Drawing.Size(447, 95);
            this.lstNetworkInterfaceRules.TabIndex = 3;
            this.lstNetworkInterfaceRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstNetworkInterfaceRules_KeyDown);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Network Process Agent";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
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

