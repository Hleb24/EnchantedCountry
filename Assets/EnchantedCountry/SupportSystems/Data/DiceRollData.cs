using System;
using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.Data {
  public interface IDiceRoll {
    public int GetStatsRoll(StatRolls statRolls);
    public void SetStarsRoll(StatRolls statRolls, int value);
    public void ChangeStatsRoll(StatRolls statRolls, int value);

    public int[] GetDiceRollValues();

    public int GetNumberOfDiceRolls();

    public void SetDiceRollValues(int[] diceRollValues);
  }

  /// <summary>
  ///   Перечесление содержить шесть игральных костей, которые участвуют в броске характеристик.
  /// </summary>
  public enum StatRolls {
    First,
    Second,
    Third,
    Fourth,
    Fifth
  }

  /// <summary>
  ///   Класс для сохранения значений, полученых броском костей характеристик.
  /// </summary>
  [Serializable]
  public class DiceRollData : IIntData, IDiceRoll {
    private readonly int[] _startRollValues = { 0, 0, 0, 0, 0 };
    public DiceRollDataSave DiceRollDataSave;

    public void Init(SaveGame saveGame = null) {
      DiceRollDataSave = new DiceRollDataSave(_startRollValues);
      if (saveGame is null) {
        return;
      }

      saveGame.DiceRollDataSave = DiceRollDataSave;
    }

    public void Save(SaveGame saveGame) {
      saveGame.DiceRollDataSave = DiceRollDataSave;
    }

    public void Loaded(SaveGame saveGame) {
      DiceRollDataSave.DiceRollValues = saveGame.DiceRollDataSave.DiceRollValues;
    }

    public int GetInt(Enum eEnum) {
      return eEnum switch {
               StatRolls.First => DiceRollDataSave.GetDiceRollValue((int)StatRolls.First),
               StatRolls.Second => DiceRollDataSave.GetDiceRollValue((int)StatRolls.Second),
               StatRolls.Third => DiceRollDataSave.GetDiceRollValue((int)StatRolls.Third),
               StatRolls.Fourth => DiceRollDataSave.GetDiceRollValue((int)StatRolls.Fourth),
               StatRolls.Fifth => DiceRollDataSave.GetDiceRollValue((int)StatRolls.Fifth),
               _ => default
             };
    }

    public void SetInt(Enum eEnum, int value) {
      switch (eEnum) {
        case StatRolls.First:
          DiceRollDataSave.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          DiceRollDataSave.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          DiceRollDataSave.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          DiceRollDataSave.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          DiceRollDataSave.SetDiceValue((int)StatRolls.Fifth, value);
          break;
        default:
          Debug.LogWarning("Броска характеристики не существует");
          break;
      }
    }

    public void IncreaseInt(Enum eEnum, int value) {
      switch (eEnum) {
        case StatRolls.First:
          value += DiceRollDataSave.GetDiceRollValue((int)StatRolls.First);
          DiceRollDataSave.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          value += DiceRollDataSave.GetDiceRollValue((int)StatRolls.Second);
          DiceRollDataSave.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          value += DiceRollDataSave.GetDiceRollValue((int)StatRolls.Third);
          DiceRollDataSave.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          value += DiceRollDataSave.GetDiceRollValue((int)StatRolls.Fourth);
          DiceRollDataSave.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          value += DiceRollDataSave.GetDiceRollValue((int)StatRolls.Fifth);
          DiceRollDataSave.SetDiceValue((int)StatRolls.Fifth, value);
          break;
      }
    }

    public int GetStatsRoll(StatRolls statRolls) {
      throw new NotImplementedException();
    }

    public void SetStarsRoll(StatRolls statRolls, int value) {
      throw new NotImplementedException();
    }

    public void ChangeStatsRoll(StatRolls statRolls, int value) {
      throw new NotImplementedException();
    }

    public int[] GetDiceRollValues() {
      throw new NotImplementedException();

    }

    public int GetNumberOfDiceRolls() {
      return DiceRollDataSave.DiceRollValues.Length;
    }

    public void SetDiceRollValues(int[] diceRollValues) {
      DiceRollDataSave.DiceRollValues = diceRollValues;
    }
  }

  [Serializable]
  public struct DiceRollDataSave {
    public int[] DiceRollValues;

    public DiceRollDataSave(int[] diceRollValues) {
      DiceRollValues = diceRollValues;
    }

    public void SetDiceValue(int index, int diceRollValue) {
      if (DiceRollValues == default) {
        Debug.LogWarning("Броски характеристик не существуют");
        return;
      }

      if (index >= DiceRollValues.Length || index < 0) {
        Debug.LogWarning("Броска характеристики не существует");
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