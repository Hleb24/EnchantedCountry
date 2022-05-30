using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  /// <summary>
  ///   The class to call via Zenject <see cref="ILauncher"/>.
  /// </summary>
  public class LauncherCaller : MonoBehaviour {
    [Inject, UsedImplicitly]
    private ILauncher _leviathan;
  }
}