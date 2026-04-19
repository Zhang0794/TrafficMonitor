using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace TrafficMonitor;

public partial class UserView : ContentView
{
    public UserView()
    {
        InitializeComponent();
        LoadFavorite();
    }

    public void SetUser(string username)
    {
        UsernameLabel.Text = username;
        InfoLabel.Text = $"Welcome, {username}";
    }

    private void LoadFavorite()
    {
        string fav = Preferences.Get("favorite_checkpoint", "None");

        FavoriteLabel.Text = fav;

        if (fav == "None")
        {
            FavoriteStatusLabel.Text = "No favorite selected";
            FavoriteStatusLabel.TextColor = Colors.Gray;
        }
        else
        {
            FavoriteStatusLabel.Text = "? Active favorite checkpoint";
            FavoriteStatusLabel.TextColor = Colors.Green;
        }
    }

    private void Refresh_Clicked(object sender, EventArgs e)
    {
        LoadFavorite();
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        string fav = Preferences.Get("favorite_checkpoint", "None");

        if (fav == "None")
        {
            await Application.Current.MainPage.DisplayAlert(
                "Info",
                "No favorite to remove",
                "OK");
            return;
        }

        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Confirm",
            $"Remove {fav} from favorites?",
            "Yes",
            "No");

        if (confirm)
        {
            Preferences.Remove("favorite_checkpoint");
            LoadFavorite();

            await Application.Current.MainPage.DisplayAlert(
                "Removed",
                "Favorite removed",
                "OK");
        }
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PopToRootAsync();
    }
}