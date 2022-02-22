using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        string _server = "localhost";
        int _port = 3306;
        string _database = "matching";
        string _id = "root";
        string _pw = "YES";
        string _connectionAddress = "";

        private DateTime _SelectedDateTime = DateTime.Now;




        Button[] btn = new Button[16];
        static int margin = 10;
        static int btnWidth = 40;
        static int btnHeight = 60;
        static int btnSide = 10;
        public Window2()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
            SizeToContent = SizeToContent.WidthAndHeight;

            canvas1.Width = 7 * margin + 7 * btnWidth + 3 * btnSide;
            canvas1.Height = 7 * margin + 7 * btnHeight + 3 * btnSide;

            for (int i = 0; i < 16; i++)
            {
                btn[i] = new Button();
                btn[i].Width = btnWidth;
                btn[i].Height = btnHeight;
                btn[i].Content = i.ToString();
                btn[i].Click += btn_Click;

                Canvas.SetLeft(btn[i], margin + i % 4 * (btnWidth + btnSide));
                Canvas.SetTop(btn[i], margin + i / 4 * (btnHeight + btnSide));

                canvas1.Children.Add(btn[i]);
            }

        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(btn.Content + "번 좌석이 선택되었습니다");
            seat.Text = $"{btn.Content}";

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();
                    string insertQuery = string.Format("INSERT INTO reserve (Time, Venue, Date, Seat) VALUES ('{0}', '{1}','{2}','{3}');", time.Text, venue.Text, Date.Text, seat.Text);
                    MySqlCommand command = new MySqlCommand(insertQuery, mysql);
                    if (command.ExecuteNonQuery() != 1)
                        MessageBox.Show("Failed to insert data.");

                    time.Text = "";
                    venue.Text = "";
                    Date.Text = "";
                    seat.Text = "";


                }
                MessageBox.Show("예약이 완료되었습니다.");
                this.Close();

            }
            catch (Exception error)

            {
                MessageBox.Show("모든 항목에 채워주세요.");
            }
        }
    }
}


