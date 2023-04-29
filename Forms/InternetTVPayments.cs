using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class InternetTVPayments : Form
    {
        DataBaseConnection database = new DataBaseConnection();
        Random rand = new Random();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public InternetTVPayments()
        {
            InitializeComponent();
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void InternetTV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void InternetTV_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void InternetTV_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void InternetTV_Load(object sender, EventArgs e)
        {
            txB_card_numberUser.Text = DataStorage.cardNumber;

            var queryChooseOperator = $"select id_service, serviceName from clientService where serviceType = 'Internet'";
            SqlDataAdapter commandChooseOperator = new SqlDataAdapter(queryChooseOperator, database.getConnection());
            database.openConnection();
            DataTable operators = new DataTable();
            commandChooseOperator.Fill(operators);
            cmb_servicesInternetTVPayments.DataSource = operators;
            cmb_servicesInternetTVPayments.ValueMember = "id_service";
            cmb_servicesInternetTVPayments.DisplayMember = "serviceName";
            database.closeConnection();
        }

        void TxB_personalAccountCommunalPayments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TxB_sum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void Btn_TransferPaymentsInternetTV_Click(object sender, EventArgs e)
        {

            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;

            string caption = "Отмена. Невозможно осуществить перевод средств";
            var PersonalAccount = txB_personallPaymentsInternetTV.Text;
            double sum = Convert.ToDouble(txB_sum.Text);
            var cardNumber = txB_card_numberUser.Text;
            var cardCVV = txB_cardCvv.Text;
            var cardDate = txB_cardDate.Text;
            var cardCVVCheck = "";
            var cardDateCheck = "";
            double cardBalanceCheck = 0;
            bool error = false;
            string cardCurrency = "";

            if (!Regex.IsMatch(txB_personallPaymentsInternetTV.Text, "^[0-9]{10}$"))
            {
                MessageBox.Show("Введите корректно ваш номер лицевого счета", caption, btn, ico);
                txB_personallPaymentsInternetTV.Select();
                return;
            }

            var queryCheckCard = $"select bank_card_cvv_code, CONCAT(FORMAT(bank_card_date, '%M'), '/', FORMAT(bank_card_date, '%y')), bank_card_balance, bank_card_currency from bank_card where bank_card_number = '{cardNumber}'";
            SqlCommand commandCheckCard = new SqlCommand(queryCheckCard, database.getConnection());
            database.openConnection();
            SqlDataReader reader = commandCheckCard.ExecuteReader();

            while (reader.Read())
            {
                cardCVVCheck = reader[0].ToString();
                cardDateCheck = reader[1].ToString();
                cardBalanceCheck = Convert.ToDouble(reader[2].ToString());
                cardCurrency = reader[3].ToString();
            }
            reader.Close();

            if (cardCurrency != "RUB")
            {
                MessageBox.Show("Пополнение мобильного может происходить только в рублях", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                error = true;
            }

            if (cardCVV != cardCVVCheck)
            {
                MessageBox.Show("Ошибка. Неверно введен CVV-код", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                error = true;
            }

            if (cardDate != cardDateCheck)
            {
                MessageBox.Show("Ошибка. Неверно введена дата карты", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                error = true;
            }

            if (sum > cardBalanceCheck)
            {
                MessageBox.Show("Ошибка. Недостаточно средств для совершения операции", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                error = true;
            }

            if (error == false)
            {
                DataStorage.bankCard = txB_card_numberUser.Text;
                Validations validations = new Validations();
                validations.ShowDialog();

                if (DataStorage.attempts > 0)
                {
                    DateTime transactionDate = DateTime.Now;
                    var transactionNumber = "P";

                    for (int i = 0; i < 10; i++)
                    {
                        transactionNumber += Convert.ToString(rand.Next(0, 10));
                    }

                    var queryTransaction1 = $"update bank_card set bank_card_balance = bank_card_balance - '{sum}' where bank_card_number = '{cardNumber}'";
                    var queryTransaction2 = $"insert into transactions(transaction_type, transaction_destination, transaction_date, transaction_number, transaction_value, id_bank_card) values('Оплата коммунальных услуг', '{cmb_servicesInternetTVPayments.GetItemText(cmb_servicesInternetTVPayments.SelectedItem)}', '{transactionDate}', '{transactionNumber}', '{sum}', (select id_bank_card from bank_card where bank_card_number = '{cardNumber}'))";
                    var queryTransaction3 = $"update clientServices set serviceBalance = serviceBalance + '{sum}' where serviceName = '{cmb_servicesInternetTVPayments.GetItemText(cmb_servicesInternetTVPayments.SelectedItem)}' and serviceType = 'communal'";
                    var queryTransaction4 = $"insert into clientPersonalAccount(personal_account, id_service, id_client) values('{txB_personallPaymentsInternetTV.Text}', (select id_service from clientServices where serviceName = '{cmb_servicesInternetTVPayments.GetItemText(cmb_servicesInternetTVPayments.SelectedItem)}'), '{DataStorage.idClient}')";

                    var command1 = new SqlCommand(queryTransaction1, database.getConnection());
                    var command2 = new SqlCommand(queryTransaction2, database.getConnection());
                    var command3 = new SqlCommand(queryTransaction3, database.getConnection());
                    var command4 = new SqlCommand(queryTransaction4, database.getConnection());

                    database.openConnection();

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();

                    database.closeConnection();

                    Close();
                }
            }
        }
    }
}
