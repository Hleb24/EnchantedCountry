using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.CharacterCreation;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter {
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
        _diceRollData = DataDealer.Peek<DiceRollData>();
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
      if (_usedGameSave) {
        return;
      }

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
        _diceRollData = DataDealer.Peek<DiceRollData>();
        SetTextsInListWithSave();
        Invoke(nameof(SetTextsInListWithSave), 0.3f);
      } else {
        SaveSystem.LoadWithInvoke(_diceRollData, SaveSystem.Constants.DiceROllData, (nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetTextsInListWithSave), 0.05f);
      }

      GenericTools.SetUIText(_info, "Info: values load.");
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
        GenericTools.SetUIText(_info, AllDiceAreRolled, true);
        _isNumberOfDiceRollOverlay = true;
        Save();
        return;
      }

      SetRollValues();
      _numberOfDiceRoll++;
    }

    private void ResetValuesOfDiceRoll() {
      GSSSingleton.Instance.SaveInGame();
      GenericTools.SetUIText(_info, "Info: reset.");
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        GenericTools.SetUIText(_diceRollValuesText[i], "0");
        _diceRollData.SetStarsRoll((StatRolls)i, 0);
      }

      _numberOfDiceRoll = 0;
      _isNumberOfDiceRollOverlay = false;
      ResetDiceRoll?.Invoke();
    }

    private void Save() {
      if (_usedGameSave) {
        GSSSingleton.Instance.SaveInGame();
      } else {
        SaveSystem.Save(_diceRollData, SaveSystem.Constants.DiceROllData);
      }

      AllDiceRollCompleteOrLoad?.Invoke();
      GenericTools.SetUIText(_info, "Info: save.");
    }

    private bool AllDiceRollsCompleted() {
      return _numberOfDiceRoll == _diceRollValuesText.Count;
    }

    private void SetRollValues() {
      GenericTools.SetUIText(_info, "Info: dice roll.");
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
}