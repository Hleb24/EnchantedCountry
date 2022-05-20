using Core.Mono.Scenes.Intro;
using Core.SO.GameSettings;
using Zenject;

namespace EnchantedCountry.Installers {
  public class IntroInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<StartSceneHolder>().FromResolveGetter<IGameSettings>(x => new StartSceneHolder(x.GetTargetScene(), x.GetNewGameScene())).AsCached();
      Container.Bind<GoToNextScene>().AsCached();
    }
  }
}