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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkfamcur = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxtype = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panelNewChild = new System.Windows.Forms.Panel();
            this.lbladdeditprofile = new System.Windows.Forms.Label();
            this.numfam = new System.Windows.Forms.NumericUpDown();
            this.btnaddedclass = new BalayPasilungan.NoFocusRec();
            this.btncanceledclass = new BalayPasilungan.NoFocusRec();
            this.groupBox2.SuspendLayout();
            this.panelNewChild.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numfam)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numfam);
            this.groupBox2.Controls.Add(this.checkfamcur);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbxtype);
            this.groupBox2.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 165);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            // 
            // checkfamcur
            // 
            this.checkfamcur.AutoSize = true;
            this.checkfamcur.Location = new System.Drawing.Point(204, 136);
            this.checkfamcur.Name = "checkfamcur";
            this.checkfamcur.Size = new System.Drawing.Size(267, 23);
            this.checkfamcur.TabIndex = 280;
            this.checkfamcur.Text = "Is the family the case study\'s current family?";
            this.checkfamcur.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label1.Location = new System.Drawing.Point(7, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 18);
            this.label1.TabIndex = 279;
            this.label1.Text = "Family Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 18);
            this.label3.TabIndex = 125;
            this.label3.Text = "Family Type";
            // 
            // cbxtype
            // 
            this.cbxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxtype.FormattingEnabled = true;
            this.cbxtype.Items.AddRange(new object[] {
            "Nuclear",
            "Single Parent",
            "Extended",
            "Childless",
            "Step",
            "Grandparent"});
            this.cbxtype.Location = new System.Drawing.Point(174, 26);
            this.cbxtype.Name = "cbxtype";
            this.cbxtype.Size = new System.Drawing.Size(297, 27);
            this.cbxtype.TabIndex = 119;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label16.Location = new System.Drawing.Point(22, 262);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 18);
            this.label16.TabIndex = 81;
            this.label16.Text = "Year Level";
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
            this.lbladdeditprofile.Size = new System.Drawing.Size(282, 47);
            this.lbladdeditprofile.TabIndex = 41;
            this.lbladdeditprofile.Text = "Add Family Info";
            // 
            // numfam
            // 
            this.numfam.Location = new System.Drawing.Point(351, 87);
            this.numfam.Name = "numfam";
            this.numfam.Size = new System.Drawing.Size(120, 27);
            this.numfam.TabIndex = 281;
            // 
            // btnaddedclass
            // 
            this.btnaddedclass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnaddedclass.FlatAppearance.BorderSize = 0;
            this.btnaddedclass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddedclass.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnaddedclass.ForeColor = System.Drawing.Color.White;
            this.btnaddedclass.Location = new System.Drawing.Point(272, 283);
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
            this.btncanceledclass.Location = new System.Drawing.Point(1, 283);
            this.btncanceledclass.Name = "btncanceledclass";
            this.btncanceledclass.Size = new System.Drawing.Size(211, 40);
            this.btncanceledclass.TabIndex = 122;
            this.btncanceledclass.Text = "CANCEL";
            this.btncanceledclass.UseVisualStyleBackColor = false;
            this.btncanceledclass.Click += new System.EventHandler(this.btncanceledclass_Click);
            // 
            // famtype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 322);
            this.ControlBox = false;
            this.Controls.Add(this.btnaddedclass);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btncanceledclass);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelNewChild);
            this.Name = "famtype";
            this.Load += new System.EventHandler(this.famtype_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelNewChild.ResumeLayout(false);
            this.panelNewChild.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numfam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NoFocusRec btnaddedclass;
        private NoFocusRec btncanceledclass;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox cbxtype;
        private System.Windows.Forms.Panel panelNewChild;
        private System.Windows.Forms.Label lbladdeditprofile;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkfamcur;
        private System.Windows.Forms.NumericUpDown numfam;
    }
}