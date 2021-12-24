using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.GameSettings {
  [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings")]
  public class GameSettings : UnityEngine.ScriptableObject {
    [SerializeField]
    private bool _isNewGame;

    public bool IsNewGame() {
      return _isNewGame;
    }
  }
}