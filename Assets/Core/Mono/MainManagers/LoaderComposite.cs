using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.Mono.MainManagers {
  public class LoaderComposite : ILoader {
    private readonly IReadOnlyList<ILoader> _loaders;

    public LoaderComposite(IReadOnlyList<ILoader> loaders) {
      _loaders = loaders;
    }

    public async UniTaskVoid Load() {
      const double timeOut = 2;

      var timeoutController = new TimeoutController();
      try {
        for (var i = 0; i < _loaders.Count; i++) {
          ILoader loader = _loaders[i];
          loader.Load().Forget();
          bool isCanceled = await UniTask.WaitUntil(() => loader.IsLoad, cancellationToken: timeoutController.Timeout(TimeSpan.FromSeconds(timeOut))).SuppressCancellationThrow();
          UniTaskUtility.ProcessResults(isCanceled, timeoutController);
        }
      } catch (Exception ex) {
        Notifier.LogError(ex.Message);
      } finally {
        timeoutController.Dispose();
        IsLoad = true;
      }
    }

    public bool IsLoad { get; private set; }
  }
}