using System.Windows;
using RecipeApp.Pages;
using RecipeApp.View.UI;

namespace RecipeApp
    {
    public partial class MainWindow : Window
        {
        private readonly HomePage homePage;
        private List<string[]> recipes = [];

        public MainWindow ()
            {
            InitializeComponent();

            // Instantiate HomePage
            homePage = new HomePage();

            // Show HomePage in the Frame
            mainFrame.NavigationService?.Navigate(homePage);

            // Subscribe to events
            homePage.UpdateMealsList += HomePage_UpdateMealsList;
            homePage.NavigateToAddRecipe += NavigateToAddRecipePage;

            // Subscribe to navigation events
            Navigation.NavigateToContentRecipesPage += NavigateToContentRecipesPage;
            Navigation.NavigateToAboutPage += NavigateToAboutPage;
            Navigation.NavigateToHomePage += NavigateToHomePage;
            Navigation.NavigateToAddRecipe += NavigateToAddRecipePage;

            Loaded += MainWindow_Loaded;

            }

        private void MainWindow_Loaded ( object? sender, RoutedEventArgs e )
            {
            ArgumentNullException.ThrowIfNull(sender);
            // Initialize the recipes list
            recipes = [];
            }

        public void HomePage_UpdateMealsList ( List<string[]> recipesList )
            {
            // Update the recipes list with new recipes
            mainFrame.Content = new ContentRecipesPage(recipesList);
            }

        public void NavigateToContentRecipesPage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the content recipes page and pass the recipes
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
                // Navigate to the existing instance of homePage
                mainFrame.Content = homePage;
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void NavigateToAddRecipePage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the about page
                mainFrame.NavigationService?.Navigate(new AddRecipePage());
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        public void NavigateToShowRecipePage ( object[] recipeDetails )
            {
            try
                {
                // Extract recipe details from the array
                string country = recipeDetails[0] as string ?? "";
                string recipeName = recipeDetails[1] as string ?? "";
                string imagePath = recipeDetails[2] as string ?? "";
                string instructions = recipeDetails[3] as string ?? "";
                string category = recipeDetails[4] as string ?? "";

                // Navigate to the recipe details page, passing the recipe details
                mainFrame.Content = new ShowRecipe(country, recipeName, imagePath, instructions, category);
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        }
    }


