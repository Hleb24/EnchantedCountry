using Core.Main.Character.Quality;
using Core.Main.Dice;
using Core.Mono.Scenes.QualityDiceRoll;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace EnchantedCountry.Installers {
  public class DiceRollsInstaller : MonoInstaller {
    [SerializeField]
    private DiceRollInfo _diceRollInfo;

    public override void InstallBindings() {
      Container.Bind<DiceRollCalculator>().AsSingle();
      Container.Bind<IDiceRoll>().FromResolveGetter(Dealers.Resolve<IDiceRoll>()).AsSingle();
      Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      Container.Bind<DiceRollInfo>().FromInstance(_diceRollInfo).AsSingle();
      Container.Bind<QualityDiceRoll>().AsSingle();
    }
  }
}