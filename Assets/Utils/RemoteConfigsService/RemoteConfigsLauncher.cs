using Core.Mono.MainManagers;
using Cysharp.Threading.Tasks;
using Unity.RemoteConfig;
using UnityEngine;

namespace EnchantedCountry.Utils.RemoteConfigsService {
  public class RemoteConfigsLauncher : ILoader {
    private const string PRODUCTION_ID = "85628059-328d-4580-9597-2f7e2895a403";
    private const string DEVELOPMENT_ID = "c7603307-2d47-4c50-9e91-d932c017c7f0";

    private bool _isReconnected;

    public async UniTaskVoid Load() {
#if PRODUCTION
      ConfigManager.SetEnvironmentID(PRODUCTION_ID);
#else
      ConfigManager.SetEnvironmentID( DEVELOPMENT_ID);
#endif

      ConfigManager.FetchCompleted += Response;
      ConfigManager.FetchConfigs(new UserAttributes(), new AppAttributes());
      await UniTask.Yield();
    }

    private void Response(ConfigResponse configResponse) {
      switch (configResponse.status) {
        case ConfigRequestStatus.None:
        case ConfigRequestStatus.Failed:
        case ConfigRequestStatus.Pending:
          OnRemoteConnectionFailure(configResponse);
          break;
        case ConfigRequestStatus.Success:
          ApplySettings(true);
          break;
        default:
          Notifier.LogWarning("Remote config status: " + configResponse.status + " is unavailable");
          ApplySettings();
          break;
      }
    }

    private void OnRemoteConnectionFailure(ConfigResponse configResponse) {
      Notifier.Log("Remote config response status:" + configResponse.status);
      Notifier.Log("Checking internet connection...");
      bool internetReachable = Application.internetReachability != NetworkReachability.NotReachable;
      if (internetReachable) {
        Notifier.Log("Connected");
        if (_isReconnected) {
          ApplySettings();
          return;
        }

        ConfigManager.FetchConfigs(new UserAttributes(), new AppAttributes());
        _isReconnected = true;
      } else {
        Debug.LogWarning("Failed to connect...");
        ApplySettings();
      }
    }

    private void ApplySettings(bool isConnected = false) {
      ConfigManager.FetchCompleted -= Response;
      IsLoad = true;

#if DISABLE_REMOTE_CONFIGS
      return;
#endif

      if (isConnected) {
        RemoteConfigsSettings.ApplyRemoteSettings();
      } else {
        RemoteConfigsSettings.InitializeDefaultData();
      }
    }

    public bool IsLoad { get; private set; }

    private struct UserAttributes { }

    private struct AppAttributes { }
  }
}