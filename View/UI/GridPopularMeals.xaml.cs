using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.View.UI
    {
    public partial class GridPopularMeals : UserControl
        {
        public GridPopularMeals ()
            {
            InitializeComponent();
            }

        // Method to handle the Click event of the buttons
        private void MealClicked ( object sender, RoutedEventArgs e )
            {
            // Extract meal name from the clicked button
            string? mealName = GetMealName(sender);

            // Proceed if meal name is not null or empty
            if (!string.IsNullOrEmpty(mealName))
                {
                // Initialize variables for recipe details
                var country = string.Empty;
                var imagePath = string.Empty;
                var instructions = string.Empty;
                var category = "Unknown Category";

                // Assign mealName to recipeName
                var recipeName = mealName;


                // Create an object array with all the string properties
                object[] recipeDetails = [country, recipeName, imagePath, instructions, category];

                // Navigate to the recipe details page, passing the recipe details
                ((MainWindow)Application.Current.MainWindow).NavigateToShowRecipePage(recipeDetails);
                }
            }

        // Helper method to extract meal name from the clicked button
        private static string? GetMealName ( object sender )
            {
            if (sender is Button clickedButton)
                {
                // Extract the content of the clicked button
                if (clickedButton.Content is Grid buttonContent)
                    {
                    // Iterate through children of the button content to find the TextBlock with the meal name
                    foreach (var child in buttonContent.Children)
                        {
                        if (child is TextBlock textBlock && textBlock.Name.StartsWith("tBNameOfMeal"))
                            {
                            // Retrieve and return the meal name
                            string mealName = textBlock.Text;
                            Console.WriteLine($"Clicked meal: {mealName}");
                            return mealName;
                            }
                        }
                    }
                }

            // Return null if meal name extraction fails
            return null;
            }
        }
    }

