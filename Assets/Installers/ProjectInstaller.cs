using Core.Mono.MainManagers;
using Core.ScriptableObject.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Aberrance.Installers {
  public class ProjectInstaller : MonoInstaller {
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private GameObject _leviathanPrefab;

    public override void InstallBindings() {
      Container.Bind<Memento>().FromMethod(GetMemento).AsSingle();
      Container.Bind<IGameSettings>().To<GameSettings>().FromScriptableObject(_gameSettings).AsSingle();
      Container.Bind<IStartGame>().To<Leviathan>().FromComponentInNewPrefab(_leviathanPrefab).AsSingle();
      Container.Bind<IDealer>().To<ScribeDealer>().AsSingle();
    }

   
    private Memento GetMemento() {
      Memento memento = new Memento();
      if (_gameSettings.StartNewGame()) {
        memento.DeleteSave();
      }

      memento.Init(out bool _);
      return memento;
    }
  }
}