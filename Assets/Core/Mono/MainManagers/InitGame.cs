using Core.Mono.Scenes.Intro;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  public class InitGame : MonoBehaviour {
    private GoToNextScene _goToNextScene;

    private void Start() {
      _goToNextScene.Go();
    }

    [Inject]
    public void Constructor(GoToNextScene goToNextScene) {
      _goToNextScene = goToNextScene;
    }
  }
}