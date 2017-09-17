namespace BalayPasilungan
{
    partial class waiting
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
            this.components = new System.ComponentModel.Container();
            this.upPanel = new System.Windows.Forms.Panel();
            this.noFocusRec1 = new BalayPasilungan.NoFocusRec();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.upPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // upPanel
            // 
            this.upPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.upPanel.Controls.Add(this.label1);
            this.upPanel.Controls.Add(this.noFocusRec1);
            this.upPanel.Location = new System.Drawing.Point(0, 0);
            this.upPanel.Name = "upPanel";
            this.upPanel.Size = new System.Drawing.Size(411, 83);
            this.upPanel.TabIndex = 18;
            // 
            // noFocusRec1
            // 
            this.noFocusRec1.FlatAppearance.BorderSize = 0;
            this.noFocusRec1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusRec1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.noFocusRec1.ForeColor = System.Drawing.Color.White;
            this.noFocusRec1.Location = new System.Drawing.Point(998, -2);
            this.noFocusRec1.Name = "noFocusRec1";
            this.noFocusRec1.Size = new System.Drawing.Size(23, 23);
            this.noFocusRec1.TabIndex = 11;
            this.noFocusRec1.Text = "X";
            this.noFocusRec1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(157, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "PLEASE WAIT";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMsg.Location = new System.Drawing.Point(12, 92);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(386, 23);
            this.lblMsg.TabIndex = 19;
            this.lblMsg.Text = "label2";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // waiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 128);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.upPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "waiting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.Activated += new System.EventHandler(this.waiting_Activated);
            this.upPanel.ResumeLayout(false);
            this.upPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel upPanel;
        private NoFocusRec noFocusRec1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer timer1;
    }
}