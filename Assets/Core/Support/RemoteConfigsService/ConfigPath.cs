namespace Core.Support.RemoteConfigsService {
  public sealed class ConfigPath {
    private const string LOCAL_FOLDER = "RemoteConfigs/";
    public readonly string RemoteConnectionInfo = LOCAL_FOLDER + "RemoteConnectionInfo";
  }
}