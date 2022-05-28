using Core.Mono.MainManagers;
using Core.SO.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace EnchantedCountry.Installers {
  public class ProjectInstaller : MonoInstaller {
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private GameObject _leviathanPrefab;

    public override void InstallBindings() {
      Container.Bind<Memento>().AsSingle();
      Container.Bind<MementoLoader>().AsSingle();
      Container.Bind<IGameSettings>().To<GameSettings>().FromScriptableObject(_gameSettings).AsSingle();
      Container.Bind<ILauncher>().To<Leviathan>().FromComponentInNewPrefab(_leviathanPrefab).AsSingle();
      Container.Bind<IDealer>().To<ScribeDealer>().AsSingle();
    }
  }
}