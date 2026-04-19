using SQLite;

namespace TrafficMonitor.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}