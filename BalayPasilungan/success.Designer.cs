namespace BalayPasilungan
{
    partial class success
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
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.lblSuccess = new System.Windows.Forms.RichTextBox();
            this.btnSuccess = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // pbHeader
            // 
            this.pbHeader.Image = global::BalayPasilungan.Properties.Resources.success_header;
            this.pbHeader.Location = new System.Drawing.Point(0, 0);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(500, 105);
            this.pbHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHeader.TabIndex = 3;
            this.pbHeader.TabStop = false;
            // 
            // lblSuccess
            // 
            this.lblSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSuccess.AutoWordSelection = true;
            this.lblSuccess.BackColor = System.Drawing.Color.White;
            this.lblSuccess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSuccess.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblSuccess.DetectUrls = false;
            this.lblSuccess.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSuccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblSuccess.Location = new System.Drawing.Point(12, 151);
            this.lblSuccess.MaxLength = 1000;
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.ReadOnly = true;
            this.lblSuccess.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lblSuccess.ShowSelectionMargin = true;
            this.lblSuccess.Size = new System.Drawing.Size(476, 64);
            this.lblSuccess.TabIndex = 5;
            this.lblSuccess.TabStop = false;
            this.lblSuccess.Text = "Confirm message here";
            // 
            // btnSuccess
            // 
            this.btnSuccess.BackColor = System.Drawing.Color.White;
            this.btnSuccess.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSuccess.FlatAppearance.BorderSize = 0;
            this.btnSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccess.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSuccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnSuccess.Location = new System.Drawing.Point(250, 247);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(249, 52);
            this.btnSuccess.TabIndex = 6;
            this.btnSuccess.Text = "GOT IT!";
            this.btnSuccess.UseVisualStyleBackColor = false;
            this.btnSuccess.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // success
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.ControlBox = false;
            this.Controls.Add(this.btnSuccess);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.pbHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "success";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.success_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHeader;
        public System.Windows.Forms.RichTextBox lblSuccess;
        private System.Windows.Forms.Button btnSuccess;
    }
}