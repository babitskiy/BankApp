using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class ChangePhoneNumber : Form
    {

        DataBaseConnection database = new DataBaseConnection();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public ChangePhoneNumber()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ChangePhoneNumber_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void ChangePhoneNumber_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void ChangePhoneNumber_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void ChangePhoneNumber_Load(object sender, EventArgs e)
        {
            txB_client_phone_number.Select();
        }

        void Btn_ChangeNumberPhone_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Ошибка";

            if (!Regex.IsMatch(txB_client_phone_number.Text, "^[+][7][9][0-9]{9}$"))
            {
                MessageBox.Show("Пожалуйста введите номер телефона корректно!", caption, btn, ico);
                txB_client_phone_number.Select();
                return;
            }

            var phoneNumber = txB_client_phone_number.Text;

            var changeNumberPhoneQuery = $"UPDATE client SET client_phone_number = '{phoneNumber}' WHERE id_client = '{DataStorage.idClient}'";
            var command = new SqlCommand(changeNumberPhoneQuery, database.getConnection());
            database.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Номер телефона успешно изменен!");
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
