using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    public static class Extensions
    {
        public static string toUtf8(this string unknown)
        {
            return string.IsNullOrEmpty(unknown)? "" : new string(unknown.ToCharArray().
                Select(x => ((x + 848) >= 'А' && (x + 848) <= 'ё') ? (char)(x + 848) : x).
                ToArray());
        }
    }
}
