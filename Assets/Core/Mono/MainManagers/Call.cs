using UnityEngine;

namespace Core.Mono.MainManagers {
  public class Call : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeMethodLoad() {
      // Leviathan.Instance.Call();
    }
  }
}