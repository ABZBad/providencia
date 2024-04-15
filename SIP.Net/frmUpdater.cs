using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using SIP.Net.Properties;

namespace SIP.Net
{
    public partial class frmUpdater : Form
    {
        
        private delegate void DelegateSetProgressBarValue(int Value);
        private BackgroundWorker bgwUnZip = new BackgroundWorker();
        private PublishedVersion pv = new PublishedVersion();
        private string LOCAL_TEMP_PATH;
        private string APP_FOLDER_TO_UNZIP;
        private string newVersionUrl = string.Empty;
        private Assembly localFileAssembly;
        private string localFileAssemblyPath = string.Empty;
        public frmUpdater()
        {
            InitializeComponent();
            LOCAL_TEMP_PATH = AppDomain.CurrentDomain.BaseDirectory + "tmp";
            bgwUnZip.DoWork += bgwUnZip_DoWork;
            bgwUnZip.RunWorkerCompleted += bgwUnZip_RunWorkerCompleted;
        }

        private void SetProgressBarValue(int Value)
        {
            if (progressBar1.InvokeRequired)
            {
                DelegateSetProgressBarValue del = new DelegateSetProgressBarValue(SetProgressBarValue);
                progressBar1.Invoke(del, new object[] {Value});
            }
            else
            {
                progressBar1.Value = Value;    
            }
            
        }

        private void RunApp()
        {
            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo(APP_FOLDER_TO_UNZIP + pv.App);
            proc.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            Process proccess = System.Diagnostics.Process.Start(proc);
            proccess.WaitForInputIdle();
            Application.Exit();

            Apis.SetForegroundWindow(proccess.MainWindowHandle);
        }
        void bgwUnZip_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            RunApp();
        }

        void bgwUnZip_DoWork(object sender, DoWorkEventArgs e)
        {

            string zipFile = e.Argument.ToString();

            using (ZipFile compressedFile = ZipFile.Read(zipFile))
            {

                int totalFiles = compressedFile.Entries.Count();
                int iCont = 0;
                foreach (ZipEntry zipEntry in compressedFile)
                {
                    iCont++;
                    
                    int progressValue = Convert.ToInt32(Math.Round((iCont / Convert.ToDouble(totalFiles)) * 100, 0));
                    SetProgressBarValue(progressValue);
                    zipEntry.Extract(APP_FOLDER_TO_UNZIP, ExtractExistingFileAction.DoNotOverwrite);
                }
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Prerequisites.CrstalReportsInstalled())
                picCrystal.Image = Resources.check;
            else
            {
                picCrystal.Image = Resources.fail;
                picCrystal.Visible = true;
                lblCRStatus.Visible = true;
                lblStatus.Visible = false;
                progressBar1.Visible = false;
                Text = "Crystal reports no está instalado";
                panelCR.Visible = true;
                return;
            }
            
            VersionManager vm = new VersionManager();
            vm.OnCheckForUpdateComplete += vm_OnCheckForUpdateComplete;
            //vm.CheckForUpdates(new Uri("http://www.trainit.com.mx/ulp2/SIP80/CurrentVersion.xml", UriKind.Absolute));
            vm.CheckForUpdates(new Uri("http://192.168.75.2/ulp2/SIP80/CurrentVersion.xml", UriKind.Absolute));
            
            
        }

        void vm_OnCheckForUpdateComplete(PublishedVersion PublishedVersion)
        {
            bool updateApp = false;

            pv = PublishedVersion;
            APP_FOLDER_TO_UNZIP = Utils.ProgramFilesx86() + pv.Installpath;
            localFileAssemblyPath = Path.Combine(Utils.ProgramFilesx86() + pv.Installpath, pv.App);


            if (File.Exists(localFileAssemblyPath))
            {
                FileVersionInfo appInfo = FileVersionInfo.GetVersionInfo(localFileAssemblyPath);

                //localFileAssembly = Assembly.LoadFile(localFileAssemblyPath);



                //newVersionUrl = pv.UrlNewPackage;

                Version publishedVersion = new Version(PublishedVersion.NewVersionNumber);

                //string localVersion = localFileAssembly.GetName().Version.ToString();
                string localVersion = appInfo.FileVersion;

                if (publishedVersion.CompareTo(new Version(localVersion)) != 0)
                {
                    updateApp = true;

                }                
            }
            else
            {
                updateApp = true;
            }

            if (updateApp)
            {
                lblStatus.Text = "Obteniendo actualización...";
                progressBar1.Visible = true;
                VersionManager vmd = new VersionManager();

                vmd.OnDownloadProgressChanged += vmd_OnDownloadProgressChanged;
                vmd.OnFileDownloaded += vmd_OnFileDownloaded;
                string savedFilePath = Path.GetTempFileName().Replace(".tmp", ".zip");

                if (File.Exists(savedFilePath))
                    File.Delete(savedFilePath);

                vmd.DownloadFile(new Uri(PublishedVersion.UrlNewPackage), savedFilePath);
            }
            else
            {
                RunApp();
            }
            

            


          //  label1.Text = string.Format("Actual : {0}, nueva: {1}",localVersion,publishedVersion);

        }

        void vmd_OnFileDownloaded(string DownloadedFile)
        {

            
            foreach (var sipInstance in System.Diagnostics.Process.GetProcessesByName("SIP"))
            {
                sipInstance.Kill();
            }


            System.Threading.Thread.Sleep(3000);


            if (Directory.Exists(APP_FOLDER_TO_UNZIP))
            {
                DirectoryInfo di = new DirectoryInfo(APP_FOLDER_TO_UNZIP);
                foreach (var file in di.GetFiles())
                {
                    
                        file.Delete();
                    
                }    
            }

            

            Directory.CreateDirectory(APP_FOLDER_TO_UNZIP);
            
            lblStatus.Text = "Instalando...";

            FileStream fs = new FileStream(DownloadedFile, FileMode.Open);

            FileInfo fi = new FileInfo(DownloadedFile);

            string zipFile = Path.Combine(APP_FOLDER_TO_UNZIP, fi.Name);

            File.Copy(DownloadedFile, zipFile);

            bgwUnZip.RunWorkerAsync(zipFile);

        }

        void vmd_OnDownloadProgressChanged(System.Net.DownloadProgressChangedEventArgs e)
        {
            

            progressBar1.Value = e.ProgressPercentage;
            Text = string.Format("Recibido: {0} KB - {1}% -", Convert.ToInt32(e.BytesReceived / 1024).ToString("N0"),e.ProgressPercentage);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void lnkDownloadCR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://scn.sap.com/docs/DOC-7824");
        }
    }
}
