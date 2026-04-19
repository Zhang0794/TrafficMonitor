using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace TrafficMonitor
{
    public partial class HomeView : ContentView
    {
        private readonly TrafficApiService api = new TrafficApiService();
        private string currentCheckpoint = "Woodlands";
        private int previousCarCount = 24;

        public HomeView()
        {
            InitializeComponent();

            LoadFavorite();
            StartAutoRefresh();

            UpdateDashboard("Woodlands", 24, "Moderate", 20, 0.75, 0.35);
        }

        
        private void StartAutoRefresh()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                Refresh_Clicked(null, null);
                return true;
            });
        }

        
        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            var data = await api.GetTrafficAsync();

            int cars = currentCheckpoint == "Woodlands"
                ? data.WoodlandsCars
                : data.TuasCars;

            string level = GetTrafficLevel(cars);
            int wait = GetWaitTime(cars);

            UpdateDashboard(currentCheckpoint, cars, level, wait,
                Math.Min(data.WoodlandsCars / 60.0, 1.0),
                Math.Min(data.TuasCars / 60.0, 1.0));
        }

        
        private async void Favorite_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("favorite_checkpoint", currentCheckpoint);

            await Application.Current.MainPage.DisplayAlert(
                "Saved",
                $"{currentCheckpoint} added to favorites",
                "OK");
        }

        private void LoadFavorite()
        {
            string fav = Preferences.Get("favorite_checkpoint", "None");

            if (fav != "None")
            {
                AlertLabel.Text = $"? Favorite: {fav}";
            }
        }

        
        private async void Share_Clicked(object sender, EventArgs e)
        {
            string text = $"?? Traffic Update ({currentCheckpoint})\n" +
                          $"Cars: {CarCountLabel.Text}\n" +
                          $"Level: {TrafficLevelLabel.Text}\n" +
                          $"Wait: {WaitTimeLabel.Text}";

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Traffic Status"
            });
        }

        
        private async void Woodlands_Clicked(object sender, EventArgs e)
        {
            currentCheckpoint = "Woodlands";
            await Navigation.PushAsync(new WoodlandsPage());
        }

        private async void Tuas_Clicked(object sender, EventArgs e)
        {
            currentCheckpoint = "Tuas";
            await Navigation.PushAsync(new TuasPage());
        }

        
        private async void Playback_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Timelapse Playback",
                $"Playing last 10 minutes traffic footage for {currentCheckpoint}",
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
                ? $"? Heavy congestion at {checkpoint}"
                : $"? {checkpoint} traffic smooth";

            AlertLabel.TextColor = carCount >= 40 ? Colors.Red : Colors.Green;

           
            WoodlandsTrafficBar.Progress = woodlandsFlow;
            TuasTrafficBar.Progress = tuasFlow;
        }

        // ?? Âß¼­
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