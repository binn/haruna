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
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Haruna;

public class RequireModAttribute : PreconditionAttribute
{
    public override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
    {
        bool isAuthorized = false;
        IGuildUser user = (IGuildUser)context.User;
        string[] roles = user.RoleIds.Select(r => r.ToString()).ToArray();
        for (int i = 0; i < roles.Length; i++)
        {
            if (GlobalConfiguration.ModIds.Contains(roles[i]) || HarunaWelcome.GlobalId.Equals(user.Id))
            {
                isAuthorized = true;
                break;
            }
        }

        if (isAuthorized)
        {
            return Task.FromResult(PreconditionResult.FromSuccess());
        }
        else
        {
            return Task.FromResult(PreconditionResult.FromError("uh oh !!! senpai told me not to let you touch me. >////< srry i can't suck your cock :(((("));
        }
    }
}
