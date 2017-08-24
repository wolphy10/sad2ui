namespace BalayPasilungan
{
    partial class others
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(others));
            this.btnContinue = new System.Windows.Forms.Button();
            this.tabSelection = new System.Windows.Forms.TabControl();
            this.tpMonth = new System.Windows.Forms.TabPage();
            this.panel = new System.Windows.Forms.Panel();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.tpYear = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtYr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSelection.SuspendLayout();
            this.tpMonth.SuspendLayout();
            this.panel.SuspendLayout();
            this.tpYear.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(-1, 171);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(396, 45);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // tabSelection
            // 
            this.tabSelection.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabSelection.Controls.Add(this.tpMonth);
            this.tabSelection.Controls.Add(this.tpYear);
            this.tabSelection.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabSelection.Location = new System.Drawing.Point(-5, -25);
            this.tabSelection.Name = "tabSelection";
            this.tabSelection.SelectedIndex = 0;
            this.tabSelection.Size = new System.Drawing.Size(410, 200);
            this.tabSelection.TabIndex = 11;
            // 
            // tpMonth
            // 
            this.tpMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.tpMonth.Controls.Add(this.panel);
            this.tpMonth.Controls.Add(this.lblMonth);
            this.tpMonth.Location = new System.Drawing.Point(4, 25);
            this.tpMonth.Name = "tpMonth";
            this.tpMonth.Padding = new System.Windows.Forms.Padding(3);
            this.tpMonth.Size = new System.Drawing.Size(402, 171);
            this.tpMonth.TabIndex = 0;
            this.tpMonth.Text = "tabPage1";
            // 
            // panel
            // 
            this.panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel.BackgroundImage")));
            this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel.Controls.Add(this.txtMonth);
            this.panel.Location = new System.Drawing.Point(13, 70);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(371, 58);
            this.panel.TabIndex = 11;
            // 
            // txtMonth
            // 
            this.txtMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.ForeColor = System.Drawing.Color.White;
            this.txtMonth.Location = new System.Drawing.Point(3, 1);
            this.txtMonth.MaxLength = 50;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(360, 29);
            this.txtMonth.TabIndex = 0;
            this.txtMonth.Text = "January or 1";
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.Enter += new System.EventHandler(this.txtMonth_Enter);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.lblMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.lblMonth.Location = new System.Drawing.Point(95, 46);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(207, 21);
            this.lblMonth.TabIndex = 10;
            this.lblMonth.Text = "Type month name or number";
            // 
            // tpYear
            // 
            this.tpYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.tpYear.Controls.Add(this.panel1);
            this.tpYear.Controls.Add(this.label1);
            this.tpYear.Location = new System.Drawing.Point(4, 25);
            this.tpYear.Name = "tpYear";
            this.tpYear.Padding = new System.Windows.Forms.Padding(3);
            this.tpYear.Size = new System.Drawing.Size(402, 171);
            this.tpYear.TabIndex = 1;
            this.tpYear.Text = "tabPage2";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.txtYr);
            this.panel1.Location = new System.Drawing.Point(13, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 58);
            this.panel1.TabIndex = 12;
            // 
            // txtYr
            // 
            this.txtYr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtYr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtYr.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYr.ForeColor = System.Drawing.Color.White;
            this.txtYr.Location = new System.Drawing.Point(3, 1);
            this.txtYr.MaxLength = 50;
            this.txtYr.Name = "txtYr";
            this.txtYr.Size = new System.Drawing.Size(360, 29);
            this.txtYr.TabIndex = 0;
            this.txtYr.Text = "1960 - 2099";
            this.txtYr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYr.Enter += new System.EventHandler(this.txtYr_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(160, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Type year";
            // 
            // others
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(395, 216);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.tabSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "others";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "others";
            this.tabSelection.ResumeLayout(false);
            this.tpMonth.ResumeLayout(false);
            this.tpMonth.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tpYear.ResumeLayout(false);
            this.tpYear.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TabPage tpMonth;
        private System.Windows.Forms.TabPage tpYear;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtYr;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TabControl tabSelection;
    }
}