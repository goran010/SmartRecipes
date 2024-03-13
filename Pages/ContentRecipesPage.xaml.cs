using System.Collections.Generic;
using System.Windows;
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
        public ContentRecipesPage()
        {
            InitializeComponent();
            Console.WriteLine("ContentRecipesPage Loaded");
        }

        public void UpdateMealsListTextBox(List<string[]> recipes)
        {
            List<RecipeCard> recipeCards = new List<RecipeCard>();

            foreach (string[] recipe in recipes)
            {
                RecipeCard recipeCard = new RecipeCard
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

