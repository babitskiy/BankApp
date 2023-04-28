using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class HistoryTransactions : Form
    {
        DataBaseConnection database = new DataBaseConnection();
        public HistoryTransactions()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        void HistoryTransactions_Load(object sender, EventArgs e)
        {
            var querySelectTransaction = $"SELECT transaction_type, transaction_destination, transaction_date, transaction_number, transaction_money, transaction_currency " +
            $"from transactions inner join bank_card on transactions.id_bank_card  = bank_card.id_bank_card inner join client on client.id_client = bank_card.id_client " +
            $"where client.id_client = '{DataStorage.idClient}'";

            SqlCommand command = new SqlCommand(querySelectTransaction, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem lvitem = new ListViewItem(reader[0].ToString());
                lvitem.SubItems.Add(reader[1].ToString());
                lvitem.SubItems.Add(reader[2].ToString());
                lvitem.SubItems.Add(reader[3].ToString());
                lvitem.SubItems.Add(reader[4].ToString());
                listView1.Items.Add(lvitem);
            }
            reader.Close();

            listView1.Sort();
        }
    }
}
