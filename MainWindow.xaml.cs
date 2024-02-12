using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RecipeApp.services;
using RecipeApp.View.UserControls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            searchBar.SearchResultReceived += UpdateMealsListTextBox;
        }

        private void UpdateMealsListTextBox(List<string> items)
        {
            MealsListTextBox.ItemsSource = items;
        }
    }
}
