using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.SO.GameSettings {
  [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
  public class GameSettings : ScriptableObject, IGameSettings {
    [SerializeField]
    private bool _startNewGame;
    [SerializeField]
    private bool _useGameSave;
    [SerializeField]
    private Scene _targetScene;
    [SerializeField]
    private Scene _startScene;

    public bool StartNewGame() {
      return _startNewGame;
    }

    public bool UseGameSave() {
      return _useGameSave;
    }

    public Scene GetTargetScene() {
      return _targetScene;
    }

    public Scene GetStartScene() {
      return _startScene;
    }
  }
}