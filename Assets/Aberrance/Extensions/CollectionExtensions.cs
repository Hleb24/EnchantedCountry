using System.Collections;
using System.Collections.Generic;
using Aberrance.Aspects;
using JetBrains.Annotations;
using UnityEngine;

namespace Aberrance.Extensions {
  public static class CollectionExtensions {
    #region IEnumerable
    [PublicAPI, Pure]
    public static bool Null(this IEnumerable enumerable) {
      return enumerable == null;
    }

    [PublicAPI, Pure]
    public static bool NotNull(this IEnumerable enumerable) {
      return enumerable != null;
    }

    [PublicAPI, Pure]
    public static bool Null<T>(this IEnumerable<T> enumerable) {
      return enumerable is null;
    }

    [PublicAPI, Pure]
    public static bool NotNull<T>(this IEnumerable<T> enumerable) {
      return enumerable != null;
    }
    #endregion

    #region List
    [PublicAPI, Pure]
    public static bool Empty<T>(this List<T> list) {
      return Fuse.Try(() => list.Count == 0);
    }

    [PublicAPI, Pure]
    public static bool NotEmpty<T>(this List<T> list) {
      return Fuse.Try(() => list.Count != 0);
    }

    [PublicAPI, Pure]
    public static bool NotNullAndEmpty<T>(this List<T> list) {
      return list != null && list.Count != 0;
    }

    [PublicAPI, Pure]
    public static bool NullOrEmpty<T>(this List<T> list) {
      return list is null || list.Count == 0;
    }

    [PublicAPI, Pure]
    public static bool CountLessThan<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count < target);
    }

    [PublicAPI, Pure]
    public static bool CountGreaterThan<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count > target);
    }

    [PublicAPI, Pure]
    public static bool CountLessThanOrEqual<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count <= target);
    }

    [PublicAPI, Pure]
    public static bool CountGreaterThanOrEqual<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count >= target);
    }

    [PublicAPI, Pure]
    public static bool CountEqual<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count == target);
    }

    [PublicAPI, Pure]
    public static bool CountNotEqual<T>(this List<T> list, int target) {
      return Fuse.Try(() => list.Count != target);
    }

    [PublicAPI, Pure]
    public static T Last<T>(this List<T> list) {
      return Fuse.Try(() => list[list.LastIndex()]);
    }

    [PublicAPI, Pure]
    public static T First<T>(this List<T> list) {
      return Fuse.Try(() => list[list.FirstIndex()]);
    }

    [PublicAPI, Pure]
    public static int FirstIndex<T>(this List<T> list) {
      if (list.Null()) {
        Debug.LogError("Спискок равен нулевой ссылке!");
      }

      return 0;
    }

    [PublicAPI, Pure]
    public static int LastIndex<T>(this List<T> list) {
      return Fuse.Try(() => list.Count - 1);
    }
    #endregion
  }
}