using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using RecipeApp.services;

namespace RecipeApp.View.UserControls
    {
    public class Meal
        {
        public string StrMeal { get; set; }
        public string? StrMealThumb { get; set; }
        public string? strArea { get; set; }

        public Meal ()
            {
            StrMeal = string.Empty;
            }
        }

    public class MealsResponse
        {
        public List<Meal> Meals { get; set; }
        public MealsResponse ()
            {
            Meals = [];
            }
        }

    public partial class SearchBar : UserControl
        {
        private readonly ApiService apiService;
        public MainWindow MainWindow { get; set; } = null!;

        public event Action<List<string[]>> SearchResultReceived = items => { };

        public SearchBar ()
            {
            InitializeComponent();

            // Initialize HttpClient
            apiService = new ApiService();

            //events
            searchTextBox.GotFocus += SearchTextBox_GotFocus;
            SearchResultReceived = delegate { };
            }

        // sending api request and showing response
        private async void SendApiRequest ( string searchText )
            {
            //api endpoint
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/search.php?s=" + searchText;

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
                        string area = string.IsNullOrEmpty(meal.strArea) ? "Unknown Area" : meal.strArea;

                        // Check if StrMealThumb is null or empty before adding it to the list
                        string mealThumb = string.IsNullOrEmpty(meal.StrMealThumb) ? "Unknown Thumb" : meal.StrMealThumb;

                        // Add an array of strings to the list
                        allItems.Add([meal.StrMeal, mealThumb, area!]);
                        }
                    // Invoke the SearchResultReceived event with the allItems list             
                    SearchResultReceived?.Invoke(allItems);
                    searchTextBox.Text = "";
                    Keyboard.ClearFocus();
                    if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                        {
                        searchPlaceholder.Visibility = Visibility.Visible;
                        }
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
        void SearchTextBox_TextChanged ( object sender, TextChangedEventArgs e )
            {
            searchPlaceholder.Visibility = System.Windows.Visibility.Collapsed;
            }

        // event handler when searchTextBox is submitet
        private void SearchTextBox_KeyDown ( object sender, KeyEventArgs e )
            {
            if (e.Key == Key.Enter)
                {
                try
                    {
                    string searchText = searchTextBox.Text;
                    SendApiRequest(searchText);
                    }
                catch (Exception ex)
                    {
                    MessageBox.Show($"Navigation error: {ex.Message}");
                    }
                }
            }

        // When the TextBox get focus, hide the placeholder
        private void SearchTextBox_GotFocus ( object sender, RoutedEventArgs e )
            {
            // When the TextBox gets focus, hide the placeholder
            searchPlaceholder.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
