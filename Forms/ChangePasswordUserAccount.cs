using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class ChangePasswordUserAccount : Form
    {
        DataBaseConnection database = new DataBaseConnection();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public ChangePasswordUserAccount()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ChangePasswordUserAccount_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void ChangePasswordUserAccount_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void ChangePasswordUserAccount_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void Btn_ChangePasswordAccount_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Ошибка";

            if (!Regex.IsMatch(txB_client_password.Text, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$^&*-]).{8,}$"))
            {
                MessageBox.Show("Пожалуйста введите пароль!\n(Не менее 8-ми символов, без кириллицы, хотя-бы одна заглавная и с символами #?!@$^&*-)", caption, btn, ico);
                txB_client_password.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_password_old.Text, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$^&*-]).{8,}$"))
            {
                MessageBox.Show("Пожалуйста введите пароль!\n(Не менее 8-ми символов, без кириллицы, хотя-бы одна заглавная и с символами #?!@$^&*-)", caption, btn, ico);
                txB_client_password.Select();
                return;
            }

            var pass = txB_client_password.Text;
            var changeNumQuery = $"update client set client_password = '{pass}' where id_client = (select id_client from client where id_client = '{DataStorage.idClient}'";
            database.openConnection();
            var command = new SqlCommand(changeNumQuery, database.getConnection());
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Пароль успешно изменен!");
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
            database.closeConnection();
        }

        void ChB_visibilityPassword_Click(object sender, EventArgs e)
        {
            if (chB_visibilityPassword.Checked)
            {
                txB_client_password.UseSystemPasswordChar = false;
                txB_client_password_old.UseSystemPasswordChar = false;
            }
            else if (!chB_visibilityPassword.Checked)
            {
                txB_client_password.UseSystemPasswordChar = true;
                txB_client_password_old.UseSystemPasswordChar = true;
            }
        }

        void ChangePasswordUserAccount_Load(object sender, EventArgs e)
        {
            txB_client_password_old.Select();
        }
    }
}
