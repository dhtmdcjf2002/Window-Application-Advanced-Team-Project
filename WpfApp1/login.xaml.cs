using MySql.Data.MySqlClient;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfApp1
{
    /// <summary>
    /// login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class login : Page
    {

        string _server = "localhost";
        int _port = 3306;
        string _database = "matching";
        string _id = "root";
        string _pw = "YES";
        string _connectionAddress = "";

        static public TcpClient client = new TcpClient();
        static public NetworkStream serverStream = default(NetworkStream);
        static public string readData = null;

        public login()
        {
            InitializeComponent();
            _connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlConnection mysql = new MySqlConnection(_connectionAddress);
                string selectQuery = "select * from member " + "where Id = '" + IDBox.Text + "' and Password = '" + this.PWBox.Password + "';";

                MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                MySqlDataReader myReader;
                mysql.Open();
                myReader = command.ExecuteReader();
                int count = 0;

                while (myReader.Read())
                {
                    count += 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("로그인 되었습니다.");
                    NavigationService.Navigate(new Uri("page1.xaml", UriKind.Relative));
                }

                else if (count > 1)
                {
                    MessageBox.Show("중복");
                    return;
                }
                else
                {
                    MessageBox.Show("다시 로그인 해주세요.");
                    return;
                }
            }
            catch (Exception error)
            {

            }

            try
            {
                client.Connect("127.0.0.1", 7777);//내부 아이피의 7777포트로 연결을 요청
                serverStream = client.GetStream();// 네트워크 스트림에 클라이언트의 스트림을 저장.

                byte[] outBytes = Encoding.Unicode.GetBytes(IDBox.Text + "$");//장소와 성별을 유니코드로 전환 
                serverStream.Write(outBytes, 0, outBytes.Length);// outByte의 길이만큼의 데이터를 네트워크 스트림에 저장
                serverStream.Flush();// 버퍼에 담아뒀던 데이터를 방류.

                //NavigationService.Navigate(new Uri("page1.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                MessageBox.Show("서버연결에 오류가 있습니다. 잠시후 다시 시도해주세요.");
            }

        }

        private void IDBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            IDBox.Foreground = Brushes.Black;
        }
    }
}
