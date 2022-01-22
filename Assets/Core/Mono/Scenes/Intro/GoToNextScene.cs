using Core.Mono.MainManagers;
using UnityEngine.SceneManagement;
using Zenject;
using Scene = Core.Mono.BaseClass.Scene;

namespace Core.Mono.Scenes.Intro {
  /// <summary>
  ///   Класс для перехода на слудующую сцену со сцены <see cref="BaseClass.Scene.Intro" />
  /// </summary>
  public class GoToNextScene {
    private readonly IStartGame _startGame;
    private readonly StartSceneHolder _startSceneHolder;
    private Scene _nextScene;

    
    public GoToNextScene(IStartGame startGame, StartSceneHolder startSceneHolder) {
      _startGame = startGame;
      _startSceneHolder = startSceneHolder;
    }

    public void Go() {
      SetNameOfNextScene();
      LoadNextSceneAsync();
    }

    private void SetNameOfNextScene() {
      _nextScene = _startGame.IsNewGame() ? _startSceneHolder.GetStartScene() : _startSceneHolder.GetTargetScene();
    }

    private void LoadNextSceneAsync() {
      SceneManager.LoadSceneAsync((int)_nextScene);
    }
  }

  public class StartSceneHolder{
    private readonly Scene _targetScene;
    private readonly Scene _startScene;
    public StartSceneHolder(Scene targetScene, Scene startScene) {
      _targetScene = targetScene;
      _startScene = startScene;
    }
    
    public Scene GetTargetScene() {
      return _targetScene;
    }
    
    public Scene GetStartScene() {
      return _startScene;
    }
  }
}