using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WatchEat.Helpers.MethodExtensions
{
    public static class NavigationExtensions
    {
       public static async Task<Page[]> PopModalToRootAsync(this INavigation navigation) 
       {
            var tasks = new List<Task<Page>>();
            while (navigation.ModalStack.Count > 0) 
            {
                tasks.Add(navigation.PopModalAsync());
            }
            return await Task.WhenAll(tasks);
        } 
    }
}
