using Core.Main.Character;
using Core.Main.Dice;
using Core.Support.Data.ClassType;
using Core.Support.Data.DiceRoll;
using Core.Support.Data.Equipment;
using Core.Support.Data.GamePonts;
using Core.Support.Data.RiskPoints;
using Core.Support.Data.Wallet;
using Core.Support.SaveSystem.SaveManagers;
using Zenject;

namespace Aberrance {
  public class GameInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<ClassTypeDataScroll>().AsSingle();
      Container.Bind<WalletDataScroll>().AsSingle();
      Container.Bind<RiskPointsScribe>().AsSingle();
      Container.Bind<EquipmentUsedDataScroll>().AsSingle();
      Container.Bind<GamePointsScribe>().AsSingle();
      Container.Bind<DiceRollScribe>().AsSingle();
      Container.Bind<DiceRollCalculator>().AsSingle();
      Container.Bind<GamePoints>().AsSingle();
      Container.Bind<IDataInit>().FromInstance(new Memento());
      // Container.Bind<IStartGame>().FromInstance(Leviathan.Instance);
      Container.Bind<IDiceRoll>().FromInstance(new DiceRollScribe());
    }
  }
}