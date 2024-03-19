using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.Pages
    {
    public partial class AddRecipePage : Page
        {
        // Create an instance of RecipeContext to interact with the database
        private readonly RecipeContext _recipeContext = new RecipeContext();

        public AddRecipePage ()
            {
            InitializeComponent();
            }

        // Event handler for the Add Recipe button click
        private void BtnAddRecipe_Click ( object sender, RoutedEventArgs e )
            {
            try
                {
                // Retrieve data entered by the user from form controls
                string recipeName = txtRecipeName.Text;
                string instructions = txtInstructions.Text;
                string country = txtCountry.Text;

                // Create a new RecipesData object and populate it with form data
                var newRecipeData = new RecipesData
                    {
                    Id = Guid.NewGuid(),        // Generate a new unique ID
                    RecipeName = recipeName,    // Set the recipe name
                    Category = "",              // Set category (assuming it's empty for now)
                    Instructions = instructions,// Set recipe instructions
                    Country = country,          // Set country of origin
                    ImagePath = ""              // Set image path (assuming it's empty for now)
                    };

                // Add the new RecipesData object to the database
                _recipeContext.RecipesData.Add(newRecipeData);

                // Save changes to the database
                _recipeContext.SaveChanges();

                // Show success message
                MessageBox.Show("Recipe added successfully!");

                // Query all recipes from the database and display them
                DisplayAllRecipes();
                }
            catch (Exception ex)
                {
                // Show error message if an exception occurs
                MessageBox.Show($"Error adding recipe: {ex.Message}");
                }
            }

        // Helper method to query all recipes from the database and display them
        private void DisplayAllRecipes ()
            {
            // Query all recipes from the database
            var allRecipes = _recipeContext.RecipesData.ToList();

            // Check if any recipes were found
            if (allRecipes.Count > 0)
                {
                Console.WriteLine("All Recipes:");
                // Loop through each recipe and display its details
                foreach (var recipe in allRecipes)
                    {
                    Console.WriteLine($"Recipe Name: {recipe.RecipeName}");
                    Console.WriteLine($"Instructions: {recipe.Instructions}");
                    Console.WriteLine($"Country: {recipe.Country}");
                    Console.WriteLine();
                    }
                }
            }
        }
    }












