using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
    public enum ClientMode
    {
        WAITFOROFFLINE,
        WAITFORONLINE,
        DISCONNECTED,
        LOGIN,
        LOGGEDIN,

        IDLE,
        SHOPRESET,
        SHOPCLOSE,
        FULLMAPPING_P,
        FULLMAPPING_NP,
        SPOTBOTTING_P,
        SPOTBOTTING_NP,
        SPOTFREE,
        PERMITUP,
        DCMODE,
        FMOWL,
        HARVESTBOTHERB,
        HARVESTBOTMINE,
        AUTOCHAT,
        AUTOBUFF,
        EVENT,
        CASSANDRA,
        MOONSPAWN,
        SHOPAFKER,
        SHOPWAIT_P,
        SHOPWAIT_NP,
        IGNStealer,
        MapleFarm,
        LoginSpam,
        WBMESOEXPLOIT
    }
}
