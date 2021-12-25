using System;
using Core.SupportSystems.SaveSystem.SaveManagers;
using Core.SupportSystems.SaveSystem.Scribe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.SupportSystems.Data {
  /// <summary>
  ///   Интерфейс для работы с бросками характеристик.
  /// </summary>
  public interface IDiceRoll {
    /// <summary>
    ///   Получить бросок характеристики.
    /// </summary>
    /// <param name="statRolls">Номер броска харасктеристики.</param>
    /// <returns>Значение броска характеристики.</returns>
    public int GetStatsRoll(StatRolls statRolls);

    /// <summary>
    ///   Установить значение броска характеристики.
    /// </summary>
    /// <param name="statRolls">Номер броска харасктеристики.</param>
    /// <param name="value">Значение броска характеристики.</param>
    public void SetStatsRoll(StatRolls statRolls, int value);

    /// <summary>
    ///   Изменить  значение броска характеристики.
    /// </summary>
    /// <param name="statRolls">Номер броска харасктеристики.</param>
    /// <param name="value">Значение на сколько надо изменить бросок характеристики.</param>
    public void ChangeStatsRoll(StatRolls statRolls, int value);

    /// <summary>
    ///   Получить все броски характеристики.
    /// </summary>
    /// <returns>Броски характеристи.</returns>
    public int[] GetDiceRollValues();

    /// <summary>
    ///   Получить количество бросков характеристики.
    /// </summary>
    /// <returns>Количество бросков характеристики.</returns>
    public int GetNumberOfDiceRolls();

    /// <summary>
    ///   Установить новые значение бросков характеристики.
    /// </summary>
    /// <param name="diceRollValues">Новые значение бросков характеристики.</param>
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
  public class DiceRollScribe : IScribe, IDiceRoll {
    private readonly int[] _startRollValues = { 0, 0, 0, 0, 0 };
    private DiceRollDataScroll _diceRollDataScroll;

    int IDiceRoll.GetStatsRoll(StatRolls statRolls) {
      // Assert.IsNotNull(_diceRollDataSave.DiceRollValues);
      return statRolls switch {
               StatRolls.First => _diceRollDataScroll.GetDiceRollValue((int)StatRolls.First),
               StatRolls.Second => _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Second),
               StatRolls.Third => _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Third),
               StatRolls.Fourth => _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Fourth),
               StatRolls.Fifth => _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Fifth),
               _ => default
             };
    }

    void IDiceRoll.SetStatsRoll(StatRolls statRolls, int value) {
      Debug.LogWarning(_diceRollDataScroll.DiceRollValues == null);
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      switch (statRolls) {
        case StatRolls.First:
          _diceRollDataScroll.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Fifth, value);
          break;
        default:
          Debug.LogWarning("Броска характеристики не существует");
          break;
      }
    }

    void IDiceRoll.ChangeStatsRoll(StatRolls statRolls, int value) {
      Debug.LogWarning(_diceRollDataScroll.DiceRollValues == null);

      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);

      switch (statRolls) {
        case StatRolls.First:
          value += _diceRollDataScroll.GetDiceRollValue((int)StatRolls.First);
          _diceRollDataScroll.SetDiceValue((int)StatRolls.First, value);
          break;
        case StatRolls.Second:
          value += _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Second);
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Second, value);
          break;
        case StatRolls.Third:
          value += _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Third);
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Third, value);
          break;
        case StatRolls.Fourth:
          value += _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Fourth);
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Fourth, value);
          break;
        case StatRolls.Fifth:
          value += _diceRollDataScroll.GetDiceRollValue((int)StatRolls.Fifth);
          _diceRollDataScroll.SetDiceValue((int)StatRolls.Fifth, value);
          break;
      }
    }

    int[] IDiceRoll.GetDiceRollValues() {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      return _diceRollDataScroll.DiceRollValues;
    }

    int IDiceRoll.GetNumberOfDiceRolls() {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      Assert.IsTrue(_diceRollDataScroll.DiceRollValues.Length > 0);
      return _diceRollDataScroll.DiceRollValues.Length;
    }

    void IDiceRoll.SetDiceRollValues(int[] diceRollValues) {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      _diceRollDataScroll.DiceRollValues = diceRollValues;
    }

    void IScribe.Init(Scrolls scrolls) {
      _diceRollDataScroll = new DiceRollDataScroll(_startRollValues);
      if (scrolls is null) {
        return;
      }

      scrolls.DiceRollDataScroll = _diceRollDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.DiceRollDataScroll = _diceRollDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _diceRollDataScroll.DiceRollValues = scrolls.DiceRollDataScroll.DiceRollValues;
      for (var i = 0; i < _diceRollDataScroll.DiceRollValues.Length; i++) {
        Debug.Log(_diceRollDataScroll.DiceRollValues[i]);
      }
    }
  }

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