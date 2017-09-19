namespace BalayPasilungan
{
    partial class confirm
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
            this.lblConfirm = new System.Windows.Forms.RichTextBox();
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.btnConfirm = new BalayPasilungan.NoFocusRec();
            this.btnBack = new BalayPasilungan.NoFocusRec();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConfirm
            // 
            this.lblConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirm.AutoWordSelection = true;
            this.lblConfirm.BackColor = System.Drawing.Color.White;
            this.lblConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblConfirm.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblConfirm.DetectUrls = false;
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblConfirm.Location = new System.Drawing.Point(12, 151);
            this.lblConfirm.MaxLength = 1000;
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.ReadOnly = true;
            this.lblConfirm.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lblConfirm.ShowSelectionMargin = true;
            this.lblConfirm.Size = new System.Drawing.Size(476, 64);
            this.lblConfirm.TabIndex = 4;
            this.lblConfirm.TabStop = false;
            this.lblConfirm.Text = "Confirm message here";
            this.lblConfirm.SelectionChanged += new System.EventHandler(this.lblConfirm_SelectionChanged);
            this.lblConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHeader_MouseDown);
            // 
            // pbHeader
            // 
            this.pbHeader.Image = global::BalayPasilungan.Properties.Resources.wait_header;
            this.pbHeader.Location = new System.Drawing.Point(0, 0);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(500, 105);
            this.pbHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHeader.TabIndex = 1;
            this.pbHeader.TabStop = false;
            this.pbHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHeader_MouseDown);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(143)))), ((int)(((byte)(5)))));
            this.btnConfirm.Location = new System.Drawing.Point(250, 248);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(249, 52);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "YES, DO IT";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnBack
            // 
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(77)))), ((int)(((byte)(56)))));
            this.btnBack.Location = new System.Drawing.Point(0, 247);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(249, 52);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "NO, I\'LL CANCEL";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.ControlBox = false;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.pbHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "confirm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "confirm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upPanel_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHeader;
        public System.Windows.Forms.RichTextBox lblConfirm;
        private NoFocusRec btnConfirm;
        private NoFocusRec btnBack;
    }
}