using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;

namespace RecipeApp.View.UserControls
{
    public class Meal
    {
        public string StrMeal { get; set; }
        public string StrMealThumb { get; set; }
        public string strArea{ get; set; }

        public Meal()
        {
            StrMeal = string.Empty;
        }
    }

    public class MealsResponse
    {
        public List<Meal> Meals { get; set; }
        public MealsResponse()
        {
            Meals = new List<Meal>();
        }
    }

    public partial class SearchBar : UserControl
    {
        private readonly ApiService apiService;
        public MainWindow MainWindow { get; set; } = null!;

        public event Action<List<string[]>> SearchResultReceived = items => { };

        public SearchBar()
        {
            InitializeComponent();

            // Initialize HttpClient
            apiService = new ApiService();

            //events
            searchTextBox.GotFocus += SearchTextBox_GotFocus;
            searchTextBox.LostFocus += SearchTextBox_LostFocus;
            SearchResultReceived = delegate { };
        }

        // sending api request and showing response
        private async void SendApiRequest(string searchText)
        {
            //api endpoint
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/search.php?s=" + searchText;

            try
            {
                // Read the response from api
                string jsonResponse = await apiService.GetMealAsync(apiEndpoint);

                // Convert the JSON string
                MealsResponse mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse);

                // Access the value of strMeal
                if (mealsResponse!.Meals?.Count > 0)
                {
                    // create list of items
                    List<string[]> allItems = new List<string[]>();

                    // adding items to list
                    foreach (Meal meal in mealsResponse.Meals)
                    {
                        allItems.Add(new string[] { meal.StrMeal, meal.StrMealThumb, meal.strArea });
                    }

                    // Invoke the SearchResultReceived event with the allItems list
                    SearchResultReceived?.Invoke(allItems);
                }
                else
                {
                    MessageBox.Show("No meals found in the response.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // event handler when searchTextBox text is changed
        void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();

            if (searchText.Length > 2)
            {
                // sending api request with searched name
                SendApiRequest(searchText);
            }
        }

        // When the TextBox get focus, hide the placeholder
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // When the TextBox gets focus, hide the placeholder
            searchPlaceholder.Visibility = System.Windows.Visibility.Collapsed;
        }

        // When the TextBox loses focus, show the placeholder if there is no text
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchPlaceholder.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
