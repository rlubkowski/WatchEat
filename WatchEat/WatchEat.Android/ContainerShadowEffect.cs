using System;
using System.Linq;
using WatchEat.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("WatchEat")]
[assembly: ExportEffect(typeof(ContainerShadowEffect), "ContainerShadowEffect")]
namespace WatchEat.Droid
{   
    public class ContainerShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control ?? Container as Android.Views.View;

                var effect = (Effects.ContainerShadowEffect)Element.Effects.FirstOrDefault(e => e is Effects.ContainerShadowEffect);


                if (effect != null)
                {
                    float radius = effect.Radius;
                    control.Elevation = radius;
                    control.TranslationZ = (effect.DistanceX + effect.DistanceY) / 2;
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