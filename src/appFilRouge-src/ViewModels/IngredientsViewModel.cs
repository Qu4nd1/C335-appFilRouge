using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using appFilRouge.Models;
using appFilRouge.Services;

namespace appFilRouge.ViewModels
{
    public partial class IngredientsViewModel : ObservableObject
    {
        private readonly IDataService _data;
        private List<Ingredient> _all = new();

        public ObservableCollection<Ingredient> Ingredients { get; } = new();

        [ObservableProperty] private string searchText = string.Empty;

        public IngredientsViewModel(IDataService data) => _data = data;

        [RelayCommand]
        public async Task LoadAsync()
        {
            _all = await _data.GetIngredientsAsync();
            ApplyFilter();
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        private void ApplyFilter()
        {
            Ingredients.Clear();
            foreach (var ingredient in _all.Where(i => i.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
            {
                Ingredients.Add(ingredient);
            }
        }

        [RelayCommand]
        private Task AddAsync() => Shell.Current.GoToAsync("NewIngredient");

        [RelayCommand]
        private async Task DeleteAsync(Ingredient ingredient)
        {
            bool isConfirmed = await Shell.Current.DisplayAlert(
                "Delete",
                $"Are you sure you want to delete the ingredient: {ingredient.Name}",
                "Yes", "No");

            if (!isConfirmed) return;

            bool deleteAccepted = await _data.DeleteIngredientAsync(ingredient.Id);

            if (!deleteAccepted)
            {
                List<Meal> meals = await _data.GetMealsUsingIngredientAsync(ingredient.Id);
                string mealNames = string.Join(", ", meals.Select(m => m.Name));
                await Shell.Current.DisplayAlert(
                    "Delete Refused",
                    $"{ingredient.Name} is used in: {mealNames}",
                    "Ok");
                return;
            }

            await LoadAsync();
        }

        [RelayCommand]
        private async Task EditAsync(Ingredient ingredient)
        {
            //Shell.Current.GoToAsync("EditIngredient");
            await Shell.Current.DisplayAlert("Edit", $"You are going to edit the ingredient: {ingredient.Name}", "OK");
            return;
        }
    }
}