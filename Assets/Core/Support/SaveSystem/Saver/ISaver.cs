using System;
using Core.Support.SaveSystem.SaveManagers;
using Cysharp.Threading.Tasks;

namespace Core.Support.SaveSystem.Saver {
  public interface ISaver {
    UniTaskVoid SaveAsync(Scrolls scrolls, Action<Exception> handler = null);

    UniTask<Scrolls> LoadAsync(Action<Exception> handler);
    Scrolls Load();
    void DeleteSave();
  }
}