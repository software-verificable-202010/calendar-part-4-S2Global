using System.Windows;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for NewUserCalendar.xaml
    /// </summary>
    public partial class SecondSignInWindow : Window
    {

        #region Methods
        public SecondSignInWindow()
        {
            InitializeComponent();
        }

        public void SignIn(object sender, RoutedEventArgs e)
        {
            if(usernameBox.Text.Length > 0)
            {
                User userSignIn = new User(usernameBox.Text);
                userSignIn.Save();
                SecondWindow secondWindow = new SecondWindow(userSignIn.Username);
                secondWindow.UpdateSecondWindow();
                secondWindow.Show();
                this.Close();
            }
        }
        #endregion
    }
}
