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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            getInfo(listView1);
        }
        //Функция обновления списка при входе в главное меню//
        void getInfo(ListView List)
        {
            listView1.Items.Clear();
            string query = "select Skate.Id_Skate , Skate.NameSkate, Status.NameStatus from Skate join Status on Skate.id_Status = Status.Id_Status order by Id_Skate ASC;";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            MySqlDataReader rd;
            try
            {
                conn.Open();
                rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string[] row = { rd.GetString(0), rd.GetString(1), rd.GetString(2) };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключеня к БД");
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            AddNewUser win = new AddNewUser();
            win.Show();
            this.Hide();
        }

        private void JumpOnAuto_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization win = new Authorization();
            win.Show();
            this.Hide();
        }

        private void NewRecordButton_Click(object sender, EventArgs e)
        {
            AddNewRecord win = new AddNewRecord();
            win.Show();
            this.Hide();
        }

        private void DeleteRecordButton_Click(object sender, EventArgs e)
        {
            if (deleteBox.Text == "")
            {
                MessageBox.Show("Полe должно быть заполнено!");
            }
            else
            {
                string query = "delete from Skate where Id_Skate = '" + deleteBox.Text + "';";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    MessageBox.Show("Запись была успешно удалена");
                    getInfo(listView1);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (findBox.Text == "")
            {
                MessageBox.Show("Полe должно быть заполнено!");
            }
            else
            {
                listView1.Items.Clear();
                string query = "select Skate.Id_Skate , Skate.NameSkate, Status.NameStatus from Skate join Status on Skate.id_Status = Status.Id_Status where Id_Skate = '" + findBox.Text + "';";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                MySqlDataReader rd;
                try
                {
                    conn.Open();
                    rd = cmDB.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            string[] row = { rd.GetString(0), rd.GetString(1), rd.GetString(2) };
                            var listViewItem = new ListViewItem(row);
                            listView1.Items.Add(listViewItem);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключеня к БД");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateRecord win = new UpdateRecord();
            win.Show();
        }
    }
}
