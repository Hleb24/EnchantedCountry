using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;

namespace Core.EnchantedCountry.ForTestScenes {
    public class TestGameSave : MonoBehaviour {
        public GameSaveSystem GameSave;
        public bool CheckSaves;

        private void Start() {
            if (CheckSaves) {
                SaveSystem.Load(GameSave, SaveSystem.Constants.GameSave);
            }
        }

        private void OnDestroy() {
            SaveSystem.Save(GameSave, SaveSystem.Constants.GameSave);
        }

        [ContextMenu("Save")]
        private void Save() {
            SaveSystem.Save(GameSave, SaveSystem.Constants.GameSave);
        }
    }
}
