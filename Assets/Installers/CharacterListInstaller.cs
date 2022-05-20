using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Character.Level;
using Core.Main.Character.Quality;
using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Point;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace EnchantedCountry.Installers {
  public static class DependenciesId {
    public const int EQUIPMENT_USED_CLONE = 100;
    public const string EQUIPMENT_USED_CLONE_WITH_TRACKING = "EquipmentUsedCloneWithTracking";
  }

  public class CharacterListInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
      Container.Bind<IRiskPoints>().FromResolveGetter(Dealers.Resolve<IRiskPoints>()).AsSingle();
      Container.Bind<IEquipment>().FromResolveGetter(Dealers.Resolve<IEquipment>()).AsSingle();
      Container.Bind<IEquipmentUsed>().FromResolveGetter(Dealers.Resolve<IEquipmentUsed>()).AsSingle();
      // Container.Bind<IEquipmentUsed>().FromResolveGetter(Dealers.Resolve<IEquipmentUsed>()).AsTransient();
      // Container.Bind<IEquipmentUsed>().WithId(IdBinding.Equipment).FromResolveGetter(Dealers.ResolveCloneWithTracking<IEquipmentUsed>()).AsTransient();
      Container.Bind<IGamePoints>().FromResolveGetter(Dealers.Resolve<IGamePoints>()).AsSingle();
      Container.Bind<IWallet>().FromResolveGetter(Dealers.Resolve<IWallet>()).AsSingle();
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<BaseLevel>().FromInstance(new BaseLevel(1));
      Container.Bind<CharacterLevel>().AsSingle();
      Container.Bind<Qualities>().AsSingle();
    }
  }
}