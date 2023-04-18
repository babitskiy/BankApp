using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class LoginForm : Form
    {
        DataBaseConnection database = new DataBaseConnection();

        public LoginForm()
        {
            InitializeComponent();
        }

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txB_enterNumberPhone.Select(); //делаем активным номер телефона при загрузке
        }

        private void lbL_CreateAccount_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog(); //если нет аккаунта - переходим на форму регистрации
        }

        private void chB_visibilityPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chB_visibilityPassword.Checked == true)
            {
                txB_enterPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txB_enterPassword.UseSystemPasswordChar = true;
            }
        }

        private void btn_EnterLoginForm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txB_enterNumberPhone.Text) && !string.IsNullOrEmpty(txB_enterPassword.Text))
            {
                var querySelectClient = $"SELECT * FROM client WHERE client_phone_number = '{txB_enterNumberPhone.Text}' AND client_password = '{txB_enterPassword.Text}'";
                var queryGetId = $"SELECT id_client FROM client WHERE client_phone_number = '{txB_enterNumberPhone.Text}'";
                var commandGetId = new SqlCommand(queryGetId, database.getConnection());

                database.openConnection();
                SqlDataReader reader = commandGetId.ExecuteReader();
                while (reader.Read())
                {
                    DataStorage.idClient = reader[0].ToString();
                }
                reader.Close();

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand(querySelectClient, database.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    txB_enterNumberPhone.Clear();
                    txB_enterPassword.Clear();
                    chB_visibilityPassword.Checked = false;

                    this.Hide();

                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();
                    mainForm = null;

                    Show();
                    txB_enterNumberPhone.Select();
                }
                else
                {
                    MessageBox.Show("Имя пользователя или пароль неверны. Попробуйте ещё раз!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txB_enterNumberPhone.Focus();
                    txB_enterNumberPhone.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txB_enterNumberPhone.Select();
            }
        }

        private void btn_closeLoginForm_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //метод для перетягивания окна программы
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
