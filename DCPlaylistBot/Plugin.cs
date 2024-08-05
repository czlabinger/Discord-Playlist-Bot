using BeatSaberPlaylistsLib.Blist;
using BeatSaverSharp;
using IPA;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace DCPlaylistBot {
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static DCBot bot = new DCBot();
        internal static BeatSaberPlaylistsLib.PlaylistManager playlistManager = new BeatSaberPlaylistsLib.PlaylistManager("Discord");
        internal static BlistPlaylistHandler playlistHandler = new BlistPlaylistHandler();
        internal static BeatSaver beatSaver = new BeatSaver("DCQueueBot", new System.Version("0.0.1"));


        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger) {
            Instance = this;
            Log = logger;
            Log.Info("DCPlaylistBot initialized.");

            playlistManager.RegisterHandler(playlistHandler);

            bot.Start();
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart() {
            Log.Debug("DCPlaylistBot Started");
            new GameObject("DCPlaylistBotController").AddComponent<DCPlaylistBotController>();

        }

        [OnExit]
        public void OnApplicationQuit() {
            Log.Debug("DCPlaylistBot Quit");

        }
    }
}
