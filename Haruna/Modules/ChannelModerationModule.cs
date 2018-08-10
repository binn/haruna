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
