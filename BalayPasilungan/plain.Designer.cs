namespace BalayPasilungan
{
    partial class plain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.fifteen = new System.Windows.Forms.TabPage();
            this.rviewcondition = new System.Windows.Forms.RichTextBox();
            this.rviewall = new System.Windows.Forms.RichTextBox();
            this.dtghealth = new System.Windows.Forms.DataGridView();
            this.label74 = new System.Windows.Forms.Label();
            this.lblvblood = new System.Windows.Forms.Label();
            this.lblvweight = new System.Windows.Forms.Label();
            this.lblvheight = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btngotohealth = new BalayPasilungan.NoFocusRec();
            this.btngotocheckup = new BalayPasilungan.NoFocusRec();
            this.btnedithealth = new BalayPasilungan.NoFocusRec();
            this.addhrecord = new BalayPasilungan.NoFocusRec();
            this.noFocusRec3 = new BalayPasilungan.NoFocusRec();
            this.tabControl.SuspendLayout();
            this.fifteen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtghealth)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.fifteen);
            this.tabControl.Location = new System.Drawing.Point(4, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(942, 547);
            this.tabControl.TabIndex = 19;
            // 
            // fifteen
            // 
            this.fifteen.Controls.Add(this.btngotohealth);
            this.fifteen.Controls.Add(this.rviewcondition);
            this.fifteen.Controls.Add(this.rviewall);
            this.fifteen.Controls.Add(this.lblvblood);
            this.fifteen.Controls.Add(this.label23);
            this.fifteen.Controls.Add(this.lblvweight);
            this.fifteen.Controls.Add(this.btngotocheckup);
            this.fifteen.Controls.Add(this.label25);
            this.fifteen.Controls.Add(this.lblvheight);
            this.fifteen.Controls.Add(this.btnedithealth);
            this.fifteen.Controls.Add(this.label26);
            this.fifteen.Controls.Add(this.addhrecord);
            this.fifteen.Controls.Add(this.label74);
            this.fifteen.Controls.Add(this.noFocusRec3);
            this.fifteen.Controls.Add(this.dtghealth);
            this.fifteen.Location = new System.Drawing.Point(4, 22);
            this.fifteen.Name = "fifteen";
            this.fifteen.Padding = new System.Windows.Forms.Padding(3);
            this.fifteen.Size = new System.Drawing.Size(934, 521);
            this.fifteen.TabIndex = 15;
            this.fifteen.Text = "15";
            this.fifteen.UseVisualStyleBackColor = true;
            // 
            // rviewcondition
            // 
            this.rviewcondition.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rviewcondition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rviewcondition.Location = new System.Drawing.Point(652, 75);
            this.rviewcondition.Name = "rviewcondition";
            this.rviewcondition.ReadOnly = true;
            this.rviewcondition.Size = new System.Drawing.Size(253, 113);
            this.rviewcondition.TabIndex = 89;
            this.rviewcondition.Text = "";
            // 
            // rviewall
            // 
            this.rviewall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rviewall.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rviewall.Location = new System.Drawing.Point(414, 75);
            this.rviewall.Name = "rviewall";
            this.rviewall.ReadOnly = true;
            this.rviewall.Size = new System.Drawing.Size(219, 113);
            this.rviewall.TabIndex = 89;
            this.rviewall.Text = "";
            // 
            // dtghealth
            // 
            this.dtghealth.AllowUserToAddRows = false;
            this.dtghealth.AllowUserToDeleteRows = false;
            this.dtghealth.AllowUserToResizeColumns = false;
            this.dtghealth.AllowUserToResizeRows = false;
            this.dtghealth.BackgroundColor = System.Drawing.Color.White;
            this.dtghealth.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtghealth.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(187)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtghealth.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtghealth.ColumnHeadersHeight = 50;
            this.dtghealth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(162)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(230)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtghealth.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtghealth.EnableHeadersVisualStyles = false;
            this.dtghealth.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtghealth.Location = new System.Drawing.Point(37, 212);
            this.dtghealth.MultiSelect = false;
            this.dtghealth.Name = "dtghealth";
            this.dtghealth.ReadOnly = true;
            this.dtghealth.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtghealth.RowHeadersVisible = false;
            this.dtghealth.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtghealth.RowTemplate.Height = 50;
            this.dtghealth.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtghealth.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dtghealth.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtghealth.ShowCellErrors = false;
            this.dtghealth.ShowCellToolTips = false;
            this.dtghealth.ShowEditingIcon = false;
            this.dtghealth.ShowRowErrors = false;
            this.dtghealth.Size = new System.Drawing.Size(868, 226);
            this.dtghealth.TabIndex = 105;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Segoe UI Semilight", 21.75F);
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.label74.Location = new System.Drawing.Point(30, 16);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(206, 40);
            this.label74.TabIndex = 101;
            this.label74.Text = "Health Records";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblvblood
            // 
            this.lblvblood.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblvblood.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(73)))), ((int)(((byte)(60)))));
            this.lblvblood.Location = new System.Drawing.Point(289, 111);
            this.lblvblood.Name = "lblvblood";
            this.lblvblood.Size = new System.Drawing.Size(58, 28);
            this.lblvblood.TabIndex = 10;
            this.lblvblood.Text = "##";
            this.lblvblood.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblvweight
            // 
            this.lblvweight.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblvweight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(157)))), ((int)(((byte)(84)))));
            this.lblvweight.Location = new System.Drawing.Point(186, 111);
            this.lblvweight.Name = "lblvweight";
            this.lblvweight.Size = new System.Drawing.Size(58, 28);
            this.lblvweight.TabIndex = 8;
            this.lblvweight.Text = "##";
            this.lblvweight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblvweight.Click += new System.EventHandler(this.lblvweight_Click);
            // 
            // lblvheight
            // 
            this.lblvheight.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblvheight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(114)))), ((int)(((byte)(170)))));
            this.lblvheight.Location = new System.Drawing.Point(80, 111);
            this.lblvheight.Name = "lblvheight";
            this.lblvheight.Size = new System.Drawing.Size(58, 28);
            this.lblvheight.TabIndex = 7;
            this.lblvheight.Text = "##";
            this.lblvheight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(73)))), ((int)(((byte)(60)))));
            this.label23.Location = new System.Drawing.Point(283, 145);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "BLOOD TYPE";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(157)))), ((int)(((byte)(84)))));
            this.label25.Location = new System.Drawing.Point(191, 145);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 13);
            this.label25.TabIndex = 2;
            this.label25.Text = "WEIGHT";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(114)))), ((int)(((byte)(170)))));
            this.label26.Location = new System.Drawing.Point(87, 145);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "HEIGHT";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btngotohealth
            // 
            this.btngotohealth.BackColor = System.Drawing.Color.Transparent;
            this.btngotohealth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btngotohealth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btngotohealth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btngotohealth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngotohealth.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btngotohealth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btngotohealth.Location = new System.Drawing.Point(286, 463);
            this.btngotohealth.Name = "btngotohealth";
            this.btngotohealth.Size = new System.Drawing.Size(166, 29);
            this.btngotohealth.TabIndex = 120;
            this.btngotohealth.Text = "???";
            this.btngotohealth.UseVisualStyleBackColor = false;
            // 
            // btngotocheckup
            // 
            this.btngotocheckup.BackColor = System.Drawing.Color.Transparent;
            this.btngotocheckup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btngotocheckup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btngotocheckup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btngotocheckup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngotocheckup.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btngotocheckup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btngotocheckup.Location = new System.Drawing.Point(736, 24);
            this.btngotocheckup.Name = "btngotocheckup";
            this.btngotocheckup.Size = new System.Drawing.Size(166, 29);
            this.btngotocheckup.TabIndex = 118;
            this.btngotocheckup.Text = "CHECKUP RECORDS";
            this.btngotocheckup.UseVisualStyleBackColor = false;
            // 
            // btnedithealth
            // 
            this.btnedithealth.BackColor = System.Drawing.Color.Transparent;
            this.btnedithealth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnedithealth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btnedithealth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btnedithealth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnedithealth.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnedithealth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnedithealth.Location = new System.Drawing.Point(564, 24);
            this.btnedithealth.Name = "btnedithealth";
            this.btnedithealth.Size = new System.Drawing.Size(166, 29);
            this.btnedithealth.TabIndex = 117;
            this.btnedithealth.Text = "EDIT HEALTH INFO";
            this.btnedithealth.UseVisualStyleBackColor = false;
            this.btnedithealth.Click += new System.EventHandler(this.btnedithealth_Click);
            // 
            // addhrecord
            // 
            this.addhrecord.BackColor = System.Drawing.Color.Transparent;
            this.addhrecord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.addhrecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.addhrecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.addhrecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addhrecord.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.addhrecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.addhrecord.Location = new System.Drawing.Point(392, 24);
            this.addhrecord.Name = "addhrecord";
            this.addhrecord.Size = new System.Drawing.Size(166, 29);
            this.addhrecord.TabIndex = 108;
            this.addhrecord.Text = "ADD HEALTH RECORD";
            this.addhrecord.UseVisualStyleBackColor = false;
            // 
            // noFocusRec3
            // 
            this.noFocusRec3.BackColor = System.Drawing.Color.Transparent;
            this.noFocusRec3.Enabled = false;
            this.noFocusRec3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.noFocusRec3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.noFocusRec3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.noFocusRec3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusRec3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.noFocusRec3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.noFocusRec3.Location = new System.Drawing.Point(37, 75);
            this.noFocusRec3.Name = "noFocusRec3";
            this.noFocusRec3.Size = new System.Drawing.Size(356, 113);
            this.noFocusRec3.TabIndex = 119;
            this.noFocusRec3.TabStop = false;
            this.noFocusRec3.UseVisualStyleBackColor = false;
            // 
            // plain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "plain";
            this.Text = "plain";
            this.tabControl.ResumeLayout(false);
            this.fifteen.ResumeLayout(false);
            this.fifteen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtghealth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage fifteen;
        private System.Windows.Forms.RichTextBox rviewcondition;
        private System.Windows.Forms.RichTextBox rviewall;
        private NoFocusRec addhrecord;
        private System.Windows.Forms.DataGridView dtghealth;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label lblvweight;
        private System.Windows.Forms.Label lblvheight;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private NoFocusRec btngotocheckup;
        private NoFocusRec btnedithealth;
        private System.Windows.Forms.Label lblvblood;
        private NoFocusRec noFocusRec3;
        private NoFocusRec btngotohealth;
    }
}