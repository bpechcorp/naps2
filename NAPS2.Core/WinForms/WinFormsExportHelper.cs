using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using NAPS2.Config;
using NAPS2.ImportExport;
using NAPS2.ImportExport.Email;
using NAPS2.ImportExport.Images;
using NAPS2.ImportExport.Pdf;
using NAPS2.Lang.Resources;
using NAPS2.Ocr;
using NAPS2.Operation;
using NAPS2.Scan.Images;
using NAPS2.Util;
using NAPS2_Alfresco.utils;
using Newtonsoft.Json.Linq;
using Xceed.Words.NET;

namespace NAPS2.WinForms
{
    public class WinFormsExportHelper
    {
        private readonly PdfSettingsContainer pdfSettingsContainer;
        private readonly ImageSettingsContainer imageSettingsContainer;
        private readonly EmailSettingsContainer emailSettingsContainer;
        private readonly DialogHelper dialogHelper;
        private readonly FileNamePlaceholders fileNamePlaceholders;
        private readonly ChangeTracker changeTracker;
        private readonly IOperationFactory operationFactory;
        private readonly IFormFactory formFactory;
        private readonly OcrDependencyManager ocrDependencyManager;
        private readonly IEmailer emailer;

        public WinFormsExportHelper(PdfSettingsContainer pdfSettingsContainer, ImageSettingsContainer imageSettingsContainer, EmailSettingsContainer emailSettingsContainer, DialogHelper dialogHelper, FileNamePlaceholders fileNamePlaceholders, ChangeTracker changeTracker, IOperationFactory operationFactory, IFormFactory formFactory, OcrDependencyManager ocrDependencyManager, IEmailer emailer)
        {
            this.pdfSettingsContainer = pdfSettingsContainer;
            this.imageSettingsContainer = imageSettingsContainer;
            this.emailSettingsContainer = emailSettingsContainer;
            this.dialogHelper = dialogHelper;
            this.fileNamePlaceholders = fileNamePlaceholders;
            this.changeTracker = changeTracker;
            this.operationFactory = operationFactory;
            this.formFactory = formFactory;
            this.ocrDependencyManager = ocrDependencyManager;
            this.emailer = emailer;
        }

        public bool SavePDF(List<ScannedImage> images, ISaveNotify notify)
        {
            if (images.Any())
            {
                string savePath;

                var pdfSettings = pdfSettingsContainer.PdfSettings;
                if (pdfSettings.SkipSavePrompt && Path.IsPathRooted(pdfSettings.DefaultFileName))
                {
                    savePath = pdfSettings.DefaultFileName;
                }
                else
                {
                    if (!dialogHelper.PromptToSavePdf(pdfSettings.DefaultFileName, out savePath))
                    {
                        return false;
                    }
                }

                var subSavePath = fileNamePlaceholders.SubstitutePlaceholders(savePath, DateTime.Now);
                if (ExportPDF(subSavePath, images, false))
                {
                    changeTracker.HasUnsavedChanges = false;
                    notify?.PdfSaved(subSavePath);
                    return true;
                }
            }
            return false;
        }

        //=========================AlfSavePDF Start=========================
        public string getOcrId(string savePath)
        {
            Byte[] bytes = File.ReadAllBytes(savePath);
            string fileName = Path.GetFileName(savePath);
            string url = "http://103.63.109.205:9999/converts/upload";
            url += "?fileName=" + fileName;

            var client = new WebClient();
            //var values = new NameValueCollection();
            //values["base64"] = "aaaa";// Convert.ToBase64String(bytes);
            var response = client.UploadFile(url, savePath);
            var responseString = Encoding.Default.GetString(response);
            //var request = (HttpWebRequest)WebRequest.Create(url);
            //var postData = "base64=" + Convert.ToBase64String(bytes);
            //var data = Encoding.UTF8.GetBytes(postData);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //    stream.Close();
            //}
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject json = JObject.Parse(responseString);
            return json.GetValue("id").ToString();
        }

        public string getBase64DocX(string id)
        {
            string url = "http://103.63.109.205:9999/converts/getBase64DocX?id=" + id;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject json = JObject.Parse(responseString);
            return json.GetValue("data").ToString();
        }

