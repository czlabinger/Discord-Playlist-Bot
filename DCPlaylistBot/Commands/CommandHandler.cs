using Discord.WebSocket;
using System.Threading.Tasks;

public class CommandHandler {
    public static async Task SlashCommandHandler(SocketSlashCommand command) {
        switch (command.Data.Name) {
            case "add":
                await AddCommand.HandleAddCommand(command);
                break;
        }
    }
}