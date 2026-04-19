using SQLite;
using TrafficMonitor.Models;

namespace TrafficMonitor.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db");
            _db = new SQLiteAsyncConnection(dbPath);
        }

        public async Task Init()
        {
            await _db.CreateTableAsync<User>();
        }

        public async Task<bool> Register(string username, string password)
        {
            var existing = await _db.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (existing != null)
                return false;

            await _db.InsertAsync(new User
            {
                Username = username,
                PasswordHash = password
            });

            return true;
        }

        public async Task<User> Login(string username, string password)
        {
            return await _db.Table<User>()
                .Where(u => u.Username == username && u.PasswordHash == password)
                .FirstOrDefaultAsync();
        }
    }
}