using System.Diagnostics;
using JetBrains.Annotations;
using Debug = UnityEngine.Debug;

namespace Core.Mono.MainManagers {
  [DebuggerStepThrough]
  public static class Notifier {
    private const string ENABLE_NOTIFIER = "ENABLE_NOTIFIER";
    
    [Conditional(ENABLE_NOTIFIER)]
    public static void Log([NotNull] string message) {
#if ENABLE_LOGS
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.Log(message);
#endif
    }

    [Conditional(ENABLE_NOTIFIER)]

    public static void LogError([NotNull] string message) {
#if ENABLE_LOGS
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.LogError(message);
#endif
    }

    [Conditional(ENABLE_NOTIFIER)]

    public static void LogWarning([NotNull] string message) {
#if ENABLE_LOGS
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.LogWarning(message);
#endif
    }
  }
}