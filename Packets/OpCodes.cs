using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB.PacketCreation
{
    internal enum R_OpCodes
    {
        //Recieve OpCodes

        //System
        R_PING = 0x12,

        R_ActionBlocked = 0x130,
        R_POPUP = 0x2D9,

        //Login
        R_Login_Response_Client = 0x0,
        //R_login_response = 0x06,
        //R_serverlist = 0x9,
        R_Character_Loadout = 0x06,
        R_Character_NameCheck = 0x0C,
        R_Character_Created = 0x0D,

        R_WRONGPIC = 0x25,


        //In-Game(Stats)
        R_StatsUpdate = 0x28,
        //In-Game(Inventory)
        R_UpdateInventory = 0x26,

        //Hottime
        R_Hottime_Reward = 0x135, 
        R_GainedItemFromNPC = 0x1F8,

        //Changing Servers
        R_ServerIP = 0x7,
        R_ServerReponse = 0x137, //Packet for entering map. First response packet
        R_MonsterFarmReponse = 0x138, //Packet for entering farm. First response packet
        //Cash-shop related
        R_IPChange = 0x10, //IP When ccing / Entering CS
        R_CSIP = 0x11, //IP When ccing / Entering CS
        R_CSReponse = 0x13A, //Entering CS. First response packet

        //Guild loadout
        R_GuildResponse = 0x5F, //Packet to identify one's guild

        //In-Game(Players)
        R_Remove_Player = 0x179,
        R_Spawn_Player = 0x178,//Packet to identify people(non permit) locations
        R_Player_Move = 0x1CB,

        //In-Game(Stores)
        R_STORE_SPAWN = 0x2C9, //Packet to identify shop(non permit) locations
        R_SECONDSTORE_CLOSE = 0x2CA, //Second packet to identify store is actually closed
        R_MUSHY_CLOSE = 0x2CB, //First packet to identify store is closing

        R_PERMIT_CLOSE = 0x17D, //Packet to identify if store has closed
        R_STORE_REPONSE = 0x37F, //Packet on successful open
        R_STORECLICKRESPONSE = 0x3F, //Store name / full inven and try to open store


        //In-Game(Chat)
        R_ALL_CHAT = 0x17A,
        R_GUILD_BL_ALI_CHAT = 0x144,
        R_WHISPER = 0x146,


        //Cassandra Trophy
        R_Cassandra_Trophy = 0x1FB,


        /*
         * 
         * 
         * TO UPDATE BELOW
         * 
         * 
         * */
        //In-Game Item (Dropped / picked up)
        R_ITEMDROPPED = 0x2B4,
        R_ItemPickedUpReponse = 0x34, //Meso too?

        //In-Game (Vein / Herb Related)
        R_HarvestStarted = 0x1F5, //Recv as npc chat clicked
        R_HarvestStatus = 0x286, //Recv as harvesting

        R_HarvestSpawn = 0x288,  //Recv as spawn*
        R_HarvestRemove = 0x28A, //Recv on success/fail

        //In-Game (NPC Related)
        R_NPCSpawn = 0x29C, //Recv on NPC Spawn
        R_NPCTalk = 0x33D, //Recv on NPC Window (Talk)
        R_NPCShopResponse = 0x33E, //Recv on NPC Shop Window
        /*
         * 
         * 
         * TO UPDATE ABOVE
         * 
         * 
         * */
    }

    internal enum S_OpCodes
    {
        //Send OpCodes
        PONG = 0x93,

        //Login
        VALIDATE_LOGIN = 0x67, //Handshake packet

        LOGIN_PASSWORD = 0x69,
        SELECT_WORLD = 0x9D,
        SELECT_CHANNEL = 0x6A,
        SELECT_CHARACTER = 0x6B,
        ENTER_SERVER = 0x6E,

        CHECK_IGN = 0x28,
        CREATE_CHARACTER = 0x45,

        //In-Game(Player movement)
        PLAYER_MOVE = 0x5C,

        //In-Game(Store Related)
        OPEN_STORE = 0x12D,
        CLICK_INVEN = 0x83,

        //In-Game(FM Room Related)
        CHANGE_FM_ROOM = 0x16A,
        CHANGE_CHANNEL = 0x50,
        OWL_Teleport = 0x8D, //??

        //In-Game(Cash Shop / Farm Related)
        ENTER_CS = 0x52,
        Enter_Farm = 0x55,
        Exit_Farm = 0x191,
        ChangeMap_ExitCS = 0x4F,

        //In-Game(World Map/Get Drops Via Packet)
        GetDrops_WM = 0x204,

        //In-Game(Expedition Related)
        MAKE_EXPED = 0x131,

        //In-Game(Chat)
        ALL_CHAT = 0x68, 
        WHISPER = 0x12A, // /find also
        GUILD_BL_ALI_EXPED_CHAT = 0x128,


        //In-Game(Add to Guild /guildinvite)
        ADD_GUILD = 0x133, 

        //In-Game(Pick up items)
        PICKUPITEM = 0x22A, //TO UPDATE

        //In-Game(Mining/Herb)
        HARVEST = 0x20B, //TO UPDATE
        AnniTownBox = 0x1E2, //TO UPDATE

        //In-Game(NPC Talking)
        NPC_Click = 0x7D,
        npcChat = 0x7F,
        npcShopAction = 0x80,

        //In-Game (Skills)
        useBuff = 0xD6,  

        abxForm = 0x15D, //TO UPDATE
        useSkill = 0xD8, 
        cancelSkill = 0xD7,

        //In-Game (Use Item)
        useItem = 0x98,
        consumeScripted = 0x9E,
        dropItem = 0x94,
        delete_Item = 0x13F,

        //MoonSpawn
        //In-Game (Quests)
        Quest_Accept = 0xED,

        Consume_Summon = 0x9B,
        //EndMoonSpawn

        //CASSANDRA TROPHY
        Cassandra = 0x178,

        //In-Game
        claimReward = 0x2F0,

        close_popup = 0x12F,
        //Exploits?
        Farm_Packet = 0x282,
        LoginSpam_Packet = 0xF0,
        Spam_Packet = 0x1F8,

        CRASH_HEADER = 0x203,
        EXPLOIT_OPEN = 0x0161,
        EXPLOIT_CLOSE = 0x72,
        R_EXPLOIT_RESPONSE = 0x2A3,
    }
}
