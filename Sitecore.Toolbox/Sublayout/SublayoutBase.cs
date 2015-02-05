using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System.Collections.Specialized;

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


        public NameValueCollection Parameters
        {
            get 
            { 
                return WebUtil.ParseUrlParameters(Sublayout.Parameters); 
            }
        }

        public bool ParameterPresent(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
                return false;

            return Request.Params[key] != null &&
                   !String.IsNullOrWhiteSpace(Request.Params[key]);
        }
    }
}
