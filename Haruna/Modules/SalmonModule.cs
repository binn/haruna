using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Haruna.Modules
{
    [Group("salmon")]
    public class SalmonModule : ModuleBase
    {
        private readonly ILogger _logger;

        public SalmonModule(ILogger<SalmonModule> logger)
        {
            _logger = logger;
        }

        [Command]
        [RequireMod]
        public async Task Salmonify(IGuildUser targetUser)
        {
            await targetUser.ModifyAsync((user) =>
            {
                user.Nickname = "salmon";
            });

            await ReplyAsync("uguuu~ ojii-sama, i salmoned `" + targetUser.ToString() + "` for y-you!!!! #hailrinixx");
            _logger.LogInformation(Context.User.ToString() + " salmonified " + targetUser.ToString() + ".");
        }

        [Command]
        public async Task SalmonifySelf(IGuildUser targetUser = null)
        {
            await ((IGuildUser) Context.User).ModifyAsync((user) =>
            {
                user.Nickname = new Random().Next(0, 1) == 0 ? "salmon" : "i salmoned myself xd";
            });

            await ReplyAsync("ehehhe >///< *you tried....* s-senpai told me t-t-to salmonify those who don't listen to h-him !!!!!!!!!!!!!!!!! ");
            _logger.LogInformation(Context.User.ToString() + " tried to salmonify someone and got cucked.");
        }
    }
}
