using TrafficMonitor.Services;
using TrafficMonitor.Models;

namespace TrafficMonitor;

public partial class LoginPage : ContentPage
{
    DatabaseService db = new DatabaseService();

    public LoginPage()
    {
        InitializeComponent();
    }

    
    async void Register_Clicked(object sender, EventArgs e)
    {
        bool success = await db.Register(usernameEntry.Text, passwordEntry.Text);

        if (success)
            await DisplayAlert("Success", "Registered successfully!", "OK");
        else
            await DisplayAlert("Error", "User already exists", "OK");
    }

    
    async void Login_Clicked(object sender, EventArgs e)
    {
        var user = await db.Login(usernameEntry.Text, passwordEntry.Text);

        if (user != null)
        {
            await Navigation.PushAsync(new MainPage(user));
        }
        else
        {
            await DisplayAlert("Error", "Invalid login", "OK");
        }
    }
}