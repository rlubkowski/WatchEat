using System.ComponentModel;
using UIKit;
using WatchEat.Controls;
using WatchEat.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumericInput), typeof(NumericInputRenderer))]
namespace WatchEat.iOS
{
    public class NumericInputRenderer : EntryRenderer
    {
        private UITextField _native = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            _native = Control as UITextField;

            _native.KeyboardType = UIKeyboardType.NumberPad;

            if ((e.NewElement as NumericInput).AllowNegative == true && (e.NewElement as NumericInput).AllowFraction == true)
            {
                _native.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            }
            else if ((e.NewElement as NumericInput).AllowNegative == true)
            {
                _native.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            }
            else if ((e.NewElement as NumericInput).AllowFraction == true)
            {
                _native.KeyboardType = UIKeyboardType.DecimalPad;
            }
            else
            {
                _native.KeyboardType = UIKeyboardType.NumberPad;
            }
            if (e.NewElement.FontFamily != null)
            {
                e.NewElement.FontFamily = e.NewElement.FontFamily.Replace(".ttf", "");
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (_native == null)
                return;
        }
    }
}
