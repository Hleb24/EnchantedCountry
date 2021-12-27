using Core.ScriptableObject.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.Assertions;

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
    private static Leviathan _instance;

    /// <summary>
    ///   Получить экземпляр класса.
    /// </summary>
    public static Leviathan Instance {
      get {
        if (_instance != null) {
          return _instance;
        }

        _instance = FindObjectOfType<Leviathan>();
        _instance ??= new GameObject(nameof(Leviathan)).AddComponent<Leviathan>();

        return _instance;
      }
    }

    private const string GameSettings = "GameSettings";
    [SerializeField]
    private GameSettings _gameSettings;

    private Memento _memento;

    private void Awake() {
      InitGameObject();
      InitMembers();
    }

    private void Start() {
      StartGame();
    }

    private void OnDestroy() {
      Save();
    }

    public bool UseGameSave() {
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

    public void Call() {
      Debug.Log("Вызвать Левиафана.");
    }

    private void Save() {
      _memento.Save();
    }

    private void InitGameObject() {
      if (_instance != null) {
        Destroy(this);
      }

      DontDestroyOnLoad(this);
    }

    private void InitMembers() {
      _memento = new Memento();
      _gameSettings = Resources.Load<GameSettings>(GameSettings);
      Assert.IsNotNull(_gameSettings);
    }

    private void StartGame() {
      StartNewGame = _gameSettings.StartNewGame();
      if (StartNewGame) {
        _memento.DeleteSave();
      }

      _memento.Init(out bool isNewGame);
      IsNewGame = isNewGame;
      StillInitializing = false;

      DataLoaded = true;
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
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