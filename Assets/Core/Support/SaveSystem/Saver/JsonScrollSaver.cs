using System;
using System.IO;
using Aberrance.Extensions;
using Core.Support.SaveSystem.SaveManagers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public class JsonScrollSaver : ISaver {
    private readonly string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Save"), "Save.json");
    private readonly string _pathToFolder = Path.Combine(Application.persistentDataPath, "Save");

    public async UniTaskVoid Save(Scrolls scrolls, Action<Exception> handler) {
      CreateDirectory(_pathToFolder);
      await using var streamWriter = new StreamWriter(_pathToFile);
      try {
        string jsonSave = JsonSaver.Serialize(scrolls);
        await streamWriter.WriteAsync(jsonSave);
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamWriter.Close();
      }
    }

    public async UniTask<Scrolls> Load(Action<Exception> handler) {
      var jsonSave = string.Empty;
      if (File.Exists(_pathToFile).IsFalse()) {
        ClearPrefs();
        CreateDirectory(_pathToFolder);
        var newScrolls = new Scrolls();
        newScrolls.NewScrollGame();
        return newScrolls;
      }

      using var streamReader = new StreamReader(_pathToFile);
      try {
        jsonSave = await streamReader.ReadToEndAsync();
        var save = JsonSaver.Deserialize<Scrolls>(jsonSave);
        return save;
      } catch (Exception e) {
        Debug.Log("Новая игра началась " + e.Data);
        ClearPrefs();
        CreateDirectory(_pathToFolder);
        if (!string.IsNullOrEmpty(jsonSave)) {
          throw;
        }

        var newScrolls = new Scrolls();
        newScrolls.NewScrollGame();
        return newScrolls;
      } finally {
        Debug.LogWarning("Close stream");
        streamReader.Close();
      }
    }

    public Scrolls Load() {
      var jsonSave = string.Empty;
      if (File.Exists(_pathToFile).IsFalse()) {
        ClearPrefs();
        CreateDirectory(_pathToFolder);
        var newScrolls = new Scrolls();
        newScrolls.NewScrollGame();
        return newScrolls;
      }

      using var streamReader = new StreamReader(_pathToFile);
      try {
        jsonSave = streamReader.ReadToEnd();
        var save = JsonSaver.Deserialize<Scrolls>(jsonSave);
        // if (save.DiceRollDataScroll.DiceRollValues == null) {
        // Debug.LogWarning("Null in save serialize");
        // }
        return save;
      } catch (Exception e) {
        Debug.Log("Новая игра началась " + e.Data);
        ClearPrefs();
        CreateDirectory(_pathToFolder);
        if (!string.IsNullOrEmpty(jsonSave)) {
          throw;
        }

        var newScrolls = new Scrolls();
        newScrolls.NewScrollGame();
        return newScrolls;
      } finally {
        streamReader.Close();
      }
    }

    public void DeleteSave() {
      if (File.Exists(_pathToFile)) {
        File.Delete(_pathToFile);
      }

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

    private void CreateDirectory(string pathToFolder) {
      if (!Directory.Exists(pathToFolder)) {
        Directory.CreateDirectory(pathToFolder);
      }
    }
  }
}