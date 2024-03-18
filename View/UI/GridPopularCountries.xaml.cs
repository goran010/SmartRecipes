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
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/filter.php?a=" + searchArea;

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

                // Invoke the method to update meals list
                ((MainWindow)System.Windows.Application.Current.MainWindow).HomePage_UpdateMealsList(allItems);

                }
            else
                {
                MessageBox.Show("No meals found in the response.");
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




