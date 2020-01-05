using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
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

namespace sm_chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public string version = "1.0";









        public static bool logged_in;
        public MainWindow()
        {
            //donot touch this ~nya
            InitializeComponent();
        }
     
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            //sending login request to the api
            var url = "https://smield.host/SMC_api/index.php";
            string pars = $"username={Username.Text}&password={Password.Password}";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                Result = wc.UploadString(url, pars);
            }
            try
            {
                
                MessageBox.Show(Result);
            }
            catch { }
        }
        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            //sending register to the api
            var url = "https://smield.host/SMC_api/insert.php?";
            string pars = $"username={Username.Text}&password={Password.Password}&email={Email.Text}";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                Result = wc.UploadString(url, pars);
            }
            try
            {

                MessageBox.Show(Result);
            }
            catch { }

        }
        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            namelabel.Visibility = Visibility.Hidden;
        }

        private void Username_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(Username.Text=="")
            {
                namelabel.Visibility = Visibility.Visible;
            }
        }

        private void Password_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Password.Password == "")
            {
                passlabel.Visibility = Visibility.Visible;
            }
        }

        private void Password_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            passlabel.Visibility = Visibility.Hidden;
        }
        public string wef;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string Uri = $"https://smield.host/SMC_api/ping?{version}";  

            using (WebClient wc = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3;
                wef = wc.DownloadString(Uri);
            }
     

            if (wef.Length > 0 && wef == version)
            {
               
                //ping successful
                emaillabel.Visibility = Visibility.Hidden;
                Email.Visibility = Visibility.Hidden;
                registerbtn.Visibility = Visibility.Hidden;
                if (!logged_in)
                {
                    //show login screen
                    tabControl.SelectedIndex = tabControl.Items.IndexOf(logintab);
                }
            }
            else
            {
                MessageBox.Show("New update is available!");
                Process.Start("https://smield.host/SMC.exe");
                this.Close();
            }
           
        }

 

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if(register.IsChecked is true)
            {
                namelabel.Content = "Name";
                loginbtn.IsEnabled = false;
                emaillabel.Visibility = Visibility.Visible;
                Email.Visibility = Visibility.Visible;
                registerbtn.Visibility = Visibility.Visible;
            }
             else
            {
                namelabel.Content = "Name or Email";
                loginbtn.IsEnabled = true;
                emaillabel.Visibility = Visibility.Hidden;
                Email.Visibility = Visibility.Hidden;
                registerbtn.Visibility = Visibility.Hidden;
            }
        }

        private void Email_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            emaillabel.Visibility = Visibility.Hidden;
        }

        private void Email_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Email.Text == "")
            {
                emaillabel.Visibility = Visibility.Visible;
            }
        }
        string Result;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //window dragging thingy
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        public bool playing = false;
        private void pausebutton_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (playing)
            {
                BitmapImage image = new BitmapImage(new Uri("playbutton.png", UriKind.Relative));
                pausebutton_png.Source = image;
                playing = false;
            }
            else
            {
                BitmapImage image = new BitmapImage(new Uri("pausebutton.png", UriKind.Relative));
                pausebutton_png.Source = image;
                playing = true;
            }
        }
    }
}
