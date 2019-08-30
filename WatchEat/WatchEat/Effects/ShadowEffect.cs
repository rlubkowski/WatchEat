using Xamarin.Forms;

namespace WatchEat.Effects
{
    public class ShadowEffect : RoutingEffect
    {
        public ShadowEffect() : base("WatchEat.LabelShadowEffect")
        {
        }

        public float Radius { get; set; }

        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }
    }
}
