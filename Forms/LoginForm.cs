using BankApp.Classes;
using BankApp.Forms;
using System;
using System.Windows.Forms;

namespace BankApp
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
            registrationForm.ShowDialog();
        }
    }
}
