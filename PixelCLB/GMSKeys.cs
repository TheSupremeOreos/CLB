using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Web;
using System.IO;
using System.Windows.Forms;


//THIS FILE IS FOR PURE TESTING PURPOSES
//DONT EVEN CODE LIKE THIS... EVER...
namespace PixelCLB
{

    class GMSKeys
    {
    private static Dictionary<ushort, byte[]> MapleStoryGlobalKeys = new Dictionary<ushort, byte[]>();

        public static void Initialize()
        {
            ushort version = ushort.Parse(Program.version.Split('.')[0]);
            string tmpkey = Program.GMSKey;
            byte[] realkey = new byte[8];
            int tmp = 0;
            for (int j = 0; j < 4 * 8 * 2; j += 4 * 2)
            {
                realkey[tmp++] = byte.Parse(tmpkey[j] + "" + tmpkey[j + 1], System.Globalization.NumberStyles.HexNumber);
            }
            MapleStoryGlobalKeys.Add(version, realkey);
            MapleStoryGlobalKeys.Add(118, new byte[] {
                0x5A, // Full key's lost
                0x22, 
                0xFB, 
                0xD1, 
                0x8F, 
                0x93, 
                0xCD, 
                0xE6, 
            });
        }

        public static byte[] GetKeyForVersion(ushort pVersion)
        {
            // Get first version known
            for (; pVersion > 0; pVersion--)
            {
                if (MapleStoryGlobalKeys.ContainsKey(pVersion))
                {
                    byte[] key = MapleStoryGlobalKeys[pVersion];
                    byte[] ret = new byte[32];
                    for (int i = 0; i < 8; i++)
                        ret[i * 4] = key[i];

 
                    return ret;
                }
            }
            Program.gui.c.updateLog("Unable to retrieve GMS Keys. Please update.");
            return null;
        }
    }
}
