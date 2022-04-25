using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetExplorer12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string chrome = "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B254D6DD8-8484-9C73-546E-9C2F1F2806C5%7D%26lang%3Den-GB%26browser%3D4%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe";
        string firebox = "https://download-ssl.firefox.com.cn/releases-sha2/stub/official/zh-CN/Firefox-latest.exe";
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Contains(checkedListBox1.Items[0]))
                DownloadFile(firebox, Application.StartupPath + @"\fireboxSetup.exe", "Mozilla Firefox", progressBar1, label4);
            if (checkedListBox1.CheckedItems.Contains(checkedListBox1.Items[1]))
                DownloadFile(chrome, Application.StartupPath + @"\chromeSetup.exe", "Google Chrome", progressBar1, label4);

            if (checkedListBox1.CheckedItems.Contains(checkedListBox1.Items[0]))
            {
                Process process = new Process();
                process.StartInfo.FileName = "fireboxSetup.exe";
                process.Start();
                process.WaitForExit();
            }
            if (checkedListBox1.CheckedItems.Contains(checkedListBox1.Items[1]))
            {
                Process process = new Process();
                process.StartInfo.FileName = "chromeSetup.exe";
                process.Start();
                process.WaitForExit();
            }
        }

        public void DownloadFile(string URL, string filename,string text, ProgressBar prog, Label label)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, by.Length);

                    percent = (float)totalDownloadedByte / totalBytes * 100;
                    label.Text = "Now Downloading : "+text+"   Progress" + Math.Round(percent, 1).ToString() + "%";
                    Application.DoEvents();
                }
                so.Close();
                st.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
