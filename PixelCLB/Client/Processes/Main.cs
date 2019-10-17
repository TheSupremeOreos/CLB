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
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;


namespace PixelCLB
{
    public partial class Client
    {

        public void onServerConnected(bool special)
        {
            modeDeter.getMode(this, special);
            while (clientMode == ClientMode.DISCONNECTED || myCharacter.Map == null || clientMode == ClientMode.IGNStealer)
                Thread.Sleep(1);
            if (clientMode != ClientMode.DISCONNECTED)
            {
                try
                {
                    if (clientMode == ClientMode.LOGGEDIN) // Regular log in
                    {
                        clientMode = ClientMode.IDLE;
                        if (timesRepeated == 0)
                        {
                            whisperCommands();
                            timesRepeated++;
                            
                        }
                        foreach (CharacterInfo infoBox in Program.gui.charWindows)
                        {
                            if (infoBox.ToString() == this.toProfile())
                            {
                                infoBox.disconnectCheck();
                            }
                        }
                        while (lootItems)
                        {
                            if (hackShield.timeLeft() < 60)
                            {
                                cashShopManagement(false, false, 0, 0);
                            }
                            Thread.Sleep(1000);
                        }
                        return;
                    }

                    else if (clientMode == ClientMode.DCMODE) // DC Mode
                    {
                        if (mode == 1)
                        {
                            if (myCharacter.Level >= 45)
                            {
                                updateAccountStatus("DCing " + dcTarget);
                                Program.gui.modeLabel.Invoke(new MethodInvoker(delegate { Program.gui.modeLabel.Text = "Mode: Disconnecting UID - " + dcTarget; }));
                                updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + "Disconnecting UID: " + dcTarget);
                                if (myCharacter.Level >= 80)
                                {
                                    makeExped("4C D3 07 00"); //HT EXPED
                                    while (clientMode == ClientMode.DCMODE)
                                    {
                                        updateAccountStatus("DCing " + dcTarget);
                                        dcUser(dcTarget, 30);
                                    }
                                }
                                else
                                {
                                    makeExped("4C D0 07 00"); //BARLOG
                                    while (clientMode == ClientMode.DCMODE)
                                    {
                                        updateAccountStatus("DCing " + dcTarget);
                                        dcUser(dcTarget, 6);
                                    }
                                }
                                return;
                            }
                            else
                            {
                                updateLog("Character level does not meet requirements for DC (45+)");
                                forceDisconnect(false, 0, false, "Char does not meet level requirement");
                                return;
                            }
                        }
                        else if (mode == 20)
                        {
                            while (clientMode == ClientMode.DCMODE)
                            {
                                updateAccountStatus("DCing " + dcTarget);
                                dcUser(dcTarget, 255);
                            }
                        }
                    }

                    else if (clientMode == ClientMode.SHOPRESET) // Reset
                    {
                        int num = 0;
                        updateLog("Attempting to get your store's ID & position.");
                        timeOut(5, 7);
                        while (!checkUserStoreSpawned(myCharacter.uid))
                        {
                            if (timeOutCheck)
                            {
                                updateLog("Store not found! Now disconnecting!");
                                forceDisconnect(false, 0, false, "Store not found!");
                                return;
                            }
                            Thread.Sleep(100);
                        }
                        Thread.Sleep(1000);
                        updateLog("Store found! Now resetting.");
                        coords = getStoreXY(myCharacter.uid);
                        characterMove(coords);
                        timeOutCheck = true;
                        while (resetIsOpen & clientMode == ClientMode.SHOPRESET)
                        {
                            Thread.Sleep(1);
                            if (timeOutCheck & resetIsOpen & !storeLoaded)
                            {
                                timeOut(10, 11);
                                storeClose();
                                num++;
                                updateLog("[Store Reset] Close attempt #" + num);
                            }
                        }
                        updateLog("[Store Reset] Store Closed.");
                        while (clientMode == ClientMode.SHOPRESET)
                        {
                            storeAC();
                            Thread.Sleep(5);
                        }
                        storeOpenStage2();
                        //updateLog("[Store Reset] Reset successfully!");
                        //updateAccountStatus("Store reset successful");
                        //forceDisconnect(false, 0, false);
                        return;
                    }

                    else if (clientMode == ClientMode.SHOPCLOSE) // Close
                    {
                        int num = 0;
                        updateLog("Attempting to get your store's ID & position.");
                        timeOut(5, 7);
                        while (!checkUserStoreSpawned(myCharacter.uid))
                        {
                            if (timeOutCheck)
                            {
                                updateLog("Store not found! Now disconnecting!");
                                forceDisconnect(false, 0, false, "Store not found: Shop Close");
                                return;
                            }
                            Thread.Sleep(100);
                        }
                        updateLog("Store found! Now closing.");
                        timeOutCheck = true;
                        while (resetIsOpen & clientMode == ClientMode.SHOPCLOSE)
                        {
                            Thread.Sleep(1);
                            if (timeOutCheck & resetIsOpen & !storeLoaded)
                            {
                                timeOut(10, 11);
                                storeClose();
                                num++;
                                updateLog("[Store Closer] Close attempt #" + num);
                            }
                        }
                    }

                    else if (clientMode == ClientMode.SPOTBOTTING_NP) // MUSHY SPAM
                    {
                        overrideRoomCheck();
                        characterMove(coords);
                        updateLog("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                        updateAccountStatus("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                        while (clientMode == ClientMode.SPOTBOTTING_NP)
                        {
                            storeAC();
                            Thread.Sleep(5);
                        }
                        storeOpenStage2();
                    }

                    else if (clientMode == ClientMode.SPOTBOTTING_P) //PERMIT SPAM
                    {
                        overrideRoomCheck();
                        if (mode == 19)
                        {
                            Thread.Sleep(1000);
                            if (getShopViaIGN(merchantCopyIGN) == null || getShopViaIGN(merchantCopyTargetIGN) == null)
                            {
                                updateLog("[Merchant Copy] Unable to find target shop");
                                updateLog("[Merchant Copy] Trying again in 5 seconds...");
                                Thread.Sleep(5000);
                                if (getShopViaIGN(merchantCopyIGN) == null || getShopViaIGN(merchantCopyTargetIGN) == null)
                                {
                                    updateLog("[Merchant Copy] Unable to find target shop");
                                    updateLog("[Merchant Copy] Disconnecting");
                                    forceDisconnect(false, 0, false, "Merchant Copy: Unable to find target");
                                }
                            }
                        }
                        characterMove(coords);
                        updateLog("[Permit AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                        updateAccountStatus("[Permit AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                        while (clientMode == ClientMode.SPOTBOTTING_P)
                        {
                            if (tries >= storeTries & mode != modeBak)
                            {
                                mode = modeBak;
                                tries = 0;
                                reset();
                            }
                            permitAC();
                            Thread.Sleep(5);
                        }
                        permitOpenStage2();
                        return;
                    }

                    else if (clientMode == ClientMode.SHOPWAIT_P)
                    {
                        overrideRoomCheck();
                        characterMove(coords);
                        Thread.Sleep(200);
                        storeTargetCoords = mapleFMShopCollection.getStoreOnTopCoords(coords);
                        storeTargetUID = mapleFMShopCollection.getStoreOnTopUID(coords);
                        if (storeTargetCoords != null)
                        {
                            if (storeTargetUID != myCharacter.uid)
                            {
                                updateLog("[Spot Camper] Camping store @" + storeTargetCoords + " via P.");
                                updateAccountStatus("[Spot Camper] Camping store @" + storeTargetCoords + " via P.");
                                while (storeTargetCoords != null & clientMode == ClientMode.SHOPWAIT_P)
                                {
                                    if (hackShield.timeLeft() <= timeBeforeCS) // 1mins
                                    {
                                        cashShopManagement(true, false, 60, 70);
                                        characterMove(coords);
                                        updateAccountStatus("[Spot Camper] Camping store @" + storeTargetCoords + " via P.");
                                    }
                                    Thread.Sleep(1);
                                }
                                while (clientMode == ClientMode.SHOPWAIT_P)
                                {
                                    if (mapleFMShopCollection.getStoreOnTopCoords(coords) != null)
                                    {
                                        Thread threadz = new Thread(() => onServerConnected(false));
                                        workerThreads.Add(threadz);
                                        threadz.Start();
                                        return;
                                    }
                                    permitAC();
                                    Thread.Sleep(5);
                                }
                                permitOpenStage2();
                                return;
                            }
                            else
                            {
                                updateLog("[Spot Botter] Why are you botting your own store?");
                                forceDisconnect(false, 0, false, "Botting own store?");
                            }
                        }
                        else
                        {
                            updateLog("Now attempting to steal spot at " + coords);
                            while (clientMode == ClientMode.SHOPWAIT_P)
                            {
                                if (mapleFMShopCollection.getStoreOnTopCoords(coords) != null)
                                {
                                    Thread threadz = new Thread(() => onServerConnected(false));
                                    workerThreads.Add(threadz);
                                    threadz.Start();
                                    return;
                                }
                                if (tries >= storeTries & mode != modeBak)
                                {
                                    mode = modeBak;
                                    tries = 100;
                                }
                                permitAC();
                                Thread.Sleep(5);
                            }
                            permitOpenStage2();
                            return;
                        }
                    }


                    else if (clientMode == ClientMode.SHOPWAIT_NP)
                    {
                        overrideRoomCheck();
                        characterMove(coords);
                        Thread.Sleep(500);
                        storeTargetCoords = mapleFMShopCollection.getStoreOnTopCoords(coords);
                        storeTargetUID = mapleFMShopCollection.getStoreOnTopUID(coords);
                        if (storeTargetCoords != null)
                        {
                            if (storeTargetUID != myCharacter.uid)
                            {
                                storeTargetUID = 0;
                                updateLog("[Spot Camper] Camping store @" + storeTargetCoords + " via N.P.");
                                while (storeTargetCoords != null & clientMode == ClientMode.SHOPWAIT_NP)
                                {
                                    while (storeTargetUID == 0 & clientMode == ClientMode.SHOPWAIT_NP)
                                    {
                                        storeAC();
                                        Thread.Sleep(1);
                                    }
                                    if (hackShield.timeLeft() <= timeBeforeCS) // 1mins
                                    {
                                        cashShopManagement(true, false, 10, 20);
                                        storeTargetUID = 0;
                                        characterMove(coords);
                                        updateAccountStatus("[Spot Camper] Camping store @" + storeTargetCoords + " via N.P.");
                                    }
                                    Thread.Sleep(1);
                                }
                                updateLog("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                                while (clientMode == ClientMode.SHOPWAIT_NP)
                                {
                                    storeAC();
                                    Thread.Sleep(5);
                                    if (mapleFMShopCollection.getStoreOnTopCoords(coords) != null)
                                    {
                                        Thread threadz = new Thread(() => onServerConnected(false));
                                        workerThreads.Add(threadz);
                                        threadz.Start();
                                        return;
                                    }
                                }
                                storeOpenStage2();
                                return;
                            }
                            else
                            {
                                updateLog("[Spot Botter] Why are you botting your own store?");
                                forceDisconnect(false, 0, false, "Botting own store?");
                            }
                        }
                        else
                        {
                            updateLog("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                            while (clientMode == ClientMode.SHOPWAIT_NP)
                            {
                                storeAC();
                                Thread.Sleep(5);
                                if (mapleFMShopCollection.getStoreOnTopCoords(coords) != null)
                                {
                                    Thread threadz = new Thread(() => onServerConnected(false));
                                    workerThreads.Add(threadz);
                                    threadz.Start();
                                    return;
                                }
                            }
                            storeOpenStage2();
                        }
                        return;
                    }


                    else if (clientMode == ClientMode.FULLMAPPING_P) // PERMIT Full MAP
                    {
                        overrideRoomCheck();
                        blackListCheck();
                        updateLog("[Permit Full Map] CH" + channel.ToString() + " FM" + RoomNum + " via permit");
                        updateAccountStatus("[Permit Full Map] CH" + channel.ToString() + " FM" + RoomNum + " via permit");
                        while (!freeSpot & clientMode == ClientMode.FULLMAPPING_P)
                        {
                            if (hackShield.timeLeft() <= timeBeforeCS) // 1mins
                            {
                                cashShopManagement(true, false, 70, 80);
                                updateAccountStatus("[Permit Full Map] CH" + channel.ToString() + " FM" + RoomNum + " via permit");
                            }
                            Thread.Sleep(1);
                        }
                        updateLog("[Permit Full Map] A store has closed! Moving to: " + coords);
                        characterMove(coords);
                        while (clientMode == ClientMode.FULLMAPPING_P)
                        {
                            permitAC();
                            Thread.Sleep(10);
                        }
                        permitOpenStage2();
                        return;
                    }

                    else if (clientMode == ClientMode.FULLMAPPING_NP) // Mushy Full Map
                    {
                        overrideRoomCheck();
                        blackListCheck();
                        updateLog("[Store Full Map] CH " + channel.ToString() + "FM " + RoomNum + " via N.P.");
                        updateAccountStatus("[Store Full Map] CH " + channel.ToString() + "FM " + RoomNum + " via N.P.");
                        while (!freeSpot & clientMode == ClientMode.FULLMAPPING_NP)
                        {
                            if (hackShield.timeLeft() <= timeBeforeCS) // 1mins
                            {
                                cashShopManagement(true, false, 10, 20);
                                updateAccountStatus("[Store Full Map] CH " + channel.ToString() + "FM " + RoomNum + " via N.P.");
                            }
                            Thread.Sleep(1);
                        }
                        updateLog("[Store Full Map] A store has closed! Moving to: " + coords);
                        characterMove(coords);
                        updateLog("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
                        while (clientMode == ClientMode.FULLMAPPING_NP)
                        {
                            storeAC();
                            Thread.Sleep(5);
                        }
                        storeOpenStage2();
                        return;
                    }

                    else if (clientMode == ClientMode.FMOWL)
                    {
                        /*
                        updateLog("FM Owl v5.0 Intiaited");
                        if (myCharacter.mapID > 910000000 & myCharacter.mapID < 910000023)
                        {
                            if (!checkFMFileTime())
                            {
                                totalStoresRead = 0;
                                storesMissed = 0;
                                moveToFM1();
                            }
                            roomNum = myCharacter.mapID - 910000000;
                            while (roomNum <= 23 & clientMode == ClientMode.FMOWL)
                            {
                                int storeCount = 0;
                                if (roomNum < 22)
                                {
                                    while ((myCharacter.mapID < 910000001 || myCharacter.mapID > 910000022) & clientMode == ClientMode.FMOWL)
                                        Thread.Sleep(1);
                                    roomNum = myCharacter.mapID - 910000000 + 1;
                                    updateAccountStatus("Moving to FM" + roomNum.ToString());
                                    changeFreeMarketRoom(roomNum.ToString(), (byte)channel);
                                    Thread.Sleep(3000); 
                                }
                                else
                                {
                                    cashShopManagement(false, false, 0, 0);
                                    updateAccountStatus("Exporting CH" + channel.ToString() + ".");
                                    
                                    while (shopsToRead.Count > 0 & clientMode == ClientMode.FMOWL)
                                    {
                                        int shopID = shopsToRead[0];
                                        shopsToRead.Remove(shopID);
                                        ses.SendPacket(PacketHandler.Enter_Store(shopID).ToArray());
                                        Thread.Sleep(50);
                                        ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                        Thread.Sleep(200); 
                                        
                                        if (hackShield != null)
                                        {
                                            if (hackShield.timeLeft() < 20)
                                            {
                                                Thread.Sleep(1000);
                                                ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                                cashShopManagement(false, false, 0, 0);
                                            }
                                        }
                                    }

                                    updateLog("[FM Owl] Moving back to FM 1");
                                    changeFreeMarketRoom("1", (byte)channel); 
                                    updateLog("[FM Owl] Channel " + channel.ToString() + " complete");
                                    updateLog("[FM Owl] Total stores processed: " + totalStoresRead);

                                    if (!Program.allChannelsOwl)
                                    {
                                        if (Program.openFMOwlWindow)
                                            exportFM(true, true, channel, true, true);
                                        else
                                            exportFM(true, false, channel, true, true);
                                        forceDisconnect(false, 0, false);
                                        return;
                                    }
                                    else
                                    {
                                        if (!Program.allChannelsOwl)
                                        {
                                            if (Program.openFMOwlWindow)
                                                exportFM(true, true, channel, true, true);
                                            else
                                                exportFM(true, false, channel, true, true);
                                            forceDisconnect(false, 0, false);
                                            return;
                                        }
                                        else
                                        {
                                            if (channel != maxChannels)
                                            {
                                                exportFM(true, false, channel, false, true);
                                                ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                                changeChannel(true, 0);
                                                roomNum = 0;
                                            }
                                            else
                                            {
                                                if (!Program.continousFMOwl)
                                                {
                                                    if (Program.openFMOwlWindow)
                                                        exportFM(true, true, channel, true, true);
                                                    else
                                                        exportFM(true, false, channel, true, true);
                                                    forceDisconnect(false, 0, false);
                                                    return;
                                                }
                                                else
                                                {
                                                    exportFM(true, false, channel, false, true);
                                                    changeChannel(false, 1);
                                                    roomNum = 0;
                                                    forceDisconnect(true, 600, false);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            updateLog("Your character is not in a valid FM room");
                            forceDisconnect(false, 0, false);
                        }
                         * */
                        updateLog("FM Owl v4.0 Intiaited");
                        if (myCharacter.mapID >= 910000000 & myCharacter.mapID <= 910000022)
                        {
                            if (myCharacter.mapID == 910000000 & clientMode == ClientMode.FMOWL)
                            {
                                Thread.Sleep(1000);
                                updateLog("[FM Owl] Character is in FM entrance");
                                updateLog("[FM Owl] Attempting to move to FM 1");
                                moveFMRoomsOwlMethod(channel, 910000001);
                            }
                            if (!checkFMFileTime())
                            {
                                totalStoresRead = 0;
                                storesMissed = 0;
                                moveToFM1();
                            }
                            roomNum = myCharacter.mapID - 910000000;
                            while (roomNum <= 23 & clientMode == ClientMode.FMOWL)
                            {
                                roomNum = myCharacter.mapID - 910000000;
                                if (roomNum <= 22 & roomNum > 0)
                                {
                                    updateLog("[FM Owl] Reading CH " + channel + " FM" + roomNum.ToString());
                                    updateAccountStatus("Reading CH " + channel + " FM" + roomNum.ToString());

                                    if (shopsToRead.Count == 0)
                                        Thread.Sleep(500);
                                    while (shopsToRead.Count > 0 & clientMode == ClientMode.FMOWL)
                                    {
                                        int shopID = shopsToRead[0];
                                        shopsToRead.Remove(shopID);
                                        ses.SendPacket(PacketHandler.Enter_Store(shopID).ToArray());
                                        Thread.Sleep(100);
                                        ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                        Thread.Sleep(200);
                                    }
                                    if (roomNum == 1)
                                    {
                                        exportFM(true, false, channel);
                                    }
                                    else if (roomNum == 4 || roomNum == 7 || roomNum == 10 || roomNum == 13 || roomNum == 16 || roomNum == 19)
                                    {
                                        exportFM(false, false, channel);
                                    }
                                    else if (roomNum >= 22)
                                    {
                                        updateAccountStatus("FM Owl Completed. Moving back to FM 1");
                                        updateLog("[FM Owl] Completed");
                                        updateLog("[FM Owl] Total stores processed: " + totalStoresRead);
                                        updateLog("[FM Owl] Moving back to FM 1");
                                        changeFreeMarketRoom("1", (byte)channel);

                                        if (!Program.allChannelsOwl)
                                        {
                                            if (Program.openFMOwlWindow)
                                                exportFM(false, true, channel, true, true);
                                            else
                                                exportFM(false, false, channel, true, true);
                                            forceDisconnect(false, 0, false, "FM owl complete");
                                            return;
                                        }
                                        else
                                        {
                                            if (channel != maxChannels)
                                            {
                                                exportFM(false, false, channel, false, true);
                                                changeChannel(true, 0);
                                                roomNum = 0;
                                                if (channel == 4)
                                                {
                                                    Thread t = new Thread(delegate()
                                                    {
                                                        string directory = Path.Combine(Program.FMExport, "FMExport", owlWorldName);
                                                        FTPLogin("ip", "user", "pass", directory, true);
                                                        updateLog("[FM Owl] Upload Complete.");
                                                    });
                                                    t.Start();
                                                }
                                            }
                                            else
                                            {
                                                if (!Program.continousFMOwl)
                                                {
                                                    if (Program.openFMOwlWindow)
                                                        exportFM(false, true, channel, true, true);
                                                    else
                                                        exportFM(false, false, channel, true, true);
                                                    forceDisconnect(false, 0, false, "FM Owl completed");
                                                    return;
                                                }
                                                else
                                                {
                                                    exportFM(false, false, channel, false, true);
                                                    changeChannel(false, 1);
                                                    roomNum = 0;
                                                    updateLog("[FM Owl] Uploading Files...");
                                                    Thread t = new Thread(delegate()
                                                    {
                                                        string directory = Path.Combine(Program.FMExport, "FMExport", owlWorldName);
                                                        FTPLogin("ip", "user", "pass", directory, true, false);
                                                        updateLog("[FM Owl] Upload Complete.");
                                                    }); 
                                                    t.Start();
                                                    forceDisconnect(true, 600, false, "FM Owl bot Complete");
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (roomNum == 0)
                                    {
                                        forceDisconnect(true, 1, false, "Owl: In FM Entrance, Restarting");
                                    }
                                    else
                                    {
                                        updateLog("[FM Owl] Character in unknown map: " + myCharacter.mapID.ToString());
                                        forceDisconnect(false, 1, false, "Owl: In unknown map?");
                                    }
                                }
                                if (hackShield != null)
                                {
                                    if (hackShield.timeLeft() < 20)
                                    {
                                        ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                        ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                        cashShopManagement(false, false, 0, 0);
                                    }
                                }
                                if (clientMode != ClientMode.DISCONNECTED)
                                {
                                    if (roomNum == 0)
                                        roomNum++;
                                    else
                                    {
                                        roomNum++;
                                        updateAccountStatus("Moving to FM" + roomNum.ToString());
                                        changeFreeMarketRoom(roomNum.ToString(), (byte)channel);
                                    }
                                }
                            }
                        }
                        else
                        {
                            updateLog("[FM Owl] Your character is not in a valid FM room");
                            forceDisconnect(false, 0, false, "Character not in valid FM room");
                        }
                    }
                    else if (clientMode == ClientMode.HARVESTBOTMINE || clientMode == ClientMode.HARVESTBOTHERB)
                    {
                        while (clientMode == ClientMode.HARVESTBOTMINE || clientMode == ClientMode.HARVESTBOTHERB)
                        {
                            if (myCharacter.mapID == 180000003)
                            {
                                updateAccountStatus("Harvesting in CH" + channel.ToString());
                                Thread.Sleep(1500);
                                while (myCharacter.Map.countHarvest() > 0 & clientMode != ClientMode.DISCONNECTED)
                                {
                                    try
                                    {
                                        foreach (KeyValuePair<int, Harvest> pair in myCharacter.Map.getHarvestList().ToList())
                                        {
                                            if ((pair.Value.Type >= HarvestType.VienHeart && clientMode == ClientMode.HARVESTBOTMINE) || (clientMode == ClientMode.HARVESTBOTHERB && pair.Value.Type >= HarvestType.HerbGold))
                                            {
                                                while (myCharacter.Map.getHarvestList().Contains(pair))
                                                {
                                                    ses.SendPacket(PacketHandler.harvestObject(pair.Value.harvestID).ToArray());
                                                    Thread.Sleep(500);
                                                }
                                            }
                                            else
                                            {
                                                myCharacter.Map.removeHarvest(pair.Key);
                                            }
                                        }
                                        Thread.Sleep(1000);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                            }
                            else if (myCharacter.mapID == 910001000)
                            {
                                //ardentMillProcess();
                            }
                            else
                            {
                                moveMap(180000003);
                            }
                            Thread.Sleep(1);
                        }
                    }
                    else if (clientMode == ClientMode.EVENT)
                    {
                        while (clientMode == ClientMode.EVENT)
                        {
                            int num = 1;
                            updateAccountStatus("Anni Boxes in Channel " + channel.ToString());
                            updateLog("[Anni Boxes] Breaking boxes in Ch " + channel.ToString());
                            while (clientMode == ClientMode.EVENT)
                            {
                                while (myCharacter.Map.countHarvest() > 0 & clientMode == ClientMode.EVENT)
                                {
                                    Dictionary<int, Harvest> harvests = new Dictionary<int, Harvest>();
                                    foreach (KeyValuePair<int, Harvest> pair in myCharacter.Map.getHarvestList())
                                    {
                                        harvests.Add(pair.Key, pair.Value);
                                    }
                                    foreach (KeyValuePair<int, Harvest> pair in harvests)
                                    {
                                        updateLog("[Anni Boxes] Breaking a box");
                                        characterMove(pair.Value.point.X.ToString() + "," + pair.Value.point.Y.ToString());
                                        Thread.Sleep(300);
                                        ses.SendPacket(PacketHandler.harvestEventBox(pair.Value.harvestID).ToArray());
                                        Thread.Sleep(2000);
                                    }
                                    harvests.Clear();
                                    Thread.Sleep(5000);
                                }
                                if (myCharacter.mapID == 240000000)
                                { // Leafre
                                    int num2 = 3;
                                    if (num == num2)
                                    {
                                        num = 1;
                                        break;
                                    }
                                    if (num == 1)
                                        characterMove("-1162,422,276");
                                    if (num == 2)
                                        characterMove("1629,32,152");
                                    num++;
                                }
                                else if (myCharacter.mapID == 200000000)
                                { //Orbis
                                    int num2 = 4;
                                    if (num == num2)
                                    {
                                        num = 1;
                                        break;
                                    }
                                    string coord = myCharacter.Map.footholds.getFraction(num, num2).X + "," + myCharacter.Map.footholds.getFraction(num, num2).Y;
                                    characterMove(coord);
                                    num++;
                                    Thread.Sleep(500);
                                }
                                Thread.Sleep(3000);
                            }
                            changeChannel(true, 0);
                            Thread.Sleep(5000);
                        }
                    }
                    else if (clientMode == ClientMode.AUTOCHAT)
                    {
                        if (myCharacter.mapID == 910000000)
                        {
                            int rand = new Random().Next(-100, 100);
                            rand = 944 + rand;
                            coords = rand.ToString() + ",-203";
                            characterMove(coords);
                        }
                        if (autoChatMode.Contains("All Chat") || autoChatMode.Contains("FM Stores"))
                        {
                            updateLog("Chatting " + autoChatText.Count() + " lines");
                            thread = new Thread(delegate()
                            {
                                try
                                {
                                    while (clientMode == ClientMode.AUTOCHAT)
                                    {
                                        foreach (string str in autoChatText)
                                        {
                                            if (clientMode == ClientMode.AUTOCHAT)
                                            {
                                                char[] delimiters = new char[] { '|', '/' };
                                                string[] strArrays = str.Split(delimiters);
                                                ses.SendPacket(PacketHandler.allChat(strArrays[0]).ToArray());
                                                Thread.Sleep(int.Parse(strArrays[2]));
                                            }
                                            else
                                                break;
                                        }
                                        if (autoChatMode.Contains("FM Stores"))
                                        {
                                            while (shopsToRead.Count > 0 & clientMode == ClientMode.AUTOCHAT)
                                            {
                                                int shopID = shopsToRead[0];
                                                updateAccountStatus("Chatting in store: " + shopID.ToString());
                                                shopsToRead.Remove(shopID);
                                                ses.SendPacket(PacketHandler.Enter_Store(shopID).ToArray());
                                                string chatString = "";
                                                int count = 0;
                                                int count2 = 0;
                                                foreach (string str in autoChatText)
                                                {
                                                    if (clientMode == ClientMode.AUTOCHAT)
                                                    {
                                                        char[] delimiters = new char[] { '|', '/' };
                                                        string[] strArrays = str.Split(delimiters);
                                                        chatString = chatString + strArrays[0] + " ";
                                                        count++; count2++;
                                                        if (count == 3 || count2 == autoChatText.Count)
                                                        {
                                                            count = 0;
                                                            Thread.Sleep(1500);
                                                            ses.SendPacket(PacketHandler.storeChat(chatString).ToArray());
                                                            if (autoChatText.Count == count2)
                                                                Thread.Sleep(100);
                                                        }
                                                    }
                                                    else
                                                        break;
                                                }
                                                updateLog("Shops left: " + shopsToRead.Count);
                                                count2 = 0;
                                                ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                                Thread.Sleep(50);
                                                if (hackShield != null)
                                                {
                                                    if (hackShield.timeLeft() < 20)
                                                    {
                                                        shopsToReadBak.Clear();
                                                        foreach (int x in shopsToRead.ToList<int>())
                                                        {
                                                            shopsToReadBak.Add(x);
                                                        }
                                                        cashShopManagement(false, false, 0, 0);
                                                        Thread.Sleep(3000);
                                                        shopsToRead.Clear();
                                                        foreach (int x in shopsToReadBak.ToList<int>())
                                                        {
                                                            shopsToRead.Add(x);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (hackShield != null)
                                        {
                                            if (hackShield.timeLeft() < 20)
                                            {
                                                cashShopManagement(false, false, 0, 0);
                                            }
                                        }
                                        if (autoChatMode.Contains("Change FM Rooms"))
                                        {
                                            int roomNum = myCharacter.mapID - 910000000;
                                            if (roomNum == 22)
                                            {
                                                roomNum = 1;
                                                updateAccountStatus("Moving to FM 1");
                                                changeFreeMarketRoom(roomNum.ToString(), (byte)channel);
                                            }
                                            else
                                            {
                                                roomNum++;
                                                updateAccountStatus("Moving to FM" + roomNum.ToString());
                                                changeFreeMarketRoom(roomNum.ToString(), (byte)channel);
                                            }
                                        }
                                    }
                                }
                                catch { }
                            });
                            workerThreads.Add(thread);
                            thread.Start();
                        }
                    }
                    else if (clientMode == ClientMode.AUTOBUFF)
                    {
                        updateLog("Using " + Database.getItemName(MPPotID) + " as MP Pots");
                        updateLog("You currently have " + myCharacter.Inventorys[InventoryType.USE].getItemCount(MPPotID).ToString() + " " + Database.getItemName(MPPotID) + "s remaining");
                        thread = new Thread(delegate()
                        {
                            while (clientMode == ClientMode.AUTOBUFF)
                            {
                                if (myCharacter.MP < MPUnderValue)
                                {
                                    useMpPot();
                                }
                                else
                                    Thread.Sleep(1);
                            }
                        });
                        workerThreads.Add(thread);
                        thread.Start();
                        updateLog("Auto buffing with " + autoBuffList.Count + "buffs");
                        updateAccountStatus("Auto buffing with " + autoBuffList.Count + "buffs");
                        foreach (string skill in autoBuffList)
                        {
                            thread = new Thread(delegate()
                            {
                                while (clientMode == ClientMode.AUTOBUFF)
                                {
                                    if (myCharacter.MP > MPUnderValue)
                                    {
                                        string[] buff = skill.Split(',');
                                        ses.SendPacket(PacketHandler.useBuff(int.Parse(buff[0]), (byte)int.Parse(buff[1]), this).ToArray());
                                        Thread.Sleep(int.Parse(buff[2]));
                                    }
                                    else
                                    {
                                        Thread.Sleep(100);
                                    }
                                }
                            });
                            workerThreads.Add(thread);
                            thread.Start();
                        }
                    }
                    else if (clientMode == ClientMode.SHOPAFKER)
                    {
                        int failed = 0;
                        while (clientMode == ClientMode.SHOPAFKER)
                        {
                            try
                            {
                                if (!sent)
                                {
                                    Thread.Sleep(2000);
                                    int ignCount = findBlackListIGNs(storeAFKIGN);
                                    if (ignCount == 0)
                                    {
                                        if (failed < 30)
                                        {
                                            failed++;
                                            updateLog("Searching for " + storeAFKIGN + "'s store");
                                            Thread.Sleep(8000);
                                        }
                                        else
                                        {
                                            updateLog("A store with this owner does not exist");
                                            return;
                                        }
                                    }
                                    else if (ignCount == 1)
                                        storeAFK(false, afkPermit);
                                    else if (ignCount == 2)
                                        storeAFK(true, afkPermit);
                                    else
                                    {
                                        updateLog("An unknown issue has occured!");
                                        forceDisconnect(false, 0, false, "Unknown issue?");
                                    }
                                }
                            }
                            catch { }
                            Thread.Sleep(10);
                        }
                    }

                    else if (clientMode == ClientMode.IGNStealer)
                    {
                        int count = 0;
                        ignChecked = false;
                        string stealingIGNS = string.Empty;
                        foreach (string ign in ignStealer)
                        {
                            if (stealingIGNS == string.Empty)
                                stealingIGNS = ign;
                            else
                                stealingIGNS = stealingIGNS + ", " + ign;
                        }
                        updateAccountStatus("Attempting to steal " + ignStealer.Count + " IGN(s)");
                        updateLog("[IGN Stealer] Attempting to steal " + ignStealer.Count + " IGN(s)");
                        updateLog("[IGN Stealer] IGNS: " + stealingIGNS);
                        while (clientMode == ClientMode.IGNStealer)
                        {
                            foreach (string ign in ignStealer)
                            {
                                ses.SendPacket(PacketHandler.checkIGNCharacter(ign).ToArray());
                                count++;
                                ignChecked = true;
                                timeOut(1, 1);
                                while (ignChecked)
                                {
                                    Thread.Sleep(5);
                                    if (timeOutCheck)
                                        ignChecked = false;
                                }
                                if (clientMode != ClientMode.IGNStealer)
                                    break;
                            }
                            if (count > 300)
                            {
                                updateAccountStatus("Attempting to steal " + ignStealer.Count + " IGN(s)");
                            }
                        }
                        Thread.Sleep(2000);
                    }

                    else if (clientMode == ClientMode.MapleFarm)
                    {
                        EnterFarm(myCharacter.uid);
                        int spams = 0;
                        while (true)
                        {
                            ses.SendPacket(PacketHandler.FarmPacket().ToArray());
                            Thread.Sleep(1);
                            spams++;
                            if (spams > 1500)
                            {
                                updateAccountStatus("Spamming MapleFarm Packet");
                                spams = 0;
                            }
                        }
                    }
                    else if (clientMode == ClientMode.LoginSpam)
                    {
                        if (mode == 99)
                        {
                            Thread.Sleep(200);
                            updateAccountStatus("Map Overloading...");
                            updateLog("[Map Lag] Overloading map...");
                            while (true)
                            {
                                ses.SendPacket(PacketHandler.LoginSpamPacket().ToArray());
                                Thread.Sleep(30);
                            }
                        }
                        else if (mode == 97) //SPECIAL HS TIMER
                        {
                            Thread.Sleep(200);
                            updateAccountStatus("Spamming Packet");
                            while (true)
                            {
                                ses.SendPacket(PacketHandler.spamLogin().ToArray());
                                Thread.Sleep(2);
                            }
                        }
                    }
                    else if (clientMode == ClientMode.MOONSPAWN) // Exploit
                    {
                        Thread.Sleep(1000);
                        updateLog("[Moon Spawn] Initiated");
                        updateAccountStatus("Sending Moon Spawn packets");
                        int countz = 0;
                        while (clientMode == ClientMode.MOONSPAWN)
                        {
                            if (countz == 20)
                                updateAccountStatus("Sending Moon Spawn packets");
                            ses.SendPacket(PacketHandler.MoonSpawn1().ToArray());
                            Thread.Sleep(15);
                            ses.SendPacket(PacketHandler.MoonSpawn2().ToArray());
                            Thread.Sleep(15);
                            ses.SendPacket(PacketHandler.MoonSpawn2().ToArray());
                            Thread.Sleep(15);
                            ses.SendPacket(PacketHandler.MoonSpawn2().ToArray());
                            Thread.Sleep(15);
                            countz++;
                        }
                    }
                    else if (clientMode == ClientMode.CASSANDRA) // Exploit
                    {
                        int rounds = 0;
                        while (clientMode == ClientMode.CASSANDRA)
                        {
                            updateAccountStatus("Cassandra Exploit Round " + rounds.ToString());
                            dropCassndraItems();
                            npcWindows = 0;
                            timeOut(1, 2);
                            ses.SendPacket(PacketHandler.accept_Quest(4, 13515, 9010010, 3129, 94).ToArray()); 
                            while (npcWindows < 1 && clientMode == ClientMode.CASSANDRA)
                            {
                                Thread.Sleep(10);
                                if (timeOutCheck)
                                    break;
                            }
                            for (int x = 0; x < 25; x++)
                            {
                                if (clientMode == ClientMode.CASSANDRA)
                                {
                                    /*
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994712).ToArray());
                                    Thread.Sleep(50);
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994713).ToArray());
                                    Thread.Sleep(50);
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994714).ToArray());
                                    Thread.Sleep(50);
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994715).ToArray());
                                    Thread.Sleep(50);
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994716).ToArray());
                                    Thread.Sleep(50);
                                    */
                                    ses.SendPacket(PacketHandler.Cassandra(1, x, 3994717).ToArray());
                                    Thread.Sleep(20);
                                }
                            }
                            npcWindows = 0;
                            timeOut(1, 2);
                            ses.SendPacket(PacketHandler.accept_Quest(4, 13510, 9010010, 3129, 94).ToArray());
                            while (npcWindows < 2 && clientMode == ClientMode.CASSANDRA && !timeOutCheck)
                                Thread.Sleep(10);
                            dropCassndraItems();
                            timeOut(1, 2);
                            npcWindows = 0;
                            ses.SendPacket(PacketHandler.accept_Quest(4, 13511, 9010010, 3129, 94).ToArray());
                            while (npcWindows < 2 && clientMode == ClientMode.CASSANDRA && !timeOutCheck)
                                Thread.Sleep(10);
                            dropCassndraItems();
                            timeOut(1, 2);
                            npcWindows = 0;
                            ses.SendPacket(PacketHandler.accept_Quest(4, 13512, 9010010, 3129, 94).ToArray());
                            while (npcWindows < 2 && clientMode == ClientMode.CASSANDRA && !timeOutCheck)
                                Thread.Sleep(10);
                            dropCassndraItems();
                            timeOut(2, 3);
                            npcWindows = 0;
                            ses.SendPacket(PacketHandler.accept_Quest(4, 13513, 9010010, 3129, 94).ToArray());
                            while (npcWindows < 3 && clientMode == ClientMode.CASSANDRA && !timeOutCheck)
                                Thread.Sleep(10);
                            rounds++;
                            if (hackShield.timeLeft() < 20)
                            {
                                cashShopManagement(false, false, 0, 0);
                                Thread.Sleep(1000);
                            }
                        }
                    }

                    else if (clientMode == ClientMode.WBMESOEXPLOIT)
                    {
                        int attempts = 0;
                        DateTime startTime = DateTime.Now;
                        while (clientMode == ClientMode.WBMESOEXPLOIT)
                        {
                            try
                            {
                                if (myCharacter.mapID == 104000003)
                                {
                                    if (hackShield.timeLeft() < 20)
                                    {
                                        cashShopManagement(false, false, 0, 0);
                                        Thread.Sleep(200);
                                    }

                                    if ((myCharacter.Inventorys[InventoryType.EQUIP].itemExists(1242005) || myCharacter.Inventorys[InventoryType.EQUIP].itemExists(1222005)) && myCharacter.Inventorys[InventoryType.EQUIP].isFull())
                                    {
                                        DateTime CompareTime = DateTime.Now;
                                        updateLog("[Inventory] Inventory full. Opening shop...");
                                        updateLog("[Status] Time to fill inventory: " + (CompareTime - startTime).TotalSeconds.ToString() + " secs");
                                        inStore = false;
                                        timeOut(2, 3);
                                        while (!inStore)
                                        {
                                            ses.SendPacket(PacketHandler.NPC_Click(3709, -146, 183).ToArray());
                                            while (!timeOutCheck && !inStore)
                                                Thread.Sleep(1);
                                        }
                                        Thread.Sleep(10);
                                        updateLog("[Inventory] Selling items...");
                                        myCharacter.Inventorys[InventoryType.EQUIP].dropAll(1242005, this);
                                        myCharacter.Inventorys[InventoryType.EQUIP].dropAll(1222005, this);
                                        foreach (MapleItem item in dropItem)
                                        {
                                            if (clientMode == ClientMode.WBMESOEXPLOIT)
                                            {
                                                ses.SendPacket(PacketHandler.sellItemToNPC(item.position, item.ID, item.quantity).ToArray());
                                                //ses.SendPacket(PacketHandler.deleteItem(item.ID).ToArray());
                                                Thread.Sleep(5);
                                            }
                                        }
                                        dropItem.Clear();
                                        Thread.Sleep(50);
                                        ses.SendPacket(PacketHandler.closeNPCShop(this).ToArray());
                                        Thread.Sleep(200);
                                        startTime = DateTime.Now;
                                    }
                                    else
                                    {
                                        openMesoExploitBoxes(2431909);
                                        //check inven 
                                        updateLog("[Meso Exploit] Round #" + attempts.ToString());
                                        /*
                                        npcWindows = 0;
                                        timeOut(2, 3);
                                        ses.SendPacket(PacketHandler.accept_Quest(4, 32401, 9010000, -94, -58).ToArray());
                                        while (npcWindows < 2 && clientMode == ClientMode.WBMESOEXPLOIT && !timeOutCheck)
                                            Thread.Sleep(10);
                                         * 
                                         */
                                        Thread.Sleep(10);
                                        ses.SendPacket(PacketHandler.accept_Quest(4, 32401, 9010000, -94, -58).ToArray());
                                        Thread.Sleep(8);
                                        ses.SendPacket(PacketHandler.Custom("81 00 05 01 00 00 00 00").ToArray());
                                        Thread.Sleep(8);
                                        ses.SendPacket(PacketHandler.Custom("81 00 0F 01 00 00 00 00").ToArray());
                                        Thread.Sleep(8);

                                        timeOut(2, 3);
                                        serverCheck = true;
                                        while (serverCheck && !timeOutCheck)
                                            Thread.Sleep(1);
                                    }
                                }
                                else if (myCharacter.mapID == 958000000)
                                {
                                    attempts++;
                                    updateLog("[Meso Exploit] Talking to NPCs...");
                                    /*
                                    npcWindows = 0;
                                    timeOut(3, 4);
                                    ses.SendPacket(PacketHandler.accept_Quest(4, 32404, 9062000, -243, 212).ToArray());
                                    while (npcWindows < 9 && clientMode == ClientMode.WBMESOEXPLOIT && !timeOutCheck)
                                        Thread.Sleep(1);
                                    npcWindows = 0;
                                    timeOut(3, 4);
                                    ses.SendPacket(PacketHandler.accept_Quest(5, 32404, 9062001, -243, 212).ToArray());
                                    while (npcWindows < 15 && clientMode == ClientMode.WBMESOEXPLOIT && !timeOutCheck)
                                        Thread.Sleep(1);
                                    npcWindows = 0;
                                    timeOut(2, 3);
                                    ses.SendPacket(PacketHandler.accept_Quest(4, 32405, 9062001, 991, -173).ToArray());
                                    while (npcWindows < 3 && clientMode == ClientMode.WBMESOEXPLOIT && !timeOutCheck)
                                        Thread.Sleep(1);
                                    ses.SendPacket(PacketHandler.NPC_Click(222276, -212, 212).ToArray());
                                    npcWindows = 0;
                                    timeOut(1, 2);
                                    while (npcWindows < 1 && clientMode == ClientMode.WBMESOEXPLOIT && !timeOutCheck)
                                        Thread.Sleep(1);
                                    */
                                    Thread.Sleep(300);
                                    ses.SendPacket(PacketHandler.accept_Quest(4, 32404, 9062000, -243, 212).ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 18 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 18 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 18 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 18 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 0F 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.accept_Quest(5, 32404, 9062001, -243, 212).ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.accept_Quest(4, 32405, 9062001, 991, -173).ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 0F 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 00 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.NPC_Click(222291, -212, 212).ToArray());
                                    Thread.Sleep(8);
                                    ses.SendPacket(PacketHandler.Custom("81 00 0F 01 00 00 00 00").ToArray());
                                    Thread.Sleep(8);
                                    
                                    timeOut(2, 3);
                                    serverCheck = true;
                                    while (serverCheck && !timeOutCheck)
                                        Thread.Sleep(1);
                                    Thread.Sleep(300);
                                     
                                }
                                Thread.Sleep(1);
                            }
                            catch { }
                        }

                    }
                       /*
                    else if (clientMode == ClientMode.EXPLOIT) // Exploit
                    {
                        int genID = Program.exploitID;
                        updateAccountStatus("Generating item " + Program.exploitID);
                        while (true)
                        {
                            ses.SendPacket(PacketHandler.EXPLOIT_OPEN(this, Program.exploitID).ToArray());
                            Thread.Sleep(50);
                            ses.SendPacket(PacketHandler.EXPLOIT_CLOSE(this).ToArray());
                            if (genID != Program.exploitID)
                            {
                                updateAccountStatus("Generating item " + Program.exploitID);
                                genID = Program.exploitID;
                            }
                        }
                    }
                    */


                }
                catch (Exception e)
                {
                    //updateLog("[Error] Main Loop!");
                    //updateLog(e.ToString());
                }
            }
        }


    }
}