using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Main.Dice {
  /// <summary>
  ///   Класс для получение конечного результата бросков кубика.
  /// </summary>
  public class DiceRollCalculator {
    private static int GetCoinsAfterMultiplier(int sum) {
      const int multiplierForCoins = 10;
      sum *= multiplierForCoins;
      return sum;
    }

    private static int SortRemoveAndSumValuesOfDiceRoll(List<int> valuesDicesRoll) {
      var sum = 0;
      valuesDicesRoll.Sort();
      valuesDicesRoll.RemoveAt(0);
      for (var i = 0; i < valuesDicesRoll.Count; i++) {
        sum += valuesDicesRoll[i];
      }

      return sum;
    }

    /// <summary>
    ///   Получить конечное значение броска кубиков для качества.
    /// </summary>
    /// <returns>Значение бросков кубика.</returns>
    [MustUseReturnValue]
    public int GetSumDiceRollForQuality() {
      DiceBox diceBox = KitOfDice.DicesKit[KitOfDice.SetWithFourSixSidedDice];
      var dicesRollValues = new List<int>(diceBox.GetNumberOfDicesInBox());
      for (var i = 0; i < diceBox.GetNumberOfDicesInBox(); i++) {
        dicesRollValues.Add(diceBox[i].GetDiceRoll());
      }

      return SortRemoveAndSumValuesOfDiceRoll(dicesRollValues);
    }

    /// <summary>
    ///   Получить конечное значение броска кубиков для стартового количества монет .
    /// </summary>
    /// <returns>Стартовое количество монет.</returns>
    [MustUseReturnValue]
    public int GetSumDiceRollForCoins() {
      DiceBox diceBox = KitOfDice.DicesKit[KitOfDice.SetWithFourSixSidedDice];
      var dicesRollValues = new List<int>(diceBox.GetNumberOfDicesInBox());
      for (var i = 0; i < diceBox.GetNumberOfDicesInBox(); i++) {
        dicesRollValues.Add(diceBox[i].GetDiceRoll());
      }

      int sum = SortRemoveAndSumValuesOfDiceRoll(dicesRollValues);
      sum = GetCoinsAfterMultiplier(sum);
      return sum;
    }
  }
}