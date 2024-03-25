using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3.QA.Utils
{
    public static class NullTypeConvertExtensionMethod
    {
        public static int? AsInt32(this string value)
        {
            int? ret = new int?();
            int val;
            if (int.TryParse(value, out val)) ret = val;
            return ret;
        }

        public static decimal? AsDecimal(this string value)
        {
            decimal? ret = new decimal?();
            decimal val;
            if (decimal.TryParse(value, out val)) ret = val;
            return ret;
        }
    }
}
