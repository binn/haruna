using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Haruna.Modules
{
    [RequireMod]
    [Alias("l")]
    [Group("lock")]
    public class LockModule : ModuleBase
    {
        private readonly ILogger _logger;

        public LockModule(ILogger<LockModule> logger)
        {
            _logger = logger;
        }

        [Command]
        public async Task LockCurrentChannel()
        {
            IGuildChannel channel = (IGuildChannel)Context.Channel;
            IRole defaultRole = channel.Guild.EveryoneRole;
            OverwritePermissions permissions = channel.GetPermissionOverwrite(defaultRole) ?? new OverwritePermissions();
            permissions = permissions.Modify(sendMessages: PermValue.Deny, addReactions: PermValue.Deny);
            await channel.AddPermissionOverwriteAsync(defaultRole, permissions);

            await ReplyAsync("i-i nylocked #" + channel.Name + " f-for you s-s-s-senpai :0 a-are you p-proud??! >//////<");
            _logger.LogInformation(Context.User.ToString() + " locked channel #" + channel.Name + " [" + channel.Id + "].");
        }

        [Command]
        public async Task LockChannel(ITextChannel channel)
        {
            IRole defaultRole = channel.Guild.EveryoneRole;
            OverwritePermissions permissions = channel.GetPermissionOverwrite(defaultRole) ?? new OverwritePermissions();
            permissions = permissions.Modify(sendMessages: PermValue.Deny, addReactions: PermValue.Deny);
            await channel.AddPermissionOverwriteAsync(defaultRole, permissions);

            await ReplyAsync("i-i nylocked #" + channel.Name + " f-for you s-s-s-senpai :0 a-are you p-proud??! >//////<");
            _logger.LogInformation(Context.User.ToString() + " locked channel #" + channel.Name + " [" + channel.Id + "].");
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
                    permissions = permissions.Modify(sendMessages: PermValue.Deny, addReactions: PermValue.Deny);
                    await channel.AddPermissionOverwriteAsync(defaultRole, permissions);
                }
                else
                {
                    _logger.LogWarning(string.Format("Channel '{0}' was not found for locking.", channelId));
                }
            }

            await ReplyAsync("wakarimashita~! i l-locked all the c-c-chawnyls fowr you.. s-s-senpai!! \\o/");
            _logger.LogInformation(Context.User.ToString() + " locked all channels [ " + string.Join(", ", GlobalConfiguration.LockableChannelIds) + " ].");
        }
    }
}
