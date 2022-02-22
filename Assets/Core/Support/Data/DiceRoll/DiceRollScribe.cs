﻿using System;
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
    public static readonly int[] StartRollValues = { 0, 0, 0, 0, 0 };
    private DiceRollDataScroll _diceRollDataScroll;

    public DiceRollScribe(DiceRollDataScroll diceRollDataScroll) {
      _diceRollDataScroll = diceRollDataScroll;
    }
    
    int IDiceRoll.GetQualitiesRoll(QualityRolls qualityRolls) {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      return qualityRolls switch {
               QualityRolls.First => _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.First),
               QualityRolls.Second => _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Second),
               QualityRolls.Third => _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Third),
               QualityRolls.Fourth => _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Fourth),
               QualityRolls.Fifth => _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Fifth),
               _ => default
             };
    }

    void IDiceRoll.SetStatsRoll(QualityRolls qualityRolls, int value) {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      switch (qualityRolls) {
        case QualityRolls.First:
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.First, value);
          break;
        case QualityRolls.Second:
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Second, value);
          break;
        case QualityRolls.Third:
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Third, value);
          break;
        case QualityRolls.Fourth:
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Fourth, value);
          break;
        case QualityRolls.Fifth:
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Fifth, value);
          break;
        default:
          Debug.LogWarning("Броска характеристики не существует");
          break;
      }
    }

    void IDiceRoll.ChangeStatsRoll(QualityRolls qualityRolls, int value) {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);

      switch (qualityRolls) {
        case QualityRolls.First:
          value += _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.First);
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.First, value);
          break;
        case QualityRolls.Second:
          value += _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Second);
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Second, value);
          break;
        case QualityRolls.Third:
          value += _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Third);
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Third, value);
          break;
        case QualityRolls.Fourth:
          value += _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Fourth);
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Fourth, value);
          break;
        case QualityRolls.Fifth:
          value += _diceRollDataScroll.GetDiceRollValue((int)QualityRolls.Fifth);
          _diceRollDataScroll.SetDiceValue((int)QualityRolls.Fifth, value);
          break;
      }
    }

    int[] IDiceRoll.GetDiceRollValues() {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      return _diceRollDataScroll.DiceRollValues;
    }

    int IDiceRoll.GetNumberOfDiceRolls() {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      return _diceRollDataScroll.DiceRollValues.Length;
    }

    void IDiceRoll.SetDiceRollValues(int[] diceRollValues) {
      Assert.IsNotNull(_diceRollDataScroll.DiceRollValues);
      _diceRollDataScroll.DiceRollValues = diceRollValues;
    }

    void IScribe.Init(Scrolls scrolls) {
      _diceRollDataScroll = new DiceRollDataScroll(StartRollValues);
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