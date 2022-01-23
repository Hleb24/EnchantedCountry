using Core.Rule.Dice;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace Aberrance.Installers {
  public class DiceRollsInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<DiceRollCalculator>().AsSingle();
      Container.Bind<IDiceRoll>().FromResolveGetter(Dealers.Resolve<IDiceRoll>());
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>());
    }
  }
}