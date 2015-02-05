using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.Toolbox.Sublayout
{
    public class SublayoutBase : System.Web.UI.UserControl
    {
        public Sitecore.Web.UI.WebControls.Sublayout Sublayout
        {
            get
            {
                return Parent as Sitecore.Web.UI.WebControls.Sublayout;
            }
        }

        public Placeholder Placeholder
        {
            get
            {
                if (Sublayout != null)
                    return Sublayout.Parent as Placeholder;

                return null;
            }
        }

        public bool DataSourcePresent
        {
            get
            {
                return !string.IsNullOrEmpty(Sublayout.DataSource);
            }
        }

        public Item DataSourceItem
        {
            get
            {
                // Ensure at the very least Context Item is returned as a fallback
                Item returnItem = Sitecore.Context.Item;

                if (Sublayout != null)
                {
                    string dataSourceString = Sublayout.DataSource;

                    if (!string.IsNullOrEmpty(dataSourceString))
                    {
                        Item dbItem = Sitecore.Context.Database.GetItem(dataSourceString);

                        if (dbItem != null)
                        {
                            returnItem = dbItem;
                        }
                    }
                }

                return returnItem;
            }
        }
    }
}
