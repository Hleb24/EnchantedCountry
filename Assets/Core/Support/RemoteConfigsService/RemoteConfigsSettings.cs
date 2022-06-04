using EnchantedCountry.Utils.RemoteConfigsService;
using Unity.RemoteConfig;

namespace Core.Support.RemoteConfigsService {
  public class RemoteConfigsSettings : RemoteDTO<RemoteConfigsSettings>, IRemoteConfig {
    public static void InitializeDefaultData() {
      RemoteDTO<RemoteConnectionInfo>.Instance = JsonAssetConvertor.LoadResource<RemoteConnectionInfo>(ConfigConstants.Paths.RemoteConnectionInfo);
    }

    public static void ApplyRemoteSettings() {
      RemoteDTO<RemoteConnectionInfo>.Instance = JsonAssetConvertor.ConvertFromJson<RemoteConnectionInfo>(GetJsonByKey(ConfigConstants.Names.RemoteConnectionInfo));
    }

    private static string GetJsonByKey(string key) {
      return ConfigManager.appConfig.GetJson(key);
    }
  }
}