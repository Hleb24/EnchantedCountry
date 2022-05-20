using Newtonsoft.Json;

namespace Core.Support.SaveSystem.Saver {
  public static class JsonSaver {
    public static string Serialize<T>(T scrolls) {
      var formatting = Formatting.None;
#if UNITY_EDITOR
      formatting = Formatting.Indented;
#endif
      return JsonConvert.SerializeObject(scrolls, formatting);
    }

    public static T Deserialize<T>(string json) {
      return JsonConvert.DeserializeObject<T>(json);
    }
  }
}