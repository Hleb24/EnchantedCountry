using System;
using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.SO.GameSettings {
  [Flags]
  public enum SavePoints {
    None = 0,
    OnPause = 1,
    OnLoseFocus = 2,
    OnQuit = 1 << 2
  }
  
  [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
  public class GameSettings : ScriptableObject, IGameSettings {
    [SerializeField]
    private bool _startNewGame;
    [SerializeField]
    private bool _useGameSave;
    [SerializeField]
    private Scene _targetScene;
    [SerializeField]
    private Scene _newGameScene;
    [SerializeField]
    private SavePoints _savePoints;

    public bool MustBeSave(SavePoints savePoint) {
      return (_savePoints & savePoint) == savePoint;
    }

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