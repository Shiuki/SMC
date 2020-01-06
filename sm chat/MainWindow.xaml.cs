using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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


            var myTimer = new Timer();
            // Tell the timer what to do when it elapses
            myTimer.Elapsed += new ElapsedEventHandler(getsong);
            // Set it to go off every five seconds
            myTimer.Interval = 5000;
            // And start it        
            myTimer.Enabled = true;

        }
        string token = "";
        string name = "";
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            login();
        }
        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
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
                set_settings();
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
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SMC");

                //if it does exist, retrieve the stored values  
                if (key != null)
                {
                    token = (key.GetValue("token").ToString());
                    name = (key.GetValue("username").ToString());
                    key.Close();
                    if (autologin())
                    {

                    }

                }
                //ping successful
                emaillabel.Visibility = Visibility.Hidden;
                Email.Visibility = Visibility.Hidden;
                registerbtn.Visibility = Visibility.Hidden;
                if (!logged_in)
                {
                    //try to login
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
        string Settings;

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
            handleplay();
        }
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        private void handleplay()
        {

            wplayer.URL = "https://kuronegai.radioca.st/;listen.pls?sid=1";

          
            if (playing)
            {
                BitmapImage image = new BitmapImage(new Uri("playbutton.png", UriKind.Relative));
                pausebutton_png.Source = image;
                playing = false;

                wplayer.controls.stop();



            }
            else
            {
                BitmapImage image = new BitmapImage(new Uri("pausebutton.png", UriKind.Relative));
                pausebutton_png.Source = image;
                playing = true;
                wplayer.controls.play();
              
 
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://smield.host/SMC_api/settings.php?";
          
            string col = colorfield.Text;

            int music = 0;
            int startup = 0;
            if (musicbtn.IsChecked == true)
            {
                music = 1;
            }
           
            if (onboot.IsChecked == true)
            {
                startup = 1;
            }
            string pars = $"email={music}&color={col}&startup={startup}&username={name}&cog={token}&update=yes";
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
        private bool autologin()
        {
            var url = "https://smield.host/SMC_api/index.php";
            string pars = $"username={name}&password={token}&usingtoken=true";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                Result = wc.UploadString(url, pars);
            }
            try
            {
                if (Result.StartsWith("login"))
                {

                    set_settings();
                    tabControl.SelectedIndex = tabControl.Items.IndexOf(settingstab);
                    logged_in = true;
                    return true;
                }

            }
            catch { }
        
                return false;
           

        }
        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (onboot.IsChecked == true)
                rk.SetValue("SMC",

System.Reflection.Assembly.GetExecutingAssembly().Location);
            else
                rk.DeleteValue("SMC", false);

        }
        private void set_settings()
        { if (token != "" && name !="")
            {
                try
                {
                    var url2 = "https://smield.host/SMC_api/settings.php?";
                    string pars2 = $"username={name}&cog={token}&update=no";

                    using (WebClient wc2 = new WebClient())
                    {
                        wc2.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        Settings = wc2.UploadString(url2, pars2);
                    }
                    //MessageBox.Show(Settings);

                    colorfield.Text= Between(Settings, "color: ", " startup");
                    int music = Convert.ToInt32(Between(Settings, "music: ", " color"));
                    if(music == 1)
                    {
                        musicbtn.IsChecked = true;
                        playing = false;
                        handleplay();
                    }
                    int startup = Convert.ToInt32(Between(Settings, "startup: ", "?"));
                    if (startup == 1)
                    {
                        onboot.IsChecked = true;
                        SetStartup();
                    }

                }
                catch { }
            }
        }
        private void login()
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

                    token = Between(Result, "token:", "-");
                    name = Between(Result, "name:", "?");

                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SMC");

                    //storing the values  
                    key.SetValue("token", token);
                    key.SetValue("username", name);
                    key.Close();
                    if (Result.StartsWith("login"))
                    {
                        MessageBox.Show("Login successful");
                    set_settings();
                    tabControl.SelectedIndex = tabControl.Items.IndexOf(settingstab);
                }
                    else
                    {
                        MessageBox.Show(Result);
                    }
                }


                catch { MessageBox.Show(Result); }
            
           
        }

        private void colorfield_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString(colorfield.Text);
                colorlaber.Foreground = brush;
            }
            catch { }
        }

        private void onboot_Checked(object sender, RoutedEventArgs e)
        {
            SetStartup();
        }

        private void onboot_Unchecked(object sender, RoutedEventArgs e)
        {
            SetStartup();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            token = "";
            name = "";

            Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\SMC",false);
            tabControl.SelectedIndex = tabControl.Items.IndexOf(logintab);
            logged_in = false;

        }

        private void songlabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.youtube.com/results?search_query="+songlabel.Content);
        }
        private void getsong(object source, ElapsedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://kuronegai.radioca.st/stats?json=1");
                dynamic data = JObject.Parse(json);
                Application.Current.Dispatcher.Invoke(new Action(() => songlabel.Content = (data.songtitle)));
                wc.Dispose();
            }
        }
    }
}
