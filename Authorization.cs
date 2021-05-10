using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExamTrainP1
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            //Изменение высоты для скрытого пароля//
            this.passBox.AutoSize = false;
            this.passBox.Size = new Size(this.passBox.Size.Width,40);
        }
        //Кнопка Войти//
        private void EnterButton_Click(object sender, EventArgs e)
        {
            int Count = 0;
            string query = "Select count(*) from UsersDB where Login = '" + logBox.Text + "' and Password = '" + passBox.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                Count = Convert.ToInt32(cmDB.ExecuteScalar());
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка авторизации. Попробуйте еще раз");
                MessageBox.Show(ex.Message);
            }
            //Если значение есть в БД(count > 0), то пропустить(открыть главное меню),если (Count < 0), то нет //
            if(Count > 0 )
            {
                MainMenu win = new MainMenu();
                win.Show();
                this.Hide();
            }
            //Если значение Count = 0, то выдать ошибку//
            if(Count == 0)
            {
                MessageBox.Show("Ошибка авторизации. Попробуйте еще раз");
            }
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }
    }
}
