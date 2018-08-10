using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Haruna.Modules
{
    public class UserModerationModule : ModuleBase
    {
        private readonly ILogger _logger;

        public UserModerationModule(ILogger<UserModerationModule> logger)
        {
            _logger = logger;
        }

        [Alias("m")]
        [RequireMod]
        [Command("mute")]
        public async Task MuteUserAsync(IGuildUser user)
        {
            IRole role = Context.Guild.GetRole(ulong.Parse(GlobalConfiguration.MuteRoleId));
            await user.AddRoleAsync(role);
            await ReplyAsync(user.ToString() + " has been muted hahahaa ehehe !!! he's getting his punishment from my onii-sama >////< *aishiteru*");

            _logger.LogInformation(Context.User.ToString() + " muted user " + user.ToString() + " indefinetely.");
        }

        [Alias("m")]
        [RequireMod]
        [Command("mute")]
        public async Task MuteUserAsync(IGuildUser user, TimeSpan timeout)
        {
            IRole role = Context.Guild.GetRole(ulong.Parse(GlobalConfiguration.MuteRoleId));
            await user.AddRoleAsync(role).ContinueWith(async (t) =>
            {
                await Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(timeout);
                    await user.RemoveRoleAsync(role);
                    _logger.LogInformation("Unmuted " + user.ToString() + ".");
                });

                await ReplyAsync(user.ToString() + " has been muted hahahaa ehehe !!! he's getting his punishment from my onii-sama >////< *aishiteru*");
            });

            _logger.LogInformation(Context.User.ToString() + " muted user " + user.ToString() + " for " + timeout.TotalMinutes + " minutes.");
        }

        [Alias("b")]
        [RequireMod]
        [Command("ban")]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string banReason = null)
        {
            string fullBanReason = Context.User.ToString() + " - " + banReason ?? "No reason specified";
            await Context.Guild.AddBanAsync(user, 1);
            await ReplyAsync("ojii-sama, i h-h-have b-banned *" + user.ToString() + "* succesfully !!!! where's the reward my panties are expecting!??! >:(");

            _logger.LogInformation(Context.User.ToString() + " banned user " + user.ToString() + " with reason: " + banReason ?? "N/A");
        }

        [Alias("sb")]
        [RequireMod]
        [Command("softban")]
        public async Task SoftBanUserAsync(IGuildUser user, [Remainder] string banReason = null)
        {
            string fullBanReason = Context.User.ToString() + " - " + banReason ?? "No reason specified";
            await Context.Guild.AddBanAsync(user, 1).ContinueWith(async (t) =>
            {
                await Context.Guild.RemoveBanAsync(user);
                await ReplyAsync("ojii-sama, i h-h-have *soft*b-banned *" + user.ToString() + "* succesfully !!!! where's the reward my panties are expecting!??! >:(");
            });

            _logger.LogInformation(Context.User.ToString() + " soft-banned user " + user.ToString() + " with reason: " + banReason ?? "N/A");
        }
    }
}
