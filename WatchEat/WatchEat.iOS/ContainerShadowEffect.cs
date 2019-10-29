using CoreGraphics;
using System;
using System.Linq;
using WatchEat.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("WatchEat")]
[assembly: ExportEffect(typeof(ContainerShadowEffect), "ContainerShadowEffect")]
namespace WatchEat.iOS
{
    public class ContainerShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (Effects.ContainerShadowEffect)Element.Effects.FirstOrDefault(e => e is Effects.ContainerShadowEffect);

                if (effect != null)
                {
                    Container.Layer.CornerRadius = effect.Radius;
                    Container.Layer.ShadowColor = effect.Color.ToCGColor();
                    Container.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
                    Container.Layer.ShadowOpacity = 0.5f;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}