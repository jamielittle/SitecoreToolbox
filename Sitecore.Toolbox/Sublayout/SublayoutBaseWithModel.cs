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
    public abstract class SublayoutBase<T> : SublayoutBase
    {
        public T Model { get; set; }

        public abstract void CreateModel();
    }
}
