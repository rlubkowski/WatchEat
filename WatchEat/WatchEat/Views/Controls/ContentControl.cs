using Xamarin.Forms;

namespace WatchEat.Views.Controls
{
    public class ContentControl : ContentView
    {      
        public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create(nameof(ContentTemplate), 
                                                                                                  typeof(DataTemplate),
                                                                                                  typeof(ContentControl),
                                                                                                  null,
                                                                                                  BindingMode.OneWay,
                                                                                                  propertyChanged: OnContentTemplateChanged);

        private static void OnContentTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = bindable as ContentControl;
            if (layout != null && layout.ContentTemplate != null)
            {
                layout.BuildLayout();
                layout.ForceLayout();
            }
        }
        
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), 
                                                                                         typeof(object), 
                                                                                         typeof(ContentControl),
                                                                                         null,
                                                                                         BindingMode.OneWay,
                                                                                         propertyChanged: OnSourceChanged);
        
        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = bindable as ContentControl;
            if (layout != null && layout.ContentTemplate != null)
            {
                layout.BuildLayout();
                layout.ForceLayout();
            }
        }

        private void BuildLayout()
        {
            var view = (View)ContentTemplate.CreateContent();
            view.BindingContext = Source;
            Content = view;
        }
    }
}

