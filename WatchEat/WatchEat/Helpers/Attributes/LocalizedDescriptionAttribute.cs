using System.ComponentModel;
using WatchEat.Resources;

namespace WatchEat.Helpers.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        public LocalizedDescriptionAttribute(string resourceId) : base(GetStringFromResource(resourceId))
        { }

        private static string GetStringFromResource(string resourceId)
        {
            return AppResource.ResourceManager.GetString(resourceId);
        }
    }
}
