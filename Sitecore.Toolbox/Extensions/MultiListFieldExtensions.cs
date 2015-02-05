using System.Linq;
using Sitecore.Data.Items;

namespace Sitecore.Toolbox.Extensions
{
    public static class MultiListFieldExtensions
    {
        public static MediaItem[] GetMediaItems(this Sitecore.Data.Fields.MultilistField multilistField)
        {
            var items = multilistField.GetItems();

            return items.Select(item => new MediaItem(item)).ToArray();
        }

    }
}
