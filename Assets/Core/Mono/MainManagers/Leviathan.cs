using System.Collections.Generic;
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
  public class Leviathan : MonoBehaviour, ILauncher {
    private static void SetDeviceSettings() {
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private IGameSettings _gameSettings;
    private Memento _memento;
    private ILoader _loader;

    private async void Awake() {
      await Launch();
    }

    private async UniTask Launch() {
      _loader.Load().Forget();
      await UniTask.WaitUntil(() => _loader.IsLoad);
      StartGame();
    }

    bool ILauncher.UseGameSave() {
      return _gameSettings.UseGameSave();
    }

    bool ILauncher.IsNewGame() {
      return IsNewGame;
    }

    bool ILauncher.StartNewGame() {
      return StartNewGame;
    }

    bool ILauncher.DataLoaded() {
      return DataLoaded;
    }

    bool ILauncher.StillInitializing() {
      return StillInitializing;
    }

    [Inject]
    public void Constructor(Memento memento, IGameSettings gameSettings, MementoLoader mementoLoader) {
      _memento = memento;
      _gameSettings = gameSettings;
      IReadOnlyList<ILoader> loaders = new List<ILoader> { mementoLoader };
      _loader = new LoaderComposite(loaders);
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

    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus && _gameSettings.MustBeSave(SavePoints.OnPause)) {
        Save();
      }
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (hasFocus) {
        return;
      }

      if (_gameSettings.MustBeSave(SavePoints.OnLoseFocus)) {
        Save();
      }
    }

    private void Save() {
      _memento.Save();
    }

    private void OnApplicationQuit() {
      if (_gameSettings.MustBeSave(SavePoints.OnQuit)) {
        _memento.SaveOnQuit();
      }
    }

    private bool StillInitializing { get; set; } = true;

    private bool DataLoaded { get; set; }

    private bool StartNewGame { get; set; }
    private bool IsNewGame { get; set; }
  }
}