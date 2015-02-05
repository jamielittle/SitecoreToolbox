using System;
using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Links;
using Sitecore.Publishing;

namespace Sitecore.Toolbox.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Get URL for current Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Item Url</returns>
        public static string Url(this Item item)
        {
            return LinkManager.GetItemUrl(item);
        }

        /// <summary>
        /// Get list of parent items
        /// </summary>
        /// <param name="item"></param>
        /// <param name="descending">Return descending</param>
        /// <param name="rootItem">Home or root top level item</param>
        /// <returns>List of parent items</returns>
        public static IEnumerable<Item> ParentList(this Item item, Boolean descending = true, Item rootItem = null)
        {
            if (item == null)
                return null;

            var parents = new List<Item>();

            var currentItem = item.Parent;

            while (currentItem != null)
            {
                if (rootItem != null)
                {
                    if (currentItem.ID == rootItem.ID)
                    {
                        break;
                    }
                }

                parents.Add(currentItem);
                currentItem = currentItem.Parent;
            }

            if (descending)
                parents.Reverse();

            return parents;
        }

        /// <summary>
        /// Is item derived from template
        /// </summary>
        /// <param name="item"></param>
        /// <param name="templateId">Template ID</param>
        /// <returns></returns>
        public static bool IsDerivedFrom(this Item item, ID templateId)
        {
            if (item == null)
                return false;

            if (templateId.IsNull)
                return false;

            var templateItem = item.Database.Templates[templateId];

            var returnValue = false;
            if (templateItem != null)
            {
                var template = TemplateManager.GetTemplate(item);

                returnValue = template != null && template.ID == templateItem.ID ||
                              template.DescendsFrom(templateItem.ID);
            }

            return returnValue;
        }

        /// <summary>
        /// Returns only children that derive from specified template
        /// </summary>
        /// <param name="item"></param>
        /// <param name="templateId">Template ID</param>
        /// <returns></returns>
        public static IEnumerable<Item> ChildrenDerivedFrom(this Item item, ID templateId)
        {
            var children = item.GetChildren();
            var childrenDerivedFrom = new List<Item>();

            foreach (Item child in children)
            {
                if (child.IsDerivedFrom(templateId))
                    childrenDerivedFrom.Add(child);
            }

            return childrenDerivedFrom;
        }

        /// <summary>
        /// Publish the item
        /// </summary>
        /// <param name="item"></param>
        public static void Publish(this Item item)
        {
            var publishOptions = new PublishOptions(item.Database,
                          Database.GetDatabase("web"),
                          PublishMode.SingleItem,
                          item.Language,
                          DateTime.Now); // Create a publisher with the publishoptions

            var publisher = new Publisher(publishOptions);

            // Choose where to publish from
            publisher.Options.RootItem = item;

            // Publish children as well?
            publisher.Options.Deep = true;

            // Do the publish!
            publisher.Publish();
        }

        public static DateTime GetDate(this Sitecore.Data.Items.Item item, string field)
        {
            Sitecore.Data.Fields.DateField dateField = item.Fields[field];
            if (dateField != null)
                return dateField.DateTime;

            return DateTime.MinValue;
        }
    }
}
