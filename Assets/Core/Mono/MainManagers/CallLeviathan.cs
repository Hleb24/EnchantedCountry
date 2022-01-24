using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  /// <summary>
  /// Класс для вызова через Zenject Левиафана.
  /// </summary>
  public class CallLeviathan : MonoBehaviour {
    //
    [Inject]
    private IStartGame _leviathan;
  }
}