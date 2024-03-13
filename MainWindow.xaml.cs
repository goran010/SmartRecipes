using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp
{
    public class RecipeCard
    {
        public string? RecipeName { get; set; }
        public string? ImagePath { get; set; }
        public string? Country { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;
           // searchBar.SearchResultReceived += UpdateMealsListTextBox;
        }

        
        private void UpdateMealsListTextBox(List<string[]> recipes)
        {
            // Create a list of RecipeCard objects
            List<RecipeCard> recipeCards = [];

            // For each string array in the recipes list, create a new RecipeCard and add it to the recipeCards list
            foreach (string[] recipe in recipes)
            {
                // Replace this with your logic to set ImagePath and Description
                RecipeCard recipeCard = new()
                {
                    //title of recipe
                    RecipeName = recipe[0],
                    //recipe image
                    ImagePath = recipe[1],
                    //country of recipe
                    Country = recipe[2]
                };

                recipeCards.Add(recipeCard);
            }

            // Set the ItemsSource of the MealsListTextBox to the list of RecipeCard objects
           // MealsListTextBox.ItemsSource = recipeCards;
        }

        private void GridCol1Row1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
