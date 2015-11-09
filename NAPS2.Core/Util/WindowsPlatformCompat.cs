using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Util
{
    public class WindowsPlatformCompat : IPlatformCompat
    {
        public bool UseToolStripRenderHack { get { return true; } }
        public bool AllowEmail { get { return true; } }
        public bool UseWebClientAsync { get { return true; } }
        public bool UseEmptyStringInListViewItems { get { return true; } }
        public bool UseStandardTextAlign { get { return true; } }
    }
}
