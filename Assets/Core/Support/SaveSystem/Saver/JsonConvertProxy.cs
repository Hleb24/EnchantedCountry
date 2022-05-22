using Newtonsoft.Json;

namespace Core.Support.SaveSystem.Saver {
  public static class JsonConvertProxy {
    public static string Serialize<T>(T data) {
      var formatting = Formatting.None;
#if UNITY_EDITOR
      formatting = Formatting.Indented;
#endif
      return JsonConvert.SerializeObject(data, formatting);
    }

    public static T Deserialize<T>(string json) {
      return JsonConvert.DeserializeObject<T>(json);
    }
  }
}