using UnityEngine;

namespace Core {
  public class PrefsSaver : ISaver {
    private const string NewGame = nameof(NewGame);

    public void Save(SaveGame saveGame) {
      string json = JsonUtility.ToJson(saveGame);
      PlayerPrefs.SetString(NewGame, json);
      PlayerPrefs.Save();
    }

    public SaveGame Load() {
      if (IsNewGame()) {
        return NewSave();
      }

      string json = PlayerPrefs.GetString(NewGame);
      return JsonUtility.FromJson<SaveGame>(json);
    }

    private bool IsNewGame() {
      return !IsSaveExists;
    }

    private SaveGame NewSave() {
      PlayerPrefs.DeleteAll();
      SaveGame saveGame = new SaveGame().NewSaveGame();
      return saveGame;
    }

    private bool IsSaveExists {
      get {
        return PlayerPrefs.HasKey(NewGame);
      }
    }
  }
}