using Core.Mono.MainManagers;
using Unity.RemoteConfig;

namespace EnchantedCountry.Utils.RemoteConfigsService {
  public class RemoteConfigsSettings {
    public static bool RemoteConfigsLoaded;

    public static void InitializeDefaultData() {
      //TODO GloryRoadRewardsConfig = JsonAssetConvertor.LoadResource<GloryRoadRewardsConfig>(ConfigConstants.ConfigPaths.GLORY_ROAD_REWARDS_CONFIG);
    }

    public static void ApplyRemoteSettings() {
      //TODO var gloryRoadRewardsConfig = JsonAssetConvertor.ConvertFromJson<GloryRoadRewardsConfig>(GetJsonByKey(ConfigConstants.ConfigNames.GLORY_ROAD_REWARDS_NAME));
      //TODO int coinsModifier = ConfigManager.appConfig.GetInt(ConfigConstants.ConfigNames.COINS_MODIFIER_NAME);

      RemoteConfigsLoaded = true;
      Notifier.Log("New settings loaded this session; update values accordingly.");
    }

    private static string GetJsonByKey(string key) {
      return ConfigManager.appConfig.GetJson(key);
    }
  }
}