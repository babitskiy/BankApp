using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class ChangeEmail : Form
    {
        DataBaseConnection database = new DataBaseConnection();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public ChangeEmail()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ChangeEmail_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void ChangeEmail_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void ChangeEmail_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void ChangeEmail_Load(object sender, EventArgs e)
        {
            txb_client_email.Select();
        }

        private void Btn_ChangeEmail_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Ошибка";

            if (!Regex.IsMatch(txb_client_email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Пожалуйста введите вашу почту", caption, btn, ico);
                txb_client_email.Select();
                return;
            }
            var clientEmail = txb_client_email.Text;

            var changeNumberPhoneQuery = $"UPDATE client SET client_email = '{clientEmail}' WHERE id_client = '{DataStorage.idClient}'";
            database.openConnection();
            var command = new SqlCommand(changeNumberPhoneQuery, database.getConnection());
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Электронная почта успешно изменена!");
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
            database.closeConnection();
        }
    }
}
