using System;
using System.Threading.Tasks;
using Core.Support.SaveSystem.Saver;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Support.Attributes {
  public class PrefsSaver : ISaver {
    public async UniTaskVoid SaveAsync<T>(T data, string pathToFolder = "", string pathToFile = "", float timeOut = 5, Action<Exception> handler = null) {
      try {
        string json = JsonConvertProxy.Serialize(data);
        PlayerPrefs.SetString(pathToFile, json);
        PlayerPrefs.Save();
        await UniTask.Yield();
      } catch (Exception ex) {
        handler?.Invoke(ex);
      }
    }

    public void Save<T>(T data, string pathToFolder = "", string pathToFile = "", Action<Exception> handler = null) {
      try {
        string json = JsonConvertProxy.Serialize(data);
        PlayerPrefs.SetString(pathToFile, json);
        PlayerPrefs.Save();
      } catch (Exception ex) {
        handler?.Invoke(ex);
      }
    }

    public async UniTask<T> LoadAsync<T>(string pathToFolder = "", string pathToFile = "", float timeOut = 5, Action<Exception> handler = null) {
      T data = default;
      if (!PlayerPrefs.HasKey(pathToFile)) {
        return default;
      }

      string json = PlayerPrefs.GetString(pathToFile);
      try {
        data = await ReadAsync<T>(json);
      } catch (Exception ex) {
        handler?.Invoke(ex);
      }

      return data;
    }

    public T Load<T>(string pathToFolder = "", string pathToFile = "", Action<Exception> handler = null) {
      T data = default;
      if (!PlayerPrefs.HasKey(pathToFile)) {
        return default;
      }

      string json = PlayerPrefs.GetString(pathToFile);
      try {
        data = JsonConvertProxy.Deserialize<T>(json);
      } catch (Exception ex) {
        handler?.Invoke(ex);
      }

      return data;
    }

    public void DeleteSaves(string pathToFile = "") {
      PlayerPrefs.DeleteAll();
    }

    public void DeleteFile(string pathToFile = "") {
      PlayerPrefs.DeleteKey(pathToFile);
    }

    private async ValueTask<T> ReadAsync<T>(string json) {
      return await Task.Run(() => JsonConvertProxy.Deserialize<T>(json));
    }
  }
}