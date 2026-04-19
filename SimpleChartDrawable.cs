using Microsoft.Maui.Graphics;

namespace TrafficMonitor;

public class SimpleChartDrawable : IDrawable
{
    private List<float> data;

    public SimpleChartDrawable(List<float> data)
    {
        this.data = data;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (data == null || data.Count < 2)
            return;

        float width = dirtyRect.Width;
        float height = dirtyRect.Height;

        float step = width / (data.Count - 1);

        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeSize = 4;

        for (int i = 0; i < data.Count - 1; i++)
        {
            float x1 = i * step;
            float y1 = height - data[i];

            float x2 = (i + 1) * step;
            float y2 = height - data[i + 1];

            
            canvas.DrawLine(x1, y1, x2, y2);

            
            canvas.FillColor = Colors.Red;
            canvas.FillCircle(x1, y1, 4);
        }
    }
}