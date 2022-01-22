using Core.Mono.MainManagers;
using UnityEngine;
using Zenject;

namespace Aberrance {
  /// <summary>
  /// Класс для вызова через Zenject Левиафана.
  /// </summary>
  public class CallLeviathan : MonoBehaviour {
    //
    [Inject]
    private IStartGame _leviathan;
  }
}