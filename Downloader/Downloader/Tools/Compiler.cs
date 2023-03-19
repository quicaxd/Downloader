using Downloader.Properties;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Downloader.Tools
{
    internal class Compiler
    {
        private string Stub = Resources.stub;
        public string downloadURL;
        public string downloadPATH;
        public Compiler(string url, string path)
        {
            this.downloadURL = url;
            this.downloadPATH = path;
            setSettings();
        }
        public void setSettings()
        {
            try
            {
                Stub = Stub.Replace("%URL%", this.downloadURL).Replace("%PATH%", this.downloadPATH);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Compile()
        {
            try
            {
                string[] referencedASM = new string[]
                {
                    "System.Net.dll", "System.dll"
                };
                Dictionary<string, string> providerOptions = new Dictionary<string, string>()
                {
                    { "CompilerVersion", "v4.0" }
                };
                var compileOption = "/target:exe /platform:x64 /optimize+";
                using (CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(providerOptions))
                {
                    CompilerParameters compilerParameters = new CompilerParameters(referencedASM)
                    {
                        GenerateExecutable = true,
                        OutputAssembly = "quicaxdDownloader.exe",
                        CompilerOptions = compileOption,
                        TreatWarningsAsErrors = true
                    };
                    CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, Stub);
                    if (compilerResults.Errors.Count > 0)
                    {
                        foreach (CompilerError compilerError in compilerResults.Errors)
                        {
                            MessageBox.Show($"An error occured while processing : {compilerError}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Succesful", "quicaxd Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}
