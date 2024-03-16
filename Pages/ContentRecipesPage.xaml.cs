using System.Collections.Generic;
using System.Windows.Controls;

namespace RecipeApp.Pages
{
    public class RecipeCard
    {
        public string? RecipeName { get; set; }
        public string? ImagePath { get; set; }
        public string? Country { get; set; }
    }

    public partial class ContentRecipesPage : Page
    {
        private readonly List<string[]> recipes;

        public ContentRecipesPage(List<string[]> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;

            // Show recipes on first load
            UpdateMealsListTextBox(recipes);

            // Subscribe to events
            searchBar.SearchResultReceived += UpdateMealsListTextBox;
        }

        private void UpdateMealsListTextBox(List<string[]> recipes)
        {
            List<RecipeCard> recipeCards = new();

            foreach (string[] recipe in recipes)
            {
                RecipeCard recipeCard = new()
                {
                    RecipeName = recipe[0],
                    ImagePath = recipe[1],
                    Country = recipe[2]
                };

                recipeCards.Add(recipeCard);
            }

            MealsListTextBox.ItemsSource = recipeCards;
           
        }
    }
}



