using System;
using Core.Support.SaveSystem.SaveManagers;
using Cysharp.Threading.Tasks;

namespace Core.Support.SaveSystem.Saver {
  public interface ISaver {
    UniTaskVoid Save(Scrolls scrolls, Action<Exception> handler = null);

    UniTask<Scrolls> Load(Action<Exception> handler);
    Scrolls Load();
    void DeleteSave();
  }
}