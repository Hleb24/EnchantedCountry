using EnchantedCountry.Utils.RemoteConfigsService;
using Unity.RemoteConfig;

namespace Core.Support.RemoteConfigsService {
  public class RemoteConfigsSettings {
    public static void InitializeDefaultData() {
      RemoteEntity<RemoteConnectionInfo>.Instance = JsonAssetConvertor.LoadResource<RemoteConnectionInfo>(ConfigConstants.Paths.RemoteConnectionInfo);
    }

    public static void ApplyRemoteSettings() {
      RemoteEntity<RemoteConnectionInfo>.Instance = JsonAssetConvertor.ConvertFromJson<RemoteConnectionInfo>(GetJsonByKey(ConfigConstants.Names.RemoteConnectionInfo));
    }

    private static string GetJsonByKey(string key) {
      return ConfigManager.appConfig.GetJson(key);
    }
  }
}