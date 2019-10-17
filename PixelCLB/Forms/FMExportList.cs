 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB.Net.Packets;
using PixelCLB;
using PixelCLB.PacketCreation;
using PixelCLB.Crypto;
using PixelCLB.CLBTools;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;

namespace PixelCLB
{
    public partial class FMExportList : Form
    {
        private static List<string[]> fmExportList = new List<string[]>();
        private bool searching = true;
        private int widthNum = 0;
        private Client c;

        public FMExportList(Client client)
        {
            c = client;
            InitializeComponent();
            widthNum = fmGrid.Width;
        }


        public void FMExportList_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Program.FMExport, "FMExport", c.owlWorldName);
                if (Directory.Exists(path))
                {
                    Thread loadFile = new Thread(delegate()
                    {
                        //string[] firstLine;
                        fmExportList.Clear();
                        GUIInvokeMethod(() => fmGrid.Rows.Clear());
                        var Files = Directory.EnumerateFiles(path, "*.txt");
                        foreach (string currentFile in Files)
                        {
                            using (StreamReader streamReader = new StreamReader(File.OpenRead(currentFile)))
                            {
                                string[] firstLine;
                                firstLine = Regex.Split(streamReader.ReadLine(), "%&");
                                DateTime date1 = DateTime.ParseExact(firstLine[0], "MMMM dd, yyyy h:mm:ss tt", null);
                                DateTime date2 = DateTime.Now;
                                System.TimeSpan difference = date2.Subtract(date1);
                                if (difference.TotalMinutes > 60)
                                {
                                    GUIInvokeMethod(() => listTime.ForeColor = System.Drawing.Color.Red);
                                    GUIInvokeMethod(() => listTime.Text = "List Generated: " + firstLine[0] + ". List generated over " + difference.TotalMinutes.ToString() + " mins ago!");
                                }
                                else
                                {
                                    GUIInvokeMethod(() => listTime.ForeColor = System.Drawing.Color.Green);
                                    GUIInvokeMethod(() => listTime.Text = "List Generated: " + firstLine[0]);
                                }
                                GUIInvokeMethod(() => totalStores.Text = "Total Stores Processed: " + firstLine[1]);
                                try
                                {
                                    int shopOwner = 0;
                                    int ownerGuild = 1;
                                    int shopName = 2;
                                    int shopTypeName = 3;
                                    int shopChannel = 4;
                                    int shopFMRoom = 5;
                                    int shopItemID = 6;
                                    int shopItemName = 7;
                                    int shopItemDescription = 8; //Not Outputted
                                    int shopItemQuan = 9;
                                    int shopItemBundle = 10;
                                    int shopItemPrice = 11;
                                    int shopItemEnhance = 12;
                                    int shopItemPot = 13;

                                    while (!streamReader.EndOfStream)
                                    {
                                        int num = 0;
                                        double num2;
                                        string line = streamReader.ReadLine();
                                        if (line != "")
                                        {
                                            string[] inputText;
                                            string[] itemOutput = Regex.Split(line, "%&");
                                            if (itemOutput.Length >= 12)
                                            {
                                                itemOutput[shopOwner] = string.Concat(itemOutput[shopOwner], Environment.NewLine, "Guild ->", itemOutput[ownerGuild]);
                                                itemOutput[shopName] = string.Concat(itemOutput[shopTypeName], Environment.NewLine, itemOutput[shopName]);
                                                if (int.TryParse(itemOutput[shopItemID], out num)) //ID
                                                {
                                                    if (int.Parse(itemOutput[shopItemID]) < 2000000)
                                                    {
                                                        if (itemOutput[shopItemPot] != "")
                                                            itemOutput[shopItemName] = string.Concat(itemOutput[shopItemName], itemOutput[shopItemPot]);
                                                        itemOutput[shopItemName] = itemOutput[shopItemName].Replace("<br>", Environment.NewLine);
                                                        if (itemOutput[shopItemEnhance] != "")
                                                            itemOutput[shopItemName] = string.Concat(itemOutput[shopItemEnhance], Environment.NewLine, itemOutput[shopItemName]);
                                                    }
                                                }
                                                if (!itemOutput[shopItemBundle].Equals("") || itemOutput[shopItemBundle].Equals("0"))
                                                {
                                                    if (int.TryParse(itemOutput[shopItemBundle], out num) & double.TryParse(itemOutput[shopItemPrice], out num2))
                                                    {
                                                        var price = double.Parse(itemOutput[shopItemPrice]).ToString("N0");
                                                        if (int.Parse(itemOutput[shopItemBundle]) > 1 & double.Parse(itemOutput[shopItemPrice]) > 0 & double.Parse(itemOutput[shopItemPrice]) <= 9999999999)
                                                        {
                                                            inputText = new string[] { itemOutput[shopItemName], price + " per " + itemOutput[shopItemBundle], itemOutput[shopItemQuan], itemOutput[shopChannel], itemOutput[shopFMRoom], itemOutput[shopName], itemOutput[shopOwner], itemOutput[shopItemID] };
                                                        }
                                                        inputText = new string[] { itemOutput[shopItemName], price, itemOutput[shopItemQuan], itemOutput[shopChannel], itemOutput[shopFMRoom], itemOutput[shopName], itemOutput[shopOwner], itemOutput[shopItemID] };
                                                    }
                                                    else
                                                    {
                                                        inputText = new string[] { itemOutput[shopItemName], itemOutput[shopItemPrice], itemOutput[shopItemQuan], itemOutput[shopChannel], itemOutput[shopFMRoom], itemOutput[shopName], itemOutput[shopOwner], itemOutput[shopItemID] };
                                                    }
                                                    fmExportList.Add(inputText);
                                                }
                                            }
                                        }
                                        Thread.Sleep(1);
                                    }
                                    fmListSearch("");
                                }
                                catch (Exception es)
                                {
                                    MessageBox.Show("Error loading FM List.");
                                }
                                streamReader.Dispose();
                            }
                        }
                    });
                    loadFile.Start();
                }
                else
                {
                    listTime.Text = "List Generated: No File Found";
                    totalStores.Text = "Total Stores Processed: N/A";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show("There was an error parsing your FM file. Please re-run FM Owl \n" + es.ToString());
            }
        }


        private void fmListSearch(string searchFor)
        {
            searching = true;
            Thread search = new Thread(delegate()
            {
                try
                {
                    GUIInvokeMethod(() => toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red);
                    GUIInvokeMethod(() => toolStripStatusLabel1.Text = "Status: Populating FM list with search parameter(s)" + searchFor + "...");
                    GUIInvokeMethod(() => fmGrid.Rows.Clear());
                    foreach (string[] fmList in FMExportList.fmExportList)
                    {
                        if (fmList[0].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[1].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[2].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[3].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[4].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[5].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[6].ToLower().Contains(searchFor.ToLower()) ||
                            fmList[7].ToLower().Contains(searchFor.ToLower()))
                            GUIInvokeMethod(() => fmGrid.Rows.Add(fmList));
                        Thread.Sleep(1);
                    }
                    GUIInvokeMethod(() => toolStripStatusLabel1.ForeColor = System.Drawing.Color.Green);
                    GUIInvokeMethod(() => toolStripStatusLabel1.Text = "Status: Idle");
                    searching = false;
                }
                catch { }
            });
            search.Start();
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
                    base.Invoke(new FMExportList.GUIInvokeMethodDelegate(GUIInvokeMethod), objArray);
                    return;
                }
                catch
                {
                }
            }
            @delegate();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (searching)
                {
                    MessageBox.Show("A search is already in progress!", "Error!", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    fmListSearch(textBox1.Text);
                    return;
                }
            }
        }

        private void FMExportList_Resize(object sender, EventArgs e)
        {
            System.Drawing.Size size = base.Size;
            int width = size.Width - 40;
            System.Drawing.Size size1 = base.Size;
            int height = size1.Height - 100;

            fmGrid.Height = height;
            fmGrid.Width = width;
        }

        private void FMExportList_ResizeEnd(object sender, EventArgs e)
        {
            searching = true;
            int num = fmGrid.Width - widthNum;
            widthNum = fmGrid.Width;
            num = num / 2;
            toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            toolStripStatusLabel1.Text = "Status: Resizing grid to adjust to new size";
            Thread.Sleep(500);
            Thread t = new Thread(delegate()
            {
                try
                {
                    GUIInvokeMethod(() => fmGrid.Columns[0].MinimumWidth = fmGrid.Columns[0].Width + num);
                    GUIInvokeMethod(() => fmGrid.Columns[5].MinimumWidth = fmGrid.Columns[5].Width + num);
                    Thread.Sleep(1);
                    GUIInvokeMethod(() => toolStripStatusLabel1.ForeColor = System.Drawing.Color.Green);
                    GUIInvokeMethod(() => toolStripStatusLabel1.Text = "Status: Idle");
                    searching = false;
                }
                catch { }
            });
            t.Start();
        }
    }
}
