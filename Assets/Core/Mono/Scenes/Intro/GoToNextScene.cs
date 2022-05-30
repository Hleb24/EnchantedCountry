using Core.Mono.MainManagers;
using Cysharp.Threading.Tasks;
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

    public async UniTask GoAsync() {
      await SetNameOfNextScene();
    }

    private async UniTask SetNameOfNextScene() {
      await UniTask.WaitUntil(() => _launcher.DataLoaded());
      _nextScene = _launcher.IsNewGame() ? _startSceneHolder.GetNewGameScene() : _startSceneHolder.GetTargetScene();
      LoadNextSceneAsync().Forget();
    }

    private async UniTask LoadNextSceneAsync() {
      await SceneManager.LoadSceneAsync((int)_nextScene);
    }
  }
}