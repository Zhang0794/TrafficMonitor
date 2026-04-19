using SQLite;

namespace TrafficMonitor;

public class TrafficRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public int Cars { get; set; }

    public string Checkpoint { get; set; }
}