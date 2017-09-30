namespace BalayPasilungan
{
    partial class login
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
            this.btnMember = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.member = new System.Windows.Forms.TabPage();
            this.btnForgot = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnPeek = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.newUser = new System.Windows.Forms.TabPage();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblIAm = new System.Windows.Forms.Label();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnSW = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnClose = new BalayPasilungan.NoFocusRec();
            this.tabControl.SuspendLayout();
            this.member.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.newUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMember
            // 
            this.btnMember.FlatAppearance.BorderSize = 0;
            this.btnMember.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMember.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMember.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.btnMember.Location = new System.Drawing.Point(0, 155);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(166, 46);
            this.btnMember.TabIndex = 5;
            this.btnMember.Text = "member";
            this.btnMember.UseVisualStyleBackColor = true;
            this.btnMember.Click += new System.EventHandler(this.btnMember_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnNew.Location = new System.Drawing.Point(166, 155);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(166, 46);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "new member";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.member);
            this.tabControl.Controls.Add(this.newUser);
            this.tabControl.Location = new System.Drawing.Point(-4, 174);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(344, 263);
            this.tabControl.TabIndex = 9;
            // 
            // member
            // 
            this.member.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(168)))), ((int)(((byte)(104)))));
            this.member.Controls.Add(this.btnForgot);
            this.member.Controls.Add(this.btnLogin);
            this.member.Controls.Add(this.panel2);
            this.member.Controls.Add(this.btnPeek);
            this.member.Controls.Add(this.btnHide);
            this.member.Controls.Add(this.lblPass);
            this.member.Controls.Add(this.lblUser);
            this.member.Controls.Add(this.panel1);
            this.member.Location = new System.Drawing.Point(4, 22);
            this.member.Name = "member";
            this.member.Padding = new System.Windows.Forms.Padding(3);
            this.member.Size = new System.Drawing.Size(336, 237);
            this.member.TabIndex = 0;
            this.member.Text = "tabPage1";
            this.member.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveable_MouseDown);
            // 
            // btnForgot
            // 
            this.btnForgot.FlatAppearance.BorderSize = 0;
            this.btnForgot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForgot.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForgot.ForeColor = System.Drawing.Color.White;
            this.btnForgot.Location = new System.Drawing.Point(96, 147);
            this.btnForgot.Name = "btnForgot";
            this.btnForgot.Size = new System.Drawing.Size(145, 23);
            this.btnForgot.TabIndex = 4;
            this.btnForgot.Text = "Forgot my password.";
            this.btnForgot.UseVisualStyleBackColor = true;
            this.btnForgot.Click += new System.EventHandler(this.btnForgot_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(0, 191);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(332, 45);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.txtUser);
            this.panel2.Location = new System.Drawing.Point(28, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 30);
            this.panel2.TabIndex = 0;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(6, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(265, 22);
            this.txtUser.TabIndex = 0;
            // 
            // btnPeek
            // 
            this.btnPeek.BackColor = System.Drawing.Color.White;
            this.btnPeek.BackgroundImage = global::BalayPasilungan.Properties.Resources.eye;
            this.btnPeek.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPeek.FlatAppearance.BorderSize = 0;
            this.btnPeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeek.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeek.ForeColor = System.Drawing.Color.White;
            this.btnPeek.Location = new System.Drawing.Point(274, 113);
            this.btnPeek.Name = "btnPeek";
            this.btnPeek.Size = new System.Drawing.Size(25, 25);
            this.btnPeek.TabIndex = 2;
            this.btnPeek.TabStop = false;
            this.btnPeek.UseVisualStyleBackColor = false;
            this.btnPeek.Click += new System.EventHandler(this.btnPeek_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.White;
            this.btnHide.BackgroundImage = global::BalayPasilungan.Properties.Resources.eyeclose;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHide.FlatAppearance.BorderSize = 0;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.ForeColor = System.Drawing.Color.White;
            this.btnHide.Location = new System.Drawing.Point(274, 113);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(25, 25);
            this.btnHide.TabIndex = 13;
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.ForeColor = System.Drawing.Color.White;
            this.lblPass.Location = new System.Drawing.Point(25, 88);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(72, 20);
            this.lblPass.TabIndex = 10;
            this.lblPass.Text = "password";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(24, 26);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 20);
            this.lblUser.TabIndex = 9;
            this.lblUser.Text = "username";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtPass);
            this.panel1.Location = new System.Drawing.Point(28, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 30);
            this.panel1.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.White;
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtPass.Location = new System.Drawing.Point(6, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '·';
            this.txtPass.Size = new System.Drawing.Size(265, 22);
            this.txtPass.TabIndex = 1;
            // 
            // newUser
            // 
            this.newUser.Controls.Add(this.btnRegister);
            this.newUser.Controls.Add(this.lblIAm);
            this.newUser.Controls.Add(this.btnStaff);
            this.newUser.Controls.Add(this.btnSW);
            this.newUser.Controls.Add(this.btnAdmin);
            this.newUser.ForeColor = System.Drawing.Color.White;
            this.newUser.Location = new System.Drawing.Point(4, 22);
            this.newUser.Name = "newUser";
            this.newUser.Padding = new System.Windows.Forms.Padding(3);
            this.newUser.Size = new System.Drawing.Size(336, 237);
            this.newUser.TabIndex = 1;
            this.newUser.Text = "tabPage2";
            this.newUser.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnRegister.Location = new System.Drawing.Point(0, 191);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(332, 45);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "PLEASE SELECT FIRST";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblIAm
            // 
            this.lblIAm.AutoSize = true;
            this.lblIAm.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblIAm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.lblIAm.Location = new System.Drawing.Point(12, 27);
            this.lblIAm.Name = "lblIAm";
            this.lblIAm.Size = new System.Drawing.Size(100, 25);
            this.lblIAm.TabIndex = 5;
            this.lblIAm.Text = "I am a/an...";
            // 
            // btnStaff
            // 
            this.btnStaff.BackgroundImage = global::BalayPasilungan.Properties.Resources.btnStaff;
            this.btnStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStaff.FlatAppearance.BorderSize = 0;
            this.btnStaff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnStaff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Location = new System.Drawing.Point(226, 66);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(92, 103);
            this.btnStaff.TabIndex = 4;
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnSW
            // 
            this.btnSW.BackgroundImage = global::BalayPasilungan.Properties.Resources.btnSW;
            this.btnSW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSW.FlatAppearance.BorderSize = 0;
            this.btnSW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnSW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSW.Location = new System.Drawing.Point(119, 66);
            this.btnSW.Name = "btnSW";
            this.btnSW.Size = new System.Drawing.Size(92, 103);
            this.btnSW.TabIndex = 6;
            this.btnSW.UseVisualStyleBackColor = true;
            this.btnSW.Click += new System.EventHandler(this.btnSW_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackgroundImage = global::BalayPasilungan.Properties.Resources.btnAdmin;
            this.btnAdmin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAdmin.FlatAppearance.BorderSize = 0;
            this.btnAdmin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnAdmin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btnAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdmin.Location = new System.Drawing.Point(12, 66);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(92, 103);
            this.btnAdmin.TabIndex = 5;
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::BalayPasilungan.Properties.Resources.login_logo;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(50, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(237, 126);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveable_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnClose.Location = new System.Drawing.Point(309, -2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(332, 432);
            this.Controls.Add(this.btnMember);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveable_MouseDown);
            this.tabControl.ResumeLayout(false);
            this.member.ResumeLayout(false);
            this.member.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.newUser.ResumeLayout(false);
            this.newUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMember;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TabPage member;
        private System.Windows.Forms.TabPage newUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnPeek;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnForgot;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnSW;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Label lblIAm;
        private System.Windows.Forms.Button btnRegister;
        private NoFocusRec btnClose;
        public System.Windows.Forms.TabControl tabControl;
    }
}