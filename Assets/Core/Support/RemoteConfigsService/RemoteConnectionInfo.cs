using System;
using JetBrains.Annotations;

namespace Core.Support.RemoteConfigsService {
  [Serializable]
  public record RemoteConnectionInfo : IRemoteConfig {
    public string ConnectionMessage { get; init; } = "script";

    [UsedImplicitly]
    public RemoteConnectionInfo() { }
  }
}