using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using RecipeApp.services;

namespace RecipeApp.View.UserControls
    {
    // Represents a meal item
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

    // Represents the response containing a list of meals
    public class MealsResponse
        {
        public List<Meal> Meals { get; set; }

        public MealsResponse ()
            {
            Meals = new List<Meal>();
            }
        }

    // User control for the search bar
    public partial class SearchBar : UserControl
        {
        // Instance of the RecipeContext and ApiService classes
        private readonly RecipeContext _recipeContext = new RecipeContext();
        private readonly ApiService _apiService = new ApiService();
        public MainWindow MainWindow { get; set; } = null!;

        // Event for notifying about search results
        public event Action<List<string[]>> SearchResultReceived = items => { };

        // Constructor
        public SearchBar ()
            {
            InitializeComponent();

            // Attach event handler for the GotFocus event of the searchTextBox
            searchTextBox.GotFocus += SearchTextBox_GotFocus;

            // Initialize the SearchResultReceived event
            SearchResultReceived = delegate { };
            }

        // Method to send an API request and handle the response
         private async void SendApiRequest ( string searchText )
     {
     try
         {
         // Construct API endpoint
         string apiEndpoint = $"https://www.themealdb.com/api/json/v1/1/search.php?s={searchText}";

         // Get response from the API
         string jsonResponse = await _apiService.GetMealAsync(apiEndpoint);

         // Deserialize JSON response
         MealsResponse mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse) ?? new MealsResponse();

         List<string[]> allItems = [];

         // Process the response
         if (mealsResponse.Meals != null && mealsResponse.Meals.Any())
             {
             // Create a list of items from the API response
             allItems = mealsResponse.Meals.Select(meal => new string[]
            {
                 meal.StrMeal ?? "Unknown Meal",
                 meal.StrMealThumb ?? "Unknown Thumb",
                 meal.StrArea ?? "Unknown Area",
                 meal.StrCategory ?? "Unknown Category",
                 meal.StrInstructions ?? "Unknown Instructions"
            }).ToList();



             // Clear the search text box and focus
             searchTextBox.Text = "";
             Keyboard.ClearFocus();
             if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                 searchPlaceholder.Visibility = Visibility.Visible;
             }
         // Search in the database for additional results
         allItems.AddRange(SearchInDatabase(searchText));

         // Update the meals list in the main window
         ((MainWindow)Application.Current.MainWindow).HomePage_UpdateMealsList(allItems);
         }
     catch (Exception ex)
         {
         MessageBox.Show($"Error: {ex.Message}");
         }
     }

        // Method to search for similar recipes in the database
        private List<string[]> SearchInDatabase ( string searchText )
            {
            List<string[]> searchResults = new List<string[]>();

            try
                {
                // Query the database to find recipes whose names contain the search string
                var matchingRecipes = _recipeContext.RecipesData
    .Where(recipe => recipe.RecipeName != null && recipe.RecipeName.Contains(searchText))
    .ToList();


                // Process the matching recipes
                foreach (var recipe in matchingRecipes)
                    {
                    // Add recipe details to the search results list
                    string[] recipeDetails =
                    {
                        recipe.RecipeName ?? "Unknown Recipe",
                        "", // Assuming no image path in the database
                        recipe.Country ?? "Unknown Country",
                        recipe.Category ?? "Unknown Category",
                        recipe.Instructions ?? "Unknown Instructions"
                    };
                    searchResults.Add(recipeDetails);
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Error searching in database: {ex.Message}");
                }

            return searchResults;
            }

        // Event handler when the searchTextBox is submitted
        private void SearchTextBox_KeyDown ( object sender, KeyEventArgs e )
            {
            if (e.Key == Key.Enter)
                {
                string searchText = searchTextBox.Text;
                SendApiRequest(searchText);
                }
            }

        // Event handler when the searchTextBox text is changed
        private void SearchTextBox_TextChanged ( object sender, TextChangedEventArgs e )
            {
            searchPlaceholder.Visibility = Visibility.Collapsed;
            }

        // Event handler when the searchTextBox gets focus
        private void SearchTextBox_GotFocus ( object sender, RoutedEventArgs e )
            {
            // Hide the placeholder when the TextBox gets focus
            searchPlaceholder.Visibility = Visibility.Collapsed;
            }
        }
    }

