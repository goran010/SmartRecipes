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

            //searchBar events
            searchBar.SearchResultReceived += UpdateMealsListTextBox;
            searchBar.NavigationRequested += NavigateToSearchRecipePage;

            //navigation events
            Navigation.NavigationRequested += NavigateToSearchRecipePage;

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

        private void NavigateToSearchRecipePage(object? sender, EventArgs e)
        {
            try
            {
                //hide home screen grid
                homeScreenGrid.Visibility = Visibility.Collapsed;
                // Navigate to the desired page using mainFrame or any other navigation method you have
                mainFrame.NavigationService?.Navigate(new Pages.ContentRecipesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}");
            }
        }
    }
}