using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.Intro {
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