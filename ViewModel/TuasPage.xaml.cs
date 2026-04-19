using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace TrafficMonitor;

public partial class TuasPage : ContentPage
{
    public TuasPage()
    {
        InitializeComponent();

        if (Preferences.Get("favorite_checkpoint", "") == "Tuas")
        {
            SmartAdviceLabel.Text = "? Your favorite checkpoint";
        }
    }

    private async void Favorite_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("favorite_checkpoint", "Tuas");

        await DisplayAlert("Saved", "Tuas added to favorites", "OK");
    }

    private async void Share_Clicked(object sender, EventArgs e)
    {
        string text =
            $"?? Tuas Traffic\n" +
            $"Status: {TrafficStatusLabel.Text}\n" +
            $"Wait: {WaitTimeLabel.Text}";

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Tuas Traffic"
        });
    }

    private void Refresh_Clicked(object sender, EventArgs e)
    {
        TrafficStatusLabel.Text = "Light";
        WaitTimeLabel.Text = "8 mins";
        TrafficFlowBar.Progress = 0.25;
        LastUpdatedLabel.Text = "Updated: " + DateTime.Now.ToString("hh:mm tt");
    }

    private async void Advice_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Advice",
            "Tuas is currently faster than Woodlands.",
            "OK");
    }
}