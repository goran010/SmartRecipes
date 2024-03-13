using System.Windows;
using System.Windows.Controls;
using RecipeApp.Pages;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.NavigationService != null)
            {
                try
                {
                    mainFrame.NavigationService.Navigate(new Pages.ContentRecipesPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("NavigationService is null.");
            }
        }

    }
}

