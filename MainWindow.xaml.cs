using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;
using RecipeApp.Pages;
{
        public MainWindow()
        {
    public class RecipeCard
    {
        public string? RecipeName { get; set; }
        public string? ImagePath { get; set; }
        public string? Country { get; set; }
    }
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;
           // searchBar.SearchResultReceived += UpdateMealsListTextBox;
        }

            MaxWidth = SystemParameters.PrimaryScreenWidth;
           // searchBar.SearchResultReceived += UpdateMealsListTextBox;
        {
            if (mainFrame.NavigationService != null)
        
        private void UpdateMealsListTextBox(List<string[]> recipes)
        private void Button_Click(object sender, RoutedEventArgs e)
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

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                if (mainFrame.NavigationService != null)
                {
                    try
                    {
                        mainFrame.NavigationService.Navigate(new Pages.ContentRecipesPage());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Navigation error: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("NavigationService is null.");
                }
            }

            recipeCards.Add(recipeCard);

        }

            // Set the ItemsSource of the MealsListTextBox to the list of RecipeCard objects
           // MealsListTextBox.ItemsSource = recipeCards;

        }
    }
}
