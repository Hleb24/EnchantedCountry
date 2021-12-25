using System;
using System.Collections.Generic;
using Core.Rule.Character.CharacterCreation;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CreateCharacter {
  public class DiceRoll : MonoBehaviour {
    private readonly int[] _valuesWithRollOfDice = new int[5];
    public static event Action AllDiceRollCompleteOrLoad;
    public static event Action ResetDiceRoll;
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
    [Inject]
    private CharacterCreation _characterCreation;
    private IDiceRoll _diceRollData;
    private int _numberOfDiceRoll;
    private bool _isNumberOfDiceRollOverlay;

    private void Start() {
      if (_usedGameSave) {
        _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
      }
    }

    private void OnEnable() {
      InitValuesArray();
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    private void InitValuesArray() {
      if (_usedGameSave) { }
    }

    private void AddListener() {
      _diceRollButton.onClick.AddListener(SetDiceRollValuesAndIncreaseCount);
      _load.onClick.AddListener(LoadAndSetDiceRollData);
      _reset.onClick.AddListener(ResetValuesOfDiceRoll);
      _save.onClick.AddListener(Save);
      QualitiesSelector.AllValuesSelected += OnAllValuesSelected;
    }

    private void RemoveListener() {
      _diceRollButton.onClick.RemoveListener(SetDiceRollValuesAndIncreaseCount);
      _load.onClick.RemoveListener(LoadAndSetDiceRollData);
      _reset.onClick.RemoveListener(ResetValuesOfDiceRoll);
      _save.onClick.RemoveListener(Save);
      QualitiesSelector.AllValuesSelected -= OnAllValuesSelected;
    }

    private void LoadAndSetDiceRollData() {
      if (_usedGameSave) {
        _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
        SetTextsInListWithSave();
        Invoke(nameof(SetTextsInListWithSave), 0.3f);
      }

      _diceRollInfo.LoadAndSetDiceRollData();
      _numberOfDiceRoll = (int)StatRolls.Fifth;
      _isNumberOfDiceRollOverlay = true;
      AllDiceRollCompleteOrLoad?.Invoke();
    }

    private void SetTextsInListWithSave() {
      var diceRollValues = new List<string>();
      for (var i = 0; i < (int)StatRolls.Fifth + 1; i++) {
        Debug.LogWarning(_diceRollData.GetStatsRoll((StatRolls)i));
        diceRollValues.Add(_diceRollData.GetStatsRoll((StatRolls)i).ToString());
      }

      _diceRollInfo.SetTextsInListWithSave(diceRollValues);
    }

    private void SetDiceRollValuesAndIncreaseCount() {
      if (_isNumberOfDiceRollOverlay) {
        return;
      }

      if (AllDiceRollsCompleted()) {
        _diceRollInfo.AddToInfo();
        _isNumberOfDiceRollOverlay = true;
        Save();
        return;
      }

      SetRollValues();
      _numberOfDiceRoll++;
    }

    private void ResetValuesOfDiceRoll() {
      _diceRollInfo.ResetValuesOfDiceRoll();
      for (var i = 0; i < (int)StatRolls.Fifth + 1; i++) {
        _diceRollData.SetStatsRoll((StatRolls)i, 0);
      }

      _numberOfDiceRoll = 0;
      _isNumberOfDiceRollOverlay = false;
      ResetDiceRoll?.Invoke();
    }

    private void Save() {
      AllDiceRollCompleteOrLoad?.Invoke();
      _diceRollInfo.SetSaveForInfo();
    }

    private bool AllDiceRollsCompleted() {
      return _numberOfDiceRoll == (int)StatRolls.Fifth + 1;
    }

    private void SetRollValues() {
      _diceRollInfo.SetDiceRollForInfo();
      _valuesWithRollOfDice[_numberOfDiceRoll] = _characterCreation.GetSumDiceRollForQuality();
      _diceRollData.SetStatsRoll((StatRolls)_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
      _diceRollInfo.SetRollValues(_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
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