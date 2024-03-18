using System.ComponentModel;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp.Pages
    {
    public partial class ShowRecipe : Page, INotifyPropertyChanged
        {
        // INotifyPropertyChanged event
        public event PropertyChangedEventHandler? PropertyChanged; // Nullable event

        // Properties with automatic getters and setters
        public string Country { get; private set; }
        public string RecipeName { get; private set; }
        public string ImagePath { get; private set; }
        public string Instructions { get; private set; }
        public string Category { get; private set; }

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

        // Method to send API request
        private async void SendApiRequest ( string searchText )
            {
            // Construct API endpoint
            string apiEndpoint = $"https://www.themealdb.com/api/json/v1/1/search.php?s={searchText}";

            // Get response from the API
            string? jsonResponse = await apiService.GetMealAsync(apiEndpoint);

            // Deserialize JSON response
            MealsResponse? mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse) ?? new MealsResponse();

            // Process the response
            if (mealsResponse.Meals != null && mealsResponse.Meals.Count > 0)
                {
                // Update properties with the values from the API response
                var meal = mealsResponse.Meals[0]; // Assuming you are interested in the first meal
                Country = meal.StrArea ?? "Unknown Area";
                RecipeName = meal.StrMeal ?? "Unknown Meal";
                ImagePath = meal.StrMealThumb ?? "Unknown Thumb";
                Category = meal.StrCategory ?? "Unknown Category";
                Instructions = meal.StrInstructions ?? "Unknown Instructions";

                // Notify property changes
                NotifyPropertyChanged(nameof(Country));
                NotifyPropertyChanged(nameof(RecipeName));
                NotifyPropertyChanged(nameof(ImagePath));
                NotifyPropertyChanged(nameof(Category));
                NotifyPropertyChanged(nameof(Instructions));
                }
            }

        // Method to notify property changes
        protected virtual void NotifyPropertyChanged ( string propertyName )
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
