using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace Sitecore.Toolbox.ContentSearch.TypeConverters
{
    public class ListIDConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(List<ID>))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(List<string>))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var scIds = new List<ID>();

            var ids = (List<string>)value;

            foreach (var id in ids)
            {
                ID scId;
                if (ID.TryParse(id, out scId))
                {
                    scIds.Add(scId);
                }
            }

            return scIds;
        }
    }
}
