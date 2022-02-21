using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Dice {
  public class DiceBox {
    private readonly List<Dice> _boxOfDices;

    public DiceBox([NotNull, ItemNotNull] IReadOnlyList<Dice> dices) {
      Assert.IsNotNull(dices, nameof(dices));
      _boxOfDices = new List<Dice>(dices);
    }

    public int GetNumberOfDicesInBox() {
      return _boxOfDices.Count;
    }

    public int GetSumRollOfBoxDices() {
      var sum = 0;
      foreach (Dice dice in _boxOfDices) {
        sum += dice.GetDiceRoll();
      }

      return sum;
    }

    public Dice this[int index] {
      get {
        return _boxOfDices[index];
      }
    }
  }
}