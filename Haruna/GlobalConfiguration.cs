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

namespace Haruna
{
    public class GlobalConfiguration
    {
        private static string _botToken;
        private static string[] _modIds;
        private static string _botPrefix;
        private static string _muteRoleId;
        private static string[] _playingStatusLoop;
        private static string[] _lockableChannelIds;

        public static string[] ModIds => _modIds;
        public static string BotToken => _botToken;
        public static string BotPrefix => _botPrefix;
        public static string MuteRoleId => _muteRoleId;
        public static string[] LockableChannelIds => _lockableChannelIds;
        public static string[] PlayingStatusMessages => _playingStatusLoop;

        public static void LoadConfiguration()
        {
            string unParsedModIds = Environment.GetEnvironmentVariable("HARUNA_MODS");
            string localBotToken = Environment.GetEnvironmentVariable("HARUNA_TOKEN");
            string userMutedRoleId = Environment.GetEnvironmentVariable("HARUNA_MUTE");
            string unParsedPlaying = Environment.GetEnvironmentVariable("HARUNA_GAMES");
            string localBotPrefix = Environment.GetEnvironmentVariable("HARUNA_PREFIX");
            string lockChannelIds = Environment.GetEnvironmentVariable("HARUNA_CHANNELS");

            if (!string.IsNullOrWhiteSpace(localBotToken))
            {
                _botToken = localBotToken;
            }

            if(!string.IsNullOrWhiteSpace(localBotPrefix))
            {
                _botPrefix = localBotPrefix;
            }

            if (!string.IsNullOrWhiteSpace(userMutedRoleId))
            {
                _muteRoleId = userMutedRoleId;
            }

            if(!string.IsNullOrWhiteSpace(unParsedPlaying))
            {
                _playingStatusLoop = unParsedPlaying.Split(';');
            }
            else
            {
                _playingStatusLoop = new string[0];
            }

            if (!string.IsNullOrWhiteSpace(unParsedModIds))
            {
                _modIds = unParsedModIds.Split(';');
            }
            else
            {
                _modIds = new string[0];
            }

            if(!string.IsNullOrWhiteSpace(lockChannelIds))
            {
                _lockableChannelIds = lockChannelIds.Split(';');
            }
            else
            {
                _lockableChannelIds = new string[0];
            }
        }
    }
}
