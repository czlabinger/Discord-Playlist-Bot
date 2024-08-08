# Discord-Playlist-Bot

This Beat Saber plugin is a Discord bot that downloads a song that was added by `/add <BeatSaver ID or BeatSaver link>` to a Playlist.

## Dependencies

* BeatSaberPlaylistLib
* BeatSaverSharp
* DataModels
* HMLib
* HMUI
* IPA.Loader
* PaylistManager
* SongCore

* And all NuGet Packages that are used

## Usage

Clone the repo, install all dependencies, build it, move the dll to your plugins folder, create an env variable named `BeatSaberDCBot` with the token of your bot as value, run BeatSaber.

### Commands
* `/add <BeatSaver ID or BeatSaver link>` adds a song to the Playlist.
* `/reload` reloads songlist in case song isn't showing up.