using System;
using System.Linq;
using System.Windows.Forms;
using NAPS2.DI.EntryPoints;
using NAPS2.DI.Modules;
using NAPS2.Host;
using NAPS2_Alfresco;
using NAPS2_Alfresco.utils;

namespace NAPS2
{
    static class Program
    {

        private static void StartApp(string[] args)
        {
            if (args.Contains(X86HostManager.HOST_ARG))
            {
                typeof(X86HostEntryPoint).GetMethod("Run").Invoke(null, new object[] { args });
            }
            else
            {
                typeof(WinFormsEntryPoint).GetMethod("Run").Invoke(null, new object[] { args });
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (SessionUtils.Session == null)
            {
                AuthInfo authInfo = AuthInfo.ReadAuthInfo();

                if (!string.IsNullOrWhiteSpace(authInfo.username) && !string.IsNullOrWhiteSpace(authInfo.password))
                {
                    SessionUtils.Session = SessionUtils.CreateSession(authInfo.serviceUrl, authInfo.username, authInfo.password);
                }
            }
            StartApp(args);
        }
    }
}
