using Core.ScriptableObject.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  /// <summary>
  ///   Интерфейс для получения данных о начале игры.
  /// </summary>
  public interface IStartGame {
    /// <summary>
    ///   Это новая игра.
    /// </summary>
    /// <returns>Истина - новая игра, ложь - продолжение с сохранений.</returns>
    public bool IsNewGame();

    /// <summary>
    ///   Начать новую игру.
    /// </summary>
    /// <returns>Истина - начать новую игру, ложь - продолжить с сохранений.</returns>
    public bool StartNewGame();

    /// <summary>
    ///   Использовать сохранения.
    /// </summary>
    /// <returns>Истина - использовать сохраненияч, ложь - не использовать.</returns>
    public bool UseGameSave();

    /// <summary>
    ///   Данные загруженый для игры.
    /// </summary>
    /// <returns>Истина - данные загружены, ложь - даные ещё загружаються</returns>
    public bool DataLoaded();

    /// <summary>
    ///   Данные игры ещё инициализируются.
    /// </summary>
    /// <returns>Истина - данные инициализируются, ложь - данные уже инициализированы.</returns>
    public bool StillInitializing();
  }

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

    private void OnDestroy() {
      Save();
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
      SetStartGameProperties();
      SetDeviceSettings();
    }

    private void SetStartGameProperties() {
      StartNewGame = _gameSettings.StartNewGame();
      IsNewGame = _memento.IsNewGame;
      StillInitializing = false;
      DataLoaded = true;
    }

    private void Save() {
      _memento.Save();
    }

    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus) {
        Save();
      }
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (hasFocus) {
        return;
      }

      Save();
    }

    private void OnApplicationQuit() {
      _memento.Save();
    }

    private bool StillInitializing { get; set; } = true;

    private bool DataLoaded { get; set; }

    private bool StartNewGame { get; set; }
    private bool IsNewGame { get; set; }
  }
}