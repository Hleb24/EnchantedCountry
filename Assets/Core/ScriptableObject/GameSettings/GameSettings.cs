using UnityEngine;

namespace Core.ScriptableObject.GameSettings {
  [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings")]
  public class GameSettings : UnityEngine.ScriptableObject {
    [SerializeField]
    private bool _startNewGame;
    [SerializeField]
    private bool  _useGameSave;
    
    public bool StartNewGame() {
      return _startNewGame;
    }

    public bool UseGameSave() {
      return _useGameSave;
    }
  }
}