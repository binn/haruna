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
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Haruna.Services;

namespace Haruna
{
    public class GlobalEvents
    {
        public static async Task HandleUserJoinAsync(SocketGuildUser user, IJoinService joinService)
        {
            if (!GlobalConfiguration.JoinImageEnabled)
                return;

            Stream avatarStream = await joinService.GetStreamFromAvatarUrlAsync(user.GetAvatarUrl(Discord.ImageFormat.Auto, 256));
            Stream templateStream = await joinService.GenerateWelcomeImageAsync(user.Username, user.Discriminator, avatarStream);
            avatarStream.Dispose();

            // TODO: Allow configuration of this channel and message to go with the file.
            ITextChannel textChannel = (ITextChannel) user.Guild.GetChannel(ulong.Parse(GlobalConfiguration.JoinImageChannel));
            await textChannel.SendFileAsync(templateStream, "the-salmon-king-welcomes-you.png", null);
        }

        public static void PrintStartupMessage()
        {
            Console.WriteLine(HarunaWelcome.WelcomeString);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Hello from Haruna!");
            Console.ResetColor();
        }
    }
}
