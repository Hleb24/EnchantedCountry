using UnityEngine;

namespace Core {
  public class Call : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeMethodLoad() {
      Leviathan.Instance.Call();
    }
  }
}