using BankApp.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankApp.Forms
{
    public partial class AddBankCard : Form
    {
        Random rand = new Random();

        DataBaseConnection database = new DataBaseConnection();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        //метод перетягивания винформ без бордера
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int IParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        public AddBankCard()
        {
            InitializeComponent();
        }

        void Btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddBankCard_Load(object sender, EventArgs e)
        {
            cmB_type_card.SelectedIndex = 0;
            cmB_currency.SelectedIndex = 0;
            cmB_payment_system.SelectedIndex = 0;
        }

        private void btn_save_client_Click(object sender, EventArgs e)
        {
            var type_card = cmB_type_card.GetItemText(cmB_type_card.SelectedItem);
            var currency = cmB_currency.GetItemText(cmB_currency.SelectedItem);
            var payment_system = cmB_payment_system.GetItemText(cmB_payment_system.SelectedItem);
            var cardNumber = "";
            var cardPin = txB_cardPin.Text;
            var cvvCode = "";
            bool isCardFree = false;
            DateTime dateTime = DateTime.Now;
            DateTime cardDate1 = dateTime.AddYears(4);
            var cardDate = cardDate1.ToString("yyyy-MM-dd");


            for (int i = 0; i < 3; i++)
            {
                cvvCode += Convert.ToString(rand.Next(0, 10));
            }

            do //генерируем номер карты до тех пор пока не окажется что такой номер карты доступен
            {
                if (payment_system == "Visa")
                {
                    cardNumber += "4";
                    for (int i = 0; i < 15; i++)
                    {
                        cardNumber += Convert.ToString(rand.Next(0, 10));
                    }
                }
                else
                {
                    cardNumber += "5";
                    for (int i = 0; i < 15; i++)
                    {
                        cardNumber += Convert.ToString(rand.Next(0, 10));
                    }
                }

                var queryCheckCardNumber = $"SELECT * FROM bank_card where bank_card_number = '{cardNumber}'";

                SqlCommand command = new SqlCommand(queryCheckCardNumber, database.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    isCardFree = true;
                }
            } while (isCardFree == false);

            var queryAddNewCard = $"INSERT INTO bank_card(bank_card_type, bank_card_number, bank_card_cvv_code, bank_card_currency, bank_card_paymentSystem, bank_card_date, id_client, bank_card_pin) values ('{type_card}', '{cardNumber}', '{cvvCode}', '{currency}', '{payment_system}', '{cardDate}', '{DataStorage.idClient}', '{cardPin}')";
            SqlCommand commandAddNewCard = new SqlCommand(queryAddNewCard, database.getConnection());
            database.openConnection();
            commandAddNewCard.ExecuteNonQuery();
            database.closeConnection();

            MessageBox.Show("Карта успешно создана", "Данные сохранены", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void AddBankCard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
    
