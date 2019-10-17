using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PixelCLB;
using PixelCLB.CLBTools;
using PixelCLB.Tools;
using PixelCLB.Net;
using PixelCLB.Net.Packets;
using PixelCLB.Crypto;
using PixelCLB.Net.Events;
using PixelCLB.PacketCreation;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO.Pipes;
using System.Management;
using System.Net.NetworkInformation;

namespace PixelCLB
{
    internal partial class ProxySettings : Form
    {
        public ProxySettings()
        {
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues("CLB Settings");
            browseTextBox.Text = sectionValues["ProxyDirectory"];
            if (sectionValues["UseProxy"].Equals("True"))
            {
                useProxyCheckBox.Checked = true;
            }
            else
            {
                useProxyCheckBox.Checked = false;
            }
        }
         
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private bool checkFileFormat()
        {
            bool proxy = false;
            Thread checkProxyFile = new Thread(delegate()
            {
                if (File.Exists(@browseTextBox.Text))
                {
                    StreamReader file = null;
                    try
                    {
                        file = new StreamReader(@browseTextBox.Text);
                        string line;
                        int lineCount = 0;
                        while ((line = file.ReadLine()) != null)
                        {
                            lineCount++;
                            if (!line.Contains("@"))
                            {
                                string[] parts = line.Split(':', '.');
                                if (parts.Length != 5)
                                {
                                    Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                                    Program.usingProxy = false;
                                    GUIInvokeMethod(() => useProxyCheckBox.Checked = false);
                                    GUIInvokeMethod(() => browseTextBox.Text = "");
                                    MessageBox.Show("Line " + lineCount.ToString() + "contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx", "Invalid Format!", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            else
                            {
                                string[] parts0 = line.Split('@');
                                string[] parts = parts0[1].Split(':', '.');
                                string[] userPass = parts0[0].Split(':');
                                if (userPass.Length != 2 || parts.Length != 5)
                                {
                                    Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                                    Program.usingProxy = false;
                                    GUIInvokeMethod(() => useProxyCheckBox.Checked = false);
                                    GUIInvokeMethod(() => browseTextBox.Text = "");
                                    MessageBox.Show("Line " + lineCount.ToString() + "contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx", "Invalid Format!", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }
                        Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", browseTextBox.Text);
                        proxy = true;
                    }
                    finally
                    {
                        if (file != null)
                            file.Close();
                    }
                }
                else
                {
                    if (browseTextBox.Text != "")
                    {
                        MessageBox.Show("File does not exist! Please make sure it is a valid text file.", "Invalid File!", MessageBoxButtons.OK);
                        Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", "");
                        GUIInvokeMethod(() => browseTextBox.Text = "");
                        GUIInvokeMethod(() => useProxyCheckBox.Checked = false);
                    }
                }
            });
            checkProxyFile.Start();
            checkProxyFile.Join();
            return proxy;
        }
        private delegate void GUIInvokeMethodDelegate(Action @delegate);
        public void GUIInvokeMethod(Action @delegate)
        {
            bool invokeRequired = !base.InvokeRequired;
            if (!invokeRequired)
            {
                try
                {
                    object[] objArray = new object[] { @delegate };
                    base.Invoke(new ProxySettings.GUIInvokeMethodDelegate(GUIInvokeMethod), objArray);
                    return;
                }
                catch
                {
                }
            }
            @delegate();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    browseTextBox.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                    checkFileFormat();
                }
                catch (IOException)
                {
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (useProxyCheckBox.Checked)
            {
                if (!browseTextBox.Text.Equals(""))
                {
                    if (checkFileFormat())
                    {
                        useProxyCheckBox.Checked = true;
                        Program.usingProxy = true;
                        Program.proxyDirectory = browseTextBox.Text;
                        Program.iniFile.WriteValue("CLB Settings", "UseProxy", "True");
                    }
                    else
                    {
                        useProxyCheckBox.Checked = false;
                        Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                    }
                }
                else
                {
                    useProxyCheckBox.Checked = false;
                    MessageBox.Show("Please browse for a valid text file", "No File Selected!", MessageBoxButtons.OK);
                }
            }
            else if (!useProxyCheckBox.Checked)
            {
                Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                Program.usingProxy = false;
            }
            this.Dispose();
        }
    }
}
