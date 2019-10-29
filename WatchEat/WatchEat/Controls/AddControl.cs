using SkiaSharp.Views.Forms;

namespace WatchEat.Controls
{
    public class AddControl : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
        }
    }
}
