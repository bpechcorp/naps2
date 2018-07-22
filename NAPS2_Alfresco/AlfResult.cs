using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2_Alfresco
{
    public class AlfResult
    {
        public const int STATUS_OK = 100;
        public const int STATUS_EXCEPTION = 101;
        public const int STATUS_FILE_NOT_FOUND = 102;

        public int status { get; set; }
        public string message { get; set; }

        public AlfResult(int status, string message)
        {
            this.status = status;
            this.message = message;
        }
    }
}
