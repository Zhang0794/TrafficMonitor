namespace TrafficMonitor;

public partial class UserView : ContentView
{
    public event Action LogoutRequested;

    public UserView(string username)
    {
        InitializeComponent();
        UsernameLabel.Text = username;
    }

    private void Logout_Clicked(object sender, EventArgs e)
    {
        LogoutRequested?.Invoke();
    }
}