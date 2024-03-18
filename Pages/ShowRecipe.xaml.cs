using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace RecipeApp.Pages
{
    public partial class ShowRecipe : Page, INotifyPropertyChanged
    {
        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; NotifyPropertyChanged(nameof(Country)); }
        }

        private string _recipeName;
        public string RecipeName
        {
            get { return _recipeName; }
            set { _recipeName = value; NotifyPropertyChanged(nameof(RecipeName)); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; NotifyPropertyChanged(nameof(ImagePath)); }
        }

        private string _instructions;
        public string Instructions
        {
            get { return _instructions; }
            set { _instructions = value; NotifyPropertyChanged(nameof(Instructions)); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; NotifyPropertyChanged(nameof(Category)); }
        }

        public ShowRecipe(string country, string recipeName, string imagePath, string instructions, string category)
        {
            InitializeComponent();

            // Initialize properties with provided values
            Country = country;
            RecipeName = recipeName;
            ImagePath = imagePath;
            Instructions = instructions;
            Category = category;


            DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
