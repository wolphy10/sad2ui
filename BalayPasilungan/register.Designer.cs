﻿namespace BalayPasilungan
{
    partial class register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(register));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnFill = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.panelEName = new System.Windows.Forms.Panel();
            this.lblFName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.lblLName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.countFName = new System.Windows.Forms.Label();
            this.countLName = new System.Windows.Forms.Label();
            this.countUser = new System.Windows.Forms.Label();
            this.countPass = new System.Windows.Forms.Label();
            this.panelEName.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))));
            this.btnCancel.Location = new System.Drawing.Point(0, 329);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(309, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(308, 329);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(318, 33);
            this.btnRegister.TabIndex = 1;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Semilight", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.lblWelcome.Location = new System.Drawing.Point(12, 16);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(226, 62);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Welcome.";
            // 
            // btnFill
            // 
            this.btnFill.AutoSize = true;
            this.btnFill.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnFill.Location = new System.Drawing.Point(26, 72);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(154, 21);
            this.btnFill.TabIndex = 3;
            this.btnFill.Text = "Please fill all details.";
            // 
            // txtFName
            // 
            this.txtFName.BackColor = System.Drawing.Color.White;
            this.txtFName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFName.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtFName.Location = new System.Drawing.Point(4, 1);
            this.txtFName.MaxLength = 50;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(265, 25);
            this.txtFName.TabIndex = 0;
            this.txtFName.TextChanged += new System.EventHandler(this.txtFName_TextChanged);
            this.txtFName.Enter += new System.EventHandler(this.txtFName_Enter);
            this.txtFName.Leave += new System.EventHandler(this.txtFName_Leave);
            // 
            // panelEName
            // 
            this.panelEName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelEName.BackgroundImage")));
            this.panelEName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelEName.Controls.Add(this.txtFName);
            this.panelEName.Location = new System.Drawing.Point(30, 126);
            this.panelEName.Name = "panelEName";
            this.panelEName.Size = new System.Drawing.Size(273, 48);
            this.panelEName.TabIndex = 4;
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblFName.Location = new System.Drawing.Point(26, 102);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(88, 21);
            this.lblFName.TabIndex = 5;
            this.lblFName.Text = "First Name";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.txtLName);
            this.panel1.Location = new System.Drawing.Point(30, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 48);
            this.panel1.TabIndex = 6;
            // 
            // txtLName
            // 
            this.txtLName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLName.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtLName.Location = new System.Drawing.Point(5, 1);
            this.txtLName.MaxLength = 50;
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(265, 25);
            this.txtLName.TabIndex = 0;
            this.txtLName.TextChanged += new System.EventHandler(this.txtLName_TextChanged);
            this.txtLName.Enter += new System.EventHandler(this.txtLName_Enter);
            this.txtLName.Leave += new System.EventHandler(this.txtLName_Leave);
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblLName.Location = new System.Drawing.Point(26, 185);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(86, 21);
            this.lblLName.TabIndex = 7;
            this.lblLName.Text = "Last Name";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.txtPass);
            this.panel2.Location = new System.Drawing.Point(318, 210);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 48);
            this.panel2.TabIndex = 10;
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtPass.Location = new System.Drawing.Point(5, 1);
            this.txtPass.MaxLength = 15;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(265, 25);
            this.txtPass.TabIndex = 0;
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            this.txtPass.Enter += new System.EventHandler(this.txtPass_Enter);
            this.txtPass.Leave += new System.EventHandler(this.txtPass_Leave);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblPass.Location = new System.Drawing.Point(314, 186);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(79, 21);
            this.lblPass.TabIndex = 11;
            this.lblPass.Text = "Password";
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Controls.Add(this.txtUser);
            this.panel3.Location = new System.Drawing.Point(318, 126);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 48);
            this.panel3.TabIndex = 8;
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.txtUser.Location = new System.Drawing.Point(4, 1);
            this.txtUser.MaxLength = 15;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(265, 25);
            this.txtUser.TabIndex = 0;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.lblUser.Location = new System.Drawing.Point(314, 103);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(83, 21);
            this.lblUser.TabIndex = 9;
            this.lblUser.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.label4.Location = new System.Drawing.Point(26, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Birthday";
            // 
            // countFName
            // 
            this.countFName.AutoSize = true;
            this.countFName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countFName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.countFName.Location = new System.Drawing.Point(269, 105);
            this.countFName.Name = "countFName";
            this.countFName.Size = new System.Drawing.Size(34, 17);
            this.countFName.TabIndex = 14;
            this.countFName.Text = "0/50";
            this.countFName.Visible = false;
            // 
            // countLName
            // 
            this.countLName.AutoSize = true;
            this.countLName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countLName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.countLName.Location = new System.Drawing.Point(269, 188);
            this.countLName.Name = "countLName";
            this.countLName.Size = new System.Drawing.Size(34, 17);
            this.countLName.TabIndex = 15;
            this.countLName.Text = "0/50";
            this.countLName.Visible = false;
            // 
            // countUser
            // 
            this.countUser.AutoSize = true;
            this.countUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.countUser.Location = new System.Drawing.Point(559, 105);
            this.countUser.Name = "countUser";
            this.countUser.Size = new System.Drawing.Size(32, 17);
            this.countUser.TabIndex = 16;
            this.countUser.Text = "0/15";
            this.countUser.Visible = false;
            // 
            // countPass
            // 
            this.countPass.AutoSize = true;
            this.countPass.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.countPass.Location = new System.Drawing.Point(559, 188);
            this.countPass.Name = "countPass";
            this.countPass.Size = new System.Drawing.Size(32, 17);
            this.countPass.TabIndex = 17;
            this.countPass.Text = "0/15";
            this.countPass.Visible = false;
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(626, 362);
            this.Controls.Add(this.countPass);
            this.Controls.Add(this.countUser);
            this.Controls.Add(this.countLName);
            this.Controls.Add(this.countFName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblLName);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.panelEName);
            this.Controls.Add(this.lblFName);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "register";
            this.panelEName.ResumeLayout(false);
            this.panelEName.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label btnFill;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Panel panelEName;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnRegister;
        public System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label countFName;
        private System.Windows.Forms.Label countLName;
        private System.Windows.Forms.Label countUser;
        private System.Windows.Forms.Label countPass;
    }
}