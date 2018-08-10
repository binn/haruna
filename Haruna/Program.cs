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
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Haruna
{
    public class Program
    {
        private static ILogger _logger;
        private static IServiceProvider _services;
        private static DiscordSocketClient _client;
        private static CommandService _commandService;
        private static DiscordSocketConfig _discordConfig;
        private static CommandServiceConfig _commandServiceConfig;

        public static async Task Main(string[] args)
        {
            await GlobalEvents.PrintStartupMessage();
            GlobalConfiguration.LoadConfiguration();
            _services = ConfigureServices(new ServiceCollection());
            _logger = _services.GetRequiredService<ILogger<Program>>();

            _discordConfig = new DiscordSocketConfig()
            {
                LargeThreshold = 250,
                AlwaysDownloadUsers = true, // RIP memory 2018
                DefaultRetryMode = RetryMode.AlwaysRetry
            };

            _commandServiceConfig = new CommandServiceConfig()
            {
                ThrowOnError = true, // haha who cares about debugging just let it fly
                CaseSensitiveCommands = false
            };

            _client = new DiscordSocketClient(_discordConfig);
            _commandService = new CommandService(_commandServiceConfig);

            _client.MessageReceived += HandleCommandAsync;
            _client.UserJoined += GlobalEvents.HandleUserJoinAsync;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly());

            _logger.LogDebug("Haruna has been loaded with all modules and commands.");
            await _client.LoginAsync(TokenType.Bot, GlobalConfiguration.BotToken, validateToken: true);
            await _client.StartAsync();
            await HandlePlayingTagsAsync();

            _logger.LogInformation("Haruna has started and is running.");
            await Task.Delay(-1);
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging((builder) =>
            {
                builder.AddDebug();
                builder.AddConsole();
            });

            return services.BuildServiceProvider();
        }

        private static async Task HandleCommandAsync(SocketMessage message)
        {
            SocketUserMessage socketMessage = (SocketUserMessage)message;
            if (message == null || message.Author.IsBot)
                return;

            int commandArgumentPosition = 0;
            if (!socketMessage.HasStringPrefix(GlobalConfiguration.BotPrefix, ref commandArgumentPosition))
            {
                if (!socketMessage.HasMentionPrefix(_client.CurrentUser, ref commandArgumentPosition))
                    return;
            }

            CommandContext commandContext = new CommandContext(_client, socketMessage);
            IResult result = await _commandService.ExecuteAsync(commandContext, commandArgumentPosition, _services, MultiMatchHandling.Best);
            _logger.LogInformation("Command executed '" + socketMessage.Content + "' from " + socketMessage.Author.ToString());

            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case CommandError.UnmetPrecondition:
                        _logger.LogWarning("Pre-condition error from " + socketMessage.Author.ToString() + ": '" + result.ErrorReason + "'");
                        await socketMessage.Channel.SendMessageAsync(result.ErrorReason);
                        break;
                    case CommandError.MultipleMatches:
                    case CommandError.Unsuccessful:
                    case CommandError.ObjectNotFound:
                    case CommandError.ParseFailed:
                        if (result is ExecuteResult failedResult)
                            _logger.LogError(failedResult.Exception, failedResult.ErrorReason);
                        else
                            _logger.LogError(result.ErrorReason);

                        await message.Channel.SendMessageAsync("**ewwor:** an unghandlwed execweptioning has happen in my panstuguu. deeteru: `" + result.ErrorReason + "`");
                        break;
                    case CommandError.UnknownCommand:
                        break; // Ignore unknown comands.
                    case CommandError.BadArgCount:
                        await message.Channel.SendMessageAsync("**ewwor:** i'm mwissing some deeteru, or you gave my panstu too many!!!! pwease spwecifiy the reqwired arjwuments and twy agwainn, *onegai* !!!!!!!!");
                        break;
                    case CommandError.Exception:
                        if (result is ExecuteResult executeResult)
                            _logger.LogError(executeResult.Exception, executeResult.ErrorReason);
                        else
                            _logger.LogError(result.ErrorReason);

                        await message.Channel.SendMessageAsync("**ewwor:** an unghandlwed execweptioning has happen in my panstuguu. deeteru: `" + result.ErrorReason + "`");
                        break;
                    default:
                        break;
                }
            }
        }

        private static async Task HandlePlayingTagsAsync()
        {
            while (true)
            {
                for (int i = 0; i < GlobalConfiguration.PlayingStatusMessages.Length; i++)
                {
                    await _client.SetGameAsync(GlobalConfiguration.PlayingStatusMessages[i]);
                    await Task.Delay(60 * 1000);
                }
            }
        }
    }
}