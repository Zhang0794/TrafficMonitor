using System;
using Microsoft.Maui.Controls;

namespace TrafficMonitor
{
    public partial class HomeView : ContentView
    {
        private readonly Random random = new Random();
        private string currentCheckpoint = "Woodlands";
        private int previousCarCount = 24;

        public HomeView()
        {
            InitializeComponent();

            
            UpdateDashboard("Woodlands", 24, "Moderate", 20, 0.75, 0.35);
        }

        
        private async void Woodlands_Clicked(object sender, EventArgs e)
        {
            currentCheckpoint = "Woodlands";

            int cars = random.Next(35, 55);
            UpdateDashboard("Woodlands", cars, "Heavy", 35, 0.80, 0.40);

           
            await Navigation.PushAsync(new WoodlandsPage());
        }

       
        private async void Tuas_Clicked(object sender, EventArgs e)
        {
            currentCheckpoint = "Tuas";

            int cars = random.Next(10, 25);
            UpdateDashboard("Tuas", cars, "Light", 10, 0.45, 0.25);

            
            await Navigation.PushAsync(new TuasPage());
        }

     
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            int cars = currentCheckpoint == "Woodlands"
                ? random.Next(30, 60)
                : random.Next(8, 28);

            string level = GetTrafficLevel(cars);
            int wait = GetWaitTime(cars);

            double woodlandsFlow = currentCheckpoint == "Woodlands"
                ? Math.Min(cars / 60.0, 1.0)
                : WoodlandsTrafficBar.Progress;

            double tuasFlow = currentCheckpoint == "Tuas"
                ? Math.Min(cars / 60.0, 1.0)
                : TuasTrafficBar.Progress;

            UpdateDashboard(currentCheckpoint, cars, level, wait, woodlandsFlow, tuasFlow);
        }

        
        private async void Playback_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Timelapse Playback",
                $"Playing last 10 minutes traffic footage for {currentCheckpoint} checkpoint.",
                "OK");
        }

       
        private void UpdateDashboard(string checkpoint, int carCount, string level, int waitMinutes,
            double woodlandsFlow, double tuasFlow)
        {
            CarCountLabel.Text = carCount.ToString();
            TrafficLevelLabel.Text = level;
            WaitTimeLabel.Text = $"{waitMinutes} mins";
            LastUpdatedLabel.Text = "Last Updated: " + DateTime.Now.ToString("hh:mm tt");

           
            TrafficTrendLabel.Text = carCount > previousCarCount
                ? "Increasing ¡ü"
                : carCount < previousCarCount
                    ? "Decreasing ¡ý"
                    : "Stable ¡ú";

            previousCarCount = carCount;

         
            TrafficLevelLabel.TextColor = level switch
            {
                "Heavy" => Colors.Red,
                "Moderate" => Colors.Orange,
                _ => Colors.Green
            };

            
            AlertLabel.Text = carCount >= 40
                ? $"? Heavy congestion detected at {checkpoint}"
                : $"? {checkpoint} traffic is moving smoothly";

            AlertLabel.TextColor = carCount >= 40 ? Colors.Red : Colors.Green;

            
            WoodlandsTrafficBar.Progress = woodlandsFlow;
            TuasTrafficBar.Progress = tuasFlow;
        }

       
        private string GetTrafficLevel(int cars)
        {
            if (cars >= 40) return "Heavy";
            if (cars >= 20) return "Moderate";
            return "Light";
        }

        
        private int GetWaitTime(int cars)
        {
            if (cars >= 40) return 35;
            if (cars >= 20) return 20;
            return 10;
        }
    }
}