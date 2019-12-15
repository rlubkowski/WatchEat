using System;
using System.Linq;
using System.Reflection;
using WatchEat.Helpers.Attributes;

namespace WatchEat.Helpers.MethodExtensions
{
    public static class EnumExtensions
    {
        public static string GetTranslation(this Enum enumval)
        {
            string result = enumval.ToString();
            var attribute = enumval.GetType().GetRuntimeField(result).GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false).SingleOrDefault() as LocalizedDescriptionAttribute;

            if (attribute != null)
                result = attribute.Description;
            return result;
        }
    }
}
