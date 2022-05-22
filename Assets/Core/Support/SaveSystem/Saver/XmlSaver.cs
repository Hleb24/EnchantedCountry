using System;
using System.IO;
using System.Xml.Serialization;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public class XmlSaver : ISaver {
    public async UniTaskVoid SaveAsync<T>(T data, string pathToFolder, string pathToFile, float timeOut = 5, Action<Exception> handler = null) {
      CreateDirectory(pathToFolder);
      await using var streamWriter = new StreamWriter(pathToFile);
      var serializer = new XmlSerializer(typeof(T));
      try {
        serializer.Serialize(streamWriter, data);
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamWriter.Close();
      }
    }

    public void Save<T>(T data, string pathToFolder, string pathToFile, Action<Exception> handler = null) {
      CreateDirectory(pathToFolder);
      using var streamWriter = new StreamWriter(pathToFile);
      var serializer = new XmlSerializer(typeof(T));
      try {
        serializer.Serialize(streamWriter, data);
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamWriter.Close();
      }
    }

    public async UniTask<T> LoadAsync<T>(string pathToFolder, string pathToFile, float timeOut = 5, Action<Exception> handler = null) {
      if (!File.Exists(pathToFile)) {
        CreateDirectory(pathToFolder);
        return default;
      }

      await using var fileStream = new FileStream(pathToFile, FileMode.Open);
      var serializer = new XmlSerializer(typeof(T));
      try {
        var save = (T)serializer.Deserialize(fileStream);
        return save;
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        fileStream.Close();
      }

      return default;
    }

    public T Load<T>(string pathToFolder, string pathToFile, Action<Exception> handler = null) {
      if (!File.Exists(pathToFile)) {
        CreateDirectory(pathToFolder);
        return default;
      }

      using var fileStream = new FileStream(pathToFile, FileMode.Open);
      var serializer = new XmlSerializer(typeof(T));
      try {
        var save = (T)serializer.Deserialize(fileStream);
        return save;
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        fileStream.Close();
      }

      return default;
    }

    public void DeleteSaves(string pathToFile) {
      if (File.Exists(pathToFile)) {
        File.Delete(pathToFile);
      }

      ClearPrefs();
    }

    public void DeleteFile(string pathToFile = "") {
      if (File.Exists(pathToFile)) {
        File.Delete(pathToFile);
      }
    }

    private void CreateDirectory(string pathToFolder) {
      if (!Directory.Exists(pathToFolder)) {
        Directory.CreateDirectory(pathToFolder);
      }
    }

    private void ClearPrefs() {
      PlayerPrefs.DeleteAll();
    }
  }
}