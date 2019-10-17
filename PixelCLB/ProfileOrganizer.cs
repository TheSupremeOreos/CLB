using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace PixelCLB
{
    public partial class ProfileOrganizer : Form
    {
        public ProfileOrganizer()
        {
            InitializeComponent();
            this.load();
        }

        private void load()
        {
            foreach (string str in Program.iniFile.GetSectionNames())
            {
                if (str != "CLB Settings")
                    profiles.Items.Add(str);
            }

        }

        private void MoveItem(int direction)
        {
            try
            {
                // Checking selected item
                if (profiles.SelectedItem == null || profiles.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = profiles.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= profiles.Items.Count)
                    return; // Index out of range - nothing to do

                string selected = profiles.SelectedItem.ToString();

                // Removing removable element
                profiles.Items.Remove(selected);
                // Insert it in new position
                profiles.Items.Insert(newIndex, selected);
                // Restore selection
                profiles.SetSelected(newIndex, true);
            }
            catch { }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }


        private void processNewfile()
        {
            DialogResult message = MessageBox.Show("Are you sure you want to reorder your file?", "Reorder?", MessageBoxButtons.OKCancel);
            if (message == DialogResult.OK)
            {
                Thread createNew = new Thread(delegate()
                {
                    try
                    {
                        Program.gui.read = false;

                        string settingsFileName = Program.settingFile.Split(Path.DirectorySeparatorChar).Last();
                        string settingsFile = Path.Combine(Program.settingFolder, settingsFileName);
                        string tempDirectory = Path.Combine(Program.settingFolder, "temp");
                        string tempSettingsFile = Path.Combine(tempDirectory, settingsFileName);

                        IniFile tempiniFile = new IniFile(tempSettingsFile);

                        string backUpPath = Path.Combine(Program.settingFolder, "backup", settingsFileName);

                        if (!Directory.Exists(Path.Combine(Program.settingFolder, "backup")))
                            Directory.CreateDirectory(Path.Combine(Program.settingFolder, "backup"));

                        File.Copy(settingsFile, backUpPath, true);


                        List<string> profilesList = new List<string>();

                        foreach (string str in profiles.Items)
                        {
                            profilesList.Add(str);
                        }

                        if (!Directory.Exists(tempDirectory))
                            Directory.CreateDirectory(tempDirectory);

                        if (File.Exists(tempSettingsFile))
                            File.Delete(tempSettingsFile);

                        File.Create(tempSettingsFile).Dispose();

                        //CLB Settings
                        List<KeyValuePair<string, string>> str2 = Program.iniFile.GetSectionValuesAsList("CLB Settings");
                        foreach (KeyValuePair<string, string> pair in str2)
                        {
                            tempiniFile.WriteValue("CLB Settings", pair.Key, pair.Value);
                        }


                        foreach (string profileName in profilesList)
                        {
                            List<KeyValuePair<string, string>> profileSettings = Program.iniFile.GetSectionValuesAsList(profileName);
                            foreach (KeyValuePair<string, string> profilePair in profileSettings)
                            {
                                tempiniFile.WriteValue(profileName, profilePair.Key, profilePair.Value);
                            }
                        }

                        if (File.Exists(settingsFile))
                            File.Delete(settingsFile);
                        File.Create(settingsFile).Dispose();

                        foreach (string profileName in tempiniFile.GetSectionNames())
                        {
                            List<KeyValuePair<string, string>> profileSettings = tempiniFile.GetSectionValuesAsList(profileName);
                            foreach (KeyValuePair<string, string> profilePair in profileSettings)
                            {
                                Program.iniFile.WriteValue(profileName, profilePair.Key, profilePair.Value);
                            }
                        }

                        if (Directory.Exists(tempDirectory))
                            Directory.Delete(tempDirectory, true);

                    }
                    catch { }
                    try
                    {
                        DialogResult message2 = MessageBox.Show("Profiles have successfully been reordered. If failed, your settings file has been backed up.\nOpen settings folder by using preferences -> Open setings folder and look in the backup folder.", "Complete", MessageBoxButtons.OK);
                        if (message2 == DialogResult.OK)
                        {
                            Program.gui.GUIInvokeMethod(() => base.Close());
                        }
                    }
                    catch { }
                });
                createNew.Start();
            }
        }

        private void reorganizeButton_Click(object sender, EventArgs e)
        {
            processNewfile();
        }
    }
}
