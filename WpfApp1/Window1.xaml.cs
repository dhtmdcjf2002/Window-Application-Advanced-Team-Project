using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{  ///중간 
   /// <summary>
   /// Window1.xaml에 대한 상호 작용 논리
   /// </summary>
    public partial class Window1 : Window
    {
        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "matching"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "YES"; // 비밀번호
        string _connectionAddress = "";
        string gender;

        public Window1()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            if (radiobutton1.IsChecked == true)
            {
                gender = "남자";
            }
            else 
            { gender = "여자"; }
            

                try
                {
                if (id.Text != null && pw.Password != null && name.Text != null && gender.ToString() != null && combo.SelectedItem != null)
                {
                    using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                    {
                        mysql.Open();
                        string insertQuery = string.Format("INSERT INTO member (Id, Password, Name, gender, S_Number) VALUES ('{0}', '{1}','{2}','{3}', '{4}');", id.Text, pw.Password, name.Text, gender, combo.SelectedItem);
                        MySqlCommand command = new MySqlCommand(insertQuery, mysql);
                        if (command.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed to insert data.");

                        id.Text = "";
                        pw.Password = "";
                        name.Text = "";
                        gender.ToString();
                        combo.SelectedItem = "";

                        
                    }
                    MessageBox.Show("회원가입에 성공하였습니다.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("모든 항목을 입력해주세요.");
                }

                
                }
                catch (Exception error)

                {

                }
            }
            

            private void Border_Loaded(object sender, RoutedEventArgs e)
            {
                combo.Items.Add("21학번");
                combo.Items.Add("20학번");
                combo.Items.Add("19학번");
                combo.Items.Add("18학번");
                combo.Items.Add("그 외");
            }
        }

    }

