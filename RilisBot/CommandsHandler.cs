using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace RilisBot;

public class CommandsHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    
    public CommandsHandler(DiscordSocketClient client, CommandService commands)
    {
        _client = client;
        _commands = commands;
    }
    public async Task InstallCommandAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), null);
    }

    private async Task HandleCommandAsync(SocketMessage msg)
    {
        var message = msg as SocketUserMessage;
        if (message == null) return;
        
        int argPos = 0;
        
        if(!(message.HasCharPrefix('?', ref argPos) || 
           message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
           message.Author.IsBot) return;
        
        var context = new SocketCommandContext(_client, message);
        
        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: null
            );
    }
}