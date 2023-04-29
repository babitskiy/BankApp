using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class MobilePhoneTransferForm : Form
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

        public MobilePhoneTransferForm()
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

        void MobilePhoneTransferForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void MobilePhoneTransferForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void MobilePhoneTransferForm_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void MobilePhoneTransferForm_Load(object sender, EventArgs e)
        {
            txB_transferMobilePhone.Text = DataStorage.phoneNumber;
            txB_card_numberUser.Text = DataStorage.cardNumber;

            var queryChooseOperator = $"select id_service, serviceName from clientService where serviceType = 'Mobile'";
            SqlDataAdapter commandChooseOperator = new SqlDataAdapter(queryChooseOperator, database.getConnection());
            database.openConnection();
            DataTable operators = new DataTable();
            commandChooseOperator.Fill(operators);
            cmb_serviceTypeMobileOperator.DataSource = operators;
            cmb_serviceTypeMobileOperator.ValueMember = "id_service";
            cmb_serviceTypeMobileOperator.DisplayMember = "serviceName";
            database.closeConnection();
        }

        void TxB_sumTransferPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void TxB_sumTransferPhone_TextChanged_1(object sender, EventArgs e)
        {
            if (txB_sum.Text == String.Empty)
            {
                txB_sum.Text = null;
                lbL_commissionTransferPhone.Text = "0";
                lbL_paymentTransferPhone.Text = "0";
            }
            else
            {
                double sum = Convert.ToDouble(txB_sum.Text);
                lbL_commissionTransferPhone.Text = Convert.ToString((sum * 2) / 100);
                lbL_paymentTransferPhone.Text = Convert.ToString(((sum * 2) / 100) + sum);
            }
        }

        void Btn_TransferPhone_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Отмена. Невозможно осуществить перевод средств";

            string temp = txB_transferMobilePhone.Text;
            string phoneNumberToCheck = String.Concat(temp[0], temp[1]);

            string selectedOperator = cmb_serviceTypeMobileOperator.GetItemText(cmb_serviceTypeMobileOperator.SelectedItem);

            bool numberCheck = false;

            if (cmb_serviceTypeMobileOperator.Text == "МТС")
            {
                var phoneOperatorCheack = txB_transferMobilePhone.Text.Substring(3, 2);
                if (phoneOperatorCheack != "10" && phoneOperatorCheack != "11" && phoneOperatorCheack != "12" && phoneOperatorCheack != "13"
                    && phoneOperatorCheack != "14" && phoneOperatorCheack != "15" && phoneOperatorCheack != "16" && phoneOperatorCheack != "17"
                    && phoneOperatorCheack != "18" && phoneOperatorCheack != "19" && phoneOperatorCheack != "81" && phoneOperatorCheack != "82"
                    && phoneOperatorCheack != "83" && phoneOperatorCheack != "84" && phoneOperatorCheack != "85" && phoneOperatorCheack != "86"
                    && phoneOperatorCheack != "87" && phoneOperatorCheack != "88" && phoneOperatorCheack != "89")
                {
                    MessageBox.Show($"Введите корректный номер телефона оператора МТС", caption, btn, ico);
                    txB_transferMobilePhone.Select();
                    numberCheck = true;
                }
            }
            if (cmb_serviceTypeMobileOperator.Text == "Мегафон")
            {
                var phoneOperatorCheack = txB_transferMobilePhone.Text.Substring(3, 2);
                if (phoneOperatorCheack != "24" && phoneOperatorCheack != "29" && phoneOperatorCheack != "34" && phoneOperatorCheack != "28"
                    && phoneOperatorCheack != "29" && phoneOperatorCheack != "38" && phoneOperatorCheack != "27" && phoneOperatorCheack != "29"
                    && phoneOperatorCheack != "38" && phoneOperatorCheack != "21" && phoneOperatorCheack != "31" && phoneOperatorCheack != "23"
                    && phoneOperatorCheack != "83" && phoneOperatorCheack != "84" && phoneOperatorCheack != "85" && phoneOperatorCheack != "86"
                    && phoneOperatorCheack != "33" && phoneOperatorCheack != "25" && phoneOperatorCheack != "26" && phoneOperatorCheack != "36"
                    && phoneOperatorCheack != "22" && phoneOperatorCheack != "32" && phoneOperatorCheack != "20" && phoneOperatorCheack != "30")
                {
                    MessageBox.Show($"Введите корректный номер телефона оператора Мегафон", caption, btn, ico);
                    txB_transferMobilePhone.Select();
                    numberCheck = true;
                }
            }
            if (cmb_serviceTypeMobileOperator.Text == "Билайн")
            {
                var phoneOperatorCheack = txB_transferMobilePhone.Text.Substring(3, 2);
                if (phoneOperatorCheack != "03" && phoneOperatorCheack != "05" && phoneOperatorCheack != "06" && phoneOperatorCheack != "09"
                    && phoneOperatorCheack != "60" && phoneOperatorCheack != "61" && phoneOperatorCheack != "62" && phoneOperatorCheack != "63"
                    && phoneOperatorCheack != "64" && phoneOperatorCheack != "65" && phoneOperatorCheack != "67" && phoneOperatorCheack != "76"
                    && phoneOperatorCheack != "01" && phoneOperatorCheack != "02" && phoneOperatorCheack != "08" && phoneOperatorCheack != "66"
                    && phoneOperatorCheack != "68")
                {
                    MessageBox.Show($"Введите корректный номер телефона оператора Мегафон", caption, btn, ico);
                    txB_transferMobilePhone.Select();
                    numberCheck = true;
                }
            }
            if (cmb_serviceTypeMobileOperator.Text == "Теле2")
            {
                var phoneOperatorCheack = txB_transferMobilePhone.Text.Substring(3, 2);
                if (phoneOperatorCheack != "01" && phoneOperatorCheack != "02" && phoneOperatorCheack != "04" && phoneOperatorCheack != "05"
                    && phoneOperatorCheack != "08" && phoneOperatorCheack != "50" && phoneOperatorCheack != "51" && phoneOperatorCheack != "52"
                    && phoneOperatorCheack != "53" && phoneOperatorCheack != "58" && phoneOperatorCheack != "11" && phoneOperatorCheack != "99"
                    && phoneOperatorCheack != "96" && phoneOperatorCheack != "93" && phoneOperatorCheack != "00" && phoneOperatorCheack != "92"
                    && phoneOperatorCheack != "95")
                {
                    MessageBox.Show($"Введите корректный номер телефона оператора Мегафон", caption, btn, ico);
                    txB_transferMobilePhone.Select();
                    numberCheck = true;
                }
            }

            if (numberCheck == false)
            {
                var phoneNumber = txB_transferMobilePhone.Text;
                double sum = Convert.ToDouble(txB_sum.Text);
                var cardNumber = txB_card_numberUser.Text;
                var cardCVV = txB_cardCvv.Text;
                var cardDate = txB_cardDate.Text;
                var cardCVVCheck = "";
                var cardDateCheck = "";
                double cardBalanceCheck = 0;
                bool error = false;
                string cardCurrency = "";

                double commission = ((Convert.ToDouble(sum) * 2) / 100);
                double totalSum = commission + Convert.ToDouble(sum);

                if (!Regex.IsMatch(txB_transferMobilePhone.Text, "^[0-9]{9}$"))
                {
                    MessageBox.Show("Пожалуйста, введите номер телефона", caption, btn, ico);
                    txB_transferMobilePhone.Select();
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

                if (Convert.ToDouble(sum) < 10.00)
                {
                    MessageBox.Show("Ошибка. Минимальная сумма пополнения 10 рублей", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        var queryTransaction1 = $"update bank_card set bank_card_balance = bank_card_balance - '{totalSum}' where bank_card_number = '{cardNumber}'";
                        var queryTransaction2 = $"insert into transactions(transaction_type, transaction_destination, transaction_date, transaction_number, transaction_value, id_bank_card) values('Пополнение мобильного', '+7{txB_transferMobilePhone.Text}', '{transactionDate}', '{transactionNumber}', '{totalSum}', (select id_bank_card from bank_card where bank_card_number = '{cardNumber}'))";
                        var queryTransaction3 = $"update clientServices set serviceBalance = serviceBalance + '{sum}' where serviceName = '{cmb_serviceTypeMobileOperator.GetItemText(cmb_serviceTypeMobileOperator.SelectedItem)}' and serviceType = 'Mobile'";

                        var command1 = new SqlCommand(queryTransaction1, database.getConnection());
                        var command2 = new SqlCommand(queryTransaction2, database.getConnection());
                        var command3 = new SqlCommand(queryTransaction3, database.getConnection());

                        database.openConnection();

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();

                        database.closeConnection();

                        Close();
                    }
                }
            }
        }
    }
}
