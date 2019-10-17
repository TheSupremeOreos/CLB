using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace PixelCLB
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            loadGeneralBotSettings();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (File.Exists(Program.lootList) & File.Exists(Program.lootIgnoreList))
            {
                refreshSettings();
            }
            else
            {
                MessageBox.Show("Missing Loot Settings File! Now Exiting");
                Environment.Exit(0);
            }
        }

        private void loadGeneralBotSettings()
        {
            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues("CLB Settings");
            if (sectionValues.ContainsKey("Whitelist"))
            {
                if (sectionValues["Whitelist"].Equals("True"))
                {
                    whiteListRadioButton.Checked = true;
                }
                else
                {
                    blackListRadioButton.Checked = true;
                }
            }
            else
                whiteListRadioButton.Checked = true;


            if (sectionValues.ContainsKey("ResetMethod"))
            {
                if (sectionValues["ResetMethod"].Equals("True"))
                {
                    cashShopRadioButton.Checked = true;
                }
                else
                {
                    ccRadioButton.Checked = true;
                }
            }
            else
                cashShopRadioButton.Checked = true;


            if (sectionValues.ContainsKey("AdminPass"))
            {
                logOffPassTextBox.Text = sectionValues["AdminPass"];
            }
            else
            {
                logOffPassTextBox.Text = "Password";
            }

            if (sectionValues.ContainsKey("ShopPrice"))
            {
                priceTextBox.Text = sectionValues["ShopPrice"];
            }
            else
            {
                priceTextBox.Text = "2147483647";
            }


            if (sectionValues.ContainsKey("ExportUIDPath"))
            {
                exportUIDPath.Text = sectionValues["ExportUIDPath"];
            }

            if (sectionValues.ContainsKey("nexonAuthRestartTime"))
            {
                nexonAuthRestartTime.Text = sectionValues["nexonAuthRestartTime"];
            }
            if (sectionValues.ContainsKey("accountLoggedRestartTime"))
            {
                accountLoggedRestartTime.Text = sectionValues["accountLoggedRestartTime"];
            }

            if (sectionValues.ContainsKey("ExportUID"))
            {
                if (sectionValues["ExportUID"].Equals("False"))
                {
                    exportUIDCheckBox.Checked = false;
                }
                else
                {
                    exportUIDCheckBox.Checked = true;
                }
            }
            else
            {
                exportUIDCheckBox.Checked = false;
            }


            if (sectionValues.ContainsKey("ExportFMTimesPath"))
            {
                fmExport.Text = sectionValues["ExportFMTimesPath"];
            }
            if (sectionValues.ContainsKey("ExportFMTimes"))
            {
                if (sectionValues["ExportFMTimes"].Equals("False"))
                {
                    exportTimesCheckBox.Checked = false;
                }
                else
                {
                    exportTimesCheckBox.Checked = true;
                }
            }
            else
            {
                exportTimesCheckBox.Checked = false;
            }
            if (sectionValues.ContainsKey("WebLogin"))
            {
                if (sectionValues["WebLogin"].Equals("True"))
                {
                    webLoginRadioButton.Checked = true;
                }
                else
                {
                    clientLoginRadioButton.Checked = true;
                }
            }
            else
            {
                webLoginRadioButton.Checked = true;
            }
            if (sectionValues["DebugMode"].Equals("True"))
            {
                debugCheckBox.Checked = true;
            }
            else
            {
                debugCheckBox.Checked = false;
            }

            if (sectionValues.ContainsKey("resetFilterIGNs"))
            {
                resetFilterIGNTextBox.Text = sectionValues["resetFilterIGNs"];
            }

            if (sectionValues.ContainsKey("FMOwlPath"))
            {
                fmOwlFolder.Text = sectionValues["FMOwlPath"];
            }

            if (sectionValues.ContainsKey("OpenFMOwlWindow"))
            {
                if (sectionValues["OpenFMOwlWindow"].Equals("False"))
                {
                    openFMExportListWindowCheckBox.Checked = false;
                }
                else
                {
                    openFMExportListWindowCheckBox.Checked = true;
                }
            }
            else
            {
                openFMExportListWindowCheckBox.Checked = false;
            }

            if (sectionValues.ContainsKey("ContinousFMOwl"))
            {
                if (sectionValues["ContinousFMOwl"].Equals("False"))
                {
                    fmowlContinouslyRunCheckBox.Checked = false;
                }
                else
                {
                    fmowlContinouslyRunCheckBox.Checked = true;
                }
            }
            else
            {
                fmowlContinouslyRunCheckBox.Checked = false;
            }

            if (sectionValues.ContainsKey("allChannelsOwl"))
            {
                if (sectionValues["allChannelsOwl"].Equals("False"))
                {
                    outputAllChannelsCheckBox.Checked = false;
                }
                else
                {
                    outputAllChannelsCheckBox.Checked = true;
                }
            }
            else
            {
                outputAllChannelsCheckBox.Checked = false;
            }


            if (sectionValues.ContainsKey("switchAccountsOwl"))
            {
                if (sectionValues["switchAccountsOwl"].Equals("False"))
                {
                    fmOwlSwitchAccountsCheckBox.Checked = false;
                }
                else
                {
                    fmOwlSwitchAccountsCheckBox.Checked = true;
                }
            }
            else
            {
                fmOwlSwitchAccountsCheckBox.Checked = false;
            }



        }

        private void refreshSettings()
        {
            if (File.Exists(Program.lootList))
            {
                lootBox.BeginUpdate();
                lootBox.Items.Clear();
                foreach (var line in System.IO.File.ReadAllLines(Program.lootList))
                {
                    lootBox.Items.Add(line);
                }
                lootBox.EndUpdate();
            }
            if (File.Exists(Program.lootIgnoreList))
            {
                ignoreBox.BeginUpdate();
                ignoreBox.Items.Clear();
                foreach (var line in System.IO.File.ReadAllLines(Program.lootIgnoreList))
                {
                    ignoreBox.Items.Add(line);
                }
                ignoreBox.EndUpdate();
            }
        }


        private void removeFromList(bool ignorelist, string item)
        {
            string line;
            int lineCount = 0;
            if (ignorelist)
            {
                using (StreamReader file = new StreamReader(Program.lootIgnoreList))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Equals(item))
                        {
                            lineCount++;
                            break;
                        }
                        lineCount++;
                    }
                }
                string newstring = RemoveLine(Program.lootIgnoreList, lineCount);
                WriteToFile(Program.lootIgnoreList, newstring);
            }
            else
            {
                using (StreamReader file = new StreamReader(Program.lootList))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Equals(item))
                        {
                            lineCount++;
                            break;
                        }
                        lineCount++;
                    }
                }
                string newstring = RemoveLine(Program.lootList, lineCount);
                WriteToFile(Program.lootList, newstring);
            }
        }


        private void addToList(bool ignoreList, string newContent)
        {
            if (ignoreList)
            {
                if (File.Exists(Program.lootIgnoreList))
                {
                    string content = File.ReadAllText(Program.lootIgnoreList);
                }
                File.AppendAllText(Program.lootIgnoreList, newContent + Environment.NewLine);
            }
            else
            {
                if (File.Exists(Program.lootList))
                {
                    string content = File.ReadAllText(Program.lootList);
                }
                File.AppendAllText(Program.lootList, newContent + Environment.NewLine);
            }
        }


        private void WriteToFile(string filepath, string contents)
        {
            StreamWriter objStreamWriter = new StreamWriter(filepath);
            objStreamWriter.Write(contents);
            objStreamWriter.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Ignore to loot
            if (ignoreBox.SelectedIndex != -1)
            {
                string str = ignoreBox.SelectedItem.ToString();
                removeFromList(true, str);
                addToList(false, str);
                refreshSettings();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Loot to ignore
            if (lootBox.SelectedIndex != -1)
            {
                string str = lootBox.SelectedItem.ToString();
                if (!str.Contains("Name not found: "))
                {
                    removeFromList(false, str);
                    addToList(true, str);
                    refreshSettings();
                }
                else
                {
                    MessageBox.Show("This item cannot be moved");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string line;
            Program.lootIDs.Clear();
            StreamReader file = new StreamReader(Program.lootList);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("Name not found: "))
                {
                    string[] lines = line.Split(':');
                    Program.lootIDs.Add(int.Parse(lines[1].Replace(" ", "")));
                }
                else
                {
                    Program.lootIDs.Add(Database.getItemID(line));
                }
            }
            file.Close();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string line;
            Program.lootIDs.Clear();
            StreamReader file = new StreamReader(Program.lootList);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("Name not found: "))
                {
                    string[] lines = line.Split(':');
                    Program.lootIDs.Add(int.Parse(lines[1].Replace(" ", "")));
                }
                else
                {
                    Program.lootIDs.Add(Database.getItemID(line));
                }
            }
            file.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.lootList))
                File.Delete(Program.lootList);
            if (File.Exists(Program.lootIgnoreList))
                File.Delete(Program.lootIgnoreList);
            if (!File.Exists(Program.lootIgnoreList))
                File.Create(Program.lootIgnoreList).Dispose();
            if (!File.Exists(Program.lootList))
            {
                StreamWriter streamWriter = File.AppendText(Program.lootList);
                foreach (KeyValuePair<int, string> items in Database.ItemCRC)
                {
                    streamWriter.WriteLine(Database.getItemName(items.Key));
                }
                streamWriter.Close();
                string line;
                StreamReader file = new StreamReader(Program.lootList);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("Name not found: "))
                    {
                        string[] lines = line.Split(':');
                        Program.lootIDs.Add(Database.getItemID(line.Replace(" ", "")));
                    }
                    else
                    {
                        Program.lootIDs.Add(Database.getItemID(line));
                    }
                }
                file.Close();
            }
            refreshSettings();
        }

        private bool checkFilterIGNEntry(string userInput)
        {
            string[] parts = userInput.Split(',');
            foreach (string str in parts)
            {
                if (Regex.IsMatch(str, @"^[a-zA-Z0-9]+$") & str.Length >= 4 & str.Length <= 12)
                    continue;
                else
                    return false;
            }
            return true;
        }

        private void updateGeneralSettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                long num;
                if (!long.TryParse(priceTextBox.Text, out num))
                {
                    MessageBox.Show("Please enter a valid price between 1 and 9,999,999,999. \nFailed to update settings.");
                    return;
                }
                else
                {
                    if (long.Parse(priceTextBox.Text) < 1 || long.Parse(priceTextBox.Text) > 9999999999)
                    {
                        MessageBox.Show("Please enter a valid price between 1 and 9,999,999,999. \nFailed to update settings.");
                        return;
                    }
                    else
                    {
                        Program.price = long.Parse(priceTextBox.Text);
                        Program.iniFile.WriteValue("CLB Settings", "ShopPrice", priceTextBox.Text);
                    }
                }

                int num2;
                if (!int.TryParse(this.accountLoggedRestartTime.Text, out num2))
                {
                    MessageBox.Show("Please enter a valid account logged in reset time between 1 and 2,147,483,647. \nFailed to update settings.");
                    return;
                }
                else
                {
                    Program.accountLoggedRestartTime = int.Parse(accountLoggedRestartTime.Text);
                    Program.iniFile.WriteValue("CLB Settings", "accountLoggedRestartTime", accountLoggedRestartTime.Text);
                }

                if (!int.TryParse(this.nexonAuthRestartTime.Text, out num2))
                {
                    MessageBox.Show("Please enter a valid nexon auth reset time between 1 and 2,147,483,647. \nFailed to update settings.");
                    return;
                }
                else
                {
                    Program.nexonAuthRestartTime = int.Parse(nexonAuthRestartTime.Text);
                    Program.iniFile.WriteValue("CLB Settings", "nexonAuthRestartTime", nexonAuthRestartTime.Text);
                }

                if (whiteListRadioButton.Checked)
                {
                    Program.whiteList = true;
                    Program.gui.tabPage3.Text = "Whitelist";
                    Program.iniFile.WriteValue("CLB Settings", "Whitelist", "True");
                }
                else
                {
                    Program.whiteList = false;
                    Program.gui.tabPage3.Text = "Blacklist";
                    Program.iniFile.WriteValue("CLB Settings", "Whitelist", "False");
                }
                Program.gui.updateWhiteList(false);

                if (cashShopRadioButton.Checked)
                {
                    Program.resetMethod = true;
                    Program.iniFile.WriteValue("CLB Settings", "ResetMethod", "True");
                }
                else
                {
                    Program.resetMethod = false;
                    Program.iniFile.WriteValue("CLB Settings", "ResetMethod", "False");
                }
                if (exportUIDCheckBox.Checked & checkDirectory(exportUIDPath.Text))
                {
                    Program.exportUIDs = true;
                    Program.iniFile.WriteValue("CLB Settings", "ExportUID", "True");
                }
                else
                {
                    Program.exportUIDs = false;
                    Program.iniFile.WriteValue("CLB Settings", "ExportUID", "False");
                }

                if (exportTimesCheckBox.Checked & checkDirectory(fmExport.Text))
                {
                    Program.exportFMTimes = true;
                    Program.iniFile.WriteValue("CLB Settings", "ExportFMTimes", "True");
                }
                else
                {
                    Program.exportFMTimes = false;
                    Program.iniFile.WriteValue("CLB Settings", "ExportFMTimes", "False");
                }

                if (webLoginRadioButton.Checked)
                {
                    Program.webLogin = true;
                    Program.iniFile.WriteValue("CLB Settings", "WebLogin", "True");
                }
                else
                {
                    Program.webLogin = false;
                    Program.iniFile.WriteValue("CLB Settings", "WebLogin", "False");
                }
                if (debugCheckBox.Checked)
                {
                    Program.userDebugMode = true;
                    Program.iniFile.WriteValue("CLB Settings", "DebugMode", "True");
                }
                else
                {
                    Program.userDebugMode = false;
                    Program.iniFile.WriteValue("CLB Settings", "DebugMode", "False");
                }
                if (logOffPassTextBox.Text != string.Empty)
                {
                    Program.iniFile.WriteValue("CLB Settings", "AdminPass", logOffPassTextBox.Text);
                    Program.gui.changeAdminPass(logOffPassTextBox.Text);
                }

                if (resetFilterIGNTextBox.Text != "")
                {
                    if (checkFilterIGNEntry(resetFilterIGNTextBox.Text))
                    {
                        Program.iniFile.WriteValue("CLB Settings", "resetFilterIGNs", resetFilterIGNTextBox.Text);
                        Program.resetFilterIGNs = resetFilterIGNTextBox.Text;
                    }
                    else
                        MessageBox.Show("Please make sure you've entered the correct format in the reset filter igns text box. \nIf you do not wish to use this function, leave the field blank.\n Format: (Seperated by ,)\n Ex: ign,ign,ign...... \nYou've entered the following:\n" + resetFilterIGNTextBox.Text);
                    return;
                }
                else
                {
                    Program.iniFile.WriteValue("CLB Settings", "resetFilterIGNs", "");
                    Program.resetFilterIGNs = "";
                }

                if (openFMExportListWindowCheckBox.Checked & checkDirectory(fmOwlFolder.Text))
                {
                    Program.openFMOwlWindow = true;
                    Program.iniFile.WriteValue("CLB Settings", "OpenFMOwlWindow", "True");
                }
                else
                {
                    Program.openFMOwlWindow = false;
                    Program.iniFile.WriteValue("CLB Settings", "OpenFMOwlWindow", "False");
                }


                if (fmowlContinouslyRunCheckBox.Checked & checkDirectory(fmOwlFolder.Text))
                {
                    Program.continousFMOwl = true;
                    Program.iniFile.WriteValue("CLB Settings", "ContinousFMOwl", "True");
                }
                else
                {
                    Program.continousFMOwl = false;
                    Program.iniFile.WriteValue("CLB Settings", "ContinousFMOwl", "False");
                }
                if (outputAllChannelsCheckBox.Checked & checkDirectory(fmOwlFolder.Text))
                {
                    Program.allChannelsOwl = true;
                    Program.iniFile.WriteValue("CLB Settings", "allChannelsOwl", "True");
                }
                else
                {
                    Program.allChannelsOwl = false;
                    Program.iniFile.WriteValue("CLB Settings", "allChannelsOwl", "False");
                }

                if (fmOwlSwitchAccountsCheckBox.Checked & checkDirectory(fmOwlFolder.Text))
                {
                    Program.switchAccountsOwl = true;
                    Program.iniFile.WriteValue("CLB Settings", "switchAccountsOwl", "True");
                }
                else
                {
                    Program.switchAccountsOwl = false;
                    Program.iniFile.WriteValue("CLB Settings", "switchAccountsOwl", "False");
                }


                Program.uidFileDirectory = exportUIDPath.Text;
                Program.iniFile.WriteValue("CLB Settings", "ExportUIDPath", exportUIDPath.Text);
                Program.FMTimesFileDirectory = fmExport.Text;
                Program.iniFile.WriteValue("CLB Settings", "ExportFMTimesPath", fmExport.Text);
                Program.FMExport = fmOwlFolder.Text;
                Program.iniFile.WriteValue("CLB Settings", "FMOwlPath", fmOwlFolder.Text);
                base.Close();
            }
            catch { }

        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    exportUIDPath.Text = folderBrowserDialog1.SelectedPath;
                    if (!checkDirectory(exportUIDPath.Text))
                    {
                        exportUIDCheckBox.Checked = false;
                        exportUIDPath.Text = "";
                    }
                }
                catch (IOException)
                {
                }
            }
            else
            {
                exportUIDCheckBox.Checked = false;
                exportUIDPath.Text = "";
            }
        }

        private bool checkDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void exportUIDCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (exportUIDCheckBox.Checked & (!checkDirectory(exportUIDPath.Text) || exportUIDPath.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        exportUIDPath.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(exportUIDPath.Text))
                        {
                            exportUIDCheckBox.Checked = false;
                            exportUIDPath.Text = "";
                        }
                        else
                        {
                            exportUIDCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void browseFMButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    fmExport.Text = folderBrowserDialog1.SelectedPath;
                    if (!checkDirectory(fmExport.Text))
                    {
                        exportTimesCheckBox.Checked = false;
                        fmExport.Text = "";
                    }
                }
                catch (IOException)
                {
                }
            }
            else
            {
                exportTimesCheckBox.Checked = false;
                fmExport.Text = "";
            }
        }

        private void exportTimesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (exportTimesCheckBox.Checked & (!checkDirectory(fmExport.Text) || fmExport.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        fmExport.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(fmExport.Text))
                        {
                            exportTimesCheckBox.Checked = false;
                            fmExport.Text = "";
                        }
                        else
                        {
                            exportTimesCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void browseFMOwlFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    fmOwlFolder.Text = folderBrowserDialog1.SelectedPath;
                    if (!checkDirectory(fmOwlFolder.Text))
                    {
                        fmOwlFolder.Text = "";
                    }
                }
                catch (IOException)
                {
                }
            }
            else
            {
                fmOwlFolder.Text = "";
            }
        }

        private void openFMExportListWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (openFMExportListWindowCheckBox.Checked & (!checkDirectory(fmOwlFolder.Text) || fmOwlFolder.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        fmOwlFolder.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(fmOwlFolder.Text))
                        {
                            openFMExportListWindowCheckBox.Checked = false;
                            fmOwlFolder.Text = "";
                        }
                        else
                        {
                            openFMExportListWindowCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void outputAllChannelsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (outputAllChannelsCheckBox.Checked & (!checkDirectory(fmOwlFolder.Text) || fmOwlFolder.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        fmOwlFolder.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(fmOwlFolder.Text))
                        {
                            outputAllChannelsCheckBox.Checked = false;
                            fmOwlFolder.Text = "";
                        }
                        else
                        {
                            outputAllChannelsCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void fmowlContinouslyRunCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fmowlContinouslyRunCheckBox.Checked & (!checkDirectory(fmOwlFolder.Text) || fmOwlFolder.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        fmOwlFolder.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(fmOwlFolder.Text))
                        {
                            fmowlContinouslyRunCheckBox.Checked = false;
                            fmOwlFolder.Text = "";
                        }
                        else
                        {
                            fmowlContinouslyRunCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void fmOwlSwitchAccountsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fmOwlSwitchAccountsCheckBox.Checked & (!checkDirectory(fmOwlFolder.Text) || fmOwlFolder.Text == ""))
            {
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        fmOwlFolder.Text = folderBrowserDialog1.SelectedPath;
                        if (!checkDirectory(fmOwlFolder.Text))
                        {
                            fmOwlSwitchAccountsCheckBox.Checked = false;
                            fmOwlFolder.Text = "";
                        }
                        else
                        {
                            fmOwlSwitchAccountsCheckBox.Checked = true;
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }
    }

}
