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

namespace RecipeApp.View.UI
{
    public partial class Navigation : UserControl
    {
        public event EventHandler NavigateToHomePage = delegate { };
        public event EventHandler NavigateToSearchRecipePage = delegate { };
        public event EventHandler NavigateToAboutPage = delegate { };
        public Navigation()
        {
            InitializeComponent();        
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
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
        private void DiscoverButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigateToSearchRecipePage?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}");
            }
        }

        private void AboutUsButton_Click(object sender, RoutedEventArgs e)
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

        private void SomethingButton_Click(object sender, RoutedEventArgs e)
        {    
            try
            {
                NavigateToSearchRecipePage?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}");
            }
        }
    }
}
