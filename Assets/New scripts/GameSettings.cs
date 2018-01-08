using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;

namespace Assets.New_scripts
{
    public static class GameSettings
    {
        public static List<string> standartNames = new List<string> { "Nick", "John", "Alex", "Ace", "Barnaba" };
        public static int LapMoney = 500;
        public static int StartLapMoney = 500;
        public static int startMoney = 40000;
        public static int NumberOfPlayers = 2;
        public static float CardTimeOut = 2f;
        public static int maxUpgradeLevel = 5;


        public static int sector1Money = 5000;
        public static int sector2Money = 10000;
    }

    
}
