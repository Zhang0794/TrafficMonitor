using TrafficMonitor.Models;

namespace TrafficMonitor;

public partial class MainPage : ContentPage
{
    private User _currentUser;

    public MainPage(User user)
    {
        InitializeComponent();
        _currentUser = user;

        ShowHome();
    }

    
    void ShowHome()
    {
        MainContent.Content = new HomeView(); 
    }

    
    void ShowAnalytics()
    {
        MainContent.Content = new AnalyticsView();
    }

    
    void ShowUser()
    {
        var view = new UserView();
        view.SetUser(_currentUser.Username);

        MainContent.Content = view;
    }

    
    void Home_Clicked(object sender, EventArgs e)
    {
        ShowHome();
    }

    void Analytics_Clicked(object sender, EventArgs e)
    {
        ShowAnalytics();
    }

    void User_Clicked(object sender, EventArgs e)
    {
        ShowUser();
    }
}