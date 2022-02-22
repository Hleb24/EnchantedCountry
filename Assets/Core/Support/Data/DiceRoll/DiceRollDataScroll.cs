using System;
using UnityEngine;

namespace Core.Support.Data.DiceRoll {
  [Serializable]
  public struct DiceRollDataScroll {
    public int[] DiceRollValues;

    public DiceRollDataScroll(int[] diceRollValues) {
      DiceRollValues = diceRollValues;
    }

    public void SetDiceValue(int index, int diceRollValue) {
      if (DiceRollValues == default) {
        Debug.LogWarning("Броски характеристик не существуют");
        return;
      }

      if (index >= DiceRollValues.Length || index < 0) {
        Debug.LogWarning($"Броска характеристики не существует: индекс {index}, длина массива {DiceRollValues.Length}");
        return;
      }

      DiceRollValues[index] = diceRollValue;
    }

    public int GetDiceRollValue(int index) {
      if (DiceRollValues == default) {
        Debug.LogWarning("Броска характеристики не существует");
        return -1;
      }

      if (index < DiceRollValues.Length && index >= 0) {
        return DiceRollValues[index];
      }

      Debug.LogWarning("Броска характеристики не существует");
      return -1;
    }
  }
}