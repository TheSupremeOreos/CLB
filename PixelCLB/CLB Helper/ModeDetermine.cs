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


namespace PixelCLB
{
    public class ModeDetermine
    {
        private Client c;

        public ModeDetermine()
        {
        }

        public void getMode(Client client, bool special)
        {
            c = client;
            if (!special)
                c.mode = c.modeBak;
            switch (c.mode)
            {
                case 0:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.regSpawn = true;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.LOGGEDIN;
                        break;
                    }
                case 1:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.regSpawn = true;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.DCMODE;
                        break;
                    }
                case 2:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.tries = 0;
                        c.sent = false;
                        c.regSpawn = false;
                        c.freeSpot = false;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SHOPRESET;
                        break;
                    }
                case 3:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.tries = 0;
                        c.sent = false;
                        c.regSpawn = true;
                        c.freeSpot = false;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SHOPCLOSE;
                        break;
                    }
                case 4:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.sent = false;
                        c.tries = 0;
                        c.freeSpot = true;
                        c.regSpawn = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SPOTBOTTING_NP;
                        break;
                    }
                case 5:
                    {
                        c.canReset = false;
                        c.resetIsOpen = true;
                        c.storeLoaded = false;
                        c.tries = 0;
                        c.freeSpot = true;
                        c.regSpawn = false;
                        c.sent = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SPOTBOTTING_P;
                        break;
                    }
                case 6:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.tries = 0;
                        c.mode = c.modeBak;
                        c.freeSpot = false;
                        c.regSpawn = true;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.FULLMAPPING_P;
                        break;
                    }
                case 7:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.tries = 0;
                        c.mode = c.modeBak;
                        c.freeSpot = false;
                        c.regSpawn = true;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.FULLMAPPING_NP;
                        break;
                    }
                case 8:
                    {
                        c.canReset = false;
                        c.sent = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = true;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.FMOWL;
                        break;
                    }
                case 10:
                    {
                        c.canReset = false;
                        c.craftDone = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.HARVESTBOTMINE;
                        break;
                    }
                case 11:
                    {
                        c.canReset = false;
                        c.craftDone = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.HARVESTBOTHERB;
                        break;
                    }
                case 12:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.EVENT;
                        break;
                    }
                case 13:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.AUTOCHAT;
                        break;
                    }
                case 14:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.mode = c.modeBak;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.AUTOBUFF;
                        break;
                    }
                case 15:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SHOPAFKER;
                        break;
                    }
                case 16:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SHOPWAIT_NP;
                        break;
                    }
                case 17:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SHOPWAIT_P;
                        break;
                    }
                case 19:
                    {
                        c.canReset = false;
                        c.resetIsOpen = true;
                        c.storeLoaded = false;
                        c.tries = 0;
                        c.freeSpot = true;
                        c.regSpawn = false;
                        c.sent = false;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.SPOTBOTTING_P;
                        break;
                    }
                case 20:
                    {
                        c.canReset = false;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.regSpawn = true;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.DCMODE;
                        break;
                    }
                case 30:
                    {
                        c.canReset = false;
                        c.mode = c.modeBak;
                        c.storeLoaded = false;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.IGNStealer;
                        break;
                    }
                case 94:
                    {
                        c.canReset = false;
                        c.mode = c.modeBak;
                        c.storeLoaded = false;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.WBMESOEXPLOIT;
                        break;
                    }
                case 95:
                    {
                        c.canReset = false;
                        c.mode = c.modeBak;
                        c.storeLoaded = false;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.CASSANDRA;
                        break;
                    }
                case 96:
                    {
                        c.canReset = false;
                        c.mode = c.modeBak;
                        c.storeLoaded = false;
                        c.regSpawn = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.MOONSPAWN;
                        break;
                    }
                case 97:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.LoginSpam;
                        break;
                    }
                case 98:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.MapleFarm;
                        break;
                    }
                case 99:
                    {
                        c.canReset = false;
                        c.regSpawn = true;
                        c.storeLoaded = false;
                        c.sent = false;
                        c.freeSpot = false;
                        c.tries = 0;
                        c.resetIsOpen = true;
                        c.storeTargetCoords = null;
                        c.storeTargetUID = 0;
                        c.clientMode = ClientMode.LoginSpam;
                        break;
                    }
            }
        }
    }
}
