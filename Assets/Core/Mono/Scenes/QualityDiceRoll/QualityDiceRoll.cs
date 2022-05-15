using System;
using System.Collections.Generic;
using Core.Main.Character.Quality;
using Core.Main.Dice;
using Core.Mono.MainManagers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Mono.Scenes.QualityDiceRoll {
  public class QualityDiceRoll {
    private readonly DiceRollCalculator _diceRollCalculator;
    private readonly IDiceRoll _diceRollData;
    private readonly IStartGame _startGame;
    private readonly DiceRollInfo _diceRollInfo;
    private readonly int[] _valuesWithRollOfDice = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
    public Action UseDiceRollWithSave;
    private int _numberOfDiceRoll;
    private bool _isNumberOfDiceRollOverlay;

    public QualityDiceRoll([NotNull] IDiceRoll diceRoll, [NotNull] DiceRollCalculator diceRollCalculator, [NotNull] DiceRollInfo diceRollInfo, [NotNull] IStartGame startGame) {
      Assert.IsNotNull(diceRoll, nameof(diceRoll));
      Assert.IsNotNull(diceRollCalculator, nameof(diceRollCalculator));
      Assert.IsNotNull(startGame, nameof(startGame));
      Assert.IsNotNull(diceRollInfo, nameof(diceRollInfo));
      _diceRollData = diceRoll;
      _diceRollCalculator = diceRollCalculator;
      _diceRollInfo = diceRollInfo;
      _startGame = startGame;
    }

    public void LoadAndSetDiceRollData() {
      if (!_startGame.UseGameSave() && _startGame.IsNewGame()) {
        SetTextsInListWithSave();
        _diceRollInfo.RefreshLoadInfo();
        _numberOfDiceRoll = (int)QualityRolls.Fifth;
        _isNumberOfDiceRollOverlay = true;
        UseDiceRollWithSave?.Invoke();
        return;
      }

      _diceRollInfo.SetSaveNotFound();
    }

    public void SetDiceRollValuesAndIncreaseCount() {
      if (_isNumberOfDiceRollOverlay) {
        return;
      }

      if (AllDiceRollsCompleted()) {
        _diceRollInfo.SetAllDiceRolledForInfo();
        _isNumberOfDiceRollOverlay = true;
        Save();
        return;
      }

      SetRollValues();
      _numberOfDiceRoll++;
    }

    public void ResetValuesOfDiceRoll() {
      _diceRollInfo.ResetTexts();
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        _diceRollData.SetStatsRoll((QualityRolls)i, 0);
      }

      _numberOfDiceRoll = 0;
      _isNumberOfDiceRollOverlay = false;
    }

    public void Save() {
      _diceRollInfo.SetSaveForInfo();
    }

    private void SetTextsInListWithSave() {
      var diceRollValues = new List<string>();
      for (var roll = 0; roll < QualityTypeHandler.NUMBER_OF_QUALITY; roll++) {
        Debug.LogWarning(_diceRollData.GetQualitiesRoll((QualityRolls)roll));
        diceRollValues.Add(_diceRollData.GetQualitiesRoll((QualityRolls)roll).ToString());
      }

      _diceRollInfo.SetDiceRollValuesText(diceRollValues);
    }

    private bool AllDiceRollsCompleted() {
      return _numberOfDiceRoll == QualityTypeHandler.NUMBER_OF_QUALITY;
    }

    private void SetRollValues() {
      _diceRollInfo.SetDiceRollForInfo();
      _valuesWithRollOfDice[_numberOfDiceRoll] = _diceRollCalculator.GetSumDiceRollForQuality();
      _diceRollData.SetStatsRoll((QualityRolls)_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
      _diceRollInfo.SetDiceRollTextValues(_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
    }
  }
}