using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp.View.UI
    {
    public partial class GridPopularCountries : UserControl
        {
        private readonly ApiService apiService; // Service to handle API requests
        public event Action<List<string[]>> SearchResultReceived = items => { }; // Event to notify when search results are received

        public GridPopularCountries ()
            {
            InitializeComponent();

            // Initialize ApiService
            apiService = new ApiService();

            // Initialize SearchResultReceived event
            SearchResultReceived = delegate { };
            }

        // Method to send API request based on search area (e.g., French, Italian)
        private async void SendApiRequest ( string searchArea )
            {
            // Construct API endpoint URL
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/filter.php?a=" + searchArea;

            try
                {
                // Get response from the API
                string jsonResponse = await apiService.GetMealAsync(apiEndpoint);

                // Deserialize JSON response
                MealsResponse mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse) ?? new MealsResponse();

                // Process the response
                if (mealsResponse.Meals != null && mealsResponse.Meals.Count > 0)
                    {
                    // Create a list of items
                    List<string[]> allItems = mealsResponse.Meals.Select(meal => new string[]
                    {
                        string.IsNullOrEmpty(meal.StrMeal) ? "Unknown Meal" : meal.StrMeal,
                        string.IsNullOrEmpty(meal.StrMealThumb) ? "Unknown Thumb" : meal.StrMealThumb,
                        string.IsNullOrEmpty(meal.StrArea) ? searchArea : meal.StrArea,
                        string.IsNullOrEmpty(meal.StrCategory) ? "Unknown Category" : meal.StrCategory,
                        string.IsNullOrEmpty(meal.StrInstructions) ? "Unknown Instructions" : meal.StrInstructions
                    }).ToList();

                    // Invoke the method to update meals list on the home page
                    ((MainWindow)System.Windows.Application.Current.MainWindow).HomePage_UpdateMealsList(allItems);
                    }
                else
                    {
                    // Show message if no meals found in the response
                    MessageBox.Show("No meals found in the response.");
                    }
                }
            catch (Exception ex)
                {
                // Show error message if API request fails
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        // Event handler for French button click
        private void French_Click ( object sender, RoutedEventArgs e )
            {
            // Send API request for French meals
            SendApiRequest("French");
            }

        // Event handler for Italian button click
        private void Italian_Click ( object sender, RoutedEventArgs e )
            {
            // Send API request for Italian meals
            SendApiRequest("Italian");
            }

        // Event handler for Indian button click
        private void Indian_Click ( object sender, RoutedEventArgs e )
            {
            // Send API request for Indian meals
            SendApiRequest("Indian");
            }
        }
    }





