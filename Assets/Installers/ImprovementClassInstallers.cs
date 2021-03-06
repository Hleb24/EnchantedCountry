using Core.Main.Character.Quality;
using Core.Mono.Scenes.QualitiesImprovement;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace EnchantedCountry.Installers {
  public class ImprovementClassInstallers : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<QualityIncrease>().AsSingle();
    }
  }
}