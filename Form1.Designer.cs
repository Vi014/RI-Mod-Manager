﻿
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbDisabled = new System.Windows.Forms.ListBox();
            this.lbEnabled = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.btnReviver = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCd = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbDisabled
            // 
            this.lbDisabled.FormattingEnabled = true;
            this.lbDisabled.Location = new System.Drawing.Point(12, 44);
            this.lbDisabled.Name = "lbDisabled";
            this.lbDisabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbDisabled.Size = new System.Drawing.Size(320, 355);
            this.lbDisabled.TabIndex = 2;
            // 
            // lbEnabled
            // 
            this.lbEnabled.FormattingEnabled = true;
            this.lbEnabled.Location = new System.Drawing.Point(389, 44);
            this.lbEnabled.Name = "lbEnabled";
            this.lbEnabled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbEnabled.Size = new System.Drawing.Size(320, 355);
            this.lbEnabled.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Disabled";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enabled";
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(338, 265);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(45, 45);
            this.btnEnable.TabIndex = 4;
            this.btnEnable.Text = ">";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(338, 116);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(45, 45);
            this.btnDisable.TabIndex = 3;
            this.btnDisable.Text = "<";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(279, 405);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 23);
            this.btnLaunch.TabIndex = 9;
            this.btnLaunch.Text = "Launch RI";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(360, 402);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(128, 17);
            this.cbClose.TabIndex = 10;
            this.cbClose.Text = "Close upon launching";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.CheckedChanged += new System.EventHandler(this.cbClose_CheckedChanged);
            // 
            // btnReviver
            // 
            this.btnReviver.Location = new System.Drawing.Point(12, 405);
            this.btnReviver.Name = "btnReviver";
            this.btnReviver.Size = new System.Drawing.Size(97, 23);
            this.btnReviver.TabIndex = 8;
            this.btnReviver.Text = "Revive catalog";
            this.btnReviver.UseVisualStyleBackColor = true;
            this.btnReviver.Click += new System.EventHandler(this.btnReviver_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(716, 116);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 45);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "/\\";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(716, 265);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 45);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "\\/";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(717, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Change\r\nload\r\norder";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(636, 405);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(125, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete selected mods";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(536, 405);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(94, 23);
            this.btnInstall.TabIndex = 12;
            this.btnInstall.Text = "Install new mod";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Catalog already patched.";
            this.label4.Visible = false;
            // 
            // btnCd
            // 
            this.btnCd.Location = new System.Drawing.Point(619, 13);
            this.btnCd.Name = "btnCd";
            this.btnCd.Size = new System.Drawing.Size(139, 23);
            this.btnCd.TabIndex = 1;
            this.btnCd.Text = "Change working directory";
            this.btnCd.UseVisualStyleBackColor = true;
            this.btnCd.Click += new System.EventHandler(this.btnCd_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(508, 13);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(105, 23);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Open RI folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.AutoSize = true;
            this.cbFullscreen.Location = new System.Drawing.Point(360, 420);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(121, 17);
            this.cbFullscreen.TabIndex = 11;
            this.cbFullscreen.Text = "Launch in fullscreen";
            this.cbFullscreen.UseVisualStyleBackColor = true;
            this.cbFullscreen.CheckedChanged += new System.EventHandler(this.cbFullscreen_CheckedChanged);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 437);
            this.Controls.Add(this.cbFullscreen);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnCd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnReviver);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbEnabled);
            this.Controls.Add(this.lbDisabled);
            this.Controls.Add(this.btnDisable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RI Mod Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
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
        private System.Windows.Forms.Button btnReviver;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCd;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.CheckBox cbFullscreen;
    }
}

