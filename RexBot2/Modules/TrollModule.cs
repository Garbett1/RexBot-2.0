﻿using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using RexBot2.Utils;
using System.Linq;
using RexBot2.Timers;
using Discord;
using Discord.WebSocket;

namespace RexBot2.Modules
{
    public class TrollModule : ModuleBase<SocketCommandContext>
    {
        private string picPath = "Data/pics/";
        [Command("eminem")]
        [Remarks("troll")]
        [Summary("Rap God")]
        public async Task eminemCmd()
        {
            string username = Context.User.ToString();
            if (RexTimers.canRunCmd(username, "eminem"))
            {
                await Context.Channel.SendFileAsync(picPath + "eminem.jpg");
                await Context.Channel.SendMessageAsync("PALMS SPAGHETTI KNEAS WEAK ARM SPAGHETTI THERES SPAGHETTI ON HIS SPAGHETTI ALREADY, MOMS SPAGHETTI", true);
                RexTimers.resetTimer(username, "eminem");
            } else
            {
                await Context.Channel.SendMessageAsync("`" + RexTimers.getWaitMsg(username, "eminem") + "`");
            }
        }

        [Command("w")]
        [Remarks("troll")]
        [Summary("A chance to be annoying")]
        public async Task wCmd()
        {
            string username = Context.User.ToString();
            if (RexTimers.canRunCmd(username, "w"))
            {
                bool www = MasterUtils.roll(17);
                string res = "`" + MasterUtils.stripName(username) + " had a 17% chance, but failed miserably`";
                res += "\n`No W's for you today " + MasterUtils.stripName(Context.User.ToString()) + "!`";
                if (!www)
                {
                    await Context.Channel.SendMessageAsync(res);
                }
                else
                {                    
                    await Context.Channel.SendMessageAsync(MasterUtils.getAnnoyingTTSString(), true);
                }
                RexTimers.resetTimer(username, "w");
            } else
            {
                await Context.Channel.SendMessageAsync("`" + RexTimers.getWaitMsg(username, "w")+ "`");
            }
        }

        [Command("report")]
        [Remarks("troll")]
        [Summary("report a fool")]
        public async Task reportCmd([Remainder] string name)
        {
            string username = Context.User.ToString();
            if (RexTimers.canRunCmd(username, "report") || MasterUtils.ContainsAny(username, GlobalVars.ADMINS))
            {
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
                    RexTimers.resetTimer(username, "report");
                }
            } else
            {
                await Context.Channel.SendMessageAsync("`" + RexTimers.getWaitMsg(username, "report") + "`");
            }
        }

        [Command("reports")]
        [Remarks("troll")]
        [Summary("show all reports")]
        public async Task reportsCmd()
        {
            string res = string.Empty;
            foreach (KeyValuePair<string, int> kv in DataUtils.reports)
            {
                res += "User " + kv.Key + ", reported " + kv.Value + " times!\n";
            }
            if (res == string.Empty)
            {
                res += "Nobody has been reported! YET...";
            }

            await Context.Channel.SendMessageAsync(res);
        }

        [Command("emoji")]
        [Remarks("troll")]
        [Summary("React to the last message with a random Emoji")]
        public async Task emoteCmd()
        {            
            var messages = await Context.Channel.GetMessagesAsync((1)).Flatten();
            foreach(SocketUserMessage msg in messages)
            {
                //int count = DataUtils.rnd.Next(1, 4);
                //for(int i=0; i < count; i++)
                //{
                await msg.AddReactionAsync(EmojiUtils.getEmoji());
                //}                
            }
        }

        [Command("fuckyourexbot")]
        [Remarks("troll")]
        [Summary("Something Nick says often")]
        public async Task fyrbCmd()
        {
            double duration = DataUtils.rnd.Next(20, 40);
            AdminUtils.addRestriction(Context.User.ToString(), duration);
            await Context.Channel.SendMessageAsync("No, fuck you " + Context.User.Mention + "\nIma restrain you for " + duration + "s\n");   
        }

        [Command("iloveyourexbot")]
        [Remarks("troll")]
        [Summary("Something Nick doesnt say often")]
        public async Task iluCmd()
        {
            double duration = DataUtils.rnd.Next(20, 40);
            AdminUtils.addRestriction(Context.User.ToString(), duration);
            await Context.Channel.SendMessageAsync("Well, I dont! lol " + Context.User.Mention + "\nIma restrain you for " + duration + "s\n");
        }

    }
}
