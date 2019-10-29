using Xamarin.Forms;

namespace WatchEat.Effects
{
    public class ContainerShadowEffect : RoutingEffect
    {
        public ContainerShadowEffect() : base("WatchEat.ContainerShadowEffect")
        {
        }

        public float Radius { get; set; }

        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }
    }
}
