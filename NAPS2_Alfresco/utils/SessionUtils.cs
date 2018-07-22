using DotCMIS;
using DotCMIS.Client;
using DotCMIS.Client.Impl;
using DotCMIS.Data.Impl;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NAPS2_Alfresco.utils
{
    public class SessionUtils
    {
        public static ISession Session;

        private static string GetAtomPubUrl(string url)
        {
            if (!url.EndsWith(AlfDefs.SUFFIX_ATOM_PUB_URL))
                return url + AlfDefs.SUFFIX_ATOM_PUB_URL;
            return url;
        }

        public static ISession CreateSession(string serviceUrl, string username, string password)
        {
            ISession session = null;
            try
            {
                // define dictonary with key value pair
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                // define binding type, in our example we are using ATOMPUB as stated above
                parameters[DotCMIS.SessionParameter.BindingType] = BindingType.AtomPub;

                // define CMIS available path which is already available under alfresco
                parameters[DotCMIS.SessionParameter.AtomPubUrl] = GetAtomPubUrl(serviceUrl);

                // alfresco portal admin user name
                parameters[DotCMIS.SessionParameter.User] = username;

                // alfresco portal admin password
                parameters[DotCMIS.SessionParameter.Password] = password;

                // define session factory
                SessionFactory factory = SessionFactory.NewInstance();

                // using session factory get the default repository, on this repository we would be performing actions & create session on this repository
                session = factory.GetRepositories(parameters)[0].CreateSession();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return session;
        }

        public static AlfResult UploadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    AuthInfo authInfo = AuthInfo.ReadAuthInfo();
                    //IFolder rootFolder = session.GetRootFolder();
                    IFolder userHomeFolder = (IFolder)Session.GetObjectByPath("/" + AlfDefs.DEFAULT_DIR_NAME_USER_HOMES + "/" + authInfo.username);

                    // document name
                    string formattedName = Path.GetFileName(filePath);

                    // define dictionary
                    IDictionary<string, object> properties = new Dictionary<string, object>();
                    properties.Add(PropertyIds.Name, formattedName);

                    // define object type as document, as we wanted to create document
                    properties.Add(PropertyIds.ObjectTypeId, "cmis:document");

                    // read a empty document with empty bytes
                    // fileUpload1: is a .net file upload control
                    byte[] datas = File.ReadAllBytes(filePath);
                    string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(filePath));
                    ContentStream contentStream = new ContentStream
                    {
                        FileName = formattedName,
                        MimeType = mimeType,
                        Length = datas.Length,
                        Stream = new MemoryStream(datas)
                    };

                    // this statment would create document in default repository
                    userHomeFolder.CreateDocument(properties, contentStream, null);

                    return new AlfResult(AlfResult.STATUS_OK, "Success");
                }
                else
                {
                    return new AlfResult(AlfResult.STATUS_FILE_NOT_FOUND, "File not found");
                }

            }
            catch (Exception e)
            {
                return new AlfResult(AlfResult.STATUS_EXCEPTION, e.StackTrace);
            }
        }
    }
}
