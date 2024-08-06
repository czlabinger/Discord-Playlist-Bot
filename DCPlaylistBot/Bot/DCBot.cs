using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;
using System;
using Discord.Net;
using Newtonsoft.Json;

public class DCBot {

    private static DiscordSocketClient _client;

    public async void Start() {
        _client = new DiscordSocketClient();

        _client.Log += Log;

        string token = Environment.GetEnvironmentVariable("BeatSaberDCBot");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        //Called when server data download finished
        _client.Ready += Create_Slash_Command;

        //Called when slash command is executed
        _client.SlashCommandExecuted += CommandHandler.SlashCommandHandler;

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private async Task Create_Slash_Command() {

        try {
            foreach(SlashCommandBuilder s in SlashCommandCreator.GetSlashCommandBuilders()) {
                await _client.CreateGlobalApplicationCommandAsync(s.Build());
            }

        } catch (HttpException exception) {
            string json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }

    private static Task Log(LogMessage msg) {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }
}
