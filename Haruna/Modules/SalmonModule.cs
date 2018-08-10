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
