using System;
using System.Threading.Tasks;
using Discord.WebSocket;

public class ReloadCommand {

    public static async Task HandleReloadCommand(SocketSlashCommand command) {
        if (SongCore.Loader.Instance == null) {
            await command.RespondAsync("No Loader instance!");
            return;
        }

        try {
            SongCore.Loader.Instance.RefreshSongs();
            await command.RespondAsync("Refreshed songlist!");
        } catch(Exception e) {
            await command.RespondAsync(e.ToString());
            return;
        }
    }
}