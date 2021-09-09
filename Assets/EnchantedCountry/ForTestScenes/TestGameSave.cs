using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;

namespace Core.EnchantedCountry.ForTestScenes {
    public class TestGameSave : MonoBehaviour {
        public GameSaveSystem GameSave;
        public bool CheckSaves;

        private void Start() {
            if (CheckSaves) {
                SaveSystem.Load(GameSave, SaveSystem.Constants.GAME_SAVE);
            }
        }

        private void OnDestroy() {
            SaveSystem.Save(GameSave, SaveSystem.Constants.GAME_SAVE);
        }

        [ContextMenu("Save")]
        private void Save() {
            SaveSystem.Save(GameSave, SaveSystem.Constants.GAME_SAVE);
        }
    }
}
