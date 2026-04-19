using Microsoft.Maui.Graphics;

namespace TrafficMonitor
{
   
    public class TrafficChartDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 4;

            float width = dirtyRect.Width;
            float height = dirtyRect.Height;

            
            canvas.DrawLine(10, height - 40, width * 0.25f, height - 100);
            canvas.DrawLine(width * 0.25f, height - 100, width * 0.5f, height - 70);
            canvas.DrawLine(width * 0.5f, height - 70, width * 0.75f, height - 140);
            canvas.DrawLine(width * 0.75f, height - 140, width - 10, height - 90);
        }
    }
}