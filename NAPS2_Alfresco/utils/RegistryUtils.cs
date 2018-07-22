using Microsoft.Win32;
using System.IO;

namespace NAPS2_Alfresco.utils
{
    public class RegistryUtils
    {
        private static RegistryKey GetHomeKey()
        {
            char separatorChar = Path.DirectorySeparatorChar;
            RegistryKey regHomeKey = Registry.CurrentUser.OpenSubKey(AlfDefs.RegistryHomePath, true);
            if (regHomeKey == null)
            {
                regHomeKey = Registry.CurrentUser.CreateSubKey(AlfDefs.RegistryHomePath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            return regHomeKey;
        }

        public static string GetValue(string keyName)
        {
            RegistryKey regHomeKey = GetHomeKey();
            string value = (string)regHomeKey.GetValue(keyName);
            if (!string.IsNullOrEmpty(value))
            {
                return value.Trim();
            }
            return string.Empty;
        }

        public static void SetValue(string keyName, string value)
        {
            RegistryKey regHomeKey = GetHomeKey();
            regHomeKey.SetValue(keyName.Trim(), value.Trim());
        }
    }
}
