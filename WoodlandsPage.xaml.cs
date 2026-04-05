using System;
using Microsoft.Maui.Controls;

namespace TrafficMonitor
{
    public partial class WoodlandsPage : ContentPage
    {
        private readonly Random random = new Random();

        public WoodlandsPage()
        {
            InitializeComponent();
            UpdateWoodlandsDashboard(26, 18, "Moderate", 0.65);
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            int cars = random.Next(18, 50);
            int wait = GetWaitTime(cars);
            string status = GetTrafficStatus(cars);
            double flow = Math.Min(cars / 50.0, 1.0);

            UpdateWoodlandsDashboard(cars, wait, status, flow);
        }

        private async void Advice_Clicked(object sender, EventArgs e)
        {
            string advice = TrafficFlowBar.Progress > 0.75
                ? "Woodlands is heavily congested. Tuas checkpoint is recommended."
                : "Woodlands traffic is acceptable. Stay on current route.";

            await DisplayAlert("?? Smart Advice", advice, "OK");
        }

        private void UpdateWoodlandsDashboard(int cars, int waitMinutes, string status, double flow)
        {
            TrafficStatusLabel.Text = status;
            WaitTimeLabel.Text = $"{waitMinutes} mins";
            TrafficFlowBar.Progress = flow;
            LastUpdatedLabel.Text = "Last Updated: " + DateTime.Now.ToString("hh:mm tt");

            // Peak hour hint
            PeakHourLabel.Text = flow > 0.75
                ? "Peak Hour: Heavy morning causeway traffic"
                : "Peak Hour: Normal weekday flow";

            // Status color
            TrafficStatusLabel.TextColor = status switch
            {
                "Heavy" => Colors.Red,
                "Moderate" => Colors.Orange,
                _ => Colors.Green
            };

            // Smart route recommendation
            if (flow > 0.75)
            {
                SmartAdviceLabel.Text = "? High congestion detected, consider Tuas checkpoint";
                SmartAdviceLabel.TextColor = Colors.Red;
            }
            else
            {
                SmartAdviceLabel.Text = "? Woodlands route currently stable";
                SmartAdviceLabel.TextColor = Colors.Green;
            }
        }

        private string GetTrafficStatus(int cars)
        {
            if (cars >= 40)
                return "Heavy";
            if (cars >= 25)
                return "Moderate";
            return "Smooth";
        }

        private int GetWaitTime(int cars)
        {
            if (cars >= 40)
                return 30;
            if (cars >= 25)
                return 18;
            return 10;
        }
    }
}
