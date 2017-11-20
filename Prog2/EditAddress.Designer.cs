namespace UPVApp
{
    partial class EditAddress
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
            this.addrsListBox = new System.Windows.Forms.ListBox();
            this.okBttn = new System.Windows.Forms.Button();
            this.cnclBttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addrsListBox
            // 
            this.addrsListBox.FormattingEnabled = true;
            this.addrsListBox.ItemHeight = 20;
            this.addrsListBox.Location = new System.Drawing.Point(37, 0);
            this.addrsListBox.Name = "addrsListBox";
            this.addrsListBox.ScrollAlwaysVisible = true;
            this.addrsListBox.Size = new System.Drawing.Size(211, 204);
            this.addrsListBox.TabIndex = 0;
            // 
            // okBttn
            // 
            this.okBttn.Location = new System.Drawing.Point(58, 268);
            this.okBttn.Name = "okBttn";
            this.okBttn.Size = new System.Drawing.Size(75, 36);
            this.okBttn.TabIndex = 12;
            this.okBttn.Text = "OK";
            this.okBttn.UseVisualStyleBackColor = true;
            this.okBttn.Click += new System.EventHandler(this.okBttn_Click);
            // 
            // cnclBttn
            // 
            this.cnclBttn.Location = new System.Drawing.Point(156, 268);
            this.cnclBttn.Name = "cnclBttn";
            this.cnclBttn.Size = new System.Drawing.Size(75, 36);
            this.cnclBttn.TabIndex = 13;
            this.cnclBttn.Text = "Cancel";
            this.cnclBttn.UseVisualStyleBackColor = true;
            this.cnclBttn.Click += new System.EventHandler(this.cnclBttn_Click);
            // 
            // EditAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 322);
            this.Controls.Add(this.cnclBttn);
            this.Controls.Add(this.okBttn);
            this.Controls.Add(this.addrsListBox);
            this.Name = "EditAddress";
            this.Text = "EditAddress";
            this.Load += new System.EventHandler(this.EditAddress_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox addrsListBox;
        private System.Windows.Forms.Button okBttn;
        private System.Windows.Forms.Button cnclBttn;
    }
}