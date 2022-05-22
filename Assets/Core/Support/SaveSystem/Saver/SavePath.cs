using System.IO;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public static class SavePath {
    public static readonly string PathToJsonFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.json");
    public static readonly string PathToXmlFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.xml");
    public static readonly string PathToFolder = Path.Combine(Application.persistentDataPath, "Save");
    public static readonly string PathToPrefsFile = "MAIN_GAME_SAVES";
  }
}