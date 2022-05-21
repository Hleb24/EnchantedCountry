using Core.Mono.Scenes.Intro;
using Core.SO.GameSettings;
using Zenject;

namespace EnchantedCountry.Installers {
  public class IntroInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<StartSceneHolder>().FromResolveGetter<IGameSettings>(CreateStartSceneHolder).AsCached();
      Container.Bind<GoToNextScene>().AsCached();
    }

    private StartSceneHolder CreateStartSceneHolder(IGameSettings gameSettings) {
      return new StartSceneHolder(gameSettings.GetTargetScene(), gameSettings.GetNewGameScene());
    }
  }
}