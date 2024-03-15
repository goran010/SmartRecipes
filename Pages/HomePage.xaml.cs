using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using RecipeApp.View.UserControls;
using RecipeApp.View.UI;

namespace RecipeApp.Pages
{
    public partial class HomePage : Page
    {
        public event Action<List<string[]>> NavigateToSearchRecipePage = Items=> { };
        public event Action<List<string[]>> UpdateMealsList = Items => { };
        public HomePage()
        {
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;

            //searchBar events
            searchBar.SearchResultReceived += UpdateMealsListAndNavigate;
        }

        private void UpdateMealsListAndNavigate(List<string[]> recipes)
        {
            try
            {
                // Invoke the UpdateMealsList event with the received recipes
               
                UpdateMealsList?.Invoke(recipes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating meals list: {ex.Message}");
            }
        }

       
    }
}

