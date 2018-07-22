using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NAPS2_Alfresco.utils
{
    public class FileUtils
    {
        public static string GetTempPath()
        {
            string tempPath = System.IO.Path.GetTempPath() + "\\AlfrescoUploader";
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            return tempPath;
        }
    }
}
