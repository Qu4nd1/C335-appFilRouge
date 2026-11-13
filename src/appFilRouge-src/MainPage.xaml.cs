namespace appFilRouge
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSearchButtonPressed(object? sender, EventArgs e)
        {
            var query = MainSearchBar.Text;
            // run your search here
        }

        private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
        {
            var query = e.NewTextValue;
            // filter as the user types
        }
        
    }
}
