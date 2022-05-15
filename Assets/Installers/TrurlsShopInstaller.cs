using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Dice;
using Core.Main.GameRule.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace EnchantedCountry.Installers {
  public class TrurlsShopInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<IWallet>().FromResolveGetter(Dealers.Resolve<IWallet>()).AsSingle();
      Container.Bind<IEquipment>().FromResolveGetter(Dealers.Resolve<IEquipment>()).AsSingle();
      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
      Container.Bind<DiceRollCalculator>().AsSingle();
    }
  }
}