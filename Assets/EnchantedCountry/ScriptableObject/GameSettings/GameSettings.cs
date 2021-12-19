using UnityEngine;

namespace Core {
  [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings")]
  public class GameSettings : ScriptableObject {
    [SerializeField]
    private bool _isNewGame;

    public bool IsNewGame() {
      return _isNewGame;
    }
  }
}