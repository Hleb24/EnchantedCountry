using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Saver;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Support.Attributes {
  /// <summary>
  ///   Класс для проверки работы атрибутов требующих точку входа.
  /// </summary>
  public class Auditor {
    /// <summary>
    ///   Проверка работу атрибутов.
    /// </summary>
    public static void AttributeValidation() {
      ValidatePrefsKeysCheck();
    }

    private static void ValidatePrefsKeysCheck() {
      var names = new List<string>();
      Type[] types = Assembly.GetExecutingAssembly().GetTypes();

      foreach (Type type in types) {
        IEnumerable<FieldInfo> methodInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
          .Where(f => f.GetCustomAttributes().Any(a => a.GetType() == typeof(PrefsKeysAttribute)));
        foreach (FieldInfo fieldInfo in methodInfos) {
          var attribute = fieldInfo.GetCustomAttribute(typeof(PrefsKeysAttribute)) as PrefsKeysAttribute;
          if (attribute == null) {
            continue;
          }

          attribute.PrefsKey = (string)fieldInfo.GetValue(null);
          if (names.Contains(attribute.PrefsKey)) {
            Debug.LogError($"Повторения ключа в префс: ключ = {attribute.PrefsKey}, константа = {fieldInfo.Name}, класс = {type.Name}.");
            continue;
          }

          names.Add(attribute.PrefsKey);
        }
      }
    }

    public class PrefsSaver : ISaver {
      [PrefsKeys]
      private const string NEW_GAME = nameof(NEW_GAME);

      public async UniTaskVoid SaveAsync(Scrolls scrolls, Action<Exception> handler) {
        try {
          string json = JsonSaver.Serialize(scrolls);
          PlayerPrefs.SetString(NEW_GAME, json);
          PlayerPrefs.Save();
          await UniTask.Yield();
        } catch (Exception ex) {
          handler?.Invoke(ex);
        }
      }

      public async UniTask<Scrolls> LoadAsync(Action<Exception> handler = null) {
        Scrolls scrolls = null;
        if (IsNewGame()) {
          scrolls = NewSave();
          return scrolls;
        }

        string json = PlayerPrefs.GetString(NEW_GAME);
        try {
          scrolls = await ReadAsync(json);
        } catch (Exception ex) {
          handler?.Invoke(ex);
        }

        return scrolls;
      }

      public Scrolls Load() {
        Scrolls scrolls = null;
        if (IsNewGame()) {
          scrolls = NewSave();
          return scrolls;
        }

        string json = PlayerPrefs.GetString(NEW_GAME);
        scrolls = JsonSaver.Deserialize<Scrolls>(json);

        return scrolls;
      }

      public void DeleteSave() {
        PlayerPrefs.DeleteAll();
      }

      private async ValueTask<Scrolls> ReadAsync(string json) {
        return await Task.Run(() => JsonSaver.Deserialize<Scrolls>(json));
      }

      private bool IsNewGame() {
        return !IsSaveExists;
      }

      private Scrolls NewSave() {
        PlayerPrefs.DeleteAll();
        var scrolls = new Scrolls();
        scrolls.NewScrollGame();
        return scrolls;
      }

      private bool IsSaveExists {
        get {
          return PlayerPrefs.HasKey(NEW_GAME);
        }
      }
    }
  }
}