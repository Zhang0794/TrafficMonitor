namespace TrafficMonitor;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        await DisplayAlert("Login", "Login Successful", "OK");

        await Navigation.PushAsync(new MainPage());
    }
}