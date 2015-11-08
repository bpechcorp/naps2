using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Util
{
    public class LinuxPlatformCompat : IPlatformCompat
    {
        public bool UseToolStripRenderHack { get { return false; } }
    }
}
