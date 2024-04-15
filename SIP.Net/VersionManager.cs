using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SIP.Net
{
    public class VersionManager
    {

        public delegate void DownloadProgressChangedHandler(DownloadProgressChangedEventArgs e);
        public event DownloadProgressChangedHandler OnDownloadProgressChanged;

        public delegate void CheckForUpdatesCompleteHandler(PublishedVersion PublishedVersion);
        public event CheckForUpdatesCompleteHandler OnCheckForUpdateComplete;

        public delegate void FileDownloadedHandler(string DownloadedFile);
        public event FileDownloadedHandler OnFileDownloaded;

        public string saveToFileName = "";

        public void CheckForUpdates(Uri Address)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadDataCompleted += webClient_DownloadDataCompleted;            
            webClient.DownloadDataAsync(Address);


        }
        private void webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            MemoryStream memoryStream = new MemoryStream(e.Result);


            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(memoryStream);

            PublishedVersion publishedVersion = new PublishedVersion();

            publishedVersion.ReleaseDate = DateTime.Parse(((XmlNode)xmlDoc.SelectSingleNode("/SIP/Release[1]/date[1]")).InnerText);                                                                                             
            publishedVersion.NewVersionNumber = xmlDoc.SelectSingleNode("/SIP/Release[1]/@version").InnerText;
            publishedVersion.UrlNewPackage = xmlDoc.SelectSingleNode("/SIP/Release[1]/url[1]").InnerText;            
            publishedVersion.Installpath = xmlDoc.SelectSingleNode("/SIP/Release[1]/installpath[1]").InnerText;
            publishedVersion.App = xmlDoc.SelectSingleNode("/SIP/Release[1]/app[1]").InnerText;
            if (this.OnCheckForUpdateComplete != null)
            {
                OnCheckForUpdateComplete(publishedVersion);
            }


        }

        public void DownloadFile(Uri Address,string SaveToFileName)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
            webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;

            saveToFileName = SaveToFileName;

            try
            {

                webClient.DownloadFileAsync(Address, SaveToFileName);
            }
            catch (Exception Ex)
            {

            }
            //Path.GetFileName(Address.LocalPath)

        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.OnDownloadProgressChanged != null)
            {
                OnDownloadProgressChanged(e);
            }
        }

        void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (this.OnFileDownloaded != null)
            {
                this.OnFileDownloaded(saveToFileName);
            }
        }
    }    
}
