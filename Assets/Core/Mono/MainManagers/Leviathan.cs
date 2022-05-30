using System;
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
    private IMemento _memento;
    private ILoader _loader;

    private async void Awake() {
      SetDeviceSettings();
      UniTaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
      await Launch();
    }

    private void OnDestroy() {
      UniTaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
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
    public void Constructor(IMemento memento, IGameSettings gameSettings, ILoader loader) {
      _memento = memento;
      _gameSettings = gameSettings;
      _loader = loader;
    }

    private void OnUnobservedTaskException(Exception exception) {
      Notifier.LogError($"Exception: {exception.Message}. Source: {exception.Source}. Trace: {exception.Data} ");
    }

    private async UniTask Launch() {
      _loader.Load().Forget();
      await UniTask.WaitUntil(() => _loader.IsLoad);
      StartGame();
    }

    private void StartGame() {
      SetStartGameProperties().Forget();
    }

    private async UniTaskVoid SetStartGameProperties() {
      StartNewGame = _gameSettings.StartNewGame();
      await UniTask.WaitUntil(() => _memento.IsNewGame.HasValue);

      if (_memento.IsNewGame.HasValue) {
        IsNewGame = _memento.IsNewGame.Value;
      }

      StillInitializing = false;
      DataLoaded = true;
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