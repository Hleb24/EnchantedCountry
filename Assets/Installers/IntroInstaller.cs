using Core.Mono.Scenes.Intro;
using Core.ScriptableObject.GameSettings;
using Zenject;

namespace Aberrance.Installers {
  public class IntroInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<StartSceneHolder>().FromResolveGetter<IGameSettings>(x => new StartSceneHolder(x.GetTargetScene(), x.GetStartScene())).AsCached();
      Container.Bind<GoToNextScene>().AsCached();
    }
  }
}