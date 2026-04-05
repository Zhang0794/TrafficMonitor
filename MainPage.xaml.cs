using Microsoft.Maui.Storage;

namespace TrafficMonitor;

public partial class MainPage : ContentPage
{
    HomeView home = new HomeView();
    AnalyticsView analytics = new AnalyticsView();
    UserView user;

    public MainPage()
    {
        InitializeComponent();

        string username = Preferences.Get("username", "Guest");
        user = new UserView(username);

        user.LogoutRequested += OnLogout;

        ShowPage(home);
        SetActive(HomeBtn);
    }

    async void ShowPage(View view)
    {
        MainContent.Opacity = 0;
        MainContent.Content = view;
        await MainContent.FadeTo(1, 250);
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        ShowPage(home);
        SetActive(HomeBtn);
    }

    private void Analytics_Clicked(object sender, EventArgs e)
    {
        ShowPage(analytics);
        SetActive(AnalyticsBtn);
    }

    private void User_Clicked(object sender, EventArgs e)
    {
        string username = Preferences.Get("username", "Guest");
        user = new UserView(username);
        user.LogoutRequested += OnLogout;

        ShowPage(user);
        SetActive(UserBtn);
    }

    private async void OnLogout()
    {
        bool confirm = await DisplayAlert("确认退出", "确定要退出登录吗？", "是", "取消");

        if (confirm)
        {
            Preferences.Remove("username");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }

    void SetActive(Button btn)
    {
        HomeBtn.BackgroundColor = Colors.Transparent;
        AnalyticsBtn.BackgroundColor = Colors.Transparent;
        UserBtn.BackgroundColor = Colors.Transparent;

        btn.BackgroundColor = Color.FromArgb("#E5E7EB");
    }
}