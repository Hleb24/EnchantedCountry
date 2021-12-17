using System;
using UnityEngine;
using UnityEngine.Assertions;

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
  public class DiceRollData : IScribe, IDiceRoll {
    private readonly int[] _startRollValues = { 0, 0, 0, 0, 0 };
    private DiceRollDataSave _diceRollDataSave;

    int IDiceRoll.GetStatsRoll(StatRolls statRolls) {
      // Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      return statRolls switch {
               StatRolls.First => _diceRollDataSave.GetDiceRollValue((int)StatRolls.First),
               StatRolls.Second => _diceRollDataSave.GetDiceRollValue((int)StatRolls.Second),
               StatRolls.Third => _diceRollDataSave.GetDiceRollValue((int)StatRolls.Third),
               StatRolls.Fourth => _diceRollDataSave.GetDiceRollValue((int)StatRolls.Fourth),
               StatRolls.Fifth => _diceRollDataSave.GetDiceRollValue((int)StatRolls.Fifth),
               _ => default
             };
    }

    void IDiceRoll.SetStarsRoll(StatRolls statRolls, int value) {
      Debug.LogWarning(_diceRollDataSave.DiceRollValues == null);
      Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      switch (statRolls) {
        case StatRolls.First:
          _diceRollDataSave.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          _diceRollDataSave.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          _diceRollDataSave.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          _diceRollDataSave.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          _diceRollDataSave.SetDiceValue((int)StatRolls.Fifth, value);
          break;
        default:
          Debug.LogWarning("Броска характеристики не существует");
          break;
      }
    }

    void IDiceRoll.ChangeStatsRoll(StatRolls statRolls, int value) {
      Debug.LogWarning(_diceRollDataSave.DiceRollValues == null);

      Assert.IsNotNull(_diceRollDataSave.DiceRollValues);

      switch (statRolls) {
        case StatRolls.First:
          value += _diceRollDataSave.GetDiceRollValue((int)StatRolls.First);
          _diceRollDataSave.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          value += _diceRollDataSave.GetDiceRollValue((int)StatRolls.Second);
          _diceRollDataSave.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          value += _diceRollDataSave.GetDiceRollValue((int)StatRolls.Third);
          _diceRollDataSave.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          value += _diceRollDataSave.GetDiceRollValue((int)StatRolls.Fourth);
          _diceRollDataSave.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          value += _diceRollDataSave.GetDiceRollValue((int)StatRolls.Fifth);
          _diceRollDataSave.SetDiceValue((int)StatRolls.Fifth, value);
          break;
      }
    }

    int[] IDiceRoll.GetDiceRollValues() {
      Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      return _diceRollDataSave.DiceRollValues;
    }

    int IDiceRoll.GetNumberOfDiceRolls() {
      Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      Assert.IsTrue(_diceRollDataSave.DiceRollValues.Length > 0);
      return _diceRollDataSave.DiceRollValues.Length;
    }

    void IDiceRoll.SetDiceRollValues(int[] diceRollValues) {
      Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      _diceRollDataSave.DiceRollValues = diceRollValues;
    }

    public void Init(SaveGame saveGame = null) {
      _diceRollDataSave = new DiceRollDataSave(_startRollValues);
      if (saveGame is null) {
        return;
      }

      saveGame.DiceRollDataSave = _diceRollDataSave;
    }

    public void Save(SaveGame saveGame) {
      saveGame.DiceRollDataSave = _diceRollDataSave;
    }

    public void Loaded(SaveGame saveGame) {
      _diceRollDataSave.DiceRollValues = saveGame.DiceRollDataSave.DiceRollValues;
      for (int i = 0; i < _diceRollDataSave.DiceRollValues.Length; i++) {
        Debug.LogWarning(_diceRollDataSave.DiceRollValues[i]);
      }
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