using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Loading.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Loading : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Loading()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


        }

        void timer_Tick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("page2.xaml", UriKind.Relative));
            timer.Stop();
        }
    }
}