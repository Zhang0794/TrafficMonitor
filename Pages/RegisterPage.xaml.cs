using TrafficMonitor.Services;

namespace TrafficMonitor.Pages
{
    public partial class RegisterPage : ContentPage
    {
        DatabaseService _db = new DatabaseService();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = newUsername.Text?.Trim();
            string password = newPassword.Text?.Trim();
            string confirm = confirmPassword.Text?.Trim();

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirm))
            {
                await DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }

            if (password != confirm)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            bool success = await _db.Register(username, password);

            if (!success)
            {
                await DisplayAlert("Error", "Username already exists", "OK");
                return;
            }

            await DisplayAlert("Success", "Registration successful!", "OK");
            await Navigation.PopAsync();
        }
    }
}