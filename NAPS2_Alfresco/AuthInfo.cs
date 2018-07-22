using NAPS2_Alfresco.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2_Alfresco
{
    public class AuthInfo
    {
        public string serviceUrl { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public AuthInfo(string serviceUrl, string username, string password)
        {
            this.serviceUrl = serviceUrl;
            this.username = username;
            this.password = password;
        }

        public static AuthInfo ReadAuthInfo()
        {
            string serviceUrl = RegistryUtils.GetValue(AlfDefs.KEY_NAME_SERVICE_URL);
            string userName = RegistryUtils.GetValue(AlfDefs.KEY_NAME_USERNAME);
            string password = RegistryUtils.GetValue(AlfDefs.KEY_NAME_PASSWORD);
            if (string.IsNullOrWhiteSpace(serviceUrl)) serviceUrl = AlfDefs.DEFAULT_SERVICE_URL;

            return new AuthInfo(serviceUrl, userName, password);
        }

        public static void SaveAuthInfo(AuthInfo authInfo)
        {
            RegistryUtils.SetValue(AlfDefs.KEY_NAME_SERVICE_URL, authInfo.serviceUrl);
            RegistryUtils.SetValue(AlfDefs.KEY_NAME_USERNAME, authInfo.username);
            RegistryUtils.SetValue(AlfDefs.KEY_NAME_PASSWORD, authInfo.password);
        }
    }
}
