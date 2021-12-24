using System;
using Core.EnchantedCountry.SupportSystems.Attributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core {
  /// <summary>
  ///   Класс отвечает за начальный запуск приложения.
  /// </summary>
  public class Leviathan : MonoBehaviour {
    private static Leviathan _instance;

    /// <summary>
    ///   Получить экземпляр класса.
    /// </summary>
    public static Leviathan Instance {
      get {
        return _instance != null ? _instance : new GameObject(nameof(Leviathan)).AddComponent<Leviathan>();
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
      IsNewGame = _gameSettings.IsNewGame();
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void OnDestroy() {
      Save();
    }

    public void Call() {
      Debug.Log("Вызвать Левиафана.");
    }

    private void Save() {
      _memento.Save();
    }

    private void InitMembers() {
      _memento = new Memento();
      _memento.Init();
      _gameSettings = Resources.Load<GameSettings>(GameSettings);
      Assert.IsNotNull(_gameSettings);
    }

    private void InitGameObject() {
      if (Instance != null) {
        Destroy(gameObject);
      }

      DontDestroyOnLoad(gameObject);
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

    public bool IsNewGame { get; private set; }
  }
}