namespace AutoPlayerStats
{
    partial class UserSettings
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
            this.btnEditList = new System.Windows.Forms.Button();
            this.btnFriends = new System.Windows.Forms.Button();
            this.btnSaveKey = new System.Windows.Forms.Button();
            this.labelHypKey = new System.Windows.Forms.Label();
            this.txtBoxKey = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditList
            // 
            this.btnEditList.Location = new System.Drawing.Point(12, 90);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(142, 23);
            this.btnEditList.TabIndex = 24;
            this.btnEditList.Text = "Edit Blacklist and Whitelist";
            this.btnEditList.UseVisualStyleBackColor = true;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click);
            // 
            // btnFriends
            // 
            this.btnFriends.Location = new System.Drawing.Point(12, 61);
            this.btnFriends.Name = "btnFriends";
            this.btnFriends.Size = new System.Drawing.Size(133, 23);
            this.btnFriends.TabIndex = 23;
            this.btnFriends.Text = "Load Friends as Whitelist";
            this.btnFriends.UseVisualStyleBackColor = true;
            this.btnFriends.Click += new System.EventHandler(this.btnFriends_Click);
            // 
            // btnSaveKey
            // 
            this.btnSaveKey.Location = new System.Drawing.Point(222, 32);
            this.btnSaveKey.Name = "btnSaveKey";
            this.btnSaveKey.Size = new System.Drawing.Size(75, 23);
            this.btnSaveKey.TabIndex = 22;
            this.btnSaveKey.Text = "Save Key";
            this.btnSaveKey.UseVisualStyleBackColor = true;
            this.btnSaveKey.Click += new System.EventHandler(this.btnSaveKey_Click);
            // 
            // labelHypKey
            // 
            this.labelHypKey.AutoSize = true;
            this.labelHypKey.Location = new System.Drawing.Point(12, 9);
            this.labelHypKey.Name = "labelHypKey";
            this.labelHypKey.Size = new System.Drawing.Size(65, 13);
            this.labelHypKey.TabIndex = 21;
            this.labelHypKey.Text = "Hypixel Key:";
            // 
            // txtBoxKey
            // 
            this.txtBoxKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxKey.Location = new System.Drawing.Point(83, 6);
            this.txtBoxKey.Name = "txtBoxKey";
            this.txtBoxKey.Size = new System.Drawing.Size(214, 20);
            this.txtBoxKey.TabIndex = 20;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(222, 114);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogFolder
            // 
            this.btnLogFolder.Location = new System.Drawing.Point(12, 32);
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.Size = new System.Drawing.Size(96, 23);
            this.btnLogFolder.TabIndex = 26;
            this.btnLogFolder.Text = "Open Log Folder";
            this.btnLogFolder.UseVisualStyleBackColor = true;
            this.btnLogFolder.Click += new System.EventHandler(this.btnLogFolder_Click);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 149);
            this.Controls.Add(this.btnLogFolder);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEditList);
            this.Controls.Add(this.btnFriends);
            this.Controls.Add(this.btnSaveKey);
            this.Controls.Add(this.labelHypKey);
            this.Controls.Add(this.txtBoxKey);
            this.Name = "UserSettings";
            this.Text = "UserSettings";
            this.Load += new System.EventHandler(this.UserSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditList;
        private System.Windows.Forms.Button btnFriends;
        private System.Windows.Forms.Button btnSaveKey;
        private System.Windows.Forms.Label labelHypKey;
        private System.Windows.Forms.TextBox txtBoxKey;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogFolder;
    }
}