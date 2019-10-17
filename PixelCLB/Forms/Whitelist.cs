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

namespace PixelCLB
{
    public partial class Whitelist : Form
    {
        public static bool read = false;
        public Client c;

        public Whitelist(Client client)
        {
            c = client;
            InitializeComponent();
            updateList();
        }

        private void updateList()
        {
            //Select Profile
            try
            {
                read = false;
                if (read == false)
                {
                    WhitelistBox.Items.Clear();
                    foreach (var line in System.IO.File.ReadAllLines(Program.FMWhiteList))
                    {
                        WhitelistBox.Items.Add(line);
                    }
                    read = true;
                }
            }
            catch
            {
                MessageBox.Show("Error updating your FM List");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> found = new List<string>();
            string line;
            int lineCount = 0;
            if (WhitelistBox.SelectedItem != null)
            {
                if (Program.whiteList)
                {
                    using (StreamReader file = new StreamReader(Program.FMWhiteList))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Equals(WhitelistBox.SelectedItem.ToString()))
                            {
                                lineCount++;
                                break;
                            }
                            lineCount++;
                        }
                    }
                    string newstring = RemoveLine(Program.FMWhiteList, lineCount);
                    WriteToFile(Program.FMWhiteList, newstring);
                }
                else
                {
                    using (StreamReader file = new StreamReader(Program.FMBlackList))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Equals(WhitelistBox.SelectedItem.ToString()))
                            {
                                lineCount++;
                                break;
                            }
                            lineCount++;
                        }
                    }
                    string newstring = RemoveLine(Program.FMBlackList, lineCount);
                    WriteToFile(Program.FMBlackList, newstring);
                }
                updateList();
            }
            else
                c.updateLog("[FMLists] Please select someone to delete.");
        }

        public string RemoveLine(string FilePath, int Position)
        {
            string OutputString = "";
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (counter != Position - 1)
                {
                    OutputString += line + Environment.NewLine;
                }
                counter++;
            }
            file.Close();
            return OutputString;
        }

        public void WriteToFile(string filepath, string contents)
        {
            StreamWriter objStreamWriter = new StreamWriter(filepath);
            objStreamWriter.Write(contents);
            objStreamWriter.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.whiteList)
            {
                if (File.Exists(Program.FMWhiteList))
                {
                    string content = File.ReadAllText(Program.FMWhiteList);
                }
                string newContent = whiteListTextBox.Text;
                File.AppendAllText(Program.FMWhiteList, newContent + Environment.NewLine);
            }
            else
            {
                if (File.Exists(Program.FMBlackList))
                {
                    string content = File.ReadAllText(Program.FMBlackList);
                }
                string newContent = whiteListTextBox.Text;
                File.AppendAllText(Program.FMBlackList, newContent + Environment.NewLine);
            }
            whiteListTextBox.Text = "";
            updateList();
        }

        private void WhitelistBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
