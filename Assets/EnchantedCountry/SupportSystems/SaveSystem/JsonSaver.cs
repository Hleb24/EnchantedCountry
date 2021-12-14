using System;
using System.IO;
using UnityEngine;

namespace Core {
  public class JsonSaver : ISaver {
    private readonly string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.json");
    private readonly string _pathToFolder = Path.Combine(Application.persistentDataPath, "Save");

    public void Save(Save save) {
      CreateDirectory();
      string jsonSave = JsonUtility.ToJson(save, true);
      using var streamWriter = new StreamWriter(_pathToFile);
      streamWriter.WriteLine(jsonSave);
    }

    public Save Load() {
      var jsonSave = string.Empty;
      try {
        using var streamReader = new StreamReader(_pathToFile);
        jsonSave = streamReader.ReadToEnd();
        var save = JsonUtility.FromJson<Save>(jsonSave);
        ;
        return save;
      } catch (Exception e) {
        ClearPrefs();
        CreateDirectory();
        if (!string.IsNullOrEmpty(jsonSave)) {
          throw;
        }

        Save newSave = new Save().LoadDefault();
        return newSave;
      }
    }

    private void ClearPrefs() {
      PlayerPrefs.DeleteAll();
    }

    private void CreateDirectory() {
      if (!Directory.Exists(_pathToFolder)) {
        Directory.CreateDirectory(_pathToFolder);
      }
    }
  }
}