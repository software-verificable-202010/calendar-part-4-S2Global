using System.Windows;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        #region Methods
        public SignInWindow()
        {
            InitializeComponent();
        }

        public void SignIn(object sender, RoutedEventArgs e)
        {
            if(usernameBox.Text.Length > 0)
            {
                User userSignIn = new User(usernameBox.Text);
                userSignIn.Save();
                MainWindow mainWindow = new MainWindow(userSignIn.Username, false);
                mainWindow.RunMainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        #endregion
    }
}
