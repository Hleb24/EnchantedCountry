using System.IO;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public static class SavePath {
    private static readonly string PersistentDataPath = Application.persistentDataPath;
    public static readonly string PathToJsonFile = Path.Combine(Path.Combine(PersistentDataPath, "Save"), "Save.json");
    public static readonly string PathToXmlFile = Path.Combine(Path.Combine(PersistentDataPath, "Save"), "Save.xml");
    public static readonly string PathToFolder = Path.Combine(PersistentDataPath, "Save");
    public static readonly string PathToPrefsFile = "MAIN_GAME_SAVES";
  }
}