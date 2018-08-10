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
using Discord.WebSocket;

namespace Haruna
{
    public class GlobalEvents
    {
        public static Task HandleUserJoinAsync(SocketGuildUser user)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        public static async Task PrintStartupMessage()
        {
            Console.WriteLine(HarunaWelcome.WelcomeString);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Hello from Haruna!");
            Console.ForegroundColor = ConsoleColor.Yellow;

            string defaultEnder = "]";
            string defaultStarter = "[=";
            int sizeOffset = defaultEnder.Length;
            int currentSize = defaultStarter.Length;

            Console.Write(defaultStarter);
            Console.CursorLeft = currentSize + 30 + defaultEnder.Length;
            Console.Write(defaultEnder);
            Console.CursorLeft = defaultStarter.Length;

            for (int i = 0; i < 30; i++)
            {
                Console.CursorLeft = currentSize;
                Console.Write("=>");
                currentSize++;
                await Task.Delay(new Random().Next(30, 256));
            }

            Console.Write(defaultEnder + Environment.NewLine);
            Console.ResetColor();
        }
    }
}
