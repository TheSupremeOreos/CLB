using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.PacketCreation;
using System.Threading;
using System.Windows.Forms;

namespace PixelCLB.Packets.Handlers
{
    class CharacterLoadout : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (c.mode == 30) //IGN Stealer
                {
                    Thread threadz = new Thread(() => c.onServerConnected(false));
                    c.workerThreads.Add(threadz);
                    threadz.Start();
                }
                else
                {
                    try
                    {
                        int tries = 0;
                        if (packet.Length > 3)
                        {
                            int job;
                            List<string> IGN = new List<string>();
                            List<int> UIDs = new List<int>();
                            List<int> gender = new List<int>();
                            List<int> skincolor = new List<int>();
                            List<int> Face = new List<int>();
                            List<int> Hair = new List<int>();
                            List<int> Job = new List<int>();
                            List<int> Str = new List<int>();
                            List<int> Dex = new List<int>();
                            List<int> Int = new List<int>();
                            List<int> Luk = new List<int>();
                            List<int> HP = new List<int>();
                            List<int> MaxHP = new List<int>();
                            List<int> MP = new List<int>();
                            List<int> MaxMP = new List<int>();
                            List<int> AP = new List<int>();
                            List<int> Level = new List<int>();
                            List<int> SP = new List<int>();
                            List<long> Exp = new List<long>();
                            List<int> Fame = new List<int>();
                            List<int> MapID = new List<int>();
                            List<byte> Spawn = new List<byte>();
                            List<int> Fatigue = new List<int>();
                            List<int> WillPowerEXP = new List<int>();
                            List<int> DilligenceExp = new List<int>();
                            List<int> EmpathyExp = new List<int>();
                            string ignList = "[Info] IGNs: ";
                            packet.Read();
                            packet.ReadMapleString();
                            int reader = packet.Read();
                            for (int i = 1; i <= reader; i++)
                                packet.ReadInt();
                            packet.Read();
                            int num = packet.ReadInt();
                            c.updateLog("[Info] Account contains [" + num.ToString() + "] characters");
                            c.maxcharnumber = num;
                            if (c.charnumber > c.maxcharnumber)
                            {
                                c.updateLog("[Error] Character #" + c.charnumber.ToString() + " does not exist on this account");
                                c.updateLog("[Error] Disconnecting");
                                c.forceDisconnect(false, 0, false, "Char # does not exist");
                                return;
                            }
                            for (int x = 1; x <= num; x++) // v176 Update UIDs in front
                            {
                                UIDs.Add(packet.ReadInt());
                            }

                            for (int x = 1; x <= num; x++)
                            {
                                packet.ReadInt(); //UID
                                packet.ReadInt(); //UNK
                                packet.ReadInt(); //UNK
                                string i = packet.ReadString(13).Replace("\0", "");
                                if (x == 1)
                                    ignList = ignList + i;
                                else
                                    ignList = ignList + ", " + i;
                                IGN.Add(i);
                                gender.Add(packet.Read());
                                skincolor.Add(packet.Read());
                                Face.Add(packet.ReadInt());
                                Hair.Add(packet.ReadInt());
                                packet.Skip(3); //UNK v176 packet.Skip(24);
                                Level.Add(packet.Read());
                                job = packet.ReadShort();
                                Job.Add(job);
                                Str.Add(packet.ReadShort());
                                Dex.Add(packet.ReadShort());
                                Int.Add(packet.ReadShort());
                                Luk.Add(packet.ReadShort());
                                HP.Add(packet.ReadInt());
                                MaxHP.Add(packet.ReadInt());
                                MP.Add(packet.ReadInt());
                                MaxMP.Add(packet.ReadInt());
                                AP.Add(packet.ReadShort());
                                if (IsExtendedSPJob(job))
                                    readExtendedSP(packet);
                                else
                                    SP.Add(packet.ReadShort());
                                Exp.Add(packet.ReadLong());
                                Fame.Add(packet.ReadInt());
                                packet.Skip(8);
                                MapID.Add(packet.ReadInt());
                                Spawn.Add(packet.Read());
                                packet.Skip(6);
                                Fatigue.Add(packet.Read());
                                packet.ReadInt();
                                packet.ReadInt();
                                packet.ReadInt();
                                WillPowerEXP.Add(packet.ReadInt());
                                DilligenceExp.Add(packet.ReadInt());
                                EmpathyExp.Add(packet.ReadInt());
                                packet.ReadInt();
                                packet.ReadShort();
                                packet.ReadShort();
                                packet.ReadShort();
                                packet.ReadShort();
                                packet.ReadShort();
                                packet.ReadShort();


                                if (c.charnumber != -1)
                                {
                                    if (c.charnumber == x+1)
                                        break;
                                }
                                else
                                {
                                    if (!i.Replace("\0", "").ToLower().Equals(c.charIGN.ToLower()))
                                        break;
                                }

                                packet.Skip(115);
                                if (job == 3001 || (job >= 3100 & job <= 3199) || (job >= 3600 & job <= 3699))
                                {
                                    packet.Skip(9);
                                }
                                for (int x2 = 1; x2 <= 3; x2++) // Loop 3x for FF FF FF
                                {
                                    while (packet.Read() != 255)
                                    {
                                    }
                                }
                                if (packet.Read() == 0xFF)
                                    packet.Skip(25);
                                else
                                    packet.Skip(24);
                                if (job == 3001 || job >= 3100 && job <= 3199) 
                                {
                                    packet.Skip(4);
                                }
                                //packet.Skip(1);
                                if (packet.ReadInt() > 0)
                                {
                                    packet.Skip(16);
                                }
                            }
                            if (c.charnumber == -1)
                            {
                                int num2 = 0;
                                foreach (string str in IGN)
                                {
                                    if (!str.Replace("\0", "").ToLower().Equals(c.charIGN.ToLower()))
                                        num2++;
                                    else
                                    {
                                        c.charnumber = num2;
                                        break;
                                    }
                                }
                                if (c.charnumber == -1)
                                {
                                    c.updateLog("Failed to login");
                                    c.updateLog("Unable to find the ign: " + c.charIGN);
                                    c.forceDisconnect(false, 0, false, "Unable to find IGN: Login");
                                    MessageBox.Show("Unable to find the ign: " + c.charIGN + ". \nPlease be sure this character exists on this account.");
                                    return;
                                }
                            }
                            int charNumOne = c.charnumber + 1;
                            c.updateLog("[Info] Characters parsed");
                            c.updateLog(ignList);
                            c.myCharacter.ign = IGN[c.charnumber].Replace(Convert.ToChar(0).ToString(), "");
                            c.myCharacter.uid = UIDs[c.charnumber];
                            c.myCharacter.Level = Level[c.charnumber];
                            c.myCharacter.mapID = MapID[c.charnumber];
                            c.myCharacter.Spawnpoint = Spawn[c.charnumber];
                            c.updateLog("[Connection] Selecting Character #" + charNumOne.ToString());
                            //c.ses.SendPacket(PacketHandler.CharSelect(c.pic, UIDs[c.charnumber]).ToArray());
                        }
                        else
                        {
                            byte id = packet.Read();
                            if (id == 0x08)
                            {
                                c.updateLog("[Connection] Trouble Connecting... channels may be down. Retrying...");
                                tries++;
                                if (tries > 3)
                                {
                                    c.updateLog("[Connection] Channel " + c.channel + " is currently offline.");
                                    c.forceDisconnect(false, 0, false, "Channel is offline");
                                    return;
                                }
                                c.webLogin = false;
                                if (!Program.webLogin & !c.webLogin)
                                {
                                    c.ses.SendPacket(PacketHandler.SelectWorldChannel_Client(c, c.world, c.channel).ToArray());
                                    c.webLogin = true;
                                    return;
                                }
                                else
                                {
                                    c.updateLog("[Error] Random connection error????");
                                    c.forceDisconnect(true, 1, false, "Random connection error");
                                }
                            }
                            else
                            {
                                c.updateLog("[Error] Random connection error???");
                                c.forceDisconnect(true, 1, false, "Random connection error");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                        c.updateLog("Error Code: 004");
                        c.updateLog("[Error] Character Parse Error, Please report.");
                        c.forceDisconnect(false, 1, false, "Character Parse Error");
                    }
                }
            }
            catch { }
        }

        public bool IsExtendedSPJob(int Job)
        {
            //11200 - 11212 Beast Tamer
            //1100/1110/1111/1112 - Dawn Warrior 
            //1200/1210/1211/1212 - Blaze Wizard 
            //1300/1310/1311/1312 - Wind Archer 
            //1400/1410/1411/1412 - Night Walker 
            //1500/1510/1511/1512 - Thunder Breaker
            if ((Job >= 11200 & Job <= 11212) || (Job >= 1100 & Job <= 1112) || (Job >= 1200 & Job <= 1212) || (Job >= 1300 & Job <= 1312) || (Job >= 1400 & Job <= 1412) || (Job >= 1500 & Job <= 1512))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private static int[] readExtendedSP(PacketReader p)
        {
            byte num = p.Read();
            if (num != 0)
            {
                int[] numArray = new int[1];
                for (int i = 0; i < num; i++)
                {
                    byte num1 = p.Read();
                    if (num1 > (int)numArray.Length - 1)
                    {
                        Array.Resize<int>(ref numArray, num1 + 1);
                    }
                    numArray[num1] = p.ReadInt();
                }
                return numArray;
            }
            else
            {
                return null;
            }
        }
    }
}
