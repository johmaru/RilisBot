using Discord.Commands;

namespace RilisBot.Commands;

public class Core : ModuleBase<SocketCommandContext>
{
    [Command("help")]
    [Summary("ヘルプコマンド")]
    public async Task HelpAsync()
    {
        await ReplyAsync("prefix : ?\r\nHelp : ヘルプコマンド\r\n qaThis : 送ったメッセージをAIに反応してもらうコマンド\r\n qaThey : チャット履歴のメッセージをAIに反応してもらうコマンド\r\n");
    }
}