using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class CreditForm : Form
    {
        DataBaseConnection database = new DataBaseConnection();
        Random rand = new Random();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        Validations validations = new Validations();
        
        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public CreditForm()
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

        void CreditForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void CreditForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void CreditForm_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void CreditForm_Load(object sender, EventArgs e)
        {
            txB_sumCredit.Text = trB_sumCredit.Value.ToString();
            txB_monthsCredit.Text = trB_monthsCredit.Value.ToString();
            panel_arrangeCredit.Visible = false;
            btn_TransferHelpChildrenPayments.Visible = false;

            var totalSum = "";
            var sum = "";
            DateTime dateRegistration = new DateTime();
            var idCredit = "";

            double creditTotalSumToCheack = 0;
            double creditSumToCheack = 0;

            var queryCheckCreditStatus = $"SELECT credit_total_sum, credit_sum FROM credits WHERE id_bank_card = (SELECT id_bank_card FROM bank_card WHERE bank_card_number = '{DataStorage.cardNumber}')";
            SqlCommand commandCheckCreditStatus = new SqlCommand(queryCheckCreditStatus, database.getConnection());
            database.openConnection();
            SqlDataReader reader3 = commandCheckCreditStatus.ExecuteReader();
            while (reader3.Read())
            {
                creditTotalSumToCheack = Convert.ToDouble(reader3[0]);
                creditSumToCheack = Convert.ToDouble(reader3[1]);
            }
            reader3.Close();

            if (creditSumToCheack >= creditTotalSumToCheack)
            {
                var queryDeleteCredits = $"DELETE FROM credits WHERE id_bank_card = (SELECT id_bank_card FROM bank_card WHERE bank_card_number = '{DataStorage.cardNumber}')";
                SqlCommand commandDeleteCredit = new SqlCommand(queryDeleteCredits, database.getConnection());
                commandDeleteCredit.ExecuteNonQuery();
            }

            var querySelectIdCard = $"SELECT credits.id_bank_card, credits.credit_total_sum, credits.credit_sum, credits.credit_date, " +
                    $"credits.id_credit FROM credits INNER JOIN bank_card on credits.id_bank_card = bank_card.id_bank_card WHERE " +
                    $"bank_card.bank_card_number = '{DataStorage.cardNumber}'";
            SqlCommand commandSelectCredit = new SqlCommand(querySelectIdCard, database.getConnection());
            SqlDataReader reader = commandSelectCredit.ExecuteReader();
            while (reader.Read())
            {
                totalSum = reader[1].ToString();
                sum = reader[2].ToString();
                dateRegistration = Convert.ToDateTime(reader[3].ToString());
                idCredit = reader[4].ToString();
            }
            reader.Close();

            SqlCommand commandSelectIdCard = new SqlCommand(querySelectIdCard, database.getConnection());
            adapter.SelectCommand = commandSelectIdCard;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                panel_arrangeCredit.Visible = true;
                btn_TransferHelpChildrenPayments.Visible = true;

                lbL_sum.Text = Math.Round(Convert.ToDouble(sum), 2).ToString();
                lbL_totalSum.Text = Math.Round(Convert.ToDouble(totalSum), 2).ToString();
                lbL_dateRegistration.Text = dateRegistration.ToShortDateString();

                double toPaySum = 0;
                DateTime repaymentDate = new DateTime();

                var querySelectRepayment = $"SELECT repayment_date, repayment_sum FROM credits WHERE id_credit = '{idCredit}'";
                SqlCommand commandSelectRepayment = new SqlCommand(querySelectRepayment, database.getConnection());
                SqlDataReader reader1 = commandSelectRepayment.ExecuteReader();
                while (reader1.Read())
                {
                    repaymentDate = Convert.ToDateTime(reader1[0].ToString());
                    toPaySum = Convert.ToDouble(reader1[1].ToString());
                }
                reader1.Close();
                database.closeConnection();

                lbL_repaymentSum.Text = Math.Round(toPaySum, 2).ToString();
                lbL_repaymentDate.Text = repaymentDate.ToShortDateString();
            }
        }

        void TxB_sumCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }

        }

        void TxB_monthsCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }

        }

        void CalculatorCredit()
        {
            double mothlyRate = 0.01;
            double sum = Convert.ToDouble(txB_sumCredit.Text);
            int numberOfMonths = Convert.ToInt32(txB_monthsCredit.Text);
            double result = sum * (mothlyRate + (mothlyRate / (Math.Pow(1 + mothlyRate, numberOfMonths) - 1)));
            lbL_sumCredit.Text = Math.Round(result, 2).ToString();
        }


        void TrB_sumCredit_Scroll(object sender, EventArgs e)
        {
            txB_sumCredit.Text = trB_sumCredit.Value.ToString();
            CalculatorCredit();
        }

        void TrB_monthsCredit_Scroll(object sender, EventArgs e)
        {
            txB_monthsCredit.Text = trB_monthsCredit.Value.ToString();
            CalculatorCredit();
        }

        void TxB_sumCredit_Click(object sender, EventArgs e)
        {
            if (txB_sumCredit.Text != "0" && !String.IsNullOrEmpty(txB_sumCredit.Text) && !String.IsNullOrEmpty(txB_monthsCredit.Text)
                && Convert.ToInt32(txB_sumCredit.Text) < 1000000)
                trB_sumCredit.Value = Convert.ToInt32(txB_sumCredit.Text);
            CalculatorCredit();
        }

        void TxB_monthsCredit_Click(object sender, EventArgs e)
        {
            if (txB_monthsCredit.Text != "0" && !String.IsNullOrEmpty(txB_sumCredit.Text) && !String.IsNullOrEmpty(txB_monthsCredit.Text)
                && Convert.ToInt32(txB_monthsCredit.Text) < 60)
                trB_monthsCredit.Value = Convert.ToInt32(txB_monthsCredit.Text);
            CalculatorCredit();
        }

        void Btn_arrangeCredit_Click(object sender, EventArgs e)
        {
            trB_sumCredit.Value = Convert.ToInt32(txB_sumCredit.Text);
            trB_monthsCredit.Value = Convert.ToInt32(txB_monthsCredit.Text);
            CalculatorCredit();

            DataStorage.bankCard = DataStorage.cardNumber;
            validations.ShowDialog();

            if (DataStorage.attempts > 0)
            {
                var totalSum = Convert.ToDouble(lbL_sumCredit.Text) * Convert.ToDouble(txB_monthsCredit.Text);
                DateTime creditDate = DateTime.Now;
                var repaymentDate = creditDate.AddMonths(1);
                var payment = lbL_sumCredit.Text;

                database.openConnection();
                var queryCredit = $"INSERT INTO credits(credit_total_sum, credit_sum, credit_date, id_bank_card) VALUES ('{totalSum}'," +
                            $"'0', '{creditDate}', (SELECT id_bank_card FROM bank_card WHERE bank_card_number = '{DataStorage.cardNumber}'))";
                var command1 = new SqlCommand(queryCredit, database.getConnection());
                command1.ExecuteNonQuery();

                var idCredit = "";
                var querySelectId = $"SELECT id_credit FROM credits WHERE id_bank_card =  (SELECT id_bank_card FROM bank_card WHERE bank_card_number = '{DataStorage.cardNumber}')";
                SqlCommand command3 = new SqlCommand(querySelectId, database.getConnection());
                SqlDataReader reader = command3.ExecuteReader();
                while (reader.Read())
                {
                    idCredit = reader[0].ToString();
                }
                reader.Close();

                var sum1 = txB_sumCredit.Text;
                var queryRepayment = $"update credits set repayment_date = '{repaymentDate}', repayment_sum = '{payment}' where id_credit = '{idCredit}'";
                var queryCardUpdate = $"update bank_card set bank_card_balance = bank_card_balance + '{sum1}' where bank_card_number = '{DataStorage.cardNumber}'";

                var command4 = new SqlCommand(queryCardUpdate, database.getConnection());
                var command2 = new SqlCommand(queryRepayment, database.getConnection());

                command4.ExecuteNonQuery();
                command2.ExecuteNonQuery();

                MessageBox.Show("Кредит оформлен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DateTime toPayDate = new DateTime();
                DateTime creditTakeDate = new DateTime();
                double creditSum = 0;
                double creditTotalSum = 0;
                double creditToPaySum = 0;

                var querySelectRepayment = $"select credit_date, credit_sum, credit_total_sum, repayment_date, repayment_sum from credits where id_bank_card = (select id_bank_card  from bank_card where bank_card_number = '{DataStorage.cardNumber}'";
                SqlCommand commandSelectRepayment = new SqlCommand(querySelectRepayment, database.getConnection());
                SqlDataReader readerUpdate = commandSelectRepayment.ExecuteReader();
                while (readerUpdate.Read())
                {
                    creditTakeDate = Convert.ToDateTime(readerUpdate[0].ToString());
                    creditSum = Convert.ToDouble(readerUpdate[1].ToString());
                    creditTotalSum = Convert.ToDouble(readerUpdate[2].ToString());
                    toPayDate = Convert.ToDateTime(readerUpdate[3].ToString());
                    creditToPaySum = Convert.ToDouble(readerUpdate[4].ToString());
                }
                readerUpdate.Close();
                database.closeConnection();

                lbL_dateRegistration.Text = creditTakeDate.ToShortDateString();
                lbL_sum.Text = Math.Round(creditSum, 2).ToString();
                lbL_totalSum.Text = Math.Round(creditTotalSum, 2).ToString();
                lbL_repaymentDate.Text = toPayDate.ToShortDateString();
                lbL_repaymentSum.Text = Math.Round(creditToPaySum, 2).ToString();

                btn_arrangeCredit.Visible = true;
                panel_arrangeCredit.Visible = true;
            }
        }

        void TxB_monthsCredit_TextChanged(object sender, EventArgs e)
        {
            if (txB_monthsCredit.Text != "0" && !String.IsNullOrEmpty(txB_sumCredit.Text) && !String.IsNullOrEmpty(txB_monthsCredit.Text)
                 && Convert.ToInt32(txB_monthsCredit.Text) < 61)
                trB_monthsCredit.Value = Convert.ToInt32(txB_monthsCredit.Text);
            CalculatorCredit();
        }

        void TxB_sumCredit_TextChanged(object sender, EventArgs e)
        {
            if (txB_sumCredit.Text != "0" && !String.IsNullOrEmpty(txB_sumCredit.Text) && !String.IsNullOrEmpty(txB_monthsCredit.Text)
                && Convert.ToInt32(txB_sumCredit.Text) < 1000000)
                trB_sumCredit.Value = Convert.ToInt32(txB_sumCredit.Text);
            CalculatorCredit();
        }

        void Btn_TransferHelpChildrenPayments_Click(object sender, EventArgs e)
        {
            DateTime toPayDate = Convert.ToDateTime(lbL_repaymentDate.Text);
            toPayDate = toPayDate.AddMonths(1);
            var sumToPay = lbL_repaymentSum.Text;
            double toPaySum = 0;
            DateTime dateRepay = new DateTime();
            bool error = false;

            database.openConnection();

            double cardBalanceCheck = 0;
            var queryCheckCard = $"select bank_card_balance from bank_card where bank_card_number = '{DataStorage.cardNumber}'";
            SqlCommand commandCheckCard = new SqlCommand(queryCheckCard, database.getConnection());
            database.openConnection();
            SqlDataReader reader = commandCheckCard.ExecuteReader();
            while (reader.Read())
            {
                cardBalanceCheck = Convert.ToDouble(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();

            double checkSum = Convert.ToDouble(lbL_sum.Text);
            double checkTotalSum = Convert.ToDouble(lbL_totalSum.Text);
            bool checkStatus = false;

            if (checkSum >= checkTotalSum)
            {
                MessageBox.Show("Кредит погашен!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                checkStatus = true;
            }

            if (checkStatus == false)
            {
                double paymentSum = Convert.ToDouble(lbL_repaymentSum.Text);
                if (paymentSum > cardBalanceCheck)
                {
                    MessageBox.Show("Ошибка. Недостаточно средст для совершения операции", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    error = true;
                }

                if (error == false)
                {
                    database.openConnection();
                    var queryPayCredit = $"update credits set repayment_date = '{toPayDate}', credit_sum = credit_sum + repayment_sum where id_bank_card = (select id_bank_card from bank_card where bank_card_number = '{DataStorage.cardNumber}'";
                    var queryPay = $"update bank_card set bank_card_balance = bank_card_balance - '{sumToPay}' where bank_card_number = '{DataStorage.cardNumber}'";

                    DateTime transactionDate = DateTime.Now;
                    var transactionNumber = "P";
                    for (int i = 0; i < 10; i++)
                    {
                        transactionNumber += Convert.ToString(rand.Next(0, 10));
                    }
                    var queryTransaction = $"insert into transactions(transaction_type, transaction_destination, transaction_date, transaction_number, transaction_value, id_bank_card) values('Кредит', 'Погашение кредита', '{transactionDate}', '{transactionNumber}', '{paymentSum}', (select id_bank_card from bank_card where bank_card_number = '{DataStorage.cardNumber}')";

                    var command = new SqlCommand(queryPayCredit, database.getConnection());
                    var command1 = new SqlCommand(queryPay, database.getConnection());
                    var command2 = new SqlCommand(queryTransaction, database.getConnection());

                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();

                    var querySelectRepayment = $"select repayment_date, credit_sum from credits where id_bank_card = (select id_bank_card from bank_card where bank_card_number = '{DataStorage.cardNumber}'";
                    SqlCommand commandSelectRepayment = new SqlCommand(querySelectRepayment, database.getConnection());
                    SqlDataReader reader1 = commandSelectRepayment.ExecuteReader();
                    while (reader1.Read())
                    {
                        dateRepay = Convert.ToDateTime(reader1[0].ToString());
                        toPaySum = Convert.ToDouble(reader1[1].ToString());
                    }
                    reader1.Close();
                    database.closeConnection();

                    lbL_sum.Text = Math.Round(toPaySum, 2).ToString();
                    lbL_repaymentDate.Text = dateRepay.ToShortDateString();

                }
            }
        }
    }
}
