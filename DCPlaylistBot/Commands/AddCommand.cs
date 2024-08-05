using Discord.WebSocket;
using System.Threading.Tasks;
using BeatSaverSharp.Models;
using System.Linq;
using System;
using SongCore;

public class AddCommand {

    public static async Task HandleAddCommand(SocketSlashCommand command) {
        try {

            string songID = "";
            string songLinkOrID = command.Data.Options.First().Value.ToString();

            if (songLinkOrID.StartsWith("http")) {
                songID = songLinkOrID.Substring(songLinkOrID.LastIndexOf('/'));
            }
            else {
                songID = songLinkOrID;
            }

            //IPlaylist playlist = PlaylistLibUtils.CreatePlaylist("Discord queue", "DC Queue", DCPlaylistBot.Plugin.playlistManager, true, false);

            Beatmap beatmap = await DCPlaylistBot.Plugin.beatSaver.Beatmap(songID);

            byte[] zip = await beatmap.LatestVersion.DownloadZIP();
            ZipExtractor.ExtractZipFromByteArray(zip, "G:\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data\\CustomLevels\\" + songID + " (" + beatmap.Name + " - " + beatmap.Uploader.Name + ")");

            Loader l = Loader.Instance;
            if (l != null) {
                l.RefreshSongs();
            } else {
                await command.RespondAsync("Please wait until I'm in menu");
                return;
            }


            await command.RespondAsync("Downloading " + beatmap.Name + " - " + beatmap.Uploader.Name);

            /*
            (string, BeatmapLevel)? beatmapLevel = SongCore.Loader.LoadCustomLevel(@"G:\Steam\steamapps\common\Beat Saber\Beat Saber_Data\CustomLevels\" + songID + "(" + beatmap.Name + " - " + beatmap.Uploader.Name + ")");

            if (beatmapLevel == null) {
                await command.RespondAsync("null");
            } else {
                await command.RespondAsync("yay");
            }

            await command.RespondAsync("string: " + beatmapLevel.Value.Item1);

            playlist.Add(beatmapLevel.Value.Item2);
            playlist.RaisePlaylistChanged();

            await command.RespondAsync("Added " + beatmap.Name + " mapped by " + beatmap.Uploader.Name +  " to playlist");*/
        } catch(Exception e) {
            await command.RespondAsync("Error: " + e);
            throw e;
        }
    }
}