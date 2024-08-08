using Discord.WebSocket;
using System.Threading.Tasks;
using BeatSaverSharp.Models;
using System.Linq;
using System;
using SongCore;
using PlaylistManager.Utilities;
using BeatSaberPlaylistsLib.Types;
using System.Text.RegularExpressions;

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

            IPlaylist playlist = PlaylistLibUtils.playlistManager.GetPlaylist("DiscordQueue");

            if (playlist == null) {
                playlist = PlaylistLibUtils.CreatePlaylist("DiscordQueue", "DCBot", DCPlaylistBot.Plugin.playlistManager, allowDups: false);
            }

            Beatmap beatmap = await DCPlaylistBot.Plugin.beatSaver.Beatmap(songID);

            string folderName = @"G:\Steam\steamapps\common\Beat Saber\Beat Saber_Data\CustomLevels\" + Regex.Replace(songID + " (" + beatmap.Name + " - " + beatmap.Uploader.Name + ")", @"[\\\/\:\*\?\""\<\>\|]", "");

            byte[] zip = await beatmap.LatestVersion.DownloadZIP();
            ZipExtractor.ExtractZipFromByteArray(zip, folderName);

            if (Loader.Instance != null) {
                Loader.Instance.RefreshSongs();
            } else {
                await command.RespondAsync("Please wait until I'm in menu");
                return;
            }
            
            (string, BeatmapLevel)? beatmapLevel = Loader.LoadCustomLevel(folderName);

            IPlaylistSong s = playlist.Add(beatmapLevel.Value.Item2);
            playlist.RaisePlaylistChanged();


            await command.RespondAsync("Added " + beatmap.Name + " mapped by " + beatmap.Uploader.Name +  " to playlist");
        } catch(Exception e) {
            await command.RespondAsync("Error: " + e);
            throw e;
        }
    }
}