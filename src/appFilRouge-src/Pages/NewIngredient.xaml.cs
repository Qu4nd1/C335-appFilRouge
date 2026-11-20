using appFilRouge.ViewModels;

namespace appFilRouge;

public partial class NewIngredient : ContentPage
{
	public NewIngredient(NewIngredientViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}