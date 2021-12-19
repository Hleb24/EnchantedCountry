using UnityEngine;

namespace Core {
  public class PrefsSaver : ISaver {
    private const string NewGame = nameof(NewGame);

    public void Save(Scrolls scrolls) {
      string json = JsonUtility.ToJson(scrolls);
      PlayerPrefs.SetString(NewGame, json);
      PlayerPrefs.Save();
    }

    public Scrolls Load() {
      if (IsNewGame()) {
        return NewSave();
      }

      string json = PlayerPrefs.GetString(NewGame);
      return JsonUtility.FromJson<Scrolls>(json);
    }

    private bool IsNewGame() {
      return !IsSaveExists;
    }

    private Scrolls NewSave() {
      PlayerPrefs.DeleteAll();
      Scrolls scrolls = new Scrolls().NewScrollGame();
      return scrolls;
    }

    private bool IsSaveExists {
      get {
        return PlayerPrefs.HasKey(NewGame);
      }
    }
  }
}