namespace Core.Support.RemoteConfigsService {
  public class RemoteEntity<T> where T : IRemoteConfig, new() {
    public static T Instance { get; internal set; } = new();
  }
}

namespace System.Runtime.CompilerServices {
  internal static class IsExternalInit { }
}