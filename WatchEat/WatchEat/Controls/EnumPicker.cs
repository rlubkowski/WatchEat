using System;
using System.Linq;
using System.Reflection;
using WatchEat.Helpers.Attributes;
using Xamarin.Forms;

namespace WatchEat.Controls
{
    public class EnumPicker<T> : Picker where T : struct
    {
        public EnumPicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
            //Fill the Items from the enum
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Items.Add(GetEnumDescription(value));
            }

            SelectedIndex = Items.IndexOf(GetEnumDescription(SelectedItem));
        }

        public new static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(T), typeof(EnumPicker<T>), default(T),
            propertyChanged: OnSelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);
        

        public new T SelectedItem
        {
            get { return (T)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = default(T);
            }
            else
            {
                //try parsing, if using description this will fail
                T match;
                if (!Enum.TryParse<T>(Items[SelectedIndex], out match))
                {
                    //find enum by Description
                    match = GetEnumByDescription(Items[SelectedIndex]);
                }
                SelectedItem = (T)Enum.Parse(typeof(T), match.ToString());
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as EnumPicker<T>;
            if (newvalue != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(GetEnumDescription(newvalue));
            }
        }

        private static string GetEnumDescription(object value)
        {
            string result = value.ToString();
            var attribute = typeof(T).GetRuntimeField(result).GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false).SingleOrDefault() as LocalizedDescriptionAttribute;

            if (attribute != null)
                result = attribute.Description;          
            return result;
        }

        private T GetEnumByDescription(string description)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(x => string.Equals(GetEnumDescription(x), description));
        }
    }
}
