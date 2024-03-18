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
            string mealName = "";
            // Check if the sender is a Button
            if (sender is Button clickedButton)
                {
                // Extract the meal name from the TextBlock associated with the clicked Button
                if (clickedButton.Content is Grid buttonContent)
                    {
                    foreach (var child in buttonContent.Children)
                        {
                        if (child is TextBlock textBlock && textBlock.Name.StartsWith("tBNameOfMeal"))
                            {
                            mealName = textBlock.Text;
                            Console.WriteLine($"Clicked meal: {mealName}");
                            break;
                            }
                        }
                    }
                }
            string country = "";
            string recipeName = mealName;
            string imagePath = "";
            string instructions = "";
            string category = "Unknown Category";

            // Create an object array with all the string properties
            object[] recipeDetails = [country, recipeName, imagePath, instructions, category];

            // Navigate to the recipe details page, passing the recipe details
            ((MainWindow)Application.Current.MainWindow).NavigateToShowRecipePage(recipeDetails);
            }
        }
    }
