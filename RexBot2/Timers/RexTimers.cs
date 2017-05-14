﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RexBot2.Timers
{
    public class RexTimers
    {
        //if (RexTimers.canRunCmd(username, cmd)) { ...  RexTimers.resetTimer(username, cmd); } else {  }
        public static Stopwatch wClock;
        public static Stopwatch systemRunClock;
        public static Dictionary<string, Dictionary<string, Stopwatch>> userCdDict;
        public static Dictionary<string, double> cmdCdDict;
        public RexTimers()
        {
            userCdDict = new Dictionary<string, Dictionary<string, Stopwatch>>();
            cmdCdDict = new Dictionary<string, double>();
            cmdCdDict.Add("w", 30);//in seconds
            cmdCdDict.Add("report", 30);
            systemRunClock = new Stopwatch();
            wClock = new Stopwatch();

            systemRunClock.Start();
        }

        public static string getTime(Stopwatch sw)
        {
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0}d {1}h {2}m {3}s",ts.Days,
                ts.Hours, ts.Minutes, ts.Seconds);
            return elapsedTime;
        }

        public static Stopwatch getTimerForCmd(string username,string cmd)
        {
            Stopwatch sw;
            if (userCdDict.ContainsKey(username) && userCdDict[username].ContainsKey("w"))
            {
                sw = userCdDict[username][cmd];
            }
            else if (userCdDict.ContainsKey(username))
            {//first time "w" usage
                sw = new Stopwatch();
                sw.Start();
                userCdDict[username].Add(cmd, sw);
            }
            else
            {//first time user
                userCdDict[username] = new Dictionary<string, Stopwatch>();
                sw = new Stopwatch();
                sw.Start();
                userCdDict[username].Add(cmd, sw);
            }
            return sw;
        }
        
        public static bool canRunCmd(string username, string cmd)
        {
            Stopwatch sw = getTimerForCmd(username, cmd);
            TimeSpan ts = sw.Elapsed;
            double totalseconds = ts.TotalSeconds;
            if (totalseconds >= cmdCdDict[cmd])
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static double getWaitTime(string username, string cmd)
        {
            Stopwatch sw = getTimerForCmd(username, cmd);
            return Math.Round(cmdCdDict[cmd] - sw.Elapsed.TotalSeconds,2);
        }

        public static string getWaitMsg(string username, string cmd)
        {
            return "You have a cooldown on this command: " + getWaitTime(username, cmd).ToString() + "s";
        }

        public static void resetTimer(string username, string cmd)
        {
            userCdDict[username][cmd].Restart();
        }

        public static string getCds(string username)
        {
            string res = string.Empty;
            foreach(KeyValuePair<string,Stopwatch> kv in userCdDict[username])
            {
                //kv.key = cmd
                //kv.val = sw
                double wait = (cmdCdDict[kv.Key] -  kv.Value.Elapsed.TotalSeconds);
                string waitStr;
                if(wait < 0)
                {
                    waitStr = "Available";
                } else
                {
                    waitStr = Math.Round(wait,2).ToString();
                }
                res += kv.Key + " : " + waitStr;
            }
            if (res == string.Empty)
            {
                res = "You have not called any functions with cooldowns yet!";
            }
            return res;
        }
    }
}