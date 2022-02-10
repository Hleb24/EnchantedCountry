using Aberrance.Extensions;
using UnityEngine;

namespace Core.Mono.GameSaveSystem
{
    public class Loader : MonoBehaviour {
        private static Loader _instance;

        public static Loader Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<Loader>();
                    if (_instance == null) {
                        _instance = new GameObject().AddComponent<Loader>();
                    }
                }

                return _instance;
            }
        }
        private void Awake() {
            if (_instance.NotNull()) Destroy(this);
            DontDestroyOnLoad(this);
            GSSSingleton.Instance.LoadData();
        }
       
    }
}
