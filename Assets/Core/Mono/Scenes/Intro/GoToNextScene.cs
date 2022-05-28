using Core.Mono.MainManagers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Core.Mono.BaseClass.Scene;

namespace Core.Mono.Scenes.Intro {
  /// <summary>
  ///   Класс для перехода на слудующую сцену со сцены <see cref="BaseClass.Scene.Intro" />
  /// </summary>
  public class GoToNextScene {
    private readonly ILauncher _launcher;
    private readonly StartSceneHolder _startSceneHolder;
    private Scene _nextScene;

    public GoToNextScene(ILauncher launcher, StartSceneHolder startSceneHolder) {
      _launcher = launcher;
      _startSceneHolder = startSceneHolder;
    }

    public void Go() {
      SetNameOfNextScene().Forget();
    }

    private async UniTaskVoid SetNameOfNextScene() {
      while (_launcher.StillInitializing()) {
        await UniTask.Yield();
      }

      Debug.LogWarning("Is new game " + _launcher.IsNewGame());
      _nextScene = _launcher.IsNewGame() ? _startSceneHolder.GetNewGameScene() : _startSceneHolder.GetTargetScene();
      LoadNextSceneAsync().Forget();
    }

    private async UniTaskVoid LoadNextSceneAsync() {
      await SceneManager.LoadSceneAsync((int)_nextScene);
    }
  }

  public class StartSceneHolder {
    private readonly Scene _targetScene;
    private readonly Scene _startScene;

    public StartSceneHolder(Scene targetScene, Scene startScene) {
      _targetScene = targetScene;
      _startScene = startScene;
    }

    public Scene GetTargetScene() {
      return _targetScene;
    }

    public Scene GetNewGameScene() {
      return _startScene;
    }
  }
}