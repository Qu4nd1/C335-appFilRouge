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
    }
}
