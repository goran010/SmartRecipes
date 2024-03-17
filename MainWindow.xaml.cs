using System.Windows;
using RecipeApp.Pages;
using RecipeApp.View.UI;

namespace RecipeApp
    {
    public partial class MainWindow : Window
        {
        private HomePage homePage;
        private List<string[]> recipes;
        public MainWindow ()
            {
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;

            // Subscribe to navigation events
            Navigation.NavigateToSearchRecipePage += NavigateToSearchRecipePage;
            Navigation.NavigateToAboutPage += NavigateToAboutPage;
            Navigation.NavigateToHomePage += NavigateToHomePage;
            Navigation.NavigateToAddRecipe += NavigateToAddRecipe;

            // Instantiate HomePage
            homePage = new HomePage();

            // Subscribe to events
            homePage.UpdateMealsList += HomePage_UpdateMealsList;

            // Show HomePage in the Frame
            mainFrame.NavigationService?.Navigate(homePage);

            Loaded += MainWindow_Loaded;

            }

        private void MainWindow_Loaded ( object sender, RoutedEventArgs e )
            {
            // Initialize the recipes list
            recipes = [];
            }

        private void HomePage_UpdateMealsList ( List<string[]> recipes )
            {
            // Update the recipes list with new recipes
            mainFrame.NavigationService?.Navigate(new ContentRecipesPage(recipes));
            }

        private void NavigateToSearchRecipePage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the search recipe page and pass the recipes
                mainFrame.NavigationService?.Navigate(new ContentRecipesPage(recipes));
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void NavigateToAboutPage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the about page
                mainFrame.NavigationService?.Navigate(new AboutPage());
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void NavigateToHomePage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the home page
                mainFrame.NavigationService?.Navigate(new HomePage());
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        private void NavigateToAddRecipe ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the home page
                mainFrame.NavigationService?.Navigate(new AddRecipePage());
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        }
    }
