using System;
using System.Linq;
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

            _logger.LogInformation(Context.User.ToString() + " muted user `" + user.ToString() + "` indefinetely.");
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
                    if(user.RoleIds.Contains(role.Id))
                        await user.RemoveRoleAsync(role);
                    _logger.LogInformation("INTERNAL_SYSTEM unmuted user " + user.ToString() + " after " + timeout.TotalMinutes + " minutes.");
                });

                await ReplyAsync(user.ToString() + " has been muted hahahaa ehehe !!! he's getting his punishment from my onii-sama >////< *aishiteru*");
            });

            _logger.LogInformation(Context.User.ToString() + " muted user `" + user.ToString() + "` for " + timeout.TotalMinutes + " minutes.");
        }

        [Alias("um")]
        [RequireMod]
        [Command("unmute")]
        public async Task UnmuteUserAsync(IGuildUser user)
        {
            IRole role = Context.Guild.GetRole(ulong.Parse(GlobalConfiguration.MuteRoleId));
            if(!user.RoleIds.Contains(role.Id))
            {
                await ReplyAsync("b-baka!!!! i can't u-unmute someone who's freakin not m-m-muted !!!! >:(");
            }
            else
            {
                await user.RemoveRoleAsync(role).ContinueWith(async (t) =>
                {
                    await ReplyAsync("i-i u-u-unmuted `" + user.ToString() + "`!! b-be good onegai~!");
                });

                _logger.LogInformation(Context.User.ToString() + " unmuted user " + user.ToString() + ".");
            }
        }

        [Alias("k")]
        [RequireMod]
        [Command("kick")]
        public async Task KickUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            string fullReason = Context.User.ToString() + " - " + reason ?? "No reason provided";
            await user.KickAsync(reason).ContinueWith(async (t) =>
            {
                await ReplyAsync("hh-hai!!! i k-kicked `" + user.ToString() + "` f-from the g-g-guild f-for you!!!! <3");
            });

            _logger.LogInformation(Context.User.ToString() + " kicked user " + user.ToString() + " with reason " + reason ?? "N/A");
        }

        [Alias("b")]
        [RequireMod]
        [Command("ban")]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string banReason = null)
        {
            string fullBanReason = Context.User.ToString() + " - " + banReason ?? "No reason specified";
            await Context.Guild.AddBanAsync(user, 1);
            await ReplyAsync("ojii-sama, i h-h-have b-banned *" + user.ToString() + "* succesfully !!!! where's the reward my panties are expecting!??! >:(");

            _logger.LogInformation(Context.User.ToString() + " banned user `" + user.ToString() + "` with reason: " + banReason ?? "N/A");
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
                await ReplyAsync("ojii-sama, i h-h-have **s-soft-b-banned** `" + user.ToString() + "` succesfully !!!! where's the reward my panties are expecting!??! >:(");
            });

            _logger.LogInformation(Context.User.ToString() + " soft-banned user " + user.ToString() + " with reason: " + banReason ?? "N/A");
        }
    }
}
