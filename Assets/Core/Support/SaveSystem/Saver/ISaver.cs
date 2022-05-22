using System;
using Cysharp.Threading.Tasks;

namespace Core.Support.SaveSystem.Saver {
  public interface ISaver {
    UniTaskVoid SaveAsync<T>(T data, string pathToFolder = "", string pathToFile = "", float timeOut = 5f, Action<Exception> handler = null);
    UniTask<T> LoadAsync<T>(string pathToFolder = "", string pathToFile = "", float timeOut = 5f, Action<Exception> handler = null);
    void Save<T>(T data, string pathToFolder = "", string pathToFile = "", Action<Exception> handler = null);
    T Load<T>(string pathToFolder = "", string pathToFile = "", Action<Exception> handler = null);
    void DeleteSaves(string pathToFile = "");
    void DeleteFile(string pathToFile = "");
  }
}