using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Page2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page2 : Page
    {
        static public string readData = null;
        Thread clThread;
        public Page2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] outBytes = Encoding.Unicode.GetBytes(sendBoard.Text + "$");
            login.serverStream.Write(outBytes, 0, outBytes.Length);// outByte의 길이만큼의 데이터를 네트워크 스트림에 저장
            login.serverStream.Flush();// 버퍼에 담아뒀던 데이터를 방류.
            sendBoard.Text = null;
        }
        private void getMessage()
        {
            string returndata;
            while (true)
            {
                login.serverStream = login.client.GetStream();
                int buffSize = 0;
                buffSize = login.client.ReceiveBufferSize;
                byte[] inStream = new byte[login.client.ReceiveBufferSize];
                login.serverStream.Read(inStream, 0, buffSize);
                returndata = Encoding.Unicode.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                readData = "" + returndata;
                msg();
            }
        }
        private void msg()
        {
            if (textBorad.Dispatcher.CheckAccess())
            {
                textBorad.Text += "\n" + ">>" + readData;
            }
            else
            {
                textBorad.Dispatcher.BeginInvoke(new Action(() => textBorad.Text += "\n" + ">>" + readData));
                return;
            }
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            clThread = new Thread(getMessage);
            clThread.Start();
            textBorad.Text = "서로에게 상처되는 말은 하지맙시다.";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var confirmResult = MessageBox.Show("채팅방을 나가시겠습니까?","알림",MessageBoxButton.YesNo);
            if (confirmResult == MessageBoxResult.Yes)
            {
                clThread.Abort();
                NavigationService.Navigate(new Uri("home.xaml", UriKind.Relative));
            }
            else
            {
               
            }
            }

        private void sendBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // 엔터키를 입력받으면 실행
            {
                sendButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
