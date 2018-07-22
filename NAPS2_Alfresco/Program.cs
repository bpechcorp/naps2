using DotCMIS;
using DotCMIS.Client;
using DotCMIS.Data.Impl;
using MimeTypes;
using NAPS2_Alfresco.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NAPS2_Alfresco
{
    static class Program
    {
        private static IFolder GetUserFolder(ISession session, string username)
        {
            //char at = '@';
            //if(username.Contains(at)) return (IFolder)session.GetObjectByPath("/User Homes/" + username.Split(at)[0]);
            return (IFolder)session.GetObjectByPath("/User Homes/" + username);
        }

        private static void UploadTest()
        {
            AuthInfo authInfo = AuthInfo.ReadAuthInfo();
            ISession session = SessionUtils.CreateSession(authInfo.serviceUrl, authInfo.username, authInfo.password);
            //IFolder rootFolder = session.GetRootFolder();
            IFolder userHomeFolder = GetUserFolder(session, authInfo.username);

            // document name
            string formattedName = "Home Page.pdf";

            // define dictionary
            IDictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add(PropertyIds.Name, formattedName);

            // define object type as document, as we wanted to create document
            properties.Add(PropertyIds.ObjectTypeId, "cmis:document");

            // read a empty document with empty bytes
            // fileUpload1: is a .net file upload control
            byte[] datas = File.ReadAllBytes("C:\\Users\\KVM\\Home Page.pdf");
            ContentStream contentStream = new ContentStream
            {
                FileName = formattedName,
                MimeType = "application/pdf",
                Length = datas.Length,
                Stream = new MemoryStream(datas)
            };

            // this statment would create document in default repository
            userHomeFolder.CreateDocument(properties, contentStream, null);
            Debug.WriteLine("OK");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string filePath = "C:\\Users\\KVM\\Home Page.pdf";
            Debug.WriteLine(MimeTypeMap.GetMimeType(Path.GetExtension(filePath)));
        }
    }
}
