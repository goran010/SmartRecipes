using System.Windows;
using RecipeApp.Pages;
using RecipeApp.View.UI;

namespace RecipeApp
    {
        {
        private readonly HomePage homePage;
        private List<string[]> recipes;

        public MainWindow ()
            {
        {
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;

            // Instantiate HomePage
            homePage = new HomePage();
            // Show HomePage in the Frame
            mainFrame.NavigationService?.Navigate(homePage);


            // Subscribe to events
            homePage.UpdateMealsList += HomePage_UpdateMealsList;

            // Subscribe to navigation events
            Navigation.NavigateToContentRecipesPage += NavigateToContentRecipesPage;
            Navigation.NavigateToAboutPage += NavigateToAboutPage;
            Navigation.NavigateToHomePage += NavigateToHomePage;

            Loaded += MainWindow_Loaded;
            }

        private void MainWindow_Loaded ( object sender, RoutedEventArgs e )
            {
            // Initialize the recipes list
            recipes = new List<string[]>();
            }


        private void HomePage_UpdateMealsList ( List<string[]> recipesList )
            {
            // Update the recipes list with new recipes
            //this.recipes = recipes;
            Console.WriteLine(recipesList.Count);
            mainFrame.Content = (new ContentRecipesPage(recipesList));
            }

        private void NavigateToContentRecipesPage ( object? sender, EventArgs e )
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
                // Navigate to the existing instance of homePage
                mainFrame.Content = homePage;
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        }
    }
}
