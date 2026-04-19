using System.Net.Http.Json;

namespace TrafficMonitor;

public class TrafficApiService
{
    private HttpClient client = new HttpClient();

    public async Task<TrafficData> GetTrafficAsync()
    {
        try
        {
            // 可替换真实接口,next week
            var data = await client.GetFromJsonAsync<TrafficData>(
                "https://mocki.io/v1/7b0a5f9e-traffic-demo");

            if (data != null)
                return data;
        }
        catch
        {
            ）
        }

        /
        return new TrafficData
        {
            WoodlandsCars = new Random().Next(30, 60),
            TuasCars = new Random().Next(10, 30)
        };
    }
}

public class TrafficData
{
    public int WoodlandsCars { get; set; }
    public int TuasCars { get; set; }
}