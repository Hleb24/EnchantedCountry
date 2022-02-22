using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Support.Data.DiceRoll {
  [Serializable]
  public struct DiceRollDataScroll {
    public int[] DiceRollValues;

    public DiceRollDataScroll(int[] diceRollValues) {
      DiceRollValues = diceRollValues;
    }

    public void SetDiceValue(int index, int diceRollValue) {
      Assert.IsNotNull(DiceRollValues, nameof(DiceRollValues));
      if (index >= DiceRollValues.Length || index < 0) {
        Debug.LogWarning($"Броска характеристики не существует: индекс {index}, длина массива {DiceRollValues.Length}");
        return;
      }

      DiceRollValues[index] = diceRollValue;
    }

    public int GetDiceRollValue(int index) {
      Assert.IsNotNull(DiceRollValues, nameof(DiceRollValues));

      if (index < DiceRollValues.Length && index >= 0) {
        return DiceRollValues[index];
      }

      Debug.LogWarning("Броска характеристики не существует");
      return -1;
    }
  }
}