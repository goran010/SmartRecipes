using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.View.UI
    {
    public partial class Navigation : UserControl
        {
        public event EventHandler NavigateToHomePage = delegate { };
        public event EventHandler NavigateToContentRecipesPage = delegate { };
        public event EventHandler NavigateToAboutPage = delegate { };
        public Navigation ()
            {
            InitializeComponent();
            }

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
            {

            try
                {
                NavigateToHomePage?.Invoke(this, EventArgs.Empty);
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        private void DiscoverButton_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                NavigateToContentRecipesPage?.Invoke(this, EventArgs.Empty);
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void AboutUsButton_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                NavigateToAboutPage?.Invoke(this, EventArgs.Empty);
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }


        }
    }
