using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.Character.Quality;
using Core.Main.Dice;
using Core.Mono.MainManagers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс отвечает за броски костей для качества.
  /// </summary>
  public class QualityDiceRoll : MonoBehaviour {
    private readonly int[] _valuesWithRollOfDice = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
    [SerializeField]
    private QualitiesSelector _qualitiesSelector;
    [SerializeField]
    private DiceRollInfo _diceRollInfo;
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private Button _reset;
    [SerializeField]
    private Button _save;
    [SerializeField]
    private Button _load;
    private DiceRollCalculator _diceRollCalculator;
    private IDiceRoll _diceRollData;
    private IStartGame _startGame;
    private int _numberOfDiceRoll;
    private bool _isNumberOfDiceRollOverlay;

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    [Inject, UsedImplicitly]
    public void Constructor(IDiceRoll diceRoll, DiceRollCalculator diceRollCalculator, IStartGame startGame) {
      _diceRollData = diceRoll;
      _diceRollCalculator = diceRollCalculator;
      _startGame = startGame;
    }

    private void AddListener() {
      _diceRollButton.onClick.AddListener(SetDiceRollValuesAndIncreaseCount);
      _load.onClick.AddListener(LoadAndSetDiceRollData);
      _reset.onClick.AddListener(ResetValuesOfDiceRoll);
      _save.onClick.AddListener(Save);
      _qualitiesSelector.AllValuesSelected += OnAllValuesSelected;
      _qualitiesSelector.DistributeValues += OnDistributeValues;
    }

    private void RemoveListener() {
      _diceRollButton.onClick.RemoveListener(SetDiceRollValuesAndIncreaseCount);
      _load.onClick.RemoveListener(LoadAndSetDiceRollData);
      _reset.onClick.RemoveListener(ResetValuesOfDiceRoll);
      _save.onClick.RemoveListener(Save);
      _qualitiesSelector.AllValuesSelected -= OnAllValuesSelected;
      _qualitiesSelector.DistributeValues -= OnDistributeValues;
    }

    private void LoadAndSetDiceRollData() {
      if (_startGame.UseGameSave() && _startGame.IsNewGame().False()) {
        SetTextsInListWithSave();
        _diceRollInfo.RefreshLoadInfo();
        _numberOfDiceRoll = (int)QualityRolls.Fifth;
        _isNumberOfDiceRollOverlay = true;
        _qualitiesSelector.EnableDistribute();
        return;
      }

      _diceRollInfo.SetMustDiceRollInfo();
    }

    private void SetTextsInListWithSave() {
      var diceRollValues = new List<string>();
      for (var roll = 0; roll < QualityTypeHandler.NUMBER_OF_QUALITY; roll++) {
        Debug.LogWarning(_diceRollData.GetQualitiesRoll((QualityRolls)roll));
        diceRollValues.Add(_diceRollData.GetQualitiesRoll((QualityRolls)roll).ToString());
      }

      _diceRollInfo.SetDiceRollValuesText(diceRollValues);
    }

    private void SetDiceRollValuesAndIncreaseCount() {
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

    private void ResetValuesOfDiceRoll() {
      _diceRollInfo.ResetTexts();
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        _diceRollData.SetStatsRoll((QualityRolls)i, 0);
      }

      _numberOfDiceRoll = 0;
      _isNumberOfDiceRollOverlay = false;
      _qualitiesSelector.DisableUI();
    }

    private void Save() {
      _qualitiesSelector.EnableDistribute();
      _diceRollInfo.SetSaveForInfo();
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

    private void OnDistributeValues() {
      DisableButtonInteractable();
    }

    private void OnAllValuesSelected() {
      DisableButtonInteractable();
    }

    private void DisableButtonInteractable() {
      for (var i = 0; i < Buttons.Length; i++) {
        Buttons[i].interactable = false;
      }
    }

    private Button[] Buttons {
      get {
        return new[] { _diceRollButton, _load, _save, _reset };
      }
    }
  }
}