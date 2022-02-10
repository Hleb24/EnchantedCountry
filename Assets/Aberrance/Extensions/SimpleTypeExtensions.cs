using JetBrains.Annotations;
using UnityEngine;

namespace Aberrance.Extensions {
  public static class SimpleTypeExtensions {
    #region Boolean
    [PublicAPI, Pure]
    public static bool True(this bool predicate) {
      return predicate;
    }

    [PublicAPI, Pure]
    public static bool False(this bool predicate) {
      return predicate == false;
    }
    #endregion

    #region Object
    [PublicAPI, Pure]
    public static bool Null(this object target) {
      return target is null;
    }

    [PublicAPI, Pure]
    public static bool NotNull(this object target) {
      return target != null;
    }
    #endregion

    #region Number
    [PublicAPI, Pure]
    public static bool Zero(this int number) {
      return number == 0;
    }

    [PublicAPI, Pure]
    public static bool NotZero(this int number) {
      return number != 0;
    }

    [PublicAPI, Pure]
    public static bool Zero(this float number) {
      return number == 0;
    }

    [PublicAPI, Pure]
    public static bool NotZero(this float number) {
      return number != 0;
    }

    public static bool Zero(this double number) {
      return number == 0;
    }

    [PublicAPI, Pure]
    public static bool NotZero(this double number) {
      return number != 0;
    }
    #endregion

    #region Vector
    [PublicAPI, Pure]
    public static bool Zero(this Vector3 vector) {
      return vector == Vector3.zero;
    }

    [PublicAPI, Pure]
    public static bool One(this Vector3 vector) {
      return vector == Vector3.one;
    }

    [PublicAPI, Pure]
    public static bool Forward(this Vector3 vector) {
      return vector == Vector3.forward;
    }

    [PublicAPI, Pure]
    public static bool Back(this Vector3 vector) {
      return vector == Vector3.back;
    }

    [PublicAPI, Pure]
    public static bool Up(this Vector3 vector) {
      return vector == Vector3.up;
    }

    [PublicAPI, Pure]
    public static bool Down(this Vector3 vector) {
      return vector == Vector3.down;
    }

    [PublicAPI, Pure]
    public static bool Right(this Vector3 vector) {
      return vector == Vector3.right;
    }

    [PublicAPI, Pure]
    public static bool Left(this Vector3 vector) {
      return vector == Vector3.left;
    }

    [PublicAPI, Pure]
    public static bool Zero(this Vector2 vector) {
      return vector == Vector2.zero;
    }

    [PublicAPI, Pure]
    public static bool One(this Vector2 vector) {
      return vector == Vector2.one;
    }

    [PublicAPI, Pure]
    public static bool Up(this Vector2 vector) {
      return vector == Vector2.up;
    }

    [PublicAPI, Pure]
    public static bool Down(this Vector2 vector) {
      return vector == Vector2.down;
    }

    [PublicAPI, Pure]
    public static bool Right(this Vector2 vector) {
      return vector == Vector2.right;
    }

    [PublicAPI, Pure]
    public static bool Left(this Vector2 vector) {
      return vector == Vector2.left;
    }
    #endregion
  }
}