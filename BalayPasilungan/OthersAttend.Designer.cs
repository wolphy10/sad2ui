namespace BalayPasilungan
{
    partial class OthersAttend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OthersAttend));
            this.countEName = new System.Windows.Forms.Label();
            this.lblEventName = new System.Windows.Forms.Label();
            this.cbARole = new System.Windows.Forms.ComboBox();
            this.lblEType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.upPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnShowAdd = new System.Windows.Forms.Button();
            this.panelEName = new System.Windows.Forms.Panel();
            this.txtFAttendName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLAttendName = new System.Windows.Forms.TextBox();
            this.lbLAttendName = new System.Windows.Forms.Label();
            this.countEName2 = new System.Windows.Forms.Label();
            this.upPanel.SuspendLayout();
            this.panelEName.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // countEName
            // 
            this.countEName.AutoSize = true;
            this.countEName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countEName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.countEName.Location = new System.Drawing.Point(383, 98);
            this.countEName.Name = "countEName";
            this.countEName.Size = new System.Drawing.Size(39, 17);
            this.countEName.TabIndex = 15;
            this.countEName.Text = "0/100";
            this.countEName.Visible = false;
            this.countEName.Click += new System.EventHandler(this.countEName_Click);
            // 
            // lblEventName
            // 
            this.lblEventName.AutoSize = true;
            this.lblEventName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEventName.Location = new System.Drawing.Point(43, 93);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(161, 21);
            this.lblEventName.TabIndex = 14;
            this.lblEventName.Text = "Attendee First Name";
            // 
            // cbARole
            // 
            this.cbARole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.cbARole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbARole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbARole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbARole.ForeColor = System.Drawing.Color.Black;
            this.cbARole.FormattingEnabled = true;
            this.cbARole.Items.AddRange(new object[] {
            "Mass",
            "Party"});
            this.cbARole.Location = new System.Drawing.Point(124, 207);
            this.cbARole.Name = "cbARole";
            this.cbARole.Size = new System.Drawing.Size(237, 29);
            this.cbARole.TabIndex = 16;
            this.cbARole.TabStop = false;
            // 
            // lblEType
            // 
            this.lblEType.AutoSize = true;
            this.lblEType.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEType.Location = new System.Drawing.Point(42, 210);
            this.lblEType.Name = "lblEType";
            this.lblEType.Size = new System.Drawing.Size(87, 21);
            this.lblEType.TabIndex = 17;
            this.lblEType.Text = "Event Role";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label6.Location = new System.Drawing.Point(38, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(292, 47);
            this.label6.TabIndex = 18;
            this.label6.Text = "Other Attendees";
            // 
            // upPanel
            // 
            this.upPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.upPanel.Controls.Add(this.btnClose);
            this.upPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upPanel.Location = new System.Drawing.Point(0, 0);
            this.upPanel.Name = "upPanel";
            this.upPanel.Size = new System.Drawing.Size(853, 24);
            this.upPanel.TabIndex = 19;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1011, -2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.btnCancel.Location = new System.Drawing.Point(10, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(341, 40);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(174)))), ((int)(((byte)(138)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(500, 272);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(344, 40);
            this.btnNext.TabIndex = 20;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "ADD";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnShowAdd
            // 
            this.btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.addsomething;
            this.btnShowAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowAdd.FlatAppearance.BorderSize = 0;
            this.btnShowAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAdd.Location = new System.Drawing.Point(372, 202);
            this.btnShowAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowAdd.Name = "btnShowAdd";
            this.btnShowAdd.Size = new System.Drawing.Size(35, 42);
            this.btnShowAdd.TabIndex = 23;
            this.btnShowAdd.TabStop = false;
            this.btnShowAdd.UseVisualStyleBackColor = true;
            this.btnShowAdd.Click += new System.EventHandler(this.btnShowAdd_Click);
            // 
            // panelEName
            // 
            this.panelEName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelEName.BackgroundImage")));
            this.panelEName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelEName.Controls.Add(this.txtFAttendName);
            this.panelEName.Location = new System.Drawing.Point(46, 121);
            this.panelEName.Name = "panelEName";
            this.panelEName.Size = new System.Drawing.Size(375, 58);
            this.panelEName.TabIndex = 13;
            this.panelEName.TabStop = true;
            // 
            // txtFAttendName
            // 
            this.txtFAttendName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFAttendName.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFAttendName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtFAttendName.Location = new System.Drawing.Point(7, -1);
            this.txtFAttendName.MaxLength = 100;
            this.txtFAttendName.Name = "txtFAttendName";
            this.txtFAttendName.Size = new System.Drawing.Size(368, 28);
            this.txtFAttendName.TabIndex = 1;
            this.txtFAttendName.TabStop = false;
            this.txtFAttendName.Text = "What is the first name of the attendee?";
            this.txtFAttendName.TextChanged += new System.EventHandler(this.txtAttendName_TextChanged);
            this.txtFAttendName.Enter += new System.EventHandler(this.txtEventName_Enter);
            this.txtFAttendName.Leave += new System.EventHandler(this.txtAttendName_Leave);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.txtLAttendName);
            this.panel1.Location = new System.Drawing.Point(460, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 58);
            this.panel1.TabIndex = 14;
            this.panel1.TabStop = true;
            // 
            // txtLAttendName
            // 
            this.txtLAttendName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLAttendName.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLAttendName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtLAttendName.Location = new System.Drawing.Point(7, 0);
            this.txtLAttendName.MaxLength = 100;
            this.txtLAttendName.Name = "txtLAttendName";
            this.txtLAttendName.Size = new System.Drawing.Size(352, 28);
            this.txtLAttendName.TabIndex = 1;
            this.txtLAttendName.TabStop = false;
            this.txtLAttendName.Text = "What is the last name of the attendee?";
            this.txtLAttendName.TextChanged += new System.EventHandler(this.txtLAttendName_TextChanged);
            this.txtLAttendName.Enter += new System.EventHandler(this.txtLAttendName_Enter);
            this.txtLAttendName.Leave += new System.EventHandler(this.txtLAttendName_Leave);
            // 
            // lbLAttendName
            // 
            this.lbLAttendName.AutoSize = true;
            this.lbLAttendName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLAttendName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lbLAttendName.Location = new System.Drawing.Point(457, 93);
            this.lbLAttendName.Name = "lbLAttendName";
            this.lbLAttendName.Size = new System.Drawing.Size(159, 21);
            this.lbLAttendName.TabIndex = 25;
            this.lbLAttendName.Text = "Attendee Last Name";
            // 
            // countEName2
            // 
            this.countEName2.AutoSize = true;
            this.countEName2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countEName2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.countEName2.Location = new System.Drawing.Point(781, 98);
            this.countEName2.Name = "countEName2";
            this.countEName2.Size = new System.Drawing.Size(39, 17);
            this.countEName2.TabIndex = 26;
            this.countEName2.Text = "0/100";
            this.countEName2.Visible = false;
            // 
            // OthersAttend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(853, 323);
            this.ControlBox = false;
            this.Controls.Add(this.countEName2);
            this.Controls.Add(this.lbLAttendName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnShowAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.upPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbARole);
            this.Controls.Add(this.lblEType);
            this.Controls.Add(this.panelEName);
            this.Controls.Add(this.countEName);
            this.Controls.Add(this.lblEventName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OthersAttend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OthersAttend";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OthersAttend_FormClosing);
            this.Load += new System.EventHandler(this.OthersAttend_Load);
            this.upPanel.ResumeLayout(false);
            this.panelEName.ResumeLayout(false);
            this.panelEName.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelEName;
        private System.Windows.Forms.TextBox txtFAttendName;
        private System.Windows.Forms.Label countEName;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.ComboBox cbARole;
        private System.Windows.Forms.Label lblEType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel upPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnShowAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLAttendName;
        private System.Windows.Forms.Label lbLAttendName;
        private System.Windows.Forms.Label countEName2;
    }
}