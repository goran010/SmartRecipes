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
        public string? StrArea { get; set; }
        public string? StrCategory { get; set; }
        public string? StrInstructions { get; set; }

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
        public event Action<List<string[]>> NavigationRequested = items => { };

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
            try
                {
                // Construct API endpoint
                string apiEndpoint = $"https://www.themealdb.com/api/json/v1/1/search.php?s={searchText}";

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
                        string.IsNullOrEmpty(meal.StrArea) ? "Unknown Area" : meal.StrArea,
                        string.IsNullOrEmpty(meal.StrCategory) ? "Unknown Category" : meal.StrCategory,
                        string.IsNullOrEmpty(meal.StrInstructions) ? "Unknown Instructions" : meal.StrInstructions
                    }).ToList();

                    // Invoke the method to update meals list
                    ((MainWindow)System.Windows.Application.Current.MainWindow).HomePage_UpdateMealsList(allItems);

                    // Clear search text box
                    searchTextBox.Text = "";

                    // Clear focus and show placeholder if search text is empty
                    Keyboard.ClearFocus();
                    if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                        searchPlaceholder.Visibility = Visibility.Visible;
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


        // event handler when searchTextBox is submitet, send api requst with searchText
        private void SearchTextBox_KeyDown ( object sender, KeyEventArgs e )
            {
            if (e.Key == Key.Enter)
                {
                string searchText = searchTextBox.Text;
                SendApiRequest(searchText);
                }
            }


        // event handler when searchTextBox text is changed
        void SearchTextBox_TextChanged ( object sender, TextChangedEventArgs e )
            {
            searchPlaceholder.Visibility = System.Windows.Visibility.Collapsed;
            }

        // When the TextBox get focus, hide the placeholder
        private void SearchTextBox_GotFocus ( object sender, RoutedEventArgs e )
            {
            // When the TextBox gets focus, hide the placeholder
            searchPlaceholder.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
