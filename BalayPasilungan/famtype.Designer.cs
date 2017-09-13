namespace BalayPasilungan
{
    partial class famtype
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
            this.btnaddedclass = new BalayPasilungan.NoFocusRec();
            this.btncanceledclass = new BalayPasilungan.NoFocusRec();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxtype = new System.Windows.Forms.ComboBox();
            this.panelNewChild = new System.Windows.Forms.Panel();
            this.lbladdeditprofile = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panelNewChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnaddedclass
            // 
            this.btnaddedclass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnaddedclass.FlatAppearance.BorderSize = 0;
            this.btnaddedclass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddedclass.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnaddedclass.ForeColor = System.Drawing.Color.White;
            this.btnaddedclass.Location = new System.Drawing.Point(276, 213);
            this.btnaddedclass.Name = "btnaddedclass";
            this.btnaddedclass.Size = new System.Drawing.Size(213, 40);
            this.btnaddedclass.TabIndex = 121;
            this.btnaddedclass.Text = "ADD";
            this.btnaddedclass.UseVisualStyleBackColor = false;
            this.btnaddedclass.Click += new System.EventHandler(this.btnaddedclass_Click);
            // 
            // btncanceledclass
            // 
            this.btncanceledclass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btncanceledclass.FlatAppearance.BorderSize = 0;
            this.btncanceledclass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncanceledclass.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btncanceledclass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.btncanceledclass.Location = new System.Drawing.Point(5, 213);
            this.btncanceledclass.Name = "btncanceledclass";
            this.btncanceledclass.Size = new System.Drawing.Size(211, 40);
            this.btncanceledclass.TabIndex = 122;
            this.btncanceledclass.Text = "CANCEL";
            this.btncanceledclass.UseVisualStyleBackColor = false;
            this.btncanceledclass.Click += new System.EventHandler(this.btncanceledclass_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxtype);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 121);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Family Type";
            // 
            // cbxtype
            // 
            this.cbxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxtype.FormattingEnabled = true;
            this.cbxtype.Location = new System.Drawing.Point(13, 42);
            this.cbxtype.Name = "cbxtype";
            this.cbxtype.Size = new System.Drawing.Size(297, 27);
            this.cbxtype.TabIndex = 119;
            // 
            // panelNewChild
            // 
            this.panelNewChild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.panelNewChild.Controls.Add(this.lbladdeditprofile);
            this.panelNewChild.Location = new System.Drawing.Point(-4, -11);
            this.panelNewChild.Name = "panelNewChild";
            this.panelNewChild.Size = new System.Drawing.Size(497, 72);
            this.panelNewChild.TabIndex = 119;
            // 
            // lbladdeditprofile
            // 
            this.lbladdeditprofile.AutoSize = true;
            this.lbladdeditprofile.BackColor = System.Drawing.Color.Transparent;
            this.lbladdeditprofile.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbladdeditprofile.ForeColor = System.Drawing.Color.White;
            this.lbladdeditprofile.Location = new System.Drawing.Point(88, 14);
            this.lbladdeditprofile.Name = "lbladdeditprofile";
            this.lbladdeditprofile.Size = new System.Drawing.Size(295, 47);
            this.lbladdeditprofile.TabIndex = 41;
            this.lbladdeditprofile.Text = "Add Family Type";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label16.Location = new System.Drawing.Point(10, 95);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 18);
            this.label16.TabIndex = 81;
            this.label16.Text = "Year Level";
            // 
            // famtype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 253);
            this.ControlBox = false;
            this.Controls.Add(this.btnaddedclass);
            this.Controls.Add(this.btncanceledclass);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelNewChild);
            this.Name = "famtype";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelNewChild.ResumeLayout(false);
            this.panelNewChild.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NoFocusRec btnaddedclass;
        private NoFocusRec btncanceledclass;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox cbxtype;
        private System.Windows.Forms.Panel panelNewChild;
        private System.Windows.Forms.Label lbladdeditprofile;
        private System.Windows.Forms.Label label16;
    }
}