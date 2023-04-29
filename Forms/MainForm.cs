using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class MainForm : Form
    {
        DataBaseConnection database = new DataBaseConnection();
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_adding_card_Click(object sender, System.EventArgs e)
        {
            AddBankCard addBankCard = new AddBankCard();
            addBankCard.ShowDialog();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            lbL_card_number.BringToFront();
            lbL_card_number.Text = "";
            lbL_cardDate.BringToFront();
            lbL_cardDate.Text = "";
            lbL_client_FIO.BringToFront();
            lbL_client_FIO.Text = "";
            picB_visa.Visible = false;
            picB_masterCard.Visible = false;

            var queryMyCards = $"SELECT id_bank_card, bank_card_number from bank_card where id_client = '{DataStorage.idClient}'";
            SqlDataAdapter commandMyCards = new SqlDataAdapter(queryMyCards, database.getConnection());
            database.openConnection();
            DataTable cards = new DataTable();
            commandMyCards.Fill(cards);
            cmb_card.DataSource = cards;
            cmb_card.ValueMember = "id_bank_card";
            cmb_card.DisplayMember = "bank_card_number";
            database.closeConnection();

            selectBankCard();
        }

        private void selectBankCard()
        {
            lbL_card_number.Text = "";
            string paymentSystem = "";
            string querySelectCard = $"SELECT bank_card_number, bank_card_cvv_code, CONCAT(FORMAT(bank_card_date, '%M'), '/', FORMAT(bank_card_date, '%y')), bank_card_paymentSystem, bank_card_balance, bank_card_currency from bank_card where bank_card_number = '{cmb_card.GetItemText(cmb_card.SelectedItem)}'";
            SqlCommand command = new SqlCommand(querySelectCard, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var cardNumber = reader[0].ToString();

                int tmp = 0;
                int tmp1 = 4;
                for (int m = 0; m < 4; m++)
                {
                    for (int n = tmp; n < tmp1; n++)
                    {
                        lbL_card_number.Text += cardNumber[n].ToString();
                    }
                    lbL_card_number.Text += " ";
                    tmp += 4;
                    tmp1 += 4;
                }

                lbL_cardCvv.Text = reader[1].ToString();
                lbL_cardDate.Text = reader[2].ToString();
                paymentSystem = reader[3].ToString();
                lbL_balanceCard.Text = Math.Round(Convert.ToDouble(reader[4]), 2).ToString();
                lbl_currency.Text = reader[5].ToString();
                DataStorage.cardCVV = lbL_cardCvv.Text;
                lbL_cardCvv.Text = "***";
            }
            reader.Close();

            if (paymentSystem == "Visa")
            {
                picB_visa.Visible = true;
                picB_masterCard.Visible = false;
            }
            else
            {
                picB_visa.Visible = false;
                picB_masterCard.Visible = true;
            }
        }

        private void btn_udpate_Click(object sender, EventArgs e)
        {
            var queryMyCards = $"select id_bank_card, bank_card_number from bank_card where id_client = '{DataStorage.idClient}'";
            SqlDataAdapter commandMyCards = new SqlDataAdapter(queryMyCards, database.getConnection());
            database.openConnection();
            DataTable cards = new DataTable();
            commandMyCards.Fill(cards);
            cmb_card.DataSource = cards;
            cmb_card.ValueMember = "id_bank_card";
            cmb_card.DisplayMember = "bank_card_number";
            database.closeConnection();

            selectBankCard();
        }

        private void lbL_cardCvv_Click(object sender, EventArgs e)
        {
            if (lbL_cardCvv.Text == "***")
            {
                lbL_cardCvv.Text = DataStorage.cardCVV;
            }
            else
                lbL_cardCvv.Text = "***";
        }

        private void btn_MoneyTransferCard_Click(object sender, EventArgs e)
        {
            MoneyTransferCardForm moneyTransferCardForm = new MoneyTransferCardForm();
        }

        private void picB_user_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
        }

        private void lbL_HistoryTransactions_Click(object sender, EventArgs e)
        {
            HistoryTransactions historyTransactions = new HistoryTransactions();
            historyTransactions.Show();
        }

        private void btn_transferMobilePhone_Click(object sender, EventArgs e)
        {
            MobilePhoneTransferForm mobilePhoneTransferForm = new MobilePhoneTransferForm();
            DataStorage.cardNumber = cmb_card.GetItemText(cmb_card.SelectedItem);
            DataStorage.phoneNumber = txB_transferMobilePhone.Text;
            txB_transferMobilePhone.Text = "";
            mobilePhoneTransferForm.Show();
        }

        private void btn_communalPayments_Click(object sender, EventArgs e)
        {
            CommunalPayments communalPayments = new CommunalPayments();
            DataStorage.cardNumber = cmb_card.GetItemText(cmb_card.SelectedItem);
            communalPayments.Show();
        }

        private void btn_internetTV_Click(object sender, EventArgs e)
        {
            InternetTVPayments internetTVPayments = new InternetTVPayments();
            DataStorage.cardNumber = cmb_card.GetItemText(cmb_card.SelectedItem);
            internetTVPayments.Show();
        }
    }
}
