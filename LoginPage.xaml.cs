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

        
        string correctUsername = "Jay";
        string correctPassword = "1234";

        if (username == correctUsername && password == correctPassword)
        {
            await DisplayAlert("Login", "Login Successful, WELCOME BACK, Jay", "OK");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Wrong username or password!", "OK");
        }
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please enter username and password", "OK");
            return;
        }
    }

}