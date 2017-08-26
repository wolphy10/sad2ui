namespace BalayPasilungan
{
    partial class moneyDonate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(moneyDonate));
            this.tabSelection = new System.Windows.Forms.TabControl();
            this.tabChoice = new System.Windows.Forms.TabPage();
            this.pbCash = new System.Windows.Forms.PictureBox();
            this.btnCash = new System.Windows.Forms.Button();
            this.tabCash = new System.Windows.Forms.TabPage();
            this.btnCashBack = new System.Windows.Forms.Button();
            this.dateCash = new System.Windows.Forms.DateTimePicker();
            this.btnCashAdd = new System.Windows.Forms.Button();
            this.lblDateCash = new System.Windows.Forms.Label();
            this.lblPeso1 = new System.Windows.Forms.Label();
            this.txtCashCent = new System.Windows.Forms.TextBox();
            this.lblCashAmount = new System.Windows.Forms.Label();
            this.lblTIN = new System.Windows.Forms.Label();
            this.txtCashAmount = new System.Windows.Forms.TextBox();
            this.lblDot1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabCheck = new System.Windows.Forms.TabPage();
            this.btnCheck = new System.Windows.Forms.Button();
            this.pbCheck = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblForm = new System.Windows.Forms.Label();
            this.txtOR = new System.Windows.Forms.TextBox();
            this.lblOR = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.txtTIN = new System.Windows.Forms.MaskedTextBox();
            this.tabSelection.SuspendLayout();
            this.tabChoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCash)).BeginInit();
            this.tabCash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabSelection
            // 
            this.tabSelection.Controls.Add(this.tabChoice);
            this.tabSelection.Controls.Add(this.tabCash);
            this.tabSelection.Controls.Add(this.tabCheck);
            this.tabSelection.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabSelection.Location = new System.Drawing.Point(-4, -22);
            this.tabSelection.Name = "tabSelection";
            this.tabSelection.SelectedIndex = 0;
            this.tabSelection.Size = new System.Drawing.Size(566, 402);
            this.tabSelection.TabIndex = 39;
            // 
            // tabChoice
            // 
            this.tabChoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.tabChoice.Controls.Add(this.lblForm);
            this.tabChoice.Controls.Add(this.btnClose);
            this.tabChoice.Controls.Add(this.pbCheck);
            this.tabChoice.Controls.Add(this.pbCash);
            this.tabChoice.Controls.Add(this.btnCheck);
            this.tabChoice.Controls.Add(this.btnCash);
            this.tabChoice.Location = new System.Drawing.Point(4, 22);
            this.tabChoice.Name = "tabChoice";
            this.tabChoice.Padding = new System.Windows.Forms.Padding(3);
            this.tabChoice.Size = new System.Drawing.Size(558, 376);
            this.tabChoice.TabIndex = 0;
            this.tabChoice.Text = "tabPage1";
            this.tabChoice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabChoice_MouseDown);
            // 
            // pbCash
            // 
            this.pbCash.BackColor = System.Drawing.Color.White;
            this.pbCash.Image = ((System.Drawing.Image)(resources.GetObject("pbCash.Image")));
            this.pbCash.Location = new System.Drawing.Point(127, 87);
            this.pbCash.Name = "pbCash";
            this.pbCash.Size = new System.Drawing.Size(55, 55);
            this.pbCash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCash.TabIndex = 16;
            this.pbCash.TabStop = false;
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.White;
            this.btnCash.FlatAppearance.BorderSize = 0;
            this.btnCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("Myriad Pro", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCash.Location = new System.Drawing.Point(0, 32);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(558, 172);
            this.btnCash.TabIndex = 12;
            this.btnCash.Text = "                  CASH";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // tabCash
            // 
            this.tabCash.Controls.Add(this.txtTIN);
            this.tabCash.Controls.Add(this.txtOR);
            this.tabCash.Controls.Add(this.lblOR);
            this.tabCash.Controls.Add(this.pictureBox3);
            this.tabCash.Controls.Add(this.btnCashBack);
            this.tabCash.Controls.Add(this.dateCash);
            this.tabCash.Controls.Add(this.btnCashAdd);
            this.tabCash.Controls.Add(this.lblDateCash);
            this.tabCash.Controls.Add(this.lblPeso1);
            this.tabCash.Controls.Add(this.txtCashCent);
            this.tabCash.Controls.Add(this.lblCashAmount);
            this.tabCash.Controls.Add(this.lblTIN);
            this.tabCash.Controls.Add(this.txtCashAmount);
            this.tabCash.Controls.Add(this.lblDot1);
            this.tabCash.Controls.Add(this.pictureBox1);
            this.tabCash.Location = new System.Drawing.Point(4, 22);
            this.tabCash.Name = "tabCash";
            this.tabCash.Padding = new System.Windows.Forms.Padding(3);
            this.tabCash.Size = new System.Drawing.Size(558, 376);
            this.tabCash.TabIndex = 1;
            this.tabCash.Text = "tabPage2";
            this.tabCash.UseVisualStyleBackColor = true;
            // 
            // btnCashBack
            // 
            this.btnCashBack.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCashBack.FlatAppearance.BorderSize = 0;
            this.btnCashBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashBack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCashBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.btnCashBack.Location = new System.Drawing.Point(0, 336);
            this.btnCashBack.Name = "btnCashBack";
            this.btnCashBack.Size = new System.Drawing.Size(146, 40);
            this.btnCashBack.TabIndex = 7;
            this.btnCashBack.Text = "CANCEL";
            this.btnCashBack.UseVisualStyleBackColor = false;
            this.btnCashBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dateCash
            // 
            this.dateCash.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCash.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.dateCash.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.dateCash.CustomFormat = "MMMM dd, yyyy";
            this.dateCash.Font = new System.Drawing.Font("Segoe UI Semilight", 14F);
            this.dateCash.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCash.Location = new System.Drawing.Point(143, 156);
            this.dateCash.MaxDate = new System.DateTime(2017, 8, 20, 0, 0, 0, 0);
            this.dateCash.Name = "dateCash";
            this.dateCash.Size = new System.Drawing.Size(403, 32);
            this.dateCash.TabIndex = 3;
            this.dateCash.Value = new System.DateTime(2017, 8, 20, 0, 0, 0, 0);
            // 
            // btnCashAdd
            // 
            this.btnCashAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCashAdd.FlatAppearance.BorderSize = 0;
            this.btnCashAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashAdd.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCashAdd.ForeColor = System.Drawing.Color.White;
            this.btnCashAdd.Location = new System.Drawing.Point(143, 336);
            this.btnCashAdd.Name = "btnCashAdd";
            this.btnCashAdd.Size = new System.Drawing.Size(415, 40);
            this.btnCashAdd.TabIndex = 6;
            this.btnCashAdd.Text = "ADD DONATION";
            this.btnCashAdd.UseVisualStyleBackColor = false;
            this.btnCashAdd.Click += new System.EventHandler(this.btnCashAdd_Click);
            // 
            // lblDateCash
            // 
            this.lblDateCash.AutoSize = true;
            this.lblDateCash.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDateCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblDateCash.Location = new System.Drawing.Point(13, 163);
            this.lblDateCash.Name = "lblDateCash";
            this.lblDateCash.Size = new System.Drawing.Size(95, 19);
            this.lblDateCash.TabIndex = 12;
            this.lblDateCash.Text = "Date Donated";
            // 
            // lblPeso1
            // 
            this.lblPeso1.AutoSize = true;
            this.lblPeso1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblPeso1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblPeso1.Location = new System.Drawing.Point(28, 236);
            this.lblPeso1.Name = "lblPeso1";
            this.lblPeso1.Size = new System.Drawing.Size(20, 21);
            this.lblPeso1.TabIndex = 9;
            this.lblPeso1.Text = "₱";
            // 
            // txtCashCent
            // 
            this.txtCashCent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashCent.Font = new System.Drawing.Font("Segoe UI Semibold", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashCent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtCashCent.Location = new System.Drawing.Point(455, 220);
            this.txtCashCent.MaxLength = 2;
            this.txtCashCent.Name = "txtCashCent";
            this.txtCashCent.Size = new System.Drawing.Size(70, 80);
            this.txtCashCent.TabIndex = 5;
            this.txtCashCent.Text = "00";
            this.txtCashCent.Enter += new System.EventHandler(this.txtCashCent_Enter);
            this.txtCashCent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCashDeci_KeyPress);
            this.txtCashCent.Leave += new System.EventHandler(this.txtCashCent_Leave);
            // 
            // lblCashAmount
            // 
            this.lblCashAmount.AutoSize = true;
            this.lblCashAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblCashAmount.Location = new System.Drawing.Point(12, 215);
            this.lblCashAmount.Name = "lblCashAmount";
            this.lblCashAmount.Size = new System.Drawing.Size(59, 19);
            this.lblCashAmount.TabIndex = 6;
            this.lblCashAmount.Text = "Amount";
            // 
            // lblTIN
            // 
            this.lblTIN.AutoSize = true;
            this.lblTIN.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblTIN.Location = new System.Drawing.Point(13, 40);
            this.lblTIN.Name = "lblTIN";
            this.lblTIN.Size = new System.Drawing.Size(32, 19);
            this.lblTIN.TabIndex = 0;
            this.lblTIN.Text = "TIN";
            // 
            // txtCashAmount
            // 
            this.txtCashAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtCashAmount.Location = new System.Drawing.Point(56, 222);
            this.txtCashAmount.MaxLength = 13;
            this.txtCashAmount.Name = "txtCashAmount";
            this.txtCashAmount.Size = new System.Drawing.Size(371, 80);
            this.txtCashAmount.TabIndex = 4;
            this.txtCashAmount.Text = "0,000,000,000";
            this.txtCashAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCashAmount.TextChanged += new System.EventHandler(this.txtCashAmount_TextChanged);
            this.txtCashAmount.Enter += new System.EventHandler(this.txtCashAmount_Enter);
            this.txtCashAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtCashAmount.Leave += new System.EventHandler(this.txtCashAmount_Leave);
            // 
            // lblDot1
            // 
            this.lblDot1.AutoSize = true;
            this.lblDot1.Font = new System.Drawing.Font("Segoe UI Semibold", 45F);
            this.lblDot1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.lblDot1.Location = new System.Drawing.Point(420, 220);
            this.lblDot1.Name = "lblDot1";
            this.lblDot1.Size = new System.Drawing.Size(49, 81);
            this.lblDot1.TabIndex = 8;
            this.lblDot1.Text = ".";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BalayPasilungan.Properties.Resources.line;
            this.pictureBox1.Location = new System.Drawing.Point(12, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(534, 23);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // tabCheck
            // 
            this.tabCheck.Location = new System.Drawing.Point(4, 22);
            this.tabCheck.Name = "tabCheck";
            this.tabCheck.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheck.Size = new System.Drawing.Size(558, 376);
            this.tabCheck.TabIndex = 2;
            this.tabCheck.Text = "tabPage1";
            this.tabCheck.UseVisualStyleBackColor = true;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Myriad Pro", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(0, 204);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(558, 172);
            this.btnCheck.TabIndex = 13;
            this.btnCheck.Text = "                   CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // pbCheck
            // 
            this.pbCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.pbCheck.Image = ((System.Drawing.Image)(resources.GetObject("pbCheck.Image")));
            this.pbCheck.Location = new System.Drawing.Point(127, 259);
            this.pbCheck.Name = "pbCheck";
            this.pbCheck.Size = new System.Drawing.Size(55, 55);
            this.pbCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCheck.TabIndex = 15;
            this.pbCheck.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Source Sans Pro Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(529, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 26);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblForm
            // 
            this.lblForm.AutoSize = true;
            this.lblForm.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.lblForm.Location = new System.Drawing.Point(6, 7);
            this.lblForm.Name = "lblForm";
            this.lblForm.Size = new System.Drawing.Size(96, 19);
            this.lblForm.TabIndex = 18;
            this.lblForm.Text = "Add Donation";
            // 
            // txtOR
            // 
            this.txtOR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOR.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtOR.Location = new System.Drawing.Point(94, 95);
            this.txtOR.MaxLength = 10;
            this.txtOR.Name = "txtOR";
            this.txtOR.Size = new System.Drawing.Size(452, 25);
            this.txtOR.TabIndex = 1;
            this.txtOR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOR
            // 
            this.lblOR.AutoSize = true;
            this.lblOR.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblOR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblOR.Location = new System.Drawing.Point(13, 101);
            this.lblOR.Name = "lblOR";
            this.lblOR.Size = new System.Drawing.Size(55, 19);
            this.lblOR.TabIndex = 14;
            this.lblOR.Text = "OR. No";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::BalayPasilungan.Properties.Resources.line;
            this.pictureBox3.Location = new System.Drawing.Point(12, 107);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(534, 23);
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // txtTIN
            // 
            this.txtTIN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTIN.Font = new System.Drawing.Font("Segoe UI Semilight", 14F);
            this.txtTIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtTIN.Location = new System.Drawing.Point(94, 34);
            this.txtTIN.Mask = "000-000-000-000";
            this.txtTIN.Name = "txtTIN";
            this.txtTIN.PromptChar = ' ';
            this.txtTIN.Size = new System.Drawing.Size(452, 25);
            this.txtTIN.TabIndex = 1;
            this.txtTIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // moneyDonate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(558, 376);
            this.ControlBox = false;
            this.Controls.Add(this.tabSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "moneyDonate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "moneyDonate";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moneyDonate_MouseDown);
            this.tabSelection.ResumeLayout(false);
            this.tabChoice.ResumeLayout(false);
            this.tabChoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCash)).EndInit();
            this.tabCash.ResumeLayout(false);
            this.tabCash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSelection;
        private System.Windows.Forms.TabPage tabChoice;
        private System.Windows.Forms.TabPage tabCash;
        private System.Windows.Forms.TabPage tabCheck;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.PictureBox pbCash;
        private System.Windows.Forms.Label lblTIN;
        private System.Windows.Forms.TextBox txtCashAmount;
        private System.Windows.Forms.Label lblCashAmount;
        private System.Windows.Forms.Label lblPeso1;
        private System.Windows.Forms.TextBox txtCashCent;
        private System.Windows.Forms.Label lblDot1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDateCash;
        private System.Windows.Forms.Button btnCashAdd;
        private System.Windows.Forms.DateTimePicker dateCash;
        private System.Windows.Forms.Button btnCashBack;
        private System.Windows.Forms.PictureBox pbCheck;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblForm;
        private System.Windows.Forms.TextBox txtOR;
        private System.Windows.Forms.Label lblOR;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.MaskedTextBox txtTIN;
    }
}