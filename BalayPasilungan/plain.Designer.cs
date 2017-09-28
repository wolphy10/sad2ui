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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.fifteen = new System.Windows.Forms.TabPage();
            this.lblBRHead1 = new System.Windows.Forms.Label();
            this.txtRequestBy = new System.Windows.Forms.TextBox();
            this.panelRequestBy = new System.Windows.Forms.PictureBox();
            this.countEVenue = new System.Windows.Forms.Label();
            this.lblEventName = new System.Windows.Forms.Label();
            this.countEName = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnBRCancel = new BalayPasilungan.NoFocusRec();
            this.btnBRNext = new BalayPasilungan.NoFocusRec();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.pictureBox29 = new System.Windows.Forms.PictureBox();
            this.panelEName = new System.Windows.Forms.PictureBox();
            this.lblEVenue = new System.Windows.Forms.Label();
            this.txtEventDes = new System.Windows.Forms.RichTextBox();
            this.lblEDes = new System.Windows.Forms.Label();
            this.countEDes = new System.Windows.Forms.Label();
            this.txtEvType = new System.Windows.Forms.TextBox();
            this.btnShowAdd = new System.Windows.Forms.Button();
            this.cbEType = new System.Windows.Forms.ComboBox();
            this.lblEType = new System.Windows.Forms.Label();
            this.panelEvType = new System.Windows.Forms.PictureBox();
            this.btnAddType = new BalayPasilungan.NoFocusRec();
            this.lblRequestBy = new System.Windows.Forms.Label();
            this.countRequestBy = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.fifteen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRequestBy)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEvType)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.fifteen);
            this.tabControl.Location = new System.Drawing.Point(4, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(942, 551);
            this.tabControl.TabIndex = 19;
            // 
            // fifteen
            // 
            this.fifteen.BackColor = System.Drawing.Color.White;
            this.fifteen.Controls.Add(this.lblRequestBy);
            this.fifteen.Controls.Add(this.countRequestBy);
            this.fifteen.Controls.Add(this.btnAddType);
            this.fifteen.Controls.Add(this.txtEvType);
            this.fifteen.Controls.Add(this.panelEvType);
            this.fifteen.Controls.Add(this.lblEType);
            this.fifteen.Controls.Add(this.cbEType);
            this.fifteen.Controls.Add(this.btnShowAdd);
            this.fifteen.Controls.Add(this.lblEDes);
            this.fifteen.Controls.Add(this.countEDes);
            this.fifteen.Controls.Add(this.txtEventDes);
            this.fifteen.Controls.Add(this.lblEVenue);
            this.fifteen.Controls.Add(this.lblBRHead1);
            this.fifteen.Controls.Add(this.txtRequestBy);
            this.fifteen.Controls.Add(this.panelRequestBy);
            this.fifteen.Controls.Add(this.countEVenue);
            this.fifteen.Controls.Add(this.lblEventName);
            this.fifteen.Controls.Add(this.countEName);
            this.fifteen.Controls.Add(this.panel8);
            this.fifteen.Controls.Add(this.txtEventName);
            this.fifteen.Controls.Add(this.pictureBox29);
            this.fifteen.Controls.Add(this.panelEName);
            this.fifteen.Location = new System.Drawing.Point(4, 22);
            this.fifteen.Name = "fifteen";
            this.fifteen.Padding = new System.Windows.Forms.Padding(3);
            this.fifteen.Size = new System.Drawing.Size(934, 525);
            this.fifteen.TabIndex = 15;
            this.fifteen.Text = "15";
            // 
            // lblBRHead1
            // 
            this.lblBRHead1.AutoSize = true;
            this.lblBRHead1.BackColor = System.Drawing.Color.Transparent;
            this.lblBRHead1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblBRHead1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(196)))), ((int)(((byte)(158)))));
            this.lblBRHead1.Location = new System.Drawing.Point(82, 248);
            this.lblBRHead1.Name = "lblBRHead1";
            this.lblBRHead1.Size = new System.Drawing.Size(49, 13);
            this.lblBRHead1.TabIndex = 329;
            this.lblBRHead1.Text = "DETAILS";
            this.lblBRHead1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRequestBy
            // 
            this.txtRequestBy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRequestBy.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.txtRequestBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtRequestBy.Location = new System.Drawing.Point(362, 155);
            this.txtRequestBy.MaxLength = 100;
            this.txtRequestBy.Name = "txtRequestBy";
            this.txtRequestBy.Size = new System.Drawing.Size(498, 22);
            this.txtRequestBy.TabIndex = 0;
            this.txtRequestBy.Text = "Who requested the event?";
            // 
            // panelRequestBy
            // 
            this.panelRequestBy.Image = global::BalayPasilungan.Properties.Resources.line;
            this.panelRequestBy.Location = new System.Drawing.Point(358, 166);
            this.panelRequestBy.Name = "panelRequestBy";
            this.panelRequestBy.Size = new System.Drawing.Size(506, 33);
            this.panelRequestBy.TabIndex = 325;
            this.panelRequestBy.TabStop = false;
            // 
            // countEVenue
            // 
            this.countEVenue.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.countEVenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.countEVenue.Location = new System.Drawing.Point(283, 177);
            this.countEVenue.Name = "countEVenue";
            this.countEVenue.Size = new System.Drawing.Size(53, 17);
            this.countEVenue.TabIndex = 320;
            this.countEVenue.Text = "0/100";
            this.countEVenue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.countEVenue.Visible = false;
            // 
            // lblEventName
            // 
            this.lblEventName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEventName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEventName.Location = new System.Drawing.Point(181, 85);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(155, 15);
            this.lblEventName.TabIndex = 315;
            this.lblEventName.Text = "EVENT NAME";
            this.lblEventName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // countEName
            // 
            this.countEName.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.countEName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.countEName.Location = new System.Drawing.Point(283, 100);
            this.countEName.Name = "countEName";
            this.countEName.Size = new System.Drawing.Size(53, 17);
            this.countEName.TabIndex = 312;
            this.countEName.Text = "0/100";
            this.countEName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.countEName.Visible = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.panel8.Controls.Add(this.btnBRCancel);
            this.panel8.Controls.Add(this.btnBRNext);
            this.panel8.Location = new System.Drawing.Point(0, 466);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(934, 59);
            this.panel8.TabIndex = 300;
            // 
            // btnBRCancel
            // 
            this.btnBRCancel.BackColor = System.Drawing.Color.White;
            this.btnBRCancel.FlatAppearance.BorderSize = 0;
            this.btnBRCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBRCancel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnBRCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnBRCancel.Location = new System.Drawing.Point(640, 24);
            this.btnBRCancel.Name = "btnBRCancel";
            this.btnBRCancel.Size = new System.Drawing.Size(97, 26);
            this.btnBRCancel.TabIndex = 151;
            this.btnBRCancel.Text = "CANCEL";
            this.btnBRCancel.UseVisualStyleBackColor = false;
            // 
            // btnBRNext
            // 
            this.btnBRNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnBRNext.FlatAppearance.BorderSize = 0;
            this.btnBRNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBRNext.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnBRNext.ForeColor = System.Drawing.Color.White;
            this.btnBRNext.Location = new System.Drawing.Point(763, 24);
            this.btnBRNext.Name = "btnBRNext";
            this.btnBRNext.Size = new System.Drawing.Size(97, 26);
            this.btnBRNext.TabIndex = 150;
            this.btnBRNext.Text = "NEXT";
            this.btnBRNext.UseVisualStyleBackColor = false;
            // 
            // txtEventName
            // 
            this.txtEventName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEventName.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.txtEventName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtEventName.Location = new System.Drawing.Point(362, 80);
            this.txtEventName.MaxLength = 100;
            this.txtEventName.Name = "txtEventName";
            this.txtEventName.Size = new System.Drawing.Size(498, 22);
            this.txtEventName.TabIndex = 301;
            this.txtEventName.Text = "What is the name of the event?";
            // 
            // pictureBox29
            // 
            this.pictureBox29.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            this.pictureBox29.Location = new System.Drawing.Point(30, 24);
            this.pictureBox29.Name = "pictureBox29";
            this.pictureBox29.Size = new System.Drawing.Size(868, 33);
            this.pictureBox29.TabIndex = 299;
            this.pictureBox29.TabStop = false;
            // 
            // panelEName
            // 
            this.panelEName.Image = global::BalayPasilungan.Properties.Resources.line;
            this.panelEName.Location = new System.Drawing.Point(358, 88);
            this.panelEName.Name = "panelEName";
            this.panelEName.Size = new System.Drawing.Size(506, 33);
            this.panelEName.TabIndex = 306;
            this.panelEName.TabStop = false;
            // 
            // lblEVenue
            // 
            this.lblEVenue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEVenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEVenue.Location = new System.Drawing.Point(181, 162);
            this.lblEVenue.Name = "lblEVenue";
            this.lblEVenue.Size = new System.Drawing.Size(155, 15);
            this.lblEVenue.TabIndex = 330;
            this.lblEVenue.Text = "REQUESTED BY";
            this.lblEVenue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEventDes
            // 
            this.txtEventDes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtEventDes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEventDes.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.txtEventDes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtEventDes.Location = new System.Drawing.Point(358, 248);
            this.txtEventDes.Margin = new System.Windows.Forms.Padding(10);
            this.txtEventDes.MaxLength = 250;
            this.txtEventDes.Name = "txtEventDes";
            this.txtEventDes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEventDes.Size = new System.Drawing.Size(506, 55);
            this.txtEventDes.TabIndex = 331;
            this.txtEventDes.Text = "Describe the event.";
            // 
            // lblEDes
            // 
            this.lblEDes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEDes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEDes.Location = new System.Drawing.Point(181, 248);
            this.lblEDes.Name = "lblEDes";
            this.lblEDes.Size = new System.Drawing.Size(155, 15);
            this.lblEDes.TabIndex = 333;
            this.lblEDes.Text = "EVENT DESCRIPTION";
            this.lblEDes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // countEDes
            // 
            this.countEDes.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.countEDes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.countEDes.Location = new System.Drawing.Point(283, 263);
            this.countEDes.Name = "countEDes";
            this.countEDes.Size = new System.Drawing.Size(53, 17);
            this.countEDes.TabIndex = 332;
            this.countEDes.Text = "0/250";
            this.countEDes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.countEDes.Visible = false;
            // 
            // txtEvType
            // 
            this.txtEvType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEvType.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.txtEvType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtEvType.Location = new System.Drawing.Point(358, 358);
            this.txtEvType.MaxLength = 100;
            this.txtEvType.Name = "txtEvType";
            this.txtEvType.Size = new System.Drawing.Size(498, 22);
            this.txtEvType.TabIndex = 334;
            this.txtEvType.Text = "Event Type...";
            this.txtEvType.Visible = false;
            // 
            // btnShowAdd
            // 
            this.btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.addsomething;
            this.btnShowAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowAdd.FlatAppearance.BorderSize = 0;
            this.btnShowAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAdd.Location = new System.Drawing.Point(562, 316);
            this.btnShowAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowAdd.Name = "btnShowAdd";
            this.btnShowAdd.Size = new System.Drawing.Size(25, 25);
            this.btnShowAdd.TabIndex = 337;
            this.btnShowAdd.UseVisualStyleBackColor = true;
            // 
            // cbEType
            // 
            this.cbEType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEType.Font = new System.Drawing.Font("Segoe UI Semilight", 10F);
            this.cbEType.FormattingEnabled = true;
            this.cbEType.Items.AddRange(new object[] {
            "Mass",
            "Party"});
            this.cbEType.Location = new System.Drawing.Point(354, 316);
            this.cbEType.Name = "cbEType";
            this.cbEType.Size = new System.Drawing.Size(193, 25);
            this.cbEType.TabIndex = 339;
            // 
            // lblEType
            // 
            this.lblEType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblEType.Location = new System.Drawing.Point(177, 321);
            this.lblEType.Name = "lblEType";
            this.lblEType.Size = new System.Drawing.Size(155, 15);
            this.lblEType.TabIndex = 341;
            this.lblEType.Text = "EVENT TYPE";
            this.lblEType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEvType
            // 
            this.panelEvType.Image = global::BalayPasilungan.Properties.Resources.line;
            this.panelEvType.Location = new System.Drawing.Point(354, 369);
            this.panelEvType.Name = "panelEvType";
            this.panelEvType.Size = new System.Drawing.Size(506, 33);
            this.panelEvType.TabIndex = 342;
            this.panelEvType.TabStop = false;
            // 
            // btnAddType
            // 
            this.btnAddType.BackColor = System.Drawing.Color.White;
            this.btnAddType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnAddType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btnAddType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btnAddType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddType.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnAddType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(141)))));
            this.btnAddType.Location = new System.Drawing.Point(655, 408);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(205, 29);
            this.btnAddType.TabIndex = 343;
            this.btnAddType.Text = "ADD TYPE";
            this.btnAddType.UseVisualStyleBackColor = false;
            this.btnAddType.Visible = false;
            // 
            // lblRequestBy
            // 
            this.lblRequestBy.AutoSize = true;
            this.lblRequestBy.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblRequestBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblRequestBy.Location = new System.Drawing.Point(81, 225);
            this.lblRequestBy.Name = "lblRequestBy";
            this.lblRequestBy.Size = new System.Drawing.Size(111, 21);
            this.lblRequestBy.TabIndex = 344;
            this.lblRequestBy.Text = "Requested by";
            // 
            // countRequestBy
            // 
            this.countRequestBy.AutoSize = true;
            this.countRequestBy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countRequestBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.countRequestBy.Location = new System.Drawing.Point(813, 229);
            this.countRequestBy.Name = "countRequestBy";
            this.countRequestBy.Size = new System.Drawing.Size(39, 17);
            this.countRequestBy.TabIndex = 346;
            this.countRequestBy.Text = "0/100";
            this.countRequestBy.Visible = false;
            // 
            // plain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 552);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "plain";
            this.Text = "plain";
            this.Load += new System.EventHandler(this.plain_Load);
            this.tabControl.ResumeLayout(false);
            this.fifteen.ResumeLayout(false);
            this.fifteen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRequestBy)).EndInit();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEvType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage fifteen;
        private System.Windows.Forms.Panel panel8;
        private NoFocusRec btnBRCancel;
        private NoFocusRec btnBRNext;
        private System.Windows.Forms.TextBox txtEventName;
        private System.Windows.Forms.PictureBox pictureBox29;
        private System.Windows.Forms.PictureBox panelEName;
        private System.Windows.Forms.Label countEVenue;
        private System.Windows.Forms.TextBox txtRequestBy;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.Label countEName;
        private System.Windows.Forms.PictureBox panelRequestBy;
        private System.Windows.Forms.Label lblBRHead1;
        private System.Windows.Forms.Label lblEVenue;
        private System.Windows.Forms.RichTextBox txtEventDes;
        private System.Windows.Forms.Label lblEDes;
        private System.Windows.Forms.Label countEDes;
        private System.Windows.Forms.TextBox txtEvType;
        private System.Windows.Forms.Button btnShowAdd;
        private System.Windows.Forms.Label lblEType;
        private System.Windows.Forms.ComboBox cbEType;
        private System.Windows.Forms.PictureBox panelEvType;
        private NoFocusRec btnAddType;
        private System.Windows.Forms.Label lblRequestBy;
        private System.Windows.Forms.Label countRequestBy;
    }
}