using Core.EnchantedCountry.CoreEnchantedCountry.Character.CharacterCreation;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.MonoBehaviourScripts.MainManagers;
using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;
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
      Container.Bind<QualitiesAfterDistributing>().AsSingle();
      Container.Bind<GamePoints>().AsSingle();
      Container.Bind<IDataInit>().FromInstance(new Memento());
      Container.Bind<IStartGame>().FromInstance(Leviathan.Instance);
    }
  }
}