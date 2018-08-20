using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Haruna.Services;

namespace Haruna.Modules
{
    [Group("join")] // Enforce mod on this?
    public class JoinTestModule : ModuleBase
    {
        private readonly IJoinService _joinService;

        public JoinTestModule(IJoinService joinService) // Add logging
        {
            _joinService = joinService;
        }

        [Command]
        public async Task JoinUserTestAsync(bool @override = false)
        {
            if(!GlobalConfiguration.JoinImageEnabled && !@override)
            {
                await ReplyAsync("i'm unable to do this !!!!!! pls enable me in the configuration !!!!");
                return;
            }

            Stream avatarStream = await _joinService.GetStreamFromAvatarUrlAsync(Context.User.GetAvatarUrl());
            Stream templateStream = await _joinService.GenerateWelcomeImageAsync(Context.User.Username, Context.User.Discriminator, avatarStream);
            avatarStream.Dispose();

            await Context.Channel.SendFileAsync(templateStream, "welcome.png", null);
        }
    }
}
