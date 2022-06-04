namespace Core.Support.RemoteConfigsService {
  public class RemoteDTO<T> where T : IRemoteConfig, new() {
    public static T Instance { get; protected set; } = new();
  }
}

