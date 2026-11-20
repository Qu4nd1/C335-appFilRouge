namespace appFilRouge;

public partial class NewIngredient : ContentPage
{
	public NewIngredient()
	{
		InitializeComponent();
	}
	private async void SaveIngredient(object sender, EventArgs e)
	{
        // A ajouter logique de sauvgarde du nouvel ingrédient
        await Shell.Current.GoToAsync(nameof(MainPage));

    }
    private async void CancelButton(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));

    }
}