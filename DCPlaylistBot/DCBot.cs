using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;
using System;
using Discord.Net;
using Newtonsoft.Json;
using DCPlaylistBot;

public class DCBot {

    private static DiscordSocketClient _client;

    public async void Start() {
        _client = new DiscordSocketClient();

        _client.Log += Log;

        string token = Environment.GetEnvironmentVariable("BeatSaberDCBot");

        Plugin.Log.Error("Token " + token);

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

        SlashCommandBuilder addCommand = new SlashCommandBuilder()
            .WithName("add")
            .WithDescription("Add a song to download")
            .AddOption("linkorid", ApplicationCommandOptionType.String, "The link or the ID of the song you want to add", isRequired: true);

        try {
            await _client.CreateGlobalApplicationCommandAsync(addCommand.Build());
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