        public string readAllDocx(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                DocX docx = DocX.Load(fs);
                return docx.Text;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public bool AlfSavePDF(List<ScannedImage> images, ISaveNotify notify)
        {
            if (images.Any())
            {
                var savePath = NAPS2_Alfresco.utils.FileUtils.GetTempPath() + "\\" + DateTime.Now.ToFileTime() + ".pdf";
                if (ExportPDF(savePath, images, false))
                {
                    changeTracker.HasUnsavedChanges = false;

                    try
                    {
                        string ocrId = getOcrId(savePath);
                        MessageBox.Show("Processing, please wait about 1 minutes", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Thread.Sleep(45000);
                        string docxData = getBase64DocX(ocrId);
                        Byte[] bytes = Convert.FromBase64String(docxData);
                        var docxSavePath = NAPS2_Alfresco.utils.FileUtils.GetTempPath() + "\\" + DateTime.Now.ToFileTime() + ".docx";
                        File.WriteAllBytes(docxSavePath, bytes);
                        string docxContent = readAllDocx(docxSavePath);
                        string docxContentLower = docxContent.ToLower();
                        //Debug.WriteLine(docxContent);

                        string desc = Regex.Split(docxContent, "\r\n|\r|\n")[0];
                        Debug.WriteLine(desc);
                        if (docxContent.Contains("HƯỚNG") || docxContentLower.Contains("hướng dẫn"))
                        {
                            SessionUtils.UploadFile(savePath, "huongdan", desc);
                            SessionUtils.UploadFile(docxSavePath, "huongdan", desc);
                        }
                        else if (docxContent.Contains("BÁO") || docxContentLower.Contains("báo cáo"))
                        {
                            SessionUtils.UploadFile(savePath, "baocao", desc);
                            SessionUtils.UploadFile(docxSavePath, "baocao", desc);
                        }
                        else if (docxContent.Contains("CHỈ") || docxContentLower.Contains("chỉ thị"))
                        {
                            SessionUtils.UploadFile(savePath, "chithi", desc);
                            SessionUtils.UploadFile(docxSavePath, "chithi", desc);
                        }

                        MessageBox.Show("SUCCESS", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //NAPS2_Alfresco.AlfResult alfResult = SessionUtils.UploadFile(savePath);
                    //if (alfResult.status == NAPS2_Alfresco.AlfResult.STATUS_OK)
                    //{
                    //    notify?.PdfSaved(savePath);
                    //    return true;
                    //}
                    //else
                    //{
                    //    MessageBox.Show(alfResult.message, "Error " + alfResult.status, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
            return false;
        }
        //=========================AlfSavePDF End=========================

        public bool ExportPDF(string filename, List<ScannedImage> images, bool email)
        {
            var op = operationFactory.Create<SavePdfOperation>();
            var progressForm = formFactory.Create<FProgress>();
            progressForm.Operation = op;

            var pdfSettings = pdfSettingsContainer.PdfSettings;
            pdfSettings.Metadata.Creator = MiscResources.NAPS2;
            if (op.Start(filename, DateTime.Now, images, pdfSettings, ocrDependencyManager.DefaultLanguageCode, email))
            {
                progressForm.ShowDialog();
            }
            return op.Status.Success;
        }

        public bool SaveImages(List<ScannedImage> images, ISaveNotify notify)
        {
            if (images.Any())
            {
                string savePath;

                var imageSettings = imageSettingsContainer.ImageSettings;
                if (imageSettings.SkipSavePrompt && Path.IsPathRooted(imageSettings.DefaultFileName))
                {
                    savePath = imageSettings.DefaultFileName;
                }
                else
                {
                    if (!dialogHelper.PromptToSaveImage(imageSettings.DefaultFileName, out savePath))
                    {
                        return false;
                    }
                }

                var op = operationFactory.Create<SaveImagesOperation>();
                var progressForm = formFactory.Create<FProgress>();
                progressForm.Operation = op;
                progressForm.Start = () => op.Start(savePath, DateTime.Now, images);
                progressForm.ShowDialog();
                if (op.Status.Success)
                {
                    changeTracker.HasUnsavedChanges = false;
                    notify?.ImagesSaved(images.Count, op.FirstFileSaved);
                    return true;
                }
            }
            return false;
        }

        public bool EmailPDF(List<ScannedImage> images)
        {
            if (images.Any())
            {
                var emailSettings = emailSettingsContainer.EmailSettings;
                var invalidChars = new HashSet<char>(Path.GetInvalidFileNameChars());
                var attachmentName = new string(emailSettings.AttachmentName.Where(x => !invalidChars.Contains(x)).ToArray());
                if (string.IsNullOrEmpty(attachmentName))
                {
                    attachmentName = "Scan.pdf";
                }
                if (!attachmentName.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
                {
                    attachmentName += ".pdf";
                }
                attachmentName = fileNamePlaceholders.SubstitutePlaceholders(attachmentName, DateTime.Now, false);

                var tempFolder = new DirectoryInfo(Path.Combine(Paths.Temp, Path.GetRandomFileName()));
                tempFolder.Create();
                try
                {
                    string targetPath = Path.Combine(tempFolder.FullName, attachmentName);
                    if (!ExportPDF(targetPath, images, true))
                    {
                        // Cancel or error
                        return false;
                    }
                    var message = new EmailMessage
                    {
                        Attachments =
                        {
                            new EmailAttachment
                            {
                                FilePath = targetPath,
                                AttachmentName = attachmentName
                            }
                        }
                    };

                    if (emailer.SendEmail(message))
                    {
                        changeTracker.HasUnsavedChanges = false;
                        return true;
                    }
                }
                finally
                {
                    tempFolder.Delete(true);
                }
            }
            return false;
        }
    }
}
