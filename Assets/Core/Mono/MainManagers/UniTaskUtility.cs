using Cysharp.Threading.Tasks;

namespace Core.Mono.MainManagers {
  public static class UniTaskUtility {
    public static void ProcessResults(bool isCanceled, TimeoutController timeoutController) {
      const string timeOutMessage = "Timeout for load.";
      const string canceledMessage = "Operation canceled.";
      if (isCanceled && timeoutController.IsTimeout()) {
        Notifier.LogError(timeOutMessage);
      } else if (isCanceled) {
        Notifier.LogError(canceledMessage);
      }

      timeoutController.Reset();
    }
  }
}