using System;
using Aberrance.Extensions;
using Core.Main.Dice;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Support.Data.DiceRoll {
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
      if (scrolls.Null()) {
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
}