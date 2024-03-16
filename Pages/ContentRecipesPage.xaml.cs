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
            }

        private void UpdateMealsListTextBox ( List<string[]> recipes )
            {
            List<RecipeCard> recipeCards = [];

            foreach (string[] recipe in recipes)
                {
                RecipeCard recipeCard = new()
                    {
                    RecipeName = recipe[0],
                    ImagePath = recipe[1],
                    Country = recipe[2],
                    Category = recipe[3],
                    Instructions = recipe[4]
                    };

                recipeCards.Add(recipeCard);
                }
            // Setting source to recipeCards list
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
                    Console.WriteLine(data.RecipeName);
                    string country = data.Country ?? "";
                    string recipeName = data.RecipeName ?? "";
                    string imagePath = data.ImagePath ?? "";
                    string instructions = data.Instructions ?? "";
                    string category = data.Category ?? "";

                    // Create an object array with all the string properties using curly braces
                    object[] recipeDetails = [country, recipeName, imagePath, instructions, category];

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





