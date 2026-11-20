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
    }
}