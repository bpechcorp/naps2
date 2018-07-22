namespace NAPS2_Alfresco
{
    partial class FLogin
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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.labelServiceUrl = new System.Windows.Forms.Label();
            this.tbServiceUrl = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::NAPS2_Alfresco.Properties.Resources.alfresco;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(264, 66);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(74, 130);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(202, 20);
            this.tbUsername.TabIndex = 1;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(9, 133);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(55, 13);
            this.lbUsername.TabIndex = 2;
            this.lbUsername.Text = "Username";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(9, 166);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(53, 13);
            this.lbPassword.TabIndex = 3;
            this.lbPassword.Text = "Password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(74, 163);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(202, 20);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(110, 203);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // labelServiceUrl
            // 
            this.labelServiceUrl.AutoSize = true;
            this.labelServiceUrl.Location = new System.Drawing.Point(9, 100);
            this.labelServiceUrl.Name = "labelServiceUrl";
            this.labelServiceUrl.Size = new System.Drawing.Size(59, 13);
            this.labelServiceUrl.TabIndex = 6;
            this.labelServiceUrl.Text = "Service Url";
            // 
            // tbServiceUrl
            // 
            this.tbServiceUrl.Location = new System.Drawing.Point(74, 97);
            this.tbServiceUrl.Name = "tbServiceUrl";
            this.tbServiceUrl.Size = new System.Drawing.Size(202, 20);
            this.tbServiceUrl.TabIndex = 7;
            // 
            // FLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 237);
            this.Controls.Add(this.tbServiceUrl);
            this.Controls.Add(this.labelServiceUrl);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.pbLogo);
            this.Name = "FLogin";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label labelServiceUrl;
        private System.Windows.Forms.TextBox tbServiceUrl;
    }
}

