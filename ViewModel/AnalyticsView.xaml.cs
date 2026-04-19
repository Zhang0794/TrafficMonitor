using Microcharts.Maui;
using Microsoft.Maui.Graphics;

namespace TrafficMonitor;

public partial class AnalyticsView : ContentView
{
    private SimpleChartDrawable chart;
    private Random random = new Random();

    
    private List<float> data = new List<float> { 50, 60, 55, 70, 65 };

    public AnalyticsView()
    {
        InitializeComponent();

        chart = new SimpleChartDrawable(data);
        ChartView.Drawable = chart;

        StartLiveUpdate();
    }

    
    private void StartLiveUpdate()
    {
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            UpdateData();
            ChartView.Invalidate(); 

            return true; 
        });
    }


    private void UpdateData()
    {
        if (data.Count > 0)
            data.RemoveAt(0);

        data.Add(random.Next(40, 100));
    }
}