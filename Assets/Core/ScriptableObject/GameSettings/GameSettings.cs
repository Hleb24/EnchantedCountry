using UnityEngine;

namespace Core.ScriptableObject.GameSettings {
  [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings")]
  public class GameSettings : UnityEngine.ScriptableObject {
    [SerializeField]
    private bool _startNewGame;

    public bool StartNewGame() {
      return _startNewGame;
    }
  }
}