using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace TrafficMonitor;

public partial class WoodlandsPage : ContentPage
{
    public WoodlandsPage()
    {
        InitializeComponent();

        if (Preferences.Get("favorite_checkpoint", "") == "Woodlands")
        {
            SmartAdviceLabel.Text = "? Your favorite checkpoint";
        }
    }

    private async void Favorite_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("favorite_checkpoint", "Woodlands");

        await DisplayAlert("Saved", "Woodlands added to favorites", "OK");
    }

    private async void Share_Clicked(object sender, EventArgs e)
    {
        string text =
            $"?? Woodlands Traffic\n" +
            $"Status: {TrafficStatusLabel.Text}\n" +
            $"Wait: {WaitTimeLabel.Text}";

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Woodlands Traffic"
        });
    }

    private void Refresh_Clicked(object sender, EventArgs e)
    {
        TrafficStatusLabel.Text = "Heavy";
        WaitTimeLabel.Text = "25 mins";
        TrafficFlowBar.Progress = 0.75;
        LastUpdatedLabel.Text = "Updated: " + DateTime.Now.ToString("hh:mm tt");
    }

    private async void Advice_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Advice",
            "Traffic is heavy. Tuas may be faster.",
            "OK");
    }
}