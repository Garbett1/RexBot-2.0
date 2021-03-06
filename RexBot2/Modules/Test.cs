﻿using Discord.Commands;
using System.Threading.Tasks;
using RexBot2.Utils;
using Discord.WebSocket;
using System;
using Discord;

namespace RexBot2.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        private CommandService _commandService;
        //private DiscordSocketClient adsc;
        public Test(CommandService commandService)//, DiscordSocketClient dsc)
        {
            _commandService = commandService;

            //adsc = dsc;

        }

        [Command("repeat")]
        [Remarks("test")]
        [Summary("Repeats what you said")]
        public async Task rpCmd(string input)
        {
            await Context.Message.AddReactionAsync(new Emoji("👎"));
            //await Context.Channel.SendMessageAsync(input);
        }

        [Command("sw")]
        [Remarks("test")]
        [Summary("sw")]
        public async Task swCmd([Remainder] string input)
        {
            string res = await WebUtils.yodaOutput(input);
            await Context.Channel.SendMessageAsync(res);
        }

        [Command("test")]
        [Remarks("test")]
        [Summary("Temporary function which should not be invoked by anyone else than Rexyrex")]
        public async Task dvtstCmd()
        {
            await Context.Channel.SendMessageAsync("NOT IMPLEMENTED MADAFAKA");
        }
    }
}
