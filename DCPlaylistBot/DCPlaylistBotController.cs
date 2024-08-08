using UnityEngine;

namespace DCPlaylistBot {

    public class DCPlaylistBotController : MonoBehaviour {
        public static DCPlaylistBotController Instance { get; private set; }

        private void Awake() {
            if (Instance != null) {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                DestroyImmediate(this);
                return;
            }
            DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            Plugin.Log?.Debug($"{name}: Awake()");
        }
        
        private void OnDestroy() {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
    }
}
