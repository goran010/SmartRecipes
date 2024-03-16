using System.Windows.Controls;

namespace RecipeApp.Pages
    {
    public partial class ShowRecipe : Page
        {
        // Properties to hold recipe details
        public string Country { get; set; }
        public string RecipeName { get; set; }
        public string ImagePath { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }

        public ShowRecipe ( string country, string recipeName, string imagePath, string instructions, string category )
            {
            InitializeComponent();

            // Initialize properties with provided values
            Country = country;
            RecipeName = recipeName;
            ImagePath = imagePath;
            Instructions = instructions;
            Category = category;
            }
        }
    }
