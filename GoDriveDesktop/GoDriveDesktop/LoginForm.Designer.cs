namespace GoDriveDesktop
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            buttonLogin = new RoundedButton();
            mainLoginPanel = new RoundedPanel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            passwordTextBoxPanel = new RoundedPanel();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            textBoxPassword = new TextBox();
            loginTextBoxPanel = new RoundedPanel();
            pictureBox2 = new PictureBox();
            textBoxLogin = new TextBox();
            pictureBox1 = new PictureBox();
            mainLoginPanel.SuspendLayout();
            passwordTextBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            loginTextBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            buttonLogin.Anchor = AnchorStyles.None;
            buttonLogin.BackColor = SystemColors.MenuHighlight;
            buttonLogin.BorderColor = Color.FromArgb(255, 192, 192);
            buttonLogin.FlatAppearance.BorderColor = Color.FromArgb(95, 168, 116);
            buttonLogin.FlatAppearance.BorderSize = 5;
            buttonLogin.FlatAppearance.MouseDownBackColor = SystemColors.MenuHighlight;
            buttonLogin.FlatAppearance.MouseOverBackColor = Color.ForestGreen;
            buttonLogin.FlatStyle = FlatStyle.Popup;
            buttonLogin.Font = new Font("Arial Unicode MS", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonLogin.ForeColor = SystemColors.ButtonHighlight;
            buttonLogin.Location = new Point(121, 433);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(430, 68);
            buttonLogin.TabIndex = 0;
            buttonLogin.TabStop = false;
            buttonLogin.Text = "Увійти";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // mainLoginPanel
            // 
            mainLoginPanel.Anchor = AnchorStyles.None;
            mainLoginPanel.BackColor = SystemColors.Window;
            mainLoginPanel.BorderColor = Color.Silver;
            mainLoginPanel.BorderRadius = 15;
            mainLoginPanel.BorderThickness = 1;
            mainLoginPanel.Controls.Add(label3);
            mainLoginPanel.Controls.Add(label2);
            mainLoginPanel.Controls.Add(label1);
            mainLoginPanel.Controls.Add(passwordTextBoxPanel);
            mainLoginPanel.Controls.Add(loginTextBoxPanel);
            mainLoginPanel.Controls.Add(buttonLogin);
            mainLoginPanel.Location = new Point(187, 147);
            mainLoginPanel.Name = "mainLoginPanel";
            mainLoginPanel.Size = new Size(650, 550);
            mainLoginPanel.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(85, 289);
            label3.Name = "label3";
            label3.Size = new Size(109, 35);
            label3.TabIndex = 6;
            label3.Text = "Пароль:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(85, 147);
            label2.Name = "label2";
            label2.Size = new Size(86, 35);
            label2.TabIndex = 5;
            label2.Text = "Логін:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(259, 44);
            label1.Name = "label1";
            label1.Size = new Size(112, 58);
            label1.TabIndex = 4;
            label1.Text = "Вхід";
            // 
            // passwordTextBoxPanel
            // 
            passwordTextBoxPanel.BackColor = SystemColors.Window;
            passwordTextBoxPanel.BorderColor = Color.Silver;
            passwordTextBoxPanel.BorderRadius = 5;
            passwordTextBoxPanel.BorderThickness = 1;
            passwordTextBoxPanel.Controls.Add(pictureBox3);
            passwordTextBoxPanel.Controls.Add(pictureBox4);
            passwordTextBoxPanel.Controls.Add(textBoxPassword);
            passwordTextBoxPanel.Location = new Point(79, 329);
            passwordTextBoxPanel.Name = "passwordTextBoxPanel";
            passwordTextBoxPanel.Size = new Size(500, 50);
            passwordTextBoxPanel.TabIndex = 4;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Left;
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Location = new Point(450, 9);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(35, 35);
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Left;
            pictureBox4.BackgroundImage = Properties.Resources.IconPassword;
            pictureBox4.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox4.Location = new Point(6, 9);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(35, 35);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = SystemColors.Window;
            textBoxPassword.BorderStyle = BorderStyle.None;
            textBoxPassword.Font = new Font("Calibri", 13.8F);
            textBoxPassword.Location = new Point(52, 9);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(392, 29);
            textBoxPassword.TabIndex = 1;
            // 
            // loginTextBoxPanel
            // 
            loginTextBoxPanel.BackColor = SystemColors.Window;
            loginTextBoxPanel.BorderColor = Color.Silver;
            loginTextBoxPanel.BorderRadius = 5;
            loginTextBoxPanel.BorderThickness = 1;
            loginTextBoxPanel.Controls.Add(pictureBox2);
            loginTextBoxPanel.Controls.Add(textBoxLogin);
            loginTextBoxPanel.Location = new Point(79, 188);
            loginTextBoxPanel.Name = "loginTextBoxPanel";
            loginTextBoxPanel.Size = new Size(500, 50);
            loginTextBoxPanel.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Left;
            pictureBox2.BackgroundImage = Properties.Resources.IconLogin;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(6, 8);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 35);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // textBoxLogin
            // 
            textBoxLogin.BackColor = SystemColors.Window;
            textBoxLogin.BorderStyle = BorderStyle.None;
            textBoxLogin.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxLogin.Location = new Point(42, 12);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(412, 29);
            textBoxLogin.TabIndex = 1;
            textBoxLogin.TextChanged += textBoxLogin_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackgroundImage = Properties.Resources.GoDrive_logo_transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(277, -9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(470, 150);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(1006, 721);
            Controls.Add(pictureBox1);
            Controls.Add(mainLoginPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1024, 768);
            Name = "LoginForm";
            Text = "Авторизація";
            Load += LoginForm_Load;
            mainLoginPanel.ResumeLayout(false);
            mainLoginPanel.PerformLayout();
            passwordTextBoxPanel.ResumeLayout(false);
            passwordTextBoxPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            loginTextBoxPanel.ResumeLayout(false);
            loginTextBoxPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RoundedButton buttonLogin;
        private RoundedPanel mainLoginPanel;
        private TextBox textBoxLogin;
        private RoundedPanel loginTextBoxPanel;
        private Label label1;
        private RoundedPanel passwordTextBoxPanel;
        private TextBox textBoxPassword;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
    }
}
