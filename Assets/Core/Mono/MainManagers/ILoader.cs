using Cysharp.Threading.Tasks;

namespace Core.Mono.MainManagers {
  public interface ILoader {
    UniTaskVoid Load();
    public bool IsLoad { get; }
  }
}