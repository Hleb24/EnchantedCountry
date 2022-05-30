using Core.Animations;
using Core.Mono.Scenes.Intro;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  public class InitGame : MonoBehaviour {
    [SerializeField]
    private LevelLoaderAnimation _levelLoader;

    private GoToNextScene _goToNextScene;

    private async void Start() {
      await _levelLoader.StartTransitionAnimation().ContinueWith(() => _goToNextScene.GoAsync());
    }

    [Inject]
    public void Constructor(GoToNextScene goToNextScene) {
      _goToNextScene = goToNextScene;
    }
  }
}