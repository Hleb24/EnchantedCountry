using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace EnchantedCountry.Installers {
  public class SelectClassInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
    }
  }
}