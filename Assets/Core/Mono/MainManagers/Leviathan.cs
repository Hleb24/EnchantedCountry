using Aberrance.Extensions;
using Core.SO.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  /// <summary>
  ///   Класс отвечает за начальный запуск приложения.
  /// </summary>
  public class Leviathan : MonoBehaviour, IStartGame {
    private static void SetDeviceSettings() {
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private IGameSettings _gameSettings;
    private Memento _memento;

    private void Awake() {
      StartGame();
    }

    bool IStartGame.UseGameSave() {
      return _gameSettings.UseGameSave();
    }

    bool IStartGame.IsNewGame() {
      return IsNewGame;
    }

    bool IStartGame.StartNewGame() {
      return StartNewGame;
    }

    bool IStartGame.DataLoaded() {
      return DataLoaded;
    }

    bool IStartGame.StillInitializing() {
      return StillInitializing;
    }

    [Inject]
    public void Constructor(Memento memento, IGameSettings gameSettings) {
      _memento = memento;
      _gameSettings = gameSettings;
    }

    private void StartGame() {
      SetStartGameProperties().Forget();
    }

    private async UniTaskVoid SetStartGameProperties() {
      StartNewGame = _gameSettings.StartNewGame();
      while (_memento.IsNewGame.HasValue.IsFalse()) {
        await UniTask.Yield();
      }

      if (_memento.IsNewGame.HasValue) {
        IsNewGame = _memento.IsNewGame.Value;
      }

      StillInitializing = false;
      DataLoaded = true;
      SetDeviceSettings();
    }

    private void OnApplicationQuit() {
      _memento.SaveOnQuit();
    }

    private bool StillInitializing { get; set; } = true;

    private bool DataLoaded { get; set; }

    private bool StartNewGame { get; set; }
    private bool IsNewGame { get; set; }
  }
}