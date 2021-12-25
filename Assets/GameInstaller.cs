using Core.Mono.MainManagers;
using Core.Mono.Scenes.CreateCharacter;
using Core.Rule.Character.CharacterCreation;
using Core.Rule.Character.GamePoints;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
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
      Container.Bind<CharacterCreation>().AsSingle();
      Container.Bind<DistributedQualities>().AsSingle();
      Container.Bind<GamePoints>().AsSingle();
      Container.Bind<IDataInit>().FromInstance(new Memento());
      Container.Bind<IStartGame>().FromInstance(Leviathan.Instance);
    }
  }
}