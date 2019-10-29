using Xamarin.Forms;

namespace WatchEat.Effects
{
    public class LabelShadowEffect : RoutingEffect
    {
        public LabelShadowEffect() : base("WatchEat.LabelShadowEffect")
        {
        }

        public float Radius { get; set; }

        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }
    }
}
