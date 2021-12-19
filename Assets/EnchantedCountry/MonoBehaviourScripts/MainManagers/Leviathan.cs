using UnityEngine;

namespace Core {
  /// <summary>
  ///   Класс отвечает за начальный запуск приложения.
  /// </summary>
  public class Leviathan : MonoBehaviour {
    /// <summary>
    ///   Получить экземпляр класса.
    /// </summary>
    public static Leviathan Instance { get; } = new GameObject(nameof(Leviathan)).AddComponent<Leviathan>();

    public static bool IsNewGame { get; private set; }

    [SerializeField]
    private bool _isNewGame;
    private Memento _memento;
    [SerializeField]
    private GameSettings _settings;
    

    private void Awake() {
      if (Instance!= null) {
        Destroy(gameObject);
      }

      DontDestroyOnLoad(gameObject);
      
      _memento = new Memento();
      _settings = Resources.Load<GameSettings>("Settings");
    }

    private void Start() {
      _memento.Init();
      IsNewGame = _settings.IsNewGame();
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void OnDestroy() {
      Debug.LogWarning("Save");

      _memento.Save();
    }

    public void Call() {
      Debug.Log("Вызвать Левиафана.");
    }

    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus) {
        _memento.Save();
      }
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (!hasFocus) {
        Debug.LogWarning("Save");

        _memento.Save();
      }
    }

    private void OnApplicationQuit() {
      Debug.LogWarning("Save");

      _memento.Save();
    }
  }
}