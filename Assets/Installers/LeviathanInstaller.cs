using Core.Mono.MainManagers;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Aberrance {
  public class LeviathanInstaller : MonoInstaller {
    public override void InstallBindings() {
      Leviathan.Instance.Call();
      Container.Bind<IStartGame>().FromInstance(Leviathan.Instance);
      Container.Bind<IDealer>().To<ScribeDealer>().AsSingle();
    }
  }
}