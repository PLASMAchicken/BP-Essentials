﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static BP_Essentials.EssentialsVariablesPlugin;
using static BP_Essentials.EssentialsMethodsPlugin;
using System.Threading;
using System.Timers;

namespace BP_Essentials
{
    class SavePeriodically : EssentialsChatPlugin
    {
        public static void Run()
        {
            try
            {
                using (System.Timers.Timer Tmer = new System.Timers.Timer())
                {
                    Tmer.Elapsed += new ElapsedEventHandler(OnTime);
                    Tmer.Interval = SaveTime * 1000;
                    Tmer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogging.Run(ex);
            }
        }
        private static void OnTime(object source, ElapsedEventArgs e)
        {
            Debug.Log(SetTimeStamp.Run() + "[INFO] Saving game..");
            foreach (var shPlayer in FindObjectsOfType<ShPlayer>())
                if (shPlayer.IsRealPlayer())
                {
                    if (shPlayer.GetSpaceIndex() >= 13) continue;
                    shPlayer.svPlayer.SendToSelf(Channel.Unsequenced, 10, "Saving game.. This can take up to 5 seconds.");
                    shPlayer.svPlayer.Save();
                }
        }
    }
}