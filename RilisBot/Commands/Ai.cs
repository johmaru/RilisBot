using Discord;
using Discord.Commands;

namespace RilisBot.Commands;

public class Ai : ModuleBase<SocketCommandContext>
{
    [Command("qaThis")]
    [Summary("送ったメッセージをAIに反応してもらうコマンド")]
    public async Task QaThisAsync()
    {
        var message = await Context.Channel.GetMessagesAsync(2).FlattenAsync();
        var prevMessage = message.ElementAt(1);
        
        ClaudeClient client = new();

       var aiResponse = await client.GetClaudeResponse(1,prevMessage.Content);

        await ReplyAsync($"AI: {aiResponse}");
    }
    [Command("qaThey")]
    [Summary("チャット履歴のメッセージをAIに反応してもらうコマンド")]
    public async Task QaTheyAsync()
    {
        var messages = await Context.Channel.GetMessagesAsync(20).FlattenAsync();
        var chatHistory = messages.Select(x => x.Content).ToList();
        
            chatHistory.RemoveAll(s => s.Contains("AI:") || s.Contains("?qaThis") || s.Contains("?qaThey"));
            
        #if DEBUG
        Console.WriteLine(string.Join("\n", chatHistory));
        #endif
        
        ClaudeClient client = new();

        var aiResponse = await client.GetClaudeResponse(1,string.Join("\n", chatHistory));

        await ReplyAsync($"AI: {aiResponse}");
    }

    [Command("transThis")]
    [Summary("送ったメッセージを翻訳してもらうコマンド")]
    public async Task TransThisAsync()
    {
        var message = await Context.Channel.GetMessagesAsync(2).FlattenAsync();
        var prevMessage = message.ElementAt(1);
        
        ClaudeClient client = new();
        
        var aiResponse = await client.GetClaudeResponse(2,prevMessage.Content);
        
        await ReplyAsync($"AI: {aiResponse}");
    }
    
    [Command("transThey")]
    [Summary("チャット履歴のメッセージを翻訳してもらうコマンド")]
    public async Task TransTheyAsync()
    {
        var messages = await Context.Channel.GetMessagesAsync(20).FlattenAsync();
        var chatHistory = messages.Select(x => x.Content).ToList();
        
        chatHistory.RemoveAll(s => s.Contains("AI:") || s.Contains("?qaThis") || s.Contains("?qaThey"));
        
        #if DEBUG
        Console.WriteLine(string.Join("\n", chatHistory));
        #endif
        
        ClaudeClient client = new();
        
        var aiResponse = await client.GetClaudeResponse(2,string.Join("\n", chatHistory));
        
        await ReplyAsync($"AI: {aiResponse}");
    }
}