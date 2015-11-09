using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Util
{
    public class LinuxPlatformCompat : IPlatformCompat
    {
        public bool UseToolStripRenderHack { get { return false; } }
        public bool AllowEmail { get { return false; } }
        public bool UseWebClientAsync { get { return false; } }
        public bool UseEmptyStringInListViewItems { get { return false; } }
        public bool UseStandardTextAlign { get { return false; } }
    }
}
