using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Page3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page3 : Page
    {
        string _server = "localhost";
        int _port = 3306;
        string _database = "matching";
        string _id = "root";
        string _pw = "YES";
        string _connectionAddress = "";

        public Page3()
        {
            InitializeComponent();

            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
            MySqlConnection mysql = new MySqlConnection(_connectionAddress);
            try
            {
                mysql.Open();
                string selectQuery = "select * from reserve";
                MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "LoadDataBinding");
                dataGridCustomers.DataContext = dataset;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            finally
            {
                mysql.Close();
            }
        }
    }
}
