namespace TrafficMonitor;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new LoginPage());
        MainPage = new NavigationPage(new MainPage());
    }
}