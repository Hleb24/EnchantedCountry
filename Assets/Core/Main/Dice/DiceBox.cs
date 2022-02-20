using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Dice {
  public class DiceBox {
    private readonly List<Dice> _setOfDice;

    public DiceBox([NotNull, ItemNotNull] IReadOnlyList<Dice> dices) {
      Assert.IsNotNull(dices, nameof(dices));
      _setOfDice = new List<Dice>(dices.Count);
      for (var i = 0; i < dices.Count; i++) {
        _setOfDice.Add(dices[i]);
      }
    }

    public int GetCountSetOfDice() {
      return _setOfDice.Count;
    }

    public int SumRollsOfDice() {
      var sum = 0;
      foreach (Dice dice in _setOfDice) {
        sum += dice.GetDiceRoll();
      }

      return sum;
    }

    public Dice this[int index] {
      get {
        return _setOfDice[index];
      }
    }
  }
}