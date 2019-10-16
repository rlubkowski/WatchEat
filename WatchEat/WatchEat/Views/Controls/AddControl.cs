using SkiaSharp.Views.Forms;

namespace WatchEat.Views.Controls
{
    public class AddControl : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
        }
    }
}
