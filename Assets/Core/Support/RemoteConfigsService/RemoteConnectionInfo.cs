using System;
using JetBrains.Annotations;

namespace Core.Support.RemoteConfigsService {
  [Serializable]
  public class RemoteConnectionInfo : IRemoteConfig {
    public string ConnectionMessage { get; init; } = "script";

    [UsedImplicitly]
    public RemoteConnectionInfo() { }
  }
}