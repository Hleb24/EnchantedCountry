using JetBrains.Annotations;
using UnityEngine;

namespace Core.Mono.MainManagers {
  public static class Notifier {
    public static void Log([NotNull] string message) {
#if ENABLE_MONO
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.Log(message);
#endif
    }

    public static void LogError([NotNull] string message) {
#if ENABLE_MONO
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.LogError(message);
#endif
    }

    public static void LogWarning([NotNull] string message) {
#if ENABLE_MONO
      if (string.IsNullOrEmpty(message)) {
        return;
      }

      Debug.LogWarning(message);
#endif
    }
  }
}