using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Web.UI;
using Sitecore.Web;

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

            return Parameters != null &&
                   Parameters[key] != null &&
                   !String.IsNullOrWhiteSpace(Parameters[key]);
        }

        public void BindDatasourceToSitecoreControls()
        {
            BindDatasourceToSitecoreControls(DataSourceItem);
        }

        public void BindDatasourceToSitecoreControls(Item dataSource)
        {
            if (Controls.Count > 0)
            {
                BindControls(this.Controls, dataSource);
            }
        }

        private void BindControls(ControlCollection controls, Item dataSource)
        {
            foreach (Control control in controls)
            {
                if (control is FieldControl)
                {
                    var fieldControl = control as FieldControl;
                    fieldControl.Item = dataSource;
                }

                if (control is FieldRenderer)
                {
                    var fieldRenderer = control as FieldRenderer;
                    fieldRenderer.Item = dataSource;
                }

                if (control.Controls.Count > 0)
                {
                    BindControls(control.Controls, dataSource);
                }
            }
        }
    }
}
