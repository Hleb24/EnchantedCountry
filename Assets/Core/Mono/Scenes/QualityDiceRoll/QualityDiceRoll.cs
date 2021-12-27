using System.Collections.Generic;
using Core.Rule.Dice;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  /// Класс отвечает за броски костей для качества.
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
    [SerializeField]
    private bool _usedGameSave;
    private DiceRollCalculator _diceRollCalculator;
    private IDiceRoll _diceRollData;
    private int _numberOfDiceRoll;
    private bool _isNumberOfDiceRollOverlay;

    private void Awake() {
      _diceRollCalculator = new DiceRollCalculator();
    }

    private void Start() {
      if (_usedGameSave) {
        _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
      }
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
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
      if (_usedGameSave) {
        _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
        SetTextsInListWithSave();
        Invoke(nameof(SetTextsInListWithSave), 0.3f);
      }

      _diceRollInfo.SetValueLoadForInfo();
      _numberOfDiceRoll = (int)StatRolls.Fifth;
      _isNumberOfDiceRollOverlay = true;
      _qualitiesSelector.EnableDistribute();
    }

    private void SetTextsInListWithSave() {
      var diceRollValues = new List<string>();
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        Debug.LogWarning(_diceRollData.GetStatsRoll((StatRolls)i));
        diceRollValues.Add(_diceRollData.GetStatsRoll((StatRolls)i).ToString());
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
        _diceRollData.SetStatsRoll((StatRolls)i, 0);
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
      _diceRollData.SetStatsRoll((StatRolls)_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
      _diceRollInfo.SetDiceRollTextValues(_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
    }

    private void OnDistributeValues() {
      DisableAllButtons();
    }

    private void OnAllValuesSelected() {
      DisableAllButtons();
    }

    private void DisableAllButtons() {
      DisableButtonInteractable(_diceRollButton);
      DisableButtonInteractable(_load);
      DisableButtonInteractable(_save);
      DisableButtonInteractable(_reset);
    }

    private void DisableButtonInteractable(Button button) {
      button.interactable = false;
    }
  }
}