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
    [Alias("p")]
    [RequireMod]
    [Group("prune")]
    public class ChannelModerationModule : ModuleBase
    {
        private readonly ILogger _logger;

        public ChannelModerationModule(ILogger<ChannelModerationModule> logger)
        {
            _logger = logger;
        }

        [Command]
        public async Task PruneMessagesAsync(int messageCount)
        {
            messageCount = messageCount > 100 ? 100 : messageCount;
            await Context.Channel.DeleteMessagesAsync(await Context.Channel.GetMessagesAsync(messageCount).Flatten());
            await ReplyAsync("s-senpai.....i h-have *deleted* " + messageCount + " m-m-m-m-m-m-messages f-from #" + Context.Channel.Name + " !!!! r-reward me kudasai *>////<*");

            _logger.LogInformation(Context.User.ToString() + " pruned " + messageCount + " messages from #" + Context.Channel.Name + " [" + Context.Channel.Id + "].");
        }

        [Command]
        public async Task PruneUserMessagesAsync(int messageCount, IGuildUser user)
        {
            messageCount = messageCount > 100 ? 100 : messageCount;
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount).Flatten();
            messages = messages.Where(c => c.Author.Id == user.Id);
            await Context.Channel.DeleteMessagesAsync(messages);
            await ReplyAsync("s-senpai.....i h-have *deleted* " + messageCount + " m-m-m-m-m-m-messages f-from #" + Context.Channel.Name + " (by `" + user.ToString() + "`) !!!! r-reward me kudasai *>////<*");

            _logger.LogInformation(Context.User.ToString() + " pruned " + messageCount + " messages from #" + Context.Channel.Name + " [" + Context.Channel.Id + "].");
        }
    }
}
