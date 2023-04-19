using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor.Properties
{
    public partial class Updater : Form
    {
        private string url = "https://api.github.com/repos/capasha/EEOEditor/releases?per_page=1";
        private string tool = "EEditor.exe";
        private string download = null;
        private bool canUpdate = false;
        public IArchiveEntry extract { get; private set; }
        public Updater()
        {
            InitializeComponent();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(75, 75, 75);
            this.ForeColor = Color.White;
            foreach (Control cntrl in this.Controls)
            {
                if (cntrl.GetType() == typeof(System.Windows.Forms.Button))
                {
                    ((System.Windows.Forms.Button)cntrl).ForeColor = Color.White;
                    ((System.Windows.Forms.Button)cntrl).BackColor = Color.FromArgb(75, 75, 75);
                    ((System.Windows.Forms.Button)cntrl).FlatStyle = FlatStyle.Flat;
                }
                if (cntrl.GetType() == typeof(GroupBox))
                {
                    cntrl.ForeColor = ColorTranslator.FromHtml("#FDB484");
                    foreach (Control cntrls in cntrl.Controls)
                    {
                        if (cntrls.GetType() == typeof(System.Windows.Forms.Label))
                        {
                            if (((System.Windows.Forms.Label)cntrls).Name.StartsWith("lbli"))
                            {
                                ((System.Windows.Forms.Label)cntrls).ForeColor = Color.White;
                                ((System.Windows.Forms.Label)cntrls).BackColor = Color.FromArgb(75, 75, 75);
                            }
                            else if (((System.Windows.Forms.Label)cntrls).Name == lblLastUpdate.Name)
                            {
                                ((System.Windows.Forms.Label)cntrls).ForeColor = ColorTranslator.FromHtml("#FFD4B7");
                                ((System.Windows.Forms.Label)cntrls).BackColor = Color.FromArgb(75, 75, 75);
                            }
                            else if (((System.Windows.Forms.Label)cntrls).Name == lblCurrent.Name || ((System.Windows.Forms.Label)cntrls).Name == lblNewest.Name)
                            {
                                ((System.Windows.Forms.Label)cntrls).ForeColor = ColorTranslator.FromHtml("#7AF8FF");
                                ((System.Windows.Forms.Label)cntrls).BackColor = Color.FromArgb(75, 75, 75);
                            }
                        }
                        if (cntrls.GetType() == typeof(CheckBox))
                        {
                            ((CheckBox)cntrls).ForeColor = Color.White;
                            ((CheckBox)cntrls).BackColor = Color.FromArgb(75, 75, 75);
                        }
                        if (cntrls.GetType() == typeof(RichTextBox))
                        {
                            ((RichTextBox)cntrls).ForeColor = Color.White;
                            ((RichTextBox)cntrls).BackColor = Color.FromArgb(65, 65, 65);
                        }
                        if (cntrls.GetType() == typeof(System.Windows.Forms.Button))
                        {
                            ((System.Windows.Forms.Button)cntrls).ForeColor = Color.White;
                            ((System.Windows.Forms.Button)cntrls).BackColor = Color.FromArgb(75, 75, 75);
                            ((System.Windows.Forms.Button)cntrls).FlatStyle = FlatStyle.Flat;
                        }
                    }
                }
            }
            if (Convert.ToInt32((MainForm.userdata.lastcheck - DateTime.Now).TotalDays) != 0) lblLastUpdate.Text = Convert.ToInt32((DateTime.Now - MainForm.userdata.lastcheck).TotalDays).ToString() + " Day(s) ago";
            else if (Convert.ToInt32((MainForm.userdata.lastcheck - DateTime.Now).TotalDays) == 0)
            {
                var date = (DateTime.Now - MainForm.userdata.lastcheck);
                lblLastUpdate.Text = $"{date.Hours.ToString("00")}:{date.Minutes.ToString("00")}:{date.Seconds.ToString("00")}";
            }
            if (System.IO.File.Exists($"{Directory.GetCurrentDirectory()}\\{tool}"))
            {

                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo($"{Directory.GetCurrentDirectory()}\\{tool}");
                string currentVersion = myFileVersionInfo.FileVersion;
                lblCurrent.Text = currentVersion;
                lblCurrent.ForeColor = ColorTranslator.FromHtml("#7AF8FF");
            }

        }
        private void updateCheck()
        {
            rtbChangelog.Clear();
            var data = getGithubData();
            if (data != null)
            {
                if (System.IO.File.Exists($"{Directory.GetCurrentDirectory()}\\{tool}"))
                {

                    FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), tool));
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo($"{Directory.GetCurrentDirectory()}\\{tool}");
                    string currentVersion = myFileVersionInfo.FileVersion;
                    int current = Convert.ToInt32(currentVersion.Replace(".", ""));
                    int latest = Convert.ToInt32(data.version.Replace(".", ""));
                    lblLastUpdate.Text = "Just now!";
                    if (current > latest)
                    {
                        lblCurrent.Text = currentVersion;
                        lblCurrent.ForeColor = ColorTranslator.FromHtml("#FF7AE9");
                        lblNewest.ForeColor = ColorTranslator.FromHtml("#7AF8FF");
                        lblNewest.Text = data.version;
                        btnDownload.Enabled = false;
                        canUpdate = false;
                        download = null;
                        rtbChangelog.Clear();
                        rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#D7FFBE");
                        rtbChangelog.AppendText("You have a newer version than the file on GitHub!");
                        MainForm.userdata.lastcheck = DateTime.Now;

                    }
                    else if (latest > current)
                    {

                        lblCurrent.Text = currentVersion;
                        lblCurrent.ForeColor = ColorTranslator.FromHtml("#FF7A7A");
                        lblNewest.ForeColor = ColorTranslator.FromHtml("#D7FFBE");
                        lblNewest.Text = data.version;
                        btnDownload.Enabled = true;
                        download = data.downloadUrl;
                        canUpdate = true;
                        rtbChangelog.Clear();
                        MainForm.userdata.lastcheck = DateTime.Now;
                        string[] split = null;

                        if (data.body.Contains("*"))
                        {
                            split = data.body.Split('*');
                        }
                        if (data.body.Contains("-"))
                        {
                            split = data.body.Split('-');
                        }
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Length > 1)
                            {
                                var value = split[i].Substring(1, split[i].Length - 1);
                                if (value.StartsWith("Added"))
                                {
                                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#D7FFBE");
                                    rtbChangelog.AppendText("Added: ");
                                    rtbChangelog.SelectionColor = Color.White;
                                    rtbChangelog.AppendText(value.Replace("Added", "").Substring(1, 1).ToUpper() + value.Replace("Added", "").Substring(2, value.Replace("Added", "").Length - 2));
                                }
                                else if (value.StartsWith("Removed"))
                                {
                                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#FF7A7A");
                                    rtbChangelog.AppendText("Removed: ");
                                    rtbChangelog.SelectionColor = Color.White;
                                    rtbChangelog.AppendText(value.Replace("Removed", "").Substring(1, 1).ToUpper() + value.Replace("Removed", "").Substring(2, value.Replace("Removed", "").Length - 2));
                                }
                                else if (value.StartsWith("Fixed"))
                                {
                                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#7AF8FF");
                                    rtbChangelog.AppendText("Fixed: ");
                                    rtbChangelog.SelectionColor = Color.White;
                                    rtbChangelog.AppendText(value.Replace("Fixed", "").Substring(1, 1).ToUpper() + value.Replace("Fixed", "").Substring(2, value.Replace("Fixed", "").Length - 2));
                                }
                                else if (value.StartsWith("Changed"))
                                {
                                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#FFEDB7");
                                    rtbChangelog.AppendText("Changed: ");
                                    rtbChangelog.SelectionColor = Color.White;
                                    rtbChangelog.AppendText(value.Replace("Changed", "").Substring(1, 1).ToUpper() + value.Replace("Changed", "").Substring(2, value.Replace("Changed", "").Length - 2));
                                }
                                else
                                {
                                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#FFD4B7");
                                    rtbChangelog.AppendText(value);
                                }
                            }
                        }
                    }
                    else
                    {
                        lblCurrent.Text = currentVersion;
                        lblCurrent.ForeColor = ColorTranslator.FromHtml("#7AF8FF");
                        lblNewest.ForeColor = ColorTranslator.FromHtml("#7AF8FF");
                        lblNewest.Text = data.version;
                        btnDownload.Enabled = false;
                        download = null;
                        canUpdate = false;
                        rtbChangelog.Clear();
                        rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#D7FFBE");
                        rtbChangelog.AppendText("You have the latest version!");
                        MainForm.userdata.lastcheck = DateTime.Now;
                    }
                }
                else
                {
                    rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#FF7A7A");
                    rtbChangelog.AppendText("Couldn't find EEOditor.exe in the current folder.\n");
                }
            }
            else
            {
                rtbChangelog.SelectionColor = ColorTranslator.FromHtml("#FF7A7A");
                rtbChangelog.AppendText("Couldn't fetch information from GitHub.\n");
            }
        }
        private GithubInfo getGithubData()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/vnd.github+json";
            request.UserAgent = "EEOditor Updater";
            try
            {
                GithubInfo info = new GithubInfo();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (request.HaveResponse && response != null)
                        {
                            string text;
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                text = reader.ReadToEnd();
                            }
                            if (!string.IsNullOrEmpty(text))
                            {

                                //Console.WriteLine(text);
                                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
                                //Console.WriteLine(stuff1);
                                if (data[0].ContainsKey("tag_name"))
                                {
                                    info.version = data[0]["tag_name"];
                                }
                                if (data[0].ContainsKey("body"))
                                {
                                    info.body = data[0]["body"];
                                }
                                if (data[0].ContainsKey("assets"))
                                {
                                    info.downloadUrl = data[0]["assets"][0]["browser_download_url"];
                                }
                            }
                        }
                    }
                }
                return info;
            }
            catch (WebException webx)
            {
                Console.WriteLine(webx.Message);
                return null;
            }

        }
        private void DownloadAndExtract()
        {
            if (canUpdate)
            {
                SharpCompress.Archives.IArchive archive = null;
                var paths = Path.GetTempPath();
                WebClient wc = new WebClient();
                wc.DownloadFileAsync(new Uri(download), $"{paths}EEOditor_downloaded.zip");
                wc.DownloadProgressChanged += delegate (object sender2, DownloadProgressChangedEventArgs m)
                {
                    if (progressBar1.InvokeRequired) { this.Invoke((MethodInvoker)delegate { progressBar1.Value = m.ProgressPercentage; }); }
                    else { progressBar1.Value = m.ProgressPercentage; }
                };
                wc.DownloadFileCompleted += delegate (object sender1, System.ComponentModel.AsyncCompletedEventArgs ee)
                {
                    if (System.IO.File.Exists($"{paths}EEOditor_downloaded.zip"))
                    {
                        string currentRunningFile = Process.GetCurrentProcess().MainModule.FileName;
                        archive = ArchiveFactory.Open($"{paths}EEOditor_downloaded.zip");
                        foreach (var entry in archive.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                if (entry.Key == "EEOditor.exe") {
                                    entry.WriteToDirectory(Directory.GetCurrentDirectory(), new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                                    break;
                                }
                            }
                        }

                        wc.CancelAsync();
                        wc.Dispose();
                        archive.Dispose();
                    }
                };
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            updateCheck();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadAndExtract();
        }

        private void Updater_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
    public class GithubInfo
    {
        public string body { get; set; }
        public string downloadUrl { get; set; }
        public string version { get; set; }
    }
}
