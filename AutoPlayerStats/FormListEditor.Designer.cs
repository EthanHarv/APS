namespace AutoPlayerStats
{
    partial class FormListEditor
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
            this.txtWhitelist = new System.Windows.Forms.RichTextBox();
            this.txtBlacklist = new System.Windows.Forms.RichTextBox();
            this.lblWhitelist = new System.Windows.Forms.Label();
            this.lblBlacklist = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtWhitelist
            // 
            this.txtWhitelist.Location = new System.Drawing.Point(12, 43);
            this.txtWhitelist.Name = "txtWhitelist";
            this.txtWhitelist.Size = new System.Drawing.Size(382, 345);
            this.txtWhitelist.TabIndex = 0;
            this.txtWhitelist.Text = "";
            // 
            // txtBlacklist
            // 
            this.txtBlacklist.Location = new System.Drawing.Point(400, 43);
            this.txtBlacklist.Name = "txtBlacklist";
            this.txtBlacklist.Size = new System.Drawing.Size(388, 345);
            this.txtBlacklist.TabIndex = 1;
            this.txtBlacklist.Text = "";
            // 
            // lblWhitelist
            // 
            this.lblWhitelist.AutoSize = true;
            this.lblWhitelist.Location = new System.Drawing.Point(173, 27);
            this.lblWhitelist.Name = "lblWhitelist";
            this.lblWhitelist.Size = new System.Drawing.Size(47, 13);
            this.lblWhitelist.TabIndex = 2;
            this.lblWhitelist.Text = "Whitelist";
            // 
            // lblBlacklist
            // 
            this.lblBlacklist.AutoSize = true;
            this.lblBlacklist.Location = new System.Drawing.Point(578, 27);
            this.lblBlacklist.Name = "lblBlacklist";
            this.lblBlacklist.Size = new System.Drawing.Size(46, 13);
            this.lblBlacklist.TabIndex = 3;
            this.lblBlacklist.Text = "Blacklist";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(632, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblBlacklist);
            this.Controls.Add(this.lblWhitelist);
            this.Controls.Add(this.txtBlacklist);
            this.Controls.Add(this.txtWhitelist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormListEditor";
            this.Text = "Edit ";
            this.Load += new System.EventHandler(this.FormListEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtWhitelist;
        private System.Windows.Forms.RichTextBox txtBlacklist;
        private System.Windows.Forms.Label lblWhitelist;
        private System.Windows.Forms.Label lblBlacklist;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}