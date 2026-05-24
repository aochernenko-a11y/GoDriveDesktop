using MySql.Data.MySqlClient;

namespace GoDriveDesktop
{
    public partial class LoginForm : FullScreenForm
    {
        private bool isPasswordVisible = false;

        public LoginForm()
        {
            InitializeComponent();

            CenterTextBoxInPanel(textBoxLogin, loginTextBoxPanel);
            CenterTextBoxInPanel(textBoxPassword, passwordTextBoxPanel);

            textBoxPassword.UseSystemPasswordChar = true;

            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = Properties.Resources.IconPasswordInvisible;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT 
                e.EmployeeId,
                e.FullName,
                p.Name AS PositionName
            FROM Employees e
            INNER JOIN Positions p ON e.PositionId = p.PositionId
            WHERE e.Login = @login AND e.Password = @password;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Невірний логін або пароль");
                            return;
                        }

                        int employeeId = reader.GetInt32("EmployeeId");
                        string fullName = reader.GetString("FullName");
                        string positionName = reader.GetString("PositionName");

                        if (positionName == "Адміністратор")
                        {
                            AdminMainForm adminMainForm = new AdminMainForm();

                            adminMainForm.FormClosed += (s, args) =>
                            {
                                this.Close();
                            };

                            adminMainForm.Show();
                            this.Hide();
                        }
                        else if (positionName == "Касир")
                        {
                            CashierMainForm cashierMainForm = new CashierMainForm(employeeId, fullName);

                            cashierMainForm.FormClosed += (s, args) =>
                            {
                                this.Close();
                            };

                            cashierMainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Для цієї посади не налаштовано форму.");
                        }
                    }
                }
            }
        }
        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }
        private void CenterLoginPanel()
        {
            mainLoginPanel.Left = (ClientSize.Width - mainLoginPanel.Width) / 2;
            mainLoginPanel.Top = (ClientSize.Height - mainLoginPanel.Height) / 2;
        }

        private void CenterTextBoxInPanel(TextBox textBox, Panel panel)
        {
            textBox.Left = (panel.Width - textBox.Width) / 2;
            textBox.Top = (panel.Height - textBox.Height) / 2;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            textBoxPassword.UseSystemPasswordChar = !isPasswordVisible;

            if (isPasswordVisible)
            {
                pictureBox3.Image = Properties.Resources.IconPasswordVisible;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.IconPasswordInvisible;
            }
        }


    }
}
