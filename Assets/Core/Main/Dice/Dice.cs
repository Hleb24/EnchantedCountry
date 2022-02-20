using JetBrains.Annotations;
using UnityEngine;

namespace Core.Main.Dice {
  /// <summary>
  ///   Класс игральной кости.
  /// </summary>
  public abstract class Dice {
    /// <summary>
    ///   Бросить кость.
    /// </summary>
    /// <returns>Значение броска кости.</returns>
    [MustUseReturnValue]
    public int GetDiceRoll() {
      int randomIndex = Random.Range(0, Edges.Length);
      return Edges[randomIndex];
    }

    protected abstract int[] Edges { get; }

    /// <summary>
    ///   Тип кости.
    /// </summary>
    [PublicAPI]
    public abstract DiceType DiceType { get; }
  }
}