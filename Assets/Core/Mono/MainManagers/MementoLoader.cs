using System.Threading.Tasks;
using Core.SO.GameSettings;
using Core.Support.SaveSystem.SaveManagers;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;
using Zenject;

namespace Core.Mono.MainManagers {
  public class MementoLoader : ILoader {
    private readonly IMemento _memento;
    private readonly IGameSettings _gameSettings;

    [Inject]
    public MementoLoader(IMemento memento, IGameSettings gameSettings) {
      Assert.IsNotNull(memento, nameof(memento));
      Assert.IsNotNull(gameSettings, nameof(gameSettings));
      _memento = memento;
      _gameSettings = gameSettings;
    }

    public async UniTaskVoid Load() {
      await Task.Run(async () => {
                       await UniTask.SwitchToMainThread();
                       if (_gameSettings.StartNewGame()) {
                         _memento.DeleteSave();
                       }

                       _memento.Init();
                     });
      IsLoad = true;
    }

    public bool IsLoad { get; private set; }
  }
}