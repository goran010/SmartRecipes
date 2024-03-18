using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp.Pages
    {
    public partial class ShowRecipe : Page
        {
        // Properties with automatic getters and setters
        public string Country { get; }
        public string RecipeName { get; }
        public string ImagePath { get; }
        public string Instructions { get; }
        public string Category { get; }

        private readonly ApiService apiService;
        // Constructor to initialize properties
        public ShowRecipe ( string country, string recipeName, string imagePath, string instructions, string category )

            {
            InitializeComponent();
            apiService = new ApiService();
            Country = country;
            RecipeName = recipeName;
            ImagePath = imagePath;
            Instructions = instructions;
            Category = category;
            DataContext = this; // Set DataContext to this instance

            // Check if RecipeName is "Unknown Meal"
            if (Category == "Unknown Category")
                {
                SendApiRequest(RecipeName);
                }
            }

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
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }

