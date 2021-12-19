using Core.EnchantedCountry.CoreEnchantedCountry.Character.CharacterCreation;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Aberrance {
  public class GameInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<ClassOfCharacterData>().AsSingle();
      Container.Bind<WalletDataScroll>().AsSingle();
      Container.Bind<RiskPointsData>().AsSingle();
      Container.Bind<EquipmentUsedDataScroll>().AsSingle();
      Container.Bind<GamePointsScribe>().AsSingle();
      Container.Bind<DiceRollScribe>().AsSingle();
      Container.Bind<CharacterCreation>().AsSingle();
      Container.Bind<QualitiesAfterDistributing>().AsSingle();
      Container.Bind<GamePoints>().AsSingle();
    }
  }
}