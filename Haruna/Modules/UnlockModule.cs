#region GPLv3 LICENSE

/*
    Haruna is a simple moderation bot for Discord.
    Copyright (c) 2018 Sarmad Wahab (bin).

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion GPLv3 LICENSE 

/* Author: https://github.com/binsenpai */

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
            _logger.LogInformation(Context.User.ToString() + " unlocked all channels [ " + string.Join(", ", GlobalConfiguration.LockableChannelIds) + " ].");
        }
    }
}
