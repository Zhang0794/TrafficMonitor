using SQLite;

namespace TrafficMonitor;

public class TrafficDatabase
{
    SQLiteAsyncConnection database;

    public async Task Init()
    {
        if (database != null)
            return;

        var path = Path.Combine(FileSystem.AppDataDirectory, "traffic.db3");
        database = new SQLiteAsyncConnection(path);
        await database.CreateTableAsync<TrafficRecord>();
    }

    public async Task SaveRecord(TrafficRecord record)
    {
        await Init();
        await database.InsertAsync(record);
    }

    public async Task<List<TrafficRecord>> GetRecords()
    {
        await Init();
        return await database.Table<TrafficRecord>()
            .OrderByDescending(x => x.Timestamp)
            .Take(10)
            .ToListAsync();
    }
}