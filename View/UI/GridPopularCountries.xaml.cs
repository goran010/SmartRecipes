using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp.View.UI
    {
    public partial class GridPopularCountries : UserControl
        {
        private readonly ApiService apiService;
        public event Action<List<string[]>> SearchResultReceived = items => { };

        public GridPopularCountries ()
            {
            InitializeComponent();
            apiService = new ApiService();
            SearchResultReceived = delegate { };
            }

        private async void SendApiRequest ( string searchArea )
            {
            Console.WriteLine(searchArea);
            //api endpoint
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/filter.php?a=" + searchArea;
            try
                {
                // Read the response from api
                string jsonResponse = await apiService.GetMealAsync(apiEndpoint);
                MealsResponse mealsResponse = new();

                // Convert the JSON string
                if (jsonResponse != null)
                    {
                    mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse)!;
                    }

                // Access the value of strMeal
                if (mealsResponse!.Meals?.Count > 0)
                    {
                    // create list of items
                    List<string[]> allItems = [];

                    // adding items to list
                    foreach (Meal meal in mealsResponse.Meals)
                        {
                        // Check if strArea is null or empty before adding it to the list
                        string area = searchArea;

                        // Check if StrMealThumb is null or empty before adding it to the list
                        string mealThumb = string.IsNullOrEmpty(meal.StrMealThumb) ? "Unknown Thumb" : meal.StrMealThumb;

                        // Add an array of strings to the list
                        allItems.Add([meal.StrMeal, mealThumb, area]);
                        }
                    // Invoke the SearchResultReceived event with the allItems list             
                    SearchResultReceived?.Invoke(allItems);
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Error: {ex.Message}");
                }
            }

        private void French_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                SendApiRequest("French");
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void Italian_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                SendApiRequest("Italian");
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }

        private void Indian_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                SendApiRequest("Indian");
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Navigation error: {ex.Message}");
                }
            }
        }
    }




