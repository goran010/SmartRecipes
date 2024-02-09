using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;


namespace RecipeApp
{

    public class Meal
    {
        public string StrMeal { get; set; }
    }
    public class MealsResponse
    {
        public List<Meal> Meals { get; set; }
    }

    public partial class MainWindow : Window
    {
        private readonly ApiService apiService;
        private  List<string> allItems;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize HttpClient
            apiService = new ApiService();
        }
        private void UpdateListBox(List<string> items)
        {
            foreach (var item in items)
            {
                MealsListTextBox.Text += item;
            }
        }
        private async void SendApiRequest(string searchText)
        {
            //api endpoint
            string apiEndpoint = "https://www.themealdb.com/api/json/v1/1/search.php?s=" + searchText;

            try
            {
                    // Read the response from api
                    string jsonResponse = await apiService.GetMealAsync(apiEndpoint);

                    // Deserialize the JSON string
                    MealsResponse mealsResponse = JsonConvert.DeserializeObject<MealsResponse>(jsonResponse);
                Console.WriteLine(mealsResponse.Meals);
                    // Access the value of strMeal
                    if (mealsResponse!.Meals?.Count > 0)
                    {
                        foreach(Meal meal in mealsResponse.Meals) {
                            Console.WriteLine(meal);
                            allItems.Add(meal.StrMeal);
                        }
                        
                    ///UpdateListBox(allItems);
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


        //event handler when searchTextBox is changed
        void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();

            if (searchText.Length>2)
            {
                //sending api request with searched name
                SendApiRequest(searchText);
            }
        }
    }
}

