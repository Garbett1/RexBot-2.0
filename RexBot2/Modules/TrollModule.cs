﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RexBot2.Utils;
using System.Linq;

namespace RexBot2.Modules
{
    public class TrollModule : ModuleBase<SocketCommandContext>
    {
        private string picPath = "Data/pics/";

        [Command("eminem")]
        [Summary("Rap God")]
        public async Task eminemCmd()
        {
            await Context.Channel.SendFileAsync(picPath + "eminem.jpg");
            await Context.Channel.SendMessageAsync("PALMS SPAGHETTI KNEAS WEAK ARM SPAGHETTI THERES SPAGHETTI ON HIS SPAGHETTI ALREADY, MOMS SPAGHETTI", true);
        }

        [Command("w")]
        [Summary("A chance to be annoying")]
        public async Task wCmd()
        {
            int randInt = DataUtils.rnd.Next(1, 11);
            int randInt2 = DataUtils.rnd.Next(1, 11);
            string res = "you rolled " + randInt + " when you should have rolled " + randInt2;
            res += "\nNo W's for you today " + UtilMaster.stripName(Context.User.ToString()) + "!";
            if(randInt != randInt2)
            {
                await Context.Channel.SendMessageAsync(res);
            } else
            {
                await Context.Channel.SendMessageAsync("W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W W, W", true);
            }
            
        }

        [Command("report")]
        [Summary("report a fool")]
        public async Task reportCmd(string name)
        {

            string user = Context.User.ToString();

            if (AliasUtils.getAliasKey(name).Contains("None"))
            {
                await Context.Channel.SendMessageAsync("You're trying to report an unregistered user!");
            }
            else
            {
                name = DataUtils.aliases[AliasUtils.getAliasKey(name)];
                if (DataUtils.reports.ContainsKey(name))
                {
                    DataUtils.reports[name]++;
                }
                else
                {
                    DataUtils.reports[name] = 1;
                }
                await Context.Channel.SendMessageAsync("Report successful");

            }
        }

        [Command("reports")]
        [Summary("show all reports")]
        public async Task reportsCmd()
        {
            string res = string.Empty;
            foreach (KeyValuePair<string, int> kv in DataUtils.reports)
            {
                res += "User " + kv.Key + ", reported " + kv.Value + " times!\n";
            }

            await Context.Channel.SendMessageAsync(res);
        }

    }
}
