using Core.Mono.BaseClass;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.GameSettings {
  [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
  public class GameSettings : ScriptableObject, IGameSettings {
    [SerializeField]
    private bool _startNewGame;
    [SerializeField]
    private bool _useGameSave;
    [SerializeField]
    private Scene _targetScene;
    [FormerlySerializedAs("_startScene"), SerializeField]
    private Scene _newGameScene;

    public bool StartNewGame() {
      return _startNewGame;
    }

    public bool UseGameSave() {
      return _useGameSave;
    }

    public Scene GetTargetScene() {
      return _targetScene;
    }

    public Scene GetNewGameScene() {
      return _newGameScene;
    }
  }
}