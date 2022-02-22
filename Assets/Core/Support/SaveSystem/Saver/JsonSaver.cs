using System;
using System.IO;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public class JsonSaver : ISaver {
    private readonly string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.json");
    private readonly string _pathToFolder = Path.Combine(Application.persistentDataPath, "Save");

    public void Save(Scrolls scrolls) {
      CreateDirectory();
      string jsonSave = JsonUtility.ToJson(scrolls, true);
      using var streamWriter = new StreamWriter(_pathToFile);
      streamWriter.WriteLine(jsonSave);
    }

    public Scrolls Load(out bool isNewGame) {
      var jsonSave = string.Empty;
      try {
        using var streamReader = new StreamReader(_pathToFile);
        jsonSave = streamReader.ReadToEnd();
        var save = JsonUtility.FromJson<Scrolls>(jsonSave);
        isNewGame = false;
        return save;
      } catch (Exception e) {
        Debug.Log("Новая игра началась " + e.Data);
        ClearPrefs();
        CreateDirectory();
        isNewGame = true;
        if (!string.IsNullOrEmpty(jsonSave)) {
          throw;
        }

        Scrolls newScrolls = new Scrolls().NewScrollGame();
        return newScrolls;
      }
    }

    public void DeleteSave() {
      File.Delete(_pathToFile);
      ClearPrefs();
    }

    public void Save<T>(T type, string pathToFolder, string pathToFile) {
      CreateDirectory(pathToFolder);
      string jsonSave = JsonUtility.ToJson(type, true);
      using var streamWriter = new StreamWriter(pathToFile);
      streamWriter.WriteLine(jsonSave);
    }

    private void ClearPrefs() {
      PlayerPrefs.DeleteAll();
    }

    private void CreateDirectory() {
      if (!Directory.Exists(_pathToFolder)) {
        Directory.CreateDirectory(_pathToFolder);
      }
    }

    private void CreateDirectory(string pathToFolder) {
      if (!Directory.Exists(pathToFolder)) {
        Directory.CreateDirectory(pathToFolder);
      }
    }
  }
}