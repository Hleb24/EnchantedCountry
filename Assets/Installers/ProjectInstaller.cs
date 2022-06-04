using System.Collections.Generic;
using Core.Mono.MainManagers;
using Core.SO.GameSettings;
using Core.Support.RemoteConfigsService;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace EnchantedCountry.Installers {
  public class ProjectInstaller : MonoInstaller {
    private static ILoader CreateLoader(InjectContext arg) {
      var srDebuggerLoader = arg.Container.Resolve<SrDebuggerLoader>();
      var mementoLoader = arg.Container.Resolve<MementoLoader>();
      var remoteConfigsLauncher = arg.Container.Resolve<RemoteConfigsLoader>();

      return new LoaderComposite(new List<ILoader> { srDebuggerLoader, mementoLoader, remoteConfigsLauncher });
    }

    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private GameObject _leviathanPrefab;

    public override void InstallBindings() {
      Container.Bind<IMemento>().To<Memento>().AsSingle();
      Container.Bind<IGameSettings>().To<GameSettings>().FromScriptableObject(_gameSettings).AsSingle();
      Container.Bind<ILauncher>().To<Leviathan>().FromComponentInNewPrefab(_leviathanPrefab).AsSingle();
      Container.Bind<IDealer>().To<ScribeDealer>().AsSingle();
      Container.Bind<MementoLoader>().AsSingle();
      Container.Bind<RemoteConfigsLoader>().AsSingle();
      Container.Bind<SrDebuggerLoader>().AsSingle();
      Container.Bind<ILoader>().FromMethod(CreateLoader).AsSingle();
    }
  }
}