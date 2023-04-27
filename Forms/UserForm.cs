using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class UserForm : Form
    {
        DataBaseConnection database = new DataBaseConnection();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public UserForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void UserForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        void Btn_update_Click(object sender, EventArgs e)
        {

        }

        void ClearControl()
        {
            lbL_FIO_user.Text = string.Empty;
            lbL_Phone_user.Text = string.Empty;
            lbL_email_user.Text = string.Empty;
        }

        #region перет.

        void Panel4_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void Panel4_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void Panel4_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void Panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void Panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void Panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void Panel2_MouseDown(object sender, MouseEventArgs e)
        {

        }
        void Panel2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void Panel2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void Panel3_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void Panel3_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void Panel3_MouseUp(object sender, MouseEventArgs e)
        {

        }

        #endregion

        void Btn_ChangeNumberPhone_Click(object sender, EventArgs e)
        {

        }

        void Btn_ChangePasswordAccount_Click(object sender, EventArgs e)
        {

        }

        void Btn_ChangeEmail_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void RefreshData()
        {
            var queryPIB = $"select concat(client_last_name, ' ', client_first_name, ' ', client_middle_name), client_phone_number, client_email from client where id_client = '{DataStorage.idClient}'";
            SqlCommand commandPIB = new SqlCommand(queryPIB, database.getConnection());
            database.openConnection();
            SqlDataReader reader = commandPIB.ExecuteReader();
            while (reader.Read())
            {
                lbL_FIO_user.Text += reader[0].ToString();
                lbL_Phone_user.Text += reader[1].ToString();
                lbL_email_user.Text += reader[2].ToString();
            }
            reader.Close();
        }

        private void ClearFields()
        {
            lbL_FIO_user.Text += String.Empty;
            lbL_Phone_user.Text += String.Empty;
            lbL_email_user.Text += String.Empty;
        }
    }
}
