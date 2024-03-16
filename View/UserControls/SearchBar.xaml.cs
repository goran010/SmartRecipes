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
                        // Check if StrMealThumb is null or empty before adding it to the list
                        string mealName = string.IsNullOrEmpty(meal.StrMeal) ? "Unknown Thumb" : meal.StrMeal;

                        // Check if strArea is null or empty before adding it to the list
                        string area = string.IsNullOrEmpty(meal.StrArea) ? "Unknown Area" : meal.StrArea;

                        // Check if StrMealThumb is null or empty before adding it to the list
                        string mealImage = string.IsNullOrEmpty(meal.StrMealThumb) ? "Unknown Thumb" : meal.StrMealThumb;

                        string strCategory = string.IsNullOrEmpty(meal.StrCategory) ? "Unknown Category" : meal.StrCategory;

                        string strInstructions = string.IsNullOrEmpty(meal.StrInstructions) ? "Unknown Category" : meal.StrInstructions;

                        // Add an array of strings to the list
                        allItems.Add([mealName, mealImage, area, strCategory, strInstructions]);
                        }
                    // Invoke the SearchResultReceived event with the allItems list             
                   ((MainWindow)System.Windows.Application.Current.MainWindow).HomePage_UpdateMealsList(allItems);
                    searchTextBox.Text = "";
                    //searchTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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
