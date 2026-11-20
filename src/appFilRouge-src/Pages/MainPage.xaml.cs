using appFilRouge.Services;
using appFilRouge.Models;
namespace appFilRouge
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();


        }
        private async void AddButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewIngredient));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var service = new JsonDataService();
            await service.SaveIngredientAsync(new Ingredient
            {
                Name = "Poulet",
                CaloriesPer100g = 165,
                ProteinsPer100g = 31
            });
            var list = await service.GetIngredientsAsync();
            System.Diagnostics.Debug.WriteLine($"Ingredients: {list.Count}");
            System.Diagnostics.Debug.WriteLine($"DATA PATH: {FileSystem.AppDataDirectory}");
        }


    }
}
