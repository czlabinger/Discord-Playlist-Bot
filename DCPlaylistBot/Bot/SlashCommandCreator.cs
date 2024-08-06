using System.Collections;
using Discord;

public class SlashCommandCreator {

    public static ArrayList GetSlashCommandBuilders() {

        ArrayList slashCommandBuilders = new ArrayList();

        SlashCommandBuilder addCommand = new SlashCommandBuilder()
        .WithName("add")
        .WithDescription("Add a song to download")
        .AddOption("linkorid", ApplicationCommandOptionType.String, "The link or the ID of the song you want to add", isRequired: true);

        SlashCommandBuilder reloadCommand = new SlashCommandBuilder()
            .WithName("reload")
            .WithDescription("Reloads my song list");

        slashCommandBuilders.Add(addCommand);
        slashCommandBuilders.Add(reloadCommand);

        return slashCommandBuilders;
    }
}
