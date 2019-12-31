using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sm_chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Username.Text!="" && Password.Password!="")
            {

            }
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

    }
}
