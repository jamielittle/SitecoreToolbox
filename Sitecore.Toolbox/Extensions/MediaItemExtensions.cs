using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace Sitecore.Toolbox.Extensions
{
    public static class MediaItemExtensions
    {
        public static string MediaUrl(this MediaItem mediaItem)
        {
            var mediaUrlOptions = new MediaUrlOptions() { };

            return Sitecore.StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(mediaItem, mediaUrlOptions));
        }
    }
}
