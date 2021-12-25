using System;
using System.Collections.Generic;
using Core.Rule.Character.CharacterCreation;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CreateCharacter {
  public class DiceRoll : MonoBehaviour {
    private const string AllDiceAreRolled = "all dice are rolled";
    private readonly int[] _valuesWithRollOfDice = new int[5];
    public static event Action AllDiceRollCompleteOrLoad;
    public static event Action ResetDiceRoll;
    [SerializeField]
    private TMP_Text _info;
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private Button _reset;
    [SerializeField]
    private Button _save;
    [SerializeField]
    private Button _load;
    [SerializeField]
    private List<TMP_Text> _diceRollValuesText;
    [SerializeField]
    private bool _usedGameSave;
    [Inject]
    private CharacterCreation _characterCreation;
    private int _numberOfDiceRoll;
    private IDiceRoll _diceRollData;
    private bool _isNumberOfDiceRollOverlay;

    private void Start() {
      // if (_usedGameSave) {
      _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
      // }
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
      _load.onClick.AddListener(LoadAndSetDiceRollData);
      _diceRollButton.onClick.AddListener(SetDiceRollValuesAndIncreaseCount);
      _reset.onClick.AddListener(ResetValuesOfDiceRoll);
      _save.onClick.AddListener(Save);
      ValuesSelectionForQualities.AllValuesSelected += OnAllValuesSelected;
    }

    private void RemoveListener() {
      _load.onClick.RemoveListener(LoadAndSetDiceRollData);
      _diceRollButton.onClick.RemoveListener(SetDiceRollValuesAndIncreaseCount);
      _reset.onClick.RemoveListener(ResetValuesOfDiceRoll);
      _save.onClick.RemoveListener(Save);
      ValuesSelectionForQualities.AllValuesSelected -= OnAllValuesSelected;
    }

    private void LoadAndSetDiceRollData() {
      if (_usedGameSave) {
        _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
        SetTextsInListWithSave();
        Invoke(nameof(SetTextsInListWithSave), 0.3f);
      }

      _info.text = "Info: values load.";
      _numberOfDiceRoll = _diceRollValuesText.Count - 1;
      _isNumberOfDiceRollOverlay = true;
      AllDiceRollCompleteOrLoad?.Invoke();
    }

    private void SetTextsInListWithSave() {
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = _diceRollData.GetStatsRoll((StatRolls)i).ToString();
      }
    }

    private void SetDiceRollValuesAndIncreaseCount() {
      if (_isNumberOfDiceRollOverlay) {
        return;
      }

      if (AllDiceRollsCompleted()) {
        _info.text += AllDiceAreRolled;
        _isNumberOfDiceRollOverlay = true;
        Save();
        return;
      }

      SetRollValues();
      _numberOfDiceRoll++;
    }

    private void ResetValuesOfDiceRoll() {
      _info.text = "Info: reset.";
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = "0";
        _diceRollData.SetStarsRoll((StatRolls)i, 0);
      }

      _numberOfDiceRoll = 0;
      _isNumberOfDiceRollOverlay = false;
      ResetDiceRoll?.Invoke();
    }

    private void Save() {
      if (_usedGameSave) {
        // GSSSingleton.Instance.SaveInGame();
      }

      AllDiceRollCompleteOrLoad?.Invoke();
      _info.text = "Info: save.";
    }

    private bool AllDiceRollsCompleted() {
      return _numberOfDiceRoll == _diceRollValuesText.Count;
    }

    private void SetRollValues() {
      _info.text = "Info: dice roll.";
      _valuesWithRollOfDice[_numberOfDiceRoll] = _characterCreation.GetSumDiceRollForQuality();
      Debug.Log($"Number dice roll: {(StatRolls)_numberOfDiceRoll}; {_diceRollData.GetDiceRollValues().Length}");
      _diceRollData.SetStarsRoll((StatRolls)_numberOfDiceRoll, _valuesWithRollOfDice[_numberOfDiceRoll]);
      _diceRollValuesText[_numberOfDiceRoll].text = _valuesWithRollOfDice[_numberOfDiceRoll].ToString();
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

  public class DiceRollVisual : MonoBehaviour {
    
  }
}