using UnityEngine;

namespace Core {
  public class InitializationManager : MonoBehaviour {
    private Memento _memento;

    private void Awake() {
      DontDestroyOnLoad(gameObject);
      _memento = new Memento();
    }

    private void Start() {
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
      _memento.Init();
    }
    
    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus) {
        _memento.Save();
      }
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (!hasFocus) {
        _memento.Save();
      }
    }

    private void OnApplicationQuit() {
      _memento.Save();
    }
  }
}