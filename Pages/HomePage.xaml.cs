using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.Pages
    {
    public partial class HomePage : Page
        {
        public event Action<List<string[]>> UpdateMealsList = items => { };
        public event EventHandler NavigateToAddRecipe = delegate { };

        public HomePage ()
            {
            InitializeComponent();
            MaxWidth = SystemParameters.PrimaryScreenWidth;

            // Subscribe to events
            searchBar.SearchResultReceived += UpdateMealsListAndNavigate;
            popularCountries.SearchResultReceived += UpdateMealsListAndNavigate;
            }

        private void UpdateMealsListAndNavigate ( List<string[]> recipes )
            {
            try
                {
                // Invoke the UpdateMealsList event with the received recipes
                Console.WriteLine(recipes[1][0]);
                UpdateMealsList?.Invoke(recipes);
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Error updating meals list: {ex.Message}");
                }
            }

        private void ButtonAddRecipe_Click ( object sender, RoutedEventArgs e )
            {
            NavigateToAddRecipe?.Invoke(this, EventArgs.Empty);
            }
        }
    }



