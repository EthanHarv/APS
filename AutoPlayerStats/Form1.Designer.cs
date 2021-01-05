﻿namespace AutoPlayerStats
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
            this.txtBoxKey = new System.Windows.Forms.TextBox();
            this.labelHypKey = new System.Windows.Forms.Label();
            this.btnSaveKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.playerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnFriends = new System.Windows.Forms.Button();
            this.btnEditList = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblApi = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblApiTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxKey
            // 
            this.txtBoxKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxKey.Location = new System.Drawing.Point(999, 13);
            this.txtBoxKey.Name = "txtBoxKey";
            this.txtBoxKey.Size = new System.Drawing.Size(189, 20);
            this.txtBoxKey.TabIndex = 3;
            // 
            // labelHypKey
            // 
            this.labelHypKey.AutoSize = true;
            this.labelHypKey.Location = new System.Drawing.Point(925, 16);
            this.labelHypKey.Name = "labelHypKey";
            this.labelHypKey.Size = new System.Drawing.Size(65, 13);
            this.labelHypKey.TabIndex = 4;
            this.labelHypKey.Text = "Hypixel Key:";
            // 
            // btnSaveKey
            // 
            this.btnSaveKey.Location = new System.Drawing.Point(1113, 39);
            this.btnSaveKey.Name = "btnSaveKey";
            this.btnSaveKey.Size = new System.Drawing.Size(75, 23);
            this.btnSaveKey.TabIndex = 5;
            this.btnSaveKey.Text = "Save Key";
            this.btnSaveKey.UseVisualStyleBackColor = true;
            this.btnSaveKey.Click += new System.EventHandler(this.btnSaveKey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(1035, 678);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Light Blue: Nicked Player";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(1035, 657);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Blue: 5+ Winstreak";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LimeGreen;
            this.label3.Location = new System.Drawing.Point(1035, 636);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Green: Whitelisted";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(1035, 743);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dark Red: Under 5 stars, 2+ fkdr";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(1034, 701);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Yellow: Under 10 games played";
            // 
            // playerPanel
            // 
            this.playerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.playerPanel.Location = new System.Drawing.Point(12, 12);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(907, 744);
            this.playerPanel.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(1035, 615);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Red: Blacklisted";
            // 
            // btnFriends
            // 
            this.btnFriends.Location = new System.Drawing.Point(928, 39);
            this.btnFriends.Name = "btnFriends";
            this.btnFriends.Size = new System.Drawing.Size(133, 23);
            this.btnFriends.TabIndex = 13;
            this.btnFriends.Text = "Load Friends as Whitelist";
            this.btnFriends.UseVisualStyleBackColor = true;
            this.btnFriends.Click += new System.EventHandler(this.btnFriends_Click);
            // 
            // btnEditList
            // 
            this.btnEditList.Location = new System.Drawing.Point(928, 68);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(75, 23);
            this.btnEditList.TabIndex = 14;
            this.btnEditList.Text = "Edit List";
            this.btnEditList.UseVisualStyleBackColor = true;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Orange;
            this.label7.Location = new System.Drawing.Point(1035, 722);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Orange: 150+ Stars or 5+ FKDR";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(925, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Last checked API Usage:";
            // 
            // lblApi
            // 
            this.lblApi.AutoSize = true;
            this.lblApi.Location = new System.Drawing.Point(1053, 99);
            this.lblApi.Name = "lblApi";
            this.lblApi.Size = new System.Drawing.Size(13, 13);
            this.lblApi.TabIndex = 17;
            this.lblApi.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(925, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Total API Usage:";
            // 
            // lblApiTotal
            // 
            this.lblApiTotal.AutoSize = true;
            this.lblApiTotal.Location = new System.Drawing.Point(1011, 121);
            this.lblApiTotal.Name = "lblApiTotal";
            this.lblApiTotal.Size = new System.Drawing.Size(13, 13);
            this.lblApiTotal.TabIndex = 19;
            this.lblApiTotal.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 765);
            this.Controls.Add(this.lblApiTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblApi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEditList);
            this.Controls.Add(this.btnFriends);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.playerPanel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveKey);
            this.Controls.Add(this.labelHypKey);
            this.Controls.Add(this.txtBoxKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Player Stats";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBoxKey;
        private System.Windows.Forms.Label labelHypKey;
        private System.Windows.Forms.Button btnSaveKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel playerPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFriends;
        private System.Windows.Forms.Button btnEditList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblApi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblApiTotal;
    }
}

