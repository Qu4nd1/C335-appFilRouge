using appFilRouge.Services;
using appFilRouge.Models;
using appFilRouge.ViewModels;
namespace appFilRouge
{
    public partial class MainPage : ContentPage
    {
        private readonly IngredientsViewModel _vm;
        public MainPage(IngredientsViewModel vm)
        {
            InitializeComponent();
            BindingContext = _vm = vm;
        }
        private async void AddButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewIngredient));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.LoadAsync();
        }
    }
}
