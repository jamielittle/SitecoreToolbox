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
        public Item DataSourceItem
        {
            get
            {
                // Ensure at the very least Context Item is returned as a fallback
                Item returnItem = Sitecore.Context.Item;

                Sitecore.Web.UI.WebControls.Sublayout controlSublayout = (this.Parent) as Sitecore.Web.UI.WebControls.Sublayout;
                if (controlSublayout != null)
                {
                    string dataSourceString = controlSublayout.DataSource;

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
