using BankApp.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class RegistrationForm : Form
    {
        DataBaseConnection database = new DataBaseConnection();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, System.EventArgs e)
        {
            txB_client_last_name.Select();
        }

        private void btn_save_client_Click(object sender, System.EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;

            string caption = "Дата сохранения";

            if (!Regex.IsMatch(txB_client_last_name.Text, "[А-Яа-я]+$"))
            {
                MessageBox.Show("Пожалуйста введите фамилию повторно!", caption, btn, ico);
                txB_client_first_name.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_first_name.Text, "[А-Яa-я]+$"))
            {
                MessageBox.Show("Пожалуйста введите имя повторно!", caption, btn, ico);
                txB_client_first_name.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_middle_name.Text, "[А-Яa-я]+$"))
            {
                MessageBox.Show("Пожалуйста введите отчество повторно!", caption, btn, ico);
                txB_client_middle_name.Select();
                return;
            }
            if (string.IsNullOrEmpty(cmb_client_gender.SelectedItem.ToString()))
            {
                MessageBox.Show("Пожалуйста выберите пол!", caption, btn, ico);
                cmb_client_gender.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_password.Text, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$^&*-]).{8,}$"))
            {
                MessageBox.Show("Пожалуйста введите пароль!\n(Не менее 8-ми символов, без кириллицы, хотя-бы одна заглавная и с символами #?!@$^&*-)", caption, btn, ico);
                txB_client_password.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_password_replay.Text, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$^&*-]).{8,}$"))
            {
                MessageBox.Show("Пожалуйста введите пароль!\n(Не менее 8-ми символов, без кириллицы, хотя-бы одна заглавная и с символами #?!@$^&*-)", caption, btn, ico);
                txB_client_password_replay.Select();
                return;
            }
            if (txB_client_password.Text != txB_client_password_replay.Text)
            {
                MessageBox.Show("Ваш пароль и пароль подтверждения не совпадают!", caption, btn, ico);
                txB_client_password_replay.Select();
                return;
            }
            if (!Regex.IsMatch(txb_client_email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Пожалуйста введите вашу почту", caption, btn, ico);
                txb_client_email.Select();
                return;
            }
            if (!Regex.IsMatch(txB_client_phone_number.Text, "^[+][7][9][0-9]{9}$"))
            {
                MessageBox.Show("Пожалуйста введите номер телефона корректно!", caption, btn, ico);
                txB_client_phone_number.Select();
                return;
            }

            string yourSQL = "SELECT client_phone_number FROM client WHERE client_phone_number = '" + txB_client_phone_number.Text + "'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand(yourSQL, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Номер телефона уже существует. Невозможно зарегистрировать аккаунт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_client_phone_number.SelectAll();
                return;
            }

            DialogResult result;
            result = MessageBox.Show("Вы хотите сохранить запись?", "Сохранение данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO client (client_last_name, client_first_name, client_middle_name, client_gender, client_password, client_email, client_phone_number) ";
                mySQL += "VALUES ('" + txB_client_last_name.Text + "','" + txB_client_first_name.Text + "','" + txB_client_middle_name.Text + "',";
                mySQL += "'" + cmb_client_gender.SelectedItem.ToString() + "','" + txB_client_password.Text + "','" + txb_client_email.Text + "','" + txB_client_phone_number.Text + "')";

                database.openConnection();
                SqlCommand commandAddNewUser = new SqlCommand(mySQL, database.getConnection());
                commandAddNewUser.ExecuteNonQuery();

                MessageBox.Show("Запись успешно сохранена", "Данные сохранены", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearControls();
                database.closeConnection();
                Close();
            }
        }

        private void clearControls()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
            txB_client_last_name.Select();
        }

        private void btn_clear_registrationForm_Click(object sender, System.EventArgs e) => clearControls();

        private void btn_close_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
