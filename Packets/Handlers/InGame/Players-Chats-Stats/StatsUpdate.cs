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
    class StatsUpdate : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                packet.Skip(1);
                int num = packet.ReadInt();
                if (num != 0)
                {
                    packet.ReadInt();
                    for (int i = 1; i <= 4194304 && i <= num; i = i * 2)
                    {
                        if ((num & i) == i)
                        {
                            int num1 = i;
                            if (num1 > 2048)
                            {
                                if (num1 > 65536)
                                {
                                    if (num1 > 524288)
                                    {
                                        if (num1 != 1048576 && num1 != 2097152)
                                        {
                                            if (num1 == 4194304)
                                            {
                                                c.myCharacter.Fatigue = packet.ReadShort();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (num1 == 131072)
                                        {
                                            c.myCharacter.Fame = packet.ReadInt();
                                        }
                                        else
                                        {
                                            if (num1 == 262144)
                                            {
                                                c.myCharacter.Meso = packet.ReadLong();
                                                Program.gui.updateMesoAmount(c);
                                            }
                                            else
                                            {
                                                if (num1 != 524288)
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (num1 > 8192)
                                    {
                                        if (num1 == 16384)
                                        {
                                            c.myCharacter.AP = packet.ReadShort();
                                        }
                                        else
                                        {
                                            if (num1 != 32768)
                                            {
                                                if (num1 == 65536)
                                                {
                                                    c.myCharacter.Exp = packet.ReadInt();
                                                    //c.updateLog("[Experience]Current EXP: " + c.myCharacter.Exp.ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (num1 == 4096)
                                        {
                                            //25 00 00 00 10 00 00 00 00 00 00 35 09 00 00 00 00
                                            c.myCharacter.MP = packet.ReadInt();
                                        }
                                        else
                                        {

                                            if (num1 == 5120)
                                            {
                                                c.myCharacter.HP = packet.ReadInt();
                                                c.myCharacter.MP = packet.ReadInt();
                                            }
                                            if (num1 == 8192)
                                            {
                                                c.myCharacter.MaxMP = packet.ReadInt();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (num1 > 64)
                                {
                                    if (num1 > 256)
                                    {
                                        if (num1 == 512)
                                        {
                                            c.myCharacter.Luk = packet.ReadShort();
                                        }
                                        else
                                        {
                                            if (num1 == 1024)
                                            {
                                                c.myCharacter.HP = packet.ReadInt();
                                            }
                                            else
                                            {
                                                if (num1 == 2048)
                                                {
                                                    c.myCharacter.MaxHP = packet.ReadInt();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (num1 == 128)
                                        {
                                            c.myCharacter.Dex = packet.ReadShort();
                                        }
                                        else
                                        {
                                            if (num1 == 256)
                                            {
                                                c.myCharacter.Int = packet.ReadShort();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (num1 > 8)
                                    {
                                        if (num1 == 16)
                                        {
                                            c.myCharacter.Level = packet.Read();
                                            c.updateLog("[Level Up]Character Level: " + c.myCharacter.Level.ToString());
                                        }
                                        else
                                        {
                                            if (num1 == 32)
                                            {
                                                c.myCharacter.Job = packet.ReadShort();
                                            }
                                            else
                                            {
                                                if (num1 == 64)
                                                {
                                                    c.myCharacter.Str = packet.ReadShort();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        switch (num1)
                                        {
                                            case 1:
                                                {
                                                    c.myCharacter.SkinColor = (byte)packet.ReadShort();
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    c.myCharacter.Face = packet.ReadInt();
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    c.myCharacter.Hair = packet.ReadInt();
                                                    break;
                                                }
                                            default:
                                                {
                                                    if (num1 == 8)
                                                    {
                                                        break;
                                                    }
                                                    break;
                                                }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                c.updateLog("Error Code: 201");
            }
        }
    }
}
