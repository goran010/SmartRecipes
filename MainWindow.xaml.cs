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
            MaxWidth = SystemParameters.PrimaryScreenWidth;

            // Subscribe to navigation events
            Navigation.NavigateToSearchRecipePage += NavigateToSearchRecipePage;
            Navigation.NavigateToAboutPage += NavigateToAboutPage;
            Navigation.NavigateToHomePage += NavigateToHomePage;

            // Instantiate HomePage
            homePage = new HomePage();

            // Show HomePage in the Frame
            mainFrame.NavigationService?.Navigate(homePage);
            }

        public void HomePage_UpdateMealsList ( List<string[]> recipes )
            {
            // Update the recipes list with new recipes
            this.recipes = recipes;
            mainFrame.Content = (new ContentRecipesPage(recipes));
            }

        private void NavigateToSearchRecipePage ( object? sender, EventArgs e )
            {
            try
                {
                // Navigate to the search recipe page and pass the recipes
                mainFrame.Content = (new ContentRecipesPage(recipes));
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
                mainFrame.Content = (new AboutPage());
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
                mainFrame.Content = (new HomePage());
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
                // Extract recipe details from the object array
                string country = recipeDetails[0] as string ?? "";
                string recipeName = recipeDetails[1] as string ?? "";
                string imagePath = recipeDetails[2] as string ?? "";
                string instructions = recipeDetails[3] as string ?? "";
                string category = recipeDetails[4] as string ?? "";

                // Navigate to the recipe details page, passing the extracted details
                mainFrame.Content = (new ShowRecipe(country, recipeName, imagePath, instructions, category));
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        }
    }


