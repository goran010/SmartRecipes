using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.Pages
    {
    public class RecipeCard
        {
        public string? RecipeName { get; set; }
        public string? ImagePath { get; set; }
        public string? Country { get; set; }
        public string? Category { get; set; }
        public string? Instructions { get; set; }
        }

    public partial class ContentRecipesPage : Page
        {
        private readonly List<string[]> recipes;

        public ContentRecipesPage ( List<string[]> recipes )
            {
            InitializeComponent();
            this.recipes = recipes;

            // Show recipes on first load
            UpdateMealsListTextBox(recipes);

            // Subscribe to events
            searchBar.SearchResultReceived += UpdateMealsListTextBox;
            }

        private void UpdateMealsListTextBox ( List<string[]> recipes )
            {
            // Create a list of RecipeCard objects using LINQ
            List<RecipeCard> recipeCards = recipes.Select(recipe => new RecipeCard
                {
                RecipeName = recipe[0],
                ImagePath = recipe[1],
                Country = recipe[2],
                Category = recipe[3],
                Instructions = recipe[4]
                }).ToList();

            // Set the ItemsSource of MealsListTextBox to the recipeCards list
            MealsListTextBox.ItemsSource = recipeCards;
            }

        private void Card_Clicked ( object sender, RoutedEventArgs e )
            {
            // Access the data context of the clicked item
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is RecipeCard data)
                {
                // Ensure data is not null before accessing its properties
                if (data != null)
                    {
                    // Extract recipe details from the data object
                    string country = data.Country ?? "";
                    string recipeName = data.RecipeName ?? "";
                    string imagePath = data.ImagePath ?? "";
                    string instructions = data.Instructions ?? "";
                    string category = data.Category ?? "";

                    // Create an object array with all the string properties
                    object[] recipeDetails = [country, recipeName, imagePath, instructions, category];

                    // Navigate to the recipe details page, passing the recipe details
                    ((MainWindow)System.Windows.Application.Current.MainWindow).NavigateToShowRecipePage(recipeDetails);
                    }
                else
                    {
                    // Handle the case where data is null (optional)
                    Console.WriteLine("Clicked item's data context is null.");
                    }
                }
            }
        }
    }





