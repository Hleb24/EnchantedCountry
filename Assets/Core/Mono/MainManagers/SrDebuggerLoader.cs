using Cysharp.Threading.Tasks;

namespace Core.Mono.MainManagers {
  public class SrDebuggerLoader : ILoader {
    public async UniTaskVoid Load() {
#if ENABLE_SRDEBUGGER && !IS_REALISE
      SRDebug.Init();
#endif
      await UniTask.Yield();
      IsLoad = true;
    }

    public bool IsLoad { get; private set; }
  }
}