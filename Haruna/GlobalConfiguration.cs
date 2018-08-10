using System;

namespace Haruna
{
    public class GlobalConfiguration
    {
        private static string _botToken;
        private static string[] _modIds;
        private static string _botPrefix;
        private static string _muteRoleId;
        private static string[] _lockableChannelIds;

        private const string _configurationNotLoadedResource = "Configuration has not been loaded";

        public static string[] ModIds => _modIds;
        public static string BotToken => _botToken;
        public static string BotPrefix => _botPrefix;
        public static string MuteRoleId => _muteRoleId;
        public static string[] LockableChannelIds => _lockableChannelIds;


        public static void LoadConfiguration()
        {
            string unParsedModIds = Environment.GetEnvironmentVariable("HARUNA_MODS");
            string localBotToken = Environment.GetEnvironmentVariable("HARUNA_TOKEN");
            string userMutedRoleId = Environment.GetEnvironmentVariable("HARUNA_MUTE");
            string localBotPrefix = Environment.GetEnvironmentVariable("HARUNA_PREFIX");
            string lockChannelIds = Environment.GetEnvironmentVariable("HARUNA_CHANNELS");

            if (!string.IsNullOrWhiteSpace(localBotToken))
            {
                _botToken = localBotToken;
            }

            if(!string.IsNullOrWhiteSpace(userMutedRoleId))
            {
                _muteRoleId = userMutedRoleId;
            }

            if (!string.IsNullOrWhiteSpace(userMutedRoleId))
            {
                _muteRoleId = userMutedRoleId;
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
