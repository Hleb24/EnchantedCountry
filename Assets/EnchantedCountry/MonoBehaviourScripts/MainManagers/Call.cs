using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.MainManagers {
  public class Call : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeMethodLoad() {
      Leviathan.Instance.Call();
    }
  }
}