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
            this.tabCash = new System.Windows.Forms.TabPage();
            this.tabCheck = new System.Windows.Forms.TabPage();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.pbCheck = new System.Windows.Forms.PictureBox();
            this.panelGreen = new System.Windows.Forms.Panel();
            this.pbCash = new System.Windows.Forms.PictureBox();
            this.tabSelection.SuspendLayout();
            this.tabChoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).BeginInit();
            this.panelGreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCash)).BeginInit();
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
            this.tabChoice.Controls.Add(this.pbCash);
            this.tabChoice.Controls.Add(this.btnCash);
            this.tabChoice.Controls.Add(this.panelGreen);
            this.tabChoice.Location = new System.Drawing.Point(4, 22);
            this.tabChoice.Name = "tabChoice";
            this.tabChoice.Padding = new System.Windows.Forms.Padding(3);
            this.tabChoice.Size = new System.Drawing.Size(558, 376);
            this.tabChoice.TabIndex = 0;
            this.tabChoice.Text = "tabPage1";
            this.tabChoice.UseVisualStyleBackColor = true;
            // 
            // tabCash
            // 
            this.tabCash.Location = new System.Drawing.Point(4, 22);
            this.tabCash.Name = "tabCash";
            this.tabCash.Padding = new System.Windows.Forms.Padding(3);
            this.tabCash.Size = new System.Drawing.Size(558, 376);
            this.tabCash.TabIndex = 1;
            this.tabCash.Text = "tabPage2";
            this.tabCash.UseVisualStyleBackColor = true;
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
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatAppearance.BorderSize = 0;
            this.btnCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("Myriad Pro Light", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCash.Location = new System.Drawing.Point(0, 3);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(558, 185);
            this.btnCash.TabIndex = 12;
            this.btnCash.Text = "                   CASH";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.Transparent;
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Myriad Pro Light", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(0, 1);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(558, 185);
            this.btnCheck.TabIndex = 13;
            this.btnCheck.Text = "                   CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // pbCheck
            // 
            this.pbCheck.Image = ((System.Drawing.Image)(resources.GetObject("pbCheck.Image")));
            this.pbCheck.Location = new System.Drawing.Point(127, 64);
            this.pbCheck.Name = "pbCheck";
            this.pbCheck.Size = new System.Drawing.Size(55, 55);
            this.pbCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCheck.TabIndex = 15;
            this.pbCheck.TabStop = false;
            // 
            // panelGreen
            // 
            this.panelGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.panelGreen.Controls.Add(this.pbCheck);
            this.panelGreen.Controls.Add(this.btnCheck);
            this.panelGreen.Location = new System.Drawing.Point(0, 190);
            this.panelGreen.Name = "panelGreen";
            this.panelGreen.Size = new System.Drawing.Size(558, 186);
            this.panelGreen.TabIndex = 16;
            // 
            // pbCash
            // 
            this.pbCash.Image = ((System.Drawing.Image)(resources.GetObject("pbCash.Image")));
            this.pbCash.Location = new System.Drawing.Point(127, 66);
            this.pbCash.Name = "pbCash";
            this.pbCash.Size = new System.Drawing.Size(55, 55);
            this.pbCash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCash.TabIndex = 16;
            this.pbCash.TabStop = false;
            // 
            // moneyDonate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).EndInit();
            this.panelGreen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSelection;
        private System.Windows.Forms.TabPage tabChoice;
        private System.Windows.Forms.TabPage tabCash;
        private System.Windows.Forms.TabPage tabCheck;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.PictureBox pbCheck;
        private System.Windows.Forms.Panel panelGreen;
        private System.Windows.Forms.PictureBox pbCash;
    }
}