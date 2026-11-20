using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using appFilRouge.Models;
using appFilRouge.Services;

namespace appFilRouge.ViewModels
{
    public partial class NewIngredientViewModel : ObservableObject
    {
        private readonly IDataService _data;

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string calories = string.Empty;
        [ObservableProperty] private string proteins = string.Empty;

        public NewIngredientViewModel(IDataService data) => _data = data;

        [RelayCommand]
        private async Task SaveAsync()
        {
            // 1. validate the name
            string cleanName = name.Trim();

            if (!(cleanName.Length >= 2))
            {
                await Shell.Current.DisplayAlert("Invalid input", "Name must have at least 2 characters.", "OK");
                return;
            }

            // 2. parse the numbers
            if (!double.TryParse(calories.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var kcal) ||
                !double.TryParse(proteins.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var protein))
            {
                await Shell.Current.DisplayAlert("Invalid input", "Calories and proteins must be valid numbers (for example 12.5).", "OK");
                return;
            }

            // 3. reject negatives
            if (kcal < 0 || protein < 0)
            {
                await Shell.Current.DisplayAlert("Invalid input", "Calories and Proteins cannot be negative !", "OK");
                return;
            }

            // 4. build the Ingredient, save it
            var ingredient = new Ingredient { Name = cleanName, CaloriesPer100g = kcal, ProteinsPer100g = protein };
            await _data.SaveIngredientAsync(ingredient);

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}