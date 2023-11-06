
namespace RI_Mod_Manager
{
    partial class Form1
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
            this.lbDisabled = new System.Windows.Forms.ListBox();
            this.lbEnabled = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbDisabled
            // 
            this.lbDisabled.FormattingEnabled = true;
            this.lbDisabled.Location = new System.Drawing.Point(12, 44);
            this.lbDisabled.Name = "lbDisabled";
            this.lbDisabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbDisabled.Size = new System.Drawing.Size(320, 355);
            this.lbDisabled.TabIndex = 0;
            // 
            // lbEnabled
            // 
            this.lbEnabled.FormattingEnabled = true;
            this.lbEnabled.Location = new System.Drawing.Point(389, 44);
            this.lbEnabled.Name = "lbEnabled";
            this.lbEnabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbEnabled.Size = new System.Drawing.Size(320, 355);
            this.lbEnabled.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Disabled";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enabled";
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(338, 265);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(45, 45);
            this.btnEnable.TabIndex = 2;
            this.btnEnable.Text = ">";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(338, 116);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(45, 45);
            this.btnDisable.TabIndex = 1;
            this.btnDisable.Text = "<";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(279, 405);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 23);
            this.btnLaunch.TabIndex = 4;
            this.btnLaunch.Text = "Launch RI";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(360, 409);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(128, 17);
            this.cbClose.TabIndex = 5;
            this.cbClose.Text = "Close upon launching";
            this.cbClose.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 436);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbEnabled);
            this.Controls.Add(this.lbDisabled);
            this.Controls.Add(this.btnDisable);
            this.Name = "Form1";
            this.Text = "RI Mod Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbDisabled;
        private System.Windows.Forms.ListBox lbEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.CheckBox cbClose;
    }
}

