using UnityEngine;

namespace Core {
  public class PrefsSaver : ISaver {
    private const string NewGame = nameof(NewGame);

    public void Save(Save save) {
      string json = JsonUtility.ToJson(save);
      PlayerPrefs.SetString(NewGame, json);
      PlayerPrefs.Save();
    }

    public Save Load() {
      if (IsNewGame()) {
        return NewSave();
      }

      string json = PlayerPrefs.GetString(NewGame);
      return JsonUtility.FromJson<Save>(json);
    }

    private bool IsNewGame() {
      return !IsSaveExists;
    }

    private Save NewSave() {
      PlayerPrefs.DeleteAll();
      Save save = new Save().LoadDefault();
      return save;
    }

    private bool IsSaveExists {
      get {
        return PlayerPrefs.HasKey(NewGame);
      }
    }
  }
}