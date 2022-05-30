using Core.Animations;
using UnityEngine;

namespace Core.Mono.BaseClass {
  public class GoToSceneMediator : MonoBehaviour {
    [SerializeField]
    private GoToScene[] _goToScenes;
    [SerializeField]
    private SceneLoaderAnimation _sceneLoaderAnimation;

    private void OnEnable() {
      AddPreloadSceneAction();
    }

    private void OnDisable() {
      RemovePreloadSceneAction();
    }

    private void AddPreloadSceneAction() {
      for (var i = 0; i < _goToScenes.Length; i++) {
        _goToScenes[i].PreloadSceneAction += _sceneLoaderAnimation.StartTransitionAnimation;
      }
    }

    private void RemovePreloadSceneAction() {
      for (var i = 0; i < _goToScenes.Length; i++) {
        _goToScenes[i].PreloadSceneAction -= _sceneLoaderAnimation.StartTransitionAnimation;
      }
    }
  }
}