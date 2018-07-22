using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NAPS2_Alfresco
{
    public class AlfDefs
    {
        public const string RegistryHomePath = "Software\\AlfrescoUploader";
        public const string KEY_NAME_SERVICE_URL = "serviceUrl";
        public const string KEY_NAME_USERNAME = "username";
        public const string KEY_NAME_PASSWORD = "password";
        public const string KEY_NAME_DIR_NAME_COMPANY_HOME = "companyHome";
        public const string KEY_NAME_DIR_NAME_USER_HOMES = "userHomes";

        public const string DEFAULT_SERVICE_URL = "http://ecm.bdoservice.vn:8088";
        public const string SUFFIX_ATOM_PUB_URL = "/alfresco/api/-default-/public/cmis/versions/1.0/atom";
        public const string DEFAULT_DIR_NAME_COMPANY_HOME = "Company Home";
        public const string DEFAULT_DIR_NAME_USER_HOMES = "User Homes";

        public const string TEXT_LOGIN = "Login";
        public const string TEXT_LOGOUT = "Logout";
    }
}
