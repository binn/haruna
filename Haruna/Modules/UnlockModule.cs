using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Haruna.Modules
{
    [RequireMod]
    [Alias("ul")]
    [Group("unlock")]
    public class UnlockModule : ModuleBase
    {
        private readonly ILogger _logger;

        public UnlockModule(ILogger<UnlockModule> logger)
        {
            _logger = logger;
        }

        [Command]
        public async Task LockCurrentChannel()
        {
            IGuildChannel channel = (IGuildChannel)Context.Channel;
            IRole defaultRole = channel.Guild.EveryoneRole;
            OverwritePermissions permissions = channel.GetPermissionOverwrite(defaultRole) ?? new OverwritePermissions();
            permissions = permissions.Modify(sendMessages: PermValue.Inherit, addReactions: PermValue.Inherit);
            await channel.AddPermissionOverwriteAsync(defaultRole, permissions);

            await ReplyAsync("uwu!! #" + channel.Name + " is nyow unlowcked!1!  uguuuu yes im so c-c-ccreative");
            _logger.LogInformation(Context.User.ToString() + " unlocked channel #" + channel.Name + " [" + channel.Id + "].");
        }

        [Command]
        public async Task LockChannel(ITextChannel channel)
        {
            IRole defaultRole = channel.Guild.EveryoneRole;
            OverwritePermissions permissions = channel.GetPermissionOverwrite(defaultRole) ?? new OverwritePermissions();
            permissions = permissions.Modify(sendMessages: PermValue.Inherit, addReactions: PermValue.Inherit);
            await channel.AddPermissionOverwriteAsync(defaultRole, permissions);

            await ReplyAsync("uwu!! #" + channel.Name + " is nyow unlowcked!1!  uguuuu yes im so c-c-ccreative");
            _logger.LogInformation(Context.User.ToString() + " unlocked channel #" + channel.Name + " [" + channel.Id + "].");
        }

        [Alias("a")]
        [Command("all")]
        public async Task LockAllChannels()
        {
            IRole defaultRole = Context.Guild.EveryoneRole;
            IReadOnlyCollection<IGuildChannel> guildChannels = await Context.Guild.GetChannelsAsync();
            for (int idLoc = 0; idLoc < GlobalConfiguration.LockableChannelIds.Length; idLoc++)
            {
                ulong channelId = ulong.Parse(GlobalConfiguration.LockableChannelIds[idLoc]);
                IGuildChannel channel = guildChannels.FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    OverwritePermissions permissions = channel.GetPermissionOverwrite(defaultRole) ?? new OverwritePermissions();
                    permissions = permissions.Modify(sendMessages: PermValue.Inherit, addReactions: PermValue.Inherit);
                    await channel.AddPermissionOverwriteAsync(defaultRole, permissions);
                }
                else
                {
                    _logger.LogWarning(string.Format("Channel '{0}' was not found for locking.", channelId));
                }
            }

            await ReplyAsync("all the chawnnels hawv bween unlocked~!! uwu p-please reward me.. ///");
            _logger.LogInformation(Context.User.ToString() + " unlocked all channels [" + string.Join(", ", GlobalConfiguration.LockableChannelIds) + "].");
        }
    }
}
