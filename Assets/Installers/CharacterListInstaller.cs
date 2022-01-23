using Core.Rule.Character.Levels;
using Core.Rule.Character.Qualities;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace Aberrance.Installers {
  public class CharacterListInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
      Container.Bind<IRiskPoints>().FromResolveGetter(Dealers.Resolve<IRiskPoints>()).AsSingle();
      Container.Bind<IEquipment>().FromResolveGetter(Dealers.Resolve<IEquipment>()).AsSingle();
      Container.Bind<IEquipmentUsed>().FromResolveGetter(Dealers.Resolve<IEquipmentUsed>()).AsSingle();
      Container.Bind<IGamePoints>().FromResolveGetter(Dealers.Resolve<IGamePoints>()).AsSingle();
      Container.Bind<IWallet>().FromResolveGetter(Dealers.Resolve<IWallet>()).AsSingle();
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<Levels>().AsSingle();
      Container.Bind<DefiningLevels>().AsSingle();
      Container.Bind<Qualities>().AsSingle();
    }
  }
}