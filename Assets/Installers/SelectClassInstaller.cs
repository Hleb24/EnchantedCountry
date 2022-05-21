using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.Mono.Scenes.SelectionClass;
using Core.SO.GameSettings;
using Core.SO.Mock;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace EnchantedCountry.Installers {
  public class SelectClassInstaller : MonoInstaller {
    [SerializeField]
    private MockQualitiesPoints _mockQualitiesPoints;
    [SerializeField]
    private GameSettings _gameSettings;

    public override void InstallBindings() {
      if (_gameSettings.UseGameSave()) {
        Container.Bind<IQualityPoints>().FromResolveGetter(Dealers.Resolve<IQualityPoints>()).AsSingle();
      } else {
        Container.Bind<IQualityPoints>().FromInstance(_mockQualitiesPoints).AsSingle();
      }

      Container.Bind<IClassType>().FromResolveGetter(Dealers.Resolve<IClassType>()).AsSingle();
      Container.Bind<AvailableCharacterClass>().AsSingle();
      Container.Bind<CharacterClassSelector>().AsSingle();
    }
  }
}