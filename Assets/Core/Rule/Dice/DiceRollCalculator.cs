using System.Collections.Generic;

namespace Core.Rule.Dice {
  /// <summary>
  ///   Класс для получение конечного результата бросков кубика.
  /// </summary>
  public class DiceRollCalculator {
    private const int Multiplier = 10;
    private List<int> _valuesWhiteDiceRoll;

    /// <summary>
    ///   Получить конечное значение броска кубиков для качества.
    /// </summary>
    /// <returns>Значение бросков кубика.</returns>
    public int GetSumDiceRollForQuality() {
      _valuesWhiteDiceRoll = new List<int>();
      DiceBox diceBox = KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice];
      for (var i = 0; i < diceBox.GetCountSetOfDice(); i++) {
        _valuesWhiteDiceRoll.Add(diceBox[i].RollOfDice());
      }

      return SortRemoveAndSumValuesWhiteDiceRoll(_valuesWhiteDiceRoll);
    }

    /// <summary>
    ///   Получить конечное значение броска кубиков для стартового количества монет .
    /// </summary>
    /// <returns>Стартовое количество монет.</returns>
    public int GetSumDiceRollForCoins() {
      _valuesWhiteDiceRoll = new List<int>();
      DiceBox diceBox = KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice];
      for (var i = 0; i < diceBox.GetCountSetOfDice(); i++) {
        _valuesWhiteDiceRoll.Add(diceBox[i].RollOfDice());
      }

      return SortRemoveAndSumValuesWhiteDiceRollForNumberOfCoins(_valuesWhiteDiceRoll);
    }

    private int SortRemoveAndSumValuesWhiteDiceRoll(List<int> valuesDiceRoll) {
      var sum = 0;
      valuesDiceRoll.Sort();
      var tempValues = new List<int>();
      tempValues.AddRange(valuesDiceRoll);
      tempValues.RemoveAt(0);
      for (var i = 0; i < tempValues.Count; i++) {
        sum += tempValues[i];
      }

      return sum;
    }

    private int SortRemoveAndSumValuesWhiteDiceRollForNumberOfCoins(List<int> valuesDiceRoll) {
      var sum = 0;
      valuesDiceRoll.Sort();
      var tempValues = new List<int>();
      tempValues.AddRange(valuesDiceRoll);
      tempValues.RemoveAt(0);
      for (var i = 0; i < tempValues.Count; i++) {
        sum += tempValues[i];
      }

      sum *= Multiplier;
      return sum;
    }
  }
}