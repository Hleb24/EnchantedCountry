namespace Core.Support.RemoteConfigsService {
  public class RemoteEntity<T> where T : IRemoteConfig, new() {
    public static T Instance { get; set; } = new();
  }
}