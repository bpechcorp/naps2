using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Util
{
    public static class Platform
    {
#if LINUX
        private static IPlatformCompat _compat = new LinuxPlatformCompat();
#else
        private static IPlatformCompat _compat = new WindowsPlatformCompat();
#endif

        public static IPlatformCompat Compat
        {
            get { return _compat; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _compat = value;
            }
        }
    }
}
