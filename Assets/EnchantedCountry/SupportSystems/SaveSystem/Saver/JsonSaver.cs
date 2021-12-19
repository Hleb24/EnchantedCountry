using System;
using System.IO;
using UnityEngine;

namespace Core {
  public class JsonSaver : ISaver {
    private readonly string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.json");
    private readonly string _pathToFolder = Path.Combine(Application.persistentDataPath, "Save");

    public void Save(Scrolls scrolls) {
      CreateDirectory();
      string jsonSave = JsonUtility.ToJson(scrolls, true);
      using var streamWriter = new StreamWriter(_pathToFile);
      streamWriter.WriteLine(jsonSave);
    }

    public Scrolls Load() {
      var jsonSave = string.Empty;
      try {
        using var streamReader = new StreamReader(_pathToFile);
        jsonSave = streamReader.ReadToEnd();
        var save = JsonUtility.FromJson<Scrolls>(jsonSave);
        return save;
      } catch (Exception e) {
        Debug.Log("Новая игра началась " + e.Data);
        ClearPrefs();
        CreateDirectory();
        if (!string.IsNullOrEmpty(jsonSave)) {
          throw;
        }

        Scrolls newScrolls = new Scrolls().NewScrollGame();
        return newScrolls;
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