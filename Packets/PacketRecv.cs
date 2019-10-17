using System;
using System.Windows.Forms;
using PixelCLB.Packets;
using PixelCLB.Net;
using PixelCLB.Net.Packets;
using System.Threading;
using System.Collections.Generic;
using PixelCLB.Packets.Handlers;

namespace PixelCLB.PacketCreation
{
    class PacketRecv
    {
        public int complete = 0;
        private static PacketRecv instance = null;
        public Dictionary<short, PacketReceiver> Ops = new Dictionary<short, PacketReceiver>();

        public PacketRecv(Client c)
        {
            try
            {
                c.updateLog("[Packets] Registering Packet Handlers");
                registerPacketHandlers(c);
                c.updateLog("[Packets] Packet handlers registration complete.");
            }
            catch { }
        }

        private void registerPacketHandlers(Client c)
        {
            /*General - Server*/
            registerHandler(c, (short)R_OpCodes.R_PING, new Pong());


            /*Login*/
            //registerHandler(c, (short)R_OpCodes.R_login_response, new LoginReponse());
            registerHandler(c, (short)R_OpCodes.R_Login_Response_Client, new LoginReponse());

            registerHandler(c, (short)R_OpCodes.R_Character_Loadout, new CharacterLoadout());
            registerHandler(c, (short)R_OpCodes.R_WRONGPIC, new IncorrectPIC());
            registerHandler(c, (short)R_OpCodes.R_ServerIP, new ChannelIP());

            registerHandler(c, (short)R_OpCodes.R_Character_Created, new CharacterCreated());
            registerHandler(c, (short)R_OpCodes.R_Character_NameCheck, new IGNCheck());


            /*InGame - ServerStuff*/
            registerHandler(c, (short)R_OpCodes.R_CSIP, new CashChannelIP());
            registerHandler(c, (short)R_OpCodes.R_IPChange, new CashChannelIP());
            registerHandler(c, (short)R_OpCodes.R_ServerReponse, new ServerReponseLogin());
            registerHandler(c, (short)R_OpCodes.R_CSReponse, new CashShopReponse());
            registerHandler(c, (short)R_OpCodes.R_MonsterFarmReponse, new MonsterFarmReponse());

            /*InGame - Players - Chats - Stats*/
            registerHandler(c, (short)R_OpCodes.R_Spawn_Player, new PlayerSpawned());
            registerHandler(c, (short)R_OpCodes.R_Remove_Player, new PlayerRemove());
            registerHandler(c, (short)R_OpCodes.R_Player_Move, new PlayerMoved());
            registerHandler(c, (short)R_OpCodes.R_GuildResponse, new GuildReponse());
            registerHandler(c, (short)R_OpCodes.R_WHISPER, new Whispered());
            registerHandler(c, (short)R_OpCodes.R_ALL_CHAT, new AllChat());
            registerHandler(c, (short)R_OpCodes.R_GUILD_BL_ALI_CHAT, new GuildBLAliChat());
            registerHandler(c, (short)R_OpCodes.R_StatsUpdate, new StatsUpdate());
            registerHandler(c, (short)R_OpCodes.R_UpdateInventory, new InventoryUpdate());
            registerHandler(c, (short)R_OpCodes.R_GainedItemFromNPC, new ItemReceivedFromNPC());

            /*InGame - Stores*/
            registerHandler(c, (short)R_OpCodes.R_STORE_SPAWN, new StoreSpawned());
            registerHandler(c, (short)R_OpCodes.R_MUSHY_CLOSE, new StoreRemoved());
            registerHandler(c, (short)R_OpCodes.R_SECONDSTORE_CLOSE, new StoreRemoved2());
            registerHandler(c, (short)R_OpCodes.R_STORECLICKRESPONSE, new StoreClickedReponse());
            registerHandler(c, (short)R_OpCodes.R_STORE_REPONSE, new StoreReponse());
            registerHandler(c, (short)R_OpCodes.R_PERMIT_CLOSE, new PermitRemoved());

            /*InGame - NPC*/
            registerHandler(c, (short)R_OpCodes.R_NPCTalk, new NPCChat());
            registerHandler(c, (short)R_OpCodes.R_NPCShopResponse, new NPCShopResponse());
            registerHandler(c, (short)R_OpCodes.R_Cassandra_Trophy, new Cassandra());

            /*InGame - Events*/
            registerHandler(c, (short)R_OpCodes.R_Hottime_Reward, new HotTimeRewardEvent());

            /*InGame - Drops*/
            registerHandler(c, (short)R_OpCodes.R_ITEMDROPPED, new ItemDropped());
            complete = 1;

        }

        public static PacketRecv getInstance(Client c)
        {
            if (instance == null)
            {
                instance = new PacketRecv(c);
            }
            return instance;
        }


        void registerHandler(Client c, short op, PacketReceiver packetHandler)
        {

            try
            {
                Ops.Add(op, packetHandler);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                c.updateLog("[Packets] Failed to register handler for packet with OP Code " + op.ToString());
            }
        }

        public PacketReceiver getPacketHandler(short op)
        {
            try
            {
                return Ops[op];
            }
            catch
            {
                return null;
            }
        }


        /*

        public void recvPacket(short op, PacketReader packet, Client c)
        {
            try
            {
                Handler packetHandler = new Handler();
                switch (op)
                {
                    case (short)R_OpCodes.R_NPCShopResponse:
                        {
                            packetHandler.NPCShopResponse(c, packet);
                            return;
                        }
                    case (short)R_OpCodes.R_NPCSpawn:
                        {
                            packetHandler.NPC_Spawned(c, packet);
                            return;
                        }
                    case (short)R_OpCodes.R_ActionBlocked:
                        {
                            packetHandler.cannotPreformAction(c, packet);
                            return;
                        }
                    case (short)R_OpCodes.R_POPUP:
                        {
                            packetHandler.popUpNotice(c, packet);
                            return;
                        }
                    case (short)R_OpCodes.R_NPCTalk:
                        {
                            packetHandler.handleNPCChat(c, packet);
                            return;
                        }
                }


                if (c.clientMode == ClientMode.DISCONNECTED || c.clientMode == ClientMode.LOGIN)
                {
                    switch (op)
                    {
                    }
                }
                else if (c.clientMode == ClientMode.EXPLOIT)
                {
                    if (op == (short)R_OpCodes.R_NPCTalk)
                    {
                        //packetHandler.confirmExploit(c);
                    }
                }
                else if (c.clientMode == ClientMode.HARVESTBOTHERB || c.clientMode == ClientMode.HARVESTBOTMINE || c.clientMode == ClientMode.EVENT)
                {
                    switch (op)
                    {
                        case (short)R_OpCodes.R_HarvestStarted:
                            {
                                packetHandler.harvestStatus(c, packet);
                                return;
                            }
                        case (short)R_OpCodes.R_HarvestStatus:
                            {
                                packetHandler.harvestStatus(c, packet);
                                return;
                            }
                        case (short)R_OpCodes.R_HarvestSpawn:
                            {
                                packetHandler.harvestSpawned(c, packet);
                                return;
                            }
                        case (short)R_OpCodes.R_HarvestRemove:
                            {
                                packetHandler.harvestRemove(c, packet);
                                return;
                            }
                        case (short)R_OpCodes.R_ITEMDROPPED:
                            {
                                packetHandler.itemDropped(c, packet);
                                return;
                            }
                        case (short)R_OpCodes.R_ItemPickedUpReponse:
                            {
                                packetHandler.itemReponse(c, packet);
                                return;
                            }
                    }
                }
            }
            catch { }
        }
         * */
    }
}