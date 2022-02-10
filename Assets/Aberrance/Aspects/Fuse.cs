using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Aberrance.Aspects {
  public static class Fuse {
    [PublicAPI, Pure]
    public static bool Try([NotNull] Func<bool> func) {
      try {
        return func.Invoke();
      } catch (Exception e) {
        Debug.LogError($"Ошибка в методе {nameof(Try)}, функции {func.Method.Name}, сообщение: {e.Message}");
      }

      return false;
    }

    [PublicAPI, Pure]
    public static T Try<T>([NotNull] Func<T> func) {
      try {
        return func.Invoke();
      } catch (Exception e) {
        Debug.LogError($"Ошибка в методе {nameof(Try)}, функции {func.Method.Name}, сообщение: {e.Message}");
      }

      return default;
    }
  }
}