using BankApp.Classes;
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

        }
    }
}
