using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.SupportSystems.SaveSystem.SaveManagers;
using Core.SupportSystems.SaveSystem.Saver;
using UnityEngine;

namespace Core.SupportSystems.Attributes {
  /// <summary>
  /// Класс для проверки работы атрибутов требующих точку входа.
  /// </summary>
  public class Auditor {
    /// <summary>
    /// Проверка работу атрибутов.
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
      private const string NewGame = nameof(NewGame);

      public void Save(Scrolls scrolls) {
        string json = JsonUtility.ToJson(scrolls);
        PlayerPrefs.SetString(NewGame, json);
        PlayerPrefs.Save();
      }

      public Scrolls Load(out bool isNewGame) {
        if (IsNewGame()) {
          isNewGame = true;
          return NewSave();
        }

        string json = PlayerPrefs.GetString(NewGame);
        isNewGame = false;
        return JsonUtility.FromJson<Scrolls>(json);
      }

      private bool IsNewGame() {
        return !IsSaveExists;
      }

      private Scrolls NewSave() {
        PlayerPrefs.DeleteAll();
        Scrolls scrolls = new Scrolls().NewScrollGame();
        return scrolls;
      }

      private bool IsSaveExists {
        get {
          return PlayerPrefs.HasKey(NewGame);
        }
      }
    }
  }
}