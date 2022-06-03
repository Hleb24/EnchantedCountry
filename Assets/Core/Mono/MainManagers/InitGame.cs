using Core.Animations;
using Core.Mono.Scenes.Intro;
using UnityEngine;
using Zenject;

namespace Core.Mono.MainManagers {
  public class InitGame : MonoBehaviour {
    [SerializeField]
    private SceneLoaderAnimation _sceneLoader;
    private GoToNextScene _goToNextScene;

   
    private async void Start() {
      await _sceneLoader.StartTransitionAnimation();
      await _goToNextScene.GoAsync();
    }

    [Inject]
    public void Constructor(GoToNextScene goToNextScene) {
      _goToNextScene = goToNextScene;
    }
  }
}