using System;
using Microsoft.Maui.Controls;

namespace TrafficMonitor
{
    public partial class TuasPage : ContentPage
    {
        private readonly Random random = new Random();
        private int previousFlow = 30;

        public TuasPage()
        {
            InitializeComponent();
            UpdateTuasDashboard(12, 8, "Smooth", 0.30);
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            int cars = random.Next(8, 28);
            int wait = GetWaitTime(cars);
            string status = GetTrafficStatus(cars);
            double flow = Math.Min(cars / 40.0, 1.0);

            UpdateTuasDashboard(cars, wait, status, flow);
        }

        private async void Advice_Clicked(object sender, EventArgs e)
        {
            string advice = TrafficFlowBar.Progress > 0.65
                ? "Tuas is getting busy. Consider Woodlands as an alternative route."
                : "Tuas remains the best route with low congestion.";

            await DisplayAlert("?? Smart Route Advice", advice, "OK");
        }

        private void UpdateTuasDashboard(int cars, int waitMinutes, string status, double flow)
        {
            TrafficStatusLabel.Text = status;
            WaitTimeLabel.Text = $"{waitMinutes} mins";
            TrafficFlowBar.Progress = flow;
            LastUpdatedLabel.Text = "Last Updated: " + DateTime.Now.ToString("hh:mm tt");

            // Status color
            TrafficStatusLabel.TextColor = status switch
            {
                "Heavy" => Colors.Red,
                "Moderate" => Colors.Orange,
                _ => Colors.Green
            };

            // Smart recommendation
            if (flow > 0.65)
            {
                SmartAdviceLabel.Text = "? Tuas congestion rising, consider Woodlands";
                SmartAdviceLabel.TextColor = Colors.Red;
            }
            else
            {
                SmartAdviceLabel.Text = "? Best route currently: Tuas checkpoint";
                SmartAdviceLabel.TextColor = Colors.Green;
            }

            previousFlow = (int)(flow * 100);
        }

        private string GetTrafficStatus(int cars)
        {
            if (cars >= 22)
                return "Heavy";
            if (cars >= 14)
                return "Moderate";
            return "Smooth";
        }

        private int GetWaitTime(int cars)
        {
            if (cars >= 22)
                return 20;
            if (cars >= 14)
                return 12;
            return 8;
        }
    }
}
