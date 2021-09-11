using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  public static class SaveSystem {
    public static void Save<T>(T type, string path) {
      string json = JsonUtility.ToJson(type, true);
      try {
        using var sw = new StreamWriter(Application.persistentDataPath + path, false, Encoding.Default);
        sw.Write(json);
      } catch (Exception e) {
        Debug.Log($"<color=red>{e.Message}</color>");
      }
    }

    public static bool Load<T>(T type, string path) {
      if (File.Exists(Application.persistentDataPath + path)) {
        try {
          using var sr = new StreamReader(Application.persistentDataPath + path);
          string json = sr.ReadToEnd();
          JsonUtility.FromJsonOverwrite(json, type);
          return true;
        } catch (Exception e) {
          Debug.Log($"<color=red>{e.Message}</color>");
          return false;
        }
      }

      Debug.Log($"The path <color=red>{Application.persistentDataPath + path}</color> not exists.");
      return false;
    }

    public static bool LoadWithInvoke<T>(T type, string path, Action<string, float> invoke, string methodName, float invokeTime) {
      bool loaded;
      if (File.Exists(Application.persistentDataPath + path)) {
        try {
          using var sr = new StreamReader(Application.persistentDataPath + path);
          string json = sr.ReadToEnd();
          Debug.Log($"Json saves: <color=green>{json}</color>");
          JsonUtility.FromJsonOverwrite(json, type);
          loaded = true;
        } catch (Exception e) {
          Debug.Log($"<color=red>{e.Message}</color>");
          loaded = false;
        }
      } else {
        loaded = false;
        Debug.Log($"The path <color=red>{Application.persistentDataPath + path}</color> not exists.");
      }

      if (loaded) {
        invoke?.Invoke(methodName, invokeTime);
      }

      return loaded;
    }

    public struct Constants {
      public const string DiceROllData = "/diceRole.json";
      public const string QualitiesAfterDistributing = "/qualitiesAfterDistributing.json";
      public const string ClassOfCharacter = "/classOfCharacter.json";
      public const string Wallet = "/wallet.json";
      public const string RiskPoints = "/riskPoints.json";
      public const string UsedEquipment = "/usedEquipment.json";
      public const string GamePoints = "/gamePoints.json";
      public const string GameSave = "/gameSave.json";
    }
  }
}