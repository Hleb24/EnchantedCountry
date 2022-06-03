using System;
using Newtonsoft.Json;
using UnityEngine;

namespace EnchantedCountry.Utils.RemoteConfigsService {
  public static class JsonAssetConvertor {
    public static T LoadResource<T>(string dataPathResource) where T : class {
      var asset = Resources.Load<TextAsset>(dataPathResource);
      string json = asset.text;

      if (string.IsNullOrEmpty(json)) {
        return null;
      }

      var obj = ConvertFromJson<T>(json);
      return obj;
    }

    public static T ConvertFromJson<T>(string json) where T : class {
      T obj = null;

      try {
        if (typeof(T).IsSubclassOf(typeof(ScriptableObject))) {
          var scriptable = ScriptableObject.CreateInstance(typeof(T));
          JsonUtility.FromJsonOverwrite(json, scriptable);
          obj = scriptable as T;
        } else {
          obj = JsonConvert.DeserializeObject<T>(json);
        }
      } catch (Exception e) {
        Debug.LogError("Can't parse json " + e.Message);
      }

      return obj;
    }
  }
}