using BankApp.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class Validations : Form
    {
        DataBaseConnection database = new DataBaseConnection();
        public Validations()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Validations_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void Validations_MouseMove(object sender, MouseEventArgs e)
        {

        }

        void Validations_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void Validations_Load(object sender, EventArgs e)
        {
            txB_cardPin.Select();
        }

        void Btn_confirm_card_pin_Click(object sender, EventArgs e)
        {
            int attemps = 3;
            int cardPin = Convert.ToInt32(txB_cardPin.Text);
            int pin = 0;

            var queryCheckPin = $"select bank_card_pin from bank_card where bank_card_number = '{DataStorage.bankCard}'";
            SqlCommand command = new SqlCommand(queryCheckPin, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pin = Convert.ToInt32(reader[0]);
            }
            reader.Close();

            if (cardPin == pin)
            {
                MessageBox.Show("Операция подтверждена", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                DataStorage.attempts = attemps;
            }
            else
            {
                MessageBox.Show("Ошибка. Неверный PIN", "Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (attemps > 0)
                    attemps--;
                else
                {
                    DataStorage.attempts = attemps;
                    MessageBox.Show("У вас закончились попытки", "Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }
    }
}
