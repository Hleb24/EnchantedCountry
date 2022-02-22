using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Main.Dice {
  public sealed class SixSidedDice : Dice {
    private const int MIN_EDGES = 2;
    private const int MAX_EDGES = 6;

    public override DiceType DiceType {
      get {
        return DiceType.TwelveEdges;
      }
    }

    protected override int[] Edges { get; } = { 1, 2, 3, 4, 5, 6 };

    /// <summary>
    ///   Бросок кости з указаным количеством граней.
    /// </summary>
    /// <param name="edges">Количество граней. Должно быть в диапазоне от <see cref="MIN_EDGES" /> до <see cref="MAX_EDGES" />.</param>
    /// <returns>Значение броска кости.</returns>
    [MustUseReturnValue]
    public int GetDiceRollAccordingToEdges(int edges) {
      Assert.IsTrue(edges >= MIN_EDGES && edges <= MAX_EDGES, nameof(edges));
      int randomIndex = Random.Range(0, Edges.Length);
      return edges switch {
               2 when randomIndex <= 2 => 1,
               2 => 2,
               3 when randomIndex <= 1 => 1,
               3 when randomIndex <= 3 => 2,
               3 when randomIndex <= 5 => 3,
               4 when randomIndex <= 3 => ++randomIndex,
               4 => 4,
               5 when randomIndex <= 4 => ++randomIndex,
               5 => 5,
               6 => ++randomIndex,
               _ => 0
             };
    }
  }
}