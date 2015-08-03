using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistControls
{
    public static class Extentions
    {
        public static bool ContainsGroup(this ListViewGroupCollection collection, Func<string, bool> match)
        {
            foreach (ListViewGroup g in collection)
                if (match(g.Name))
                    return true;
            return false;
        }
    }
}
