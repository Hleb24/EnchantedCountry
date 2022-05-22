﻿using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Support.SaveSystem.Saver {
  public class JsonSaver : ISaver {
    private const string TIME_OUT_MESSAGE = "Time out on load async!";

    public async UniTaskVoid SaveAsync<T>(T data, string pathToFolder, string pathToFile, float timeOut = 5, Action<Exception> handler = null) {
      CreateDirectory(pathToFolder);
      await using var streamWriter = new StreamWriter(pathToFile);
      try {
        string jsonSave = JsonConvertProxy.Serialize(data);
        bool timeout = await streamWriter.WriteAsync(jsonSave).AsUniTask().TimeoutWithoutException(TimeSpan.FromSeconds(timeOut));
        if (timeout) {
          handler?.Invoke(new TimeoutException(TIME_OUT_MESSAGE));
        }
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamWriter.Close();
      }
    }

    public void Save<T>(T data, string pathToFolder, string pathToFile, Action<Exception> handler = null) {
      CreateDirectory(pathToFolder);
      using var streamWriter = new StreamWriter(pathToFile);
      try {
        string jsonSave = JsonConvertProxy.Serialize(data);
        streamWriter.WriteLine(jsonSave);
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

      using var streamReader = new StreamReader(pathToFile);
      try {
        (bool isTimeOut, string json) = await streamReader.ReadToEndAsync().AsUniTask().TimeoutWithoutException(TimeSpan.FromSeconds(timeOut));
        if (isTimeOut) {
          handler?.Invoke(new TimeoutException(TIME_OUT_MESSAGE));
          return default;
        }

        var save = JsonConvertProxy.Deserialize<T>(json);
        return save;
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamReader.Close();
      }

      return default;
    }

    public T Load<T>(string pathToFolder, string pathToFile, Action<Exception> handler = null) {
      if (!File.Exists(pathToFile)) {
        CreateDirectory(pathToFolder);
        return default;
      }

      using var streamReader = new StreamReader(pathToFile);
      try {
        string json = streamReader.ReadToEnd();
        var save = JsonConvertProxy.Deserialize<T>(json);
        return save;
      } catch (Exception e) {
        handler?.Invoke(e);
      } finally {
        streamReader.Close();
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