namespace TrafficMonitor;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Woodlands_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Checkpoint", "Opening Woodlands traffic view", "OK");
        await Navigation.PushAsync(new WoodlandsPage());
    }

    private async void Tuas_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Checkpoint", "Opening Tuas traffic view", "OK");
        await Navigation.PushAsync(new TuasPage());
    }

    private async void Refresh_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Refresh", "Traffic data updated", "OK");
    }
}