using System;
using System.Collections.Generic;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CreateCharacter {
  public class ValuesSelectionForQualities : MonoBehaviour {
    private static bool IsStrength(int index) {
      return index == 0;
    }

    private static bool IsAgility(int index) {
      return index == 1;
    }

    private static bool IsConstitution(int index) {
      return index == 2;
    }

    private static bool IsWisdom(int index) {
      return index == 3;
    }

    private static bool IsCourage(int index) {
      return index == 4;
    }

    private static int SetIndexOfQualityAtIndexFromButtonLockArray(int index) {
      return index;
    }

    private static void AssignDiceRollValueToStringForQualityText(TMP_Text quality, int diceRollValue) {
      quality.text = diceRollValue.ToString();
    }

    private static bool IsOneInteractableButton(int count) {
      return count == 1;
    }

    private static int IncreaseNumberOfInteractiveButtons(int count) {
      count++;
      return count;
    }

    private static bool IsButtonInteractable(Button button) {
      return button.interactable;
    }

    public static event Action AllValuesSelected;
    public static event Action SaveQualities;
    public static event Action DistributeValues;

    [SerializeField]
    private List<TMP_Text> _valuesForQualityText;
    [SerializeField]
    private Button[] _nextPreviousStrength;
    [SerializeField]
    private Button[] _nextPreviousAgility;
    [SerializeField]
    private Button[] _nextPreviousConstitution;
    [SerializeField]
    private Button[] _nextPreviousWisdom;
    [SerializeField]
    private Button[] _nextPreviousCourage;
    [FormerlySerializedAs("_buttonLocks"), SerializeField]
    private Button[] _buttonAccept;
    [SerializeField]
    private Button _buttonDistribute;
    [FormerlySerializedAs("_usedQualitiesData"), SerializeField]
    private bool _useGameSave;
    [Inject]
    private QualitiesAfterDistributing _qualitiesAfterDistributing;
    private IDiceRoll _diceRollData;
    private IQualityPoints _qualityPoints;

    private int[] _qualities;
    private bool[] _isValueNotSelected;
    private int[] _valuesFromDiceRollData;
    private int[] _indexOfCurrentValueInQualityText;
    private int _indexOfCurrentValue = -1;
    private int _indexOfQuality;
    private bool? _isNext;
    private int _indexOfQualityText = -1;

    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
      _isValueNotSelected = new bool[_diceRollData.GetDiceRollValues().Length];
      _qualities = new int[_diceRollData.GetDiceRollValues().Length];
      _indexOfCurrentValueInQualityText = new int[_diceRollData.GetDiceRollValues().Length];
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    private void SetIndexOfQualityText(int index) {
      _indexOfQualityText = index;
    }

    private void AddListener() {
      AddListenersForNextPreviousButtons();
      AddListenerForButtonAccept();
      AddListenerForButtonDistribute();
      DiceRoll.AllDiceRollCompleteOrLoad += OnAllDiceRollCompleteOrLoad;
      DiceRoll.ResetDiceRoll += OnResetDiceRoll;
    }

    private void AddListenersForNextPreviousButtons() {
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        int index = i;
        if (IsStrength(i)) {
          _nextPreviousStrength[0].onClick.AddListener(() => { SetHandlersForNextArrowsOfSelection(index); });
          _nextPreviousStrength[1].onClick.AddListener(() => { SetHandlersForPreviousArrowsOfSelection(index); });
          continue;
        }

        if (IsAgility(i)) {
          _nextPreviousAgility[0].onClick.AddListener(() => { SetHandlersForNextArrowsOfSelection(index); });
          _nextPreviousAgility[1].onClick.AddListener(() => { SetHandlersForPreviousArrowsOfSelection(index); });
          continue;
        }

        if (IsConstitution(i)) {
          _nextPreviousConstitution[0].onClick.AddListener(() => { SetHandlersForNextArrowsOfSelection(index); });
          _nextPreviousConstitution[1].onClick.AddListener(() => { SetHandlersForPreviousArrowsOfSelection(index); });
          continue;
        }

        if (IsWisdom(i)) {
          _nextPreviousWisdom[0].onClick.AddListener(() => { SetHandlersForNextArrowsOfSelection(index); });
          _nextPreviousWisdom[1].onClick.AddListener(() => { SetHandlersForPreviousArrowsOfSelection(index); });
          continue;
        }

        if (IsCourage(i)) {
          _nextPreviousCourage[0].onClick.AddListener(() => { SetHandlersForNextArrowsOfSelection(index); });
          _nextPreviousCourage[1].onClick.AddListener(() => { SetHandlersForPreviousArrowsOfSelection(index); });
        }
      }
    }

    private void AddListenerForButtonAccept() {
      for (var i = 0; i < _buttonAccept.Length; i++) {
        int index = i;
        if (IsStrength(i)) {
          _buttonAccept[i].onClick.AddListener(() => { DisableButtonsInteractable(_nextPreviousStrength); });
        }

        if (IsAgility(i)) {
          _buttonAccept[i].onClick.AddListener(() => { DisableButtonsInteractable(_nextPreviousAgility); });
        }

        if (IsConstitution(i)) {
          _buttonAccept[i].onClick.AddListener(() => { DisableButtonsInteractable(_nextPreviousConstitution); });
        }

        if (IsWisdom(i)) {
          _buttonAccept[i].onClick.AddListener(() => { DisableButtonsInteractable(_nextPreviousWisdom); });
        }

        if (IsCourage(i)) {
          _buttonAccept[i].onClick.AddListener(() => { DisableButtonsInteractable(_nextPreviousCourage); });
        }

        _buttonAccept[i].onClick.AddListener(() => {
                                               SetBooleanForIsValueSelectedArray(_buttonAccept[index]);
                                               FindIndexForQuality(_buttonAccept[index]);
                                               SaveSelectionValue(_buttonAccept[index]);
                                               DisableButtonInteractable(_buttonAccept[index]);
                                               SetValueForAllText();
                                             });
      }
    }

    private void AddListenerForButtonDistribute() {
      _buttonDistribute.onClick.AddListener(() => {
                                              EnableButtonsInteractable(_nextPreviousStrength);
                                              EnableButtonsInteractable(_nextPreviousAgility);
                                              EnableButtonsInteractable(_nextPreviousConstitution);
                                              EnableButtonsInteractable(_nextPreviousWisdom);
                                              EnableButtonsInteractable(_nextPreviousCourage);
                                              EnableButtonsInteractable(_buttonAccept);
                                              InitLengthAndCopyValuesForValuesFromDiceRollDataArray();
                                              SetFalseForAllIsValueNotSelectedArray();
                                              SetFirstTimeDiceRollValuesForQualityText();
                                              DistributeValues?.Invoke();
                                            });
    }

    private void RemoveListeners() {
      RemoveAllListenersForNextPreviousButtons();
      RemoveAllListenersForButtonAccept();
      RemoveAllListenerForButtonDistribute();
      DiceRoll.AllDiceRollCompleteOrLoad -= OnAllDiceRollCompleteOrLoad;
      DiceRoll.ResetDiceRoll -= OnResetDiceRoll;
    }

    private void RemoveAllListenersForNextPreviousButtons() {
      for (var i = 0; i < _nextPreviousStrength.Length; i++) {
        _nextPreviousStrength[i].onClick.RemoveAllListeners();
        _nextPreviousAgility[i].onClick.RemoveAllListeners();
        _nextPreviousConstitution[i].onClick.RemoveAllListeners();
        _nextPreviousWisdom[i].onClick.RemoveAllListeners();
        _nextPreviousCourage[i].onClick.RemoveAllListeners();
      }
    }

    private void RemoveAllListenersForButtonAccept() {
      foreach (Button buttonAccept in _buttonAccept) {
        buttonAccept.onClick.RemoveAllListeners();
      }
    }

    private void RemoveAllListenerForButtonDistribute() {
      _buttonDistribute.onClick.RemoveAllListeners();
    }

    private void SetBooleanForIsValueSelectedArray(Button buttonLock) {
      int indexOfCurrentValue = -1;
      for (var i = 0; i < _buttonAccept.Length; i++) {
        if (IsTargetButtonLock(buttonLock, i)) {
          indexOfCurrentValue = GetIndexOfCurrentValueInQualityText(i);
          break;
        }
      }

      SetTrueForIsValueNotSelectedArrayAtIndexOfCurrentValueInQualityText(indexOfCurrentValue);
    }

    private bool IsTargetButtonLock(Button targetButtonLock, int index) {
      return _buttonAccept[index] == targetButtonLock;
    }

    private int GetIndexOfCurrentValueInQualityText(int index) {
      return _indexOfCurrentValueInQualityText[index];
    }

    private void SetTrueForIsValueNotSelectedArrayAtIndexOfCurrentValueInQualityText(int indexOfCurrentValue) {
      _isValueNotSelected[indexOfCurrentValue] = true;
    }

    private void OnAllDiceRollCompleteOrLoad() {
      EnableInteractableForButtonDistribute();
    }

    private void EnableInteractableForButtonDistribute() {
      _buttonDistribute.interactable = true;
    }

    private void OnResetDiceRoll() {
      DisableInteractableForButtonDistributeAndAllButtonsToo();
    }

    private void DisableInteractableForButtonDistributeAndAllButtonsToo() {
      _buttonDistribute.interactable = false;
      DisableButtonsInteractable(_nextPreviousStrength);
      DisableButtonsInteractable(_nextPreviousAgility);
      DisableButtonsInteractable(_nextPreviousConstitution);
      DisableButtonsInteractable(_nextPreviousWisdom);
      DisableButtonsInteractable(_nextPreviousCourage);
      DisableButtonsInteractable(_buttonAccept);
    }

    private void SetFalseForAllIsValueNotSelectedArray() {
      for (var i = 0; i < _isValueNotSelected.Length; i++) {
        _isValueNotSelected[i] = false;
      }
    }

    private void InitLengthAndCopyValuesForValuesFromDiceRollDataArray() {
      if (!IsArrayNull()) {
        return;
      }

      _valuesFromDiceRollData = new int[_diceRollData.GetDiceRollValues().Length];
      _diceRollData.GetDiceRollValues().CopyTo(_valuesFromDiceRollData, 0);
    }

    private void SetFirstTimeDiceRollValuesForQualityText() {
      for (var i = 0; i < _valuesFromDiceRollData.Length; i++) {
        SetFirstTimeTextForQuality(_valuesForQualityText[i], _valuesFromDiceRollData[i]);
      }
    }

    private void SetFirstTimeTextForQuality(TMP_Text quality, int diceRollValue) {
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        if (IsTargetQualityText(quality, i)) {
          SetFirstTimeIndexOfCurrentValueAtIndexOfCurrentValueInQualityTextArrayForIndex(i);
          break;
        }
      }

      AssignDiceRollValueToStringForQualityText(quality, diceRollValue);
    }

    private void SetFirstTimeIndexOfCurrentValueAtIndexOfCurrentValueInQualityTextArrayForIndex(int index) {
      _indexOfCurrentValueInQualityText[index] = index;
      AssignFirstTimeZeroForIndexOfCurrentValue();
    }

    private void AssignFirstTimeZeroForIndexOfCurrentValue() {
      _indexOfCurrentValue = 0;
    }

    private void SetHandlersForNextArrowsOfSelection(int index) {
      NextValue(_valuesForQualityText[index]);
    }

    private void SetHandlersForPreviousArrowsOfSelection(int index) {
      PreviousValue(_valuesForQualityText[index]);
    }

    private void FindIndexForQuality(Button button) {
      for (var i = 0; i < _buttonAccept.Length; i++) {
        if (IsTargetButtonLock(button, i)) {
          _indexOfQuality = SetIndexOfQualityAtIndexFromButtonLockArray(i);
          return;
        }
      }
    }

    private void SetValueForAllText() {
      if (AllValuesIsSelect()) {
        return;
      }

      MovePointerForwardAndCheckIndexAndCanGetValueFromArray(out int value);
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        if (IsButtonNotInteractable(i)) {
          continue;
        }

        SetTextForQuality(_valuesForQualityText[i], value);
      }
    }

    private bool IsButtonNotInteractable(int index) {
      return _buttonAccept[index].interactable == false;
    }

    private void MovePointerForwardAndCheckIndexAndCanGetValueFromArray(out int diceRollValue) {
      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        diceRollValue = GetValueForQualityForIndexOfCurrentValue();
      } else {
        MovePointerForward();
        MovePointerForwardAndCheckIndexAndCanGetValueFromArray(out diceRollValue);
      }
    }

    private void NextValue(TMP_Text qualityValue) {
      if (ThisIsDifferentDirectionOfPointerMovementAndDifferentQualityText(true, qualityValue)) {
        _indexOfCurrentValue = GetIndexOfCurrentValueInQualityTextAtText(qualityValue);
      }

      MovePointerForward();
      if (IsNextNull()) {
        SetIsNextTrue();
      }

      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        int diceRollValue = GetValueForQualityForIndexOfCurrentValue();
        SetTextForQuality(qualityValue, diceRollValue);
        SetIsNextTrue();
        SetIndexOfQualityText(GetIndexOfQualityText(qualityValue));
      } else {
        SetIsNextTrue();
        NextValue(qualityValue);
      }
    }

    private void PreviousValue(TMP_Text qualityValue) {
      if (ThisIsDifferentDirectionOfPointerMovementAndDifferentQualityText(false, qualityValue)) {
        _indexOfCurrentValue = GetIndexOfCurrentValueInQualityTextAtText(qualityValue);
      }

      MovePointerToBack();
      if (IsNextNull()) {
        SetIsNextFalse();
      }

      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        int diceRollValue = GetValueForQualityForIndexOfCurrentValue();
        SetTextForQuality(qualityValue, diceRollValue);
        SetIsNextFalse();
        SetIndexOfQualityText(GetIndexOfQualityText(qualityValue));
      } else {
        SetIsNextFalse();
        PreviousValue(qualityValue);
      }
    }

    private bool ThisIsDifferentDirectionOfPointerMovementAndDifferentQualityText(bool aIsNext, TMP_Text quality) {
      int index = GetIndexOfQualityText(quality);
      if (_isNext != aIsNext && index != _indexOfQualityText) {
        return true;
      }

      return false;
    }

    private int GetIndexOfCurrentValueInQualityTextAtText(TMP_Text quality) {
      return GetIndexOfCurrentValueInQualityText(GetIndexOfQualityText(quality));
    }

    private bool IsNextNull() {
      bool isNull = _isNext == null;
      return isNull;
    }

    private void SetIsNextTrue() {
      _isNext = true;
    }

    private void SetIsNextFalse() {
      _isNext = false;
    }

    private bool IsArrayNull() {
      return _valuesFromDiceRollData == null;
    }

    private void MovePointerForward() {
      _indexOfCurrentValue++;
      if (GreaterThanLastIndexOfArray(_indexOfCurrentValue, _qualities)) {
        SetZeroForIndexOfCurrentValue();
      }
    }

    private void MovePointerToBack() {
      _indexOfCurrentValue--;
      if (LessThanFistIndexOfArray(_indexOfCurrentValue)) {
        SetLengthMinusOneForIndexOfCurrentValue(_valuesFromDiceRollData);
      }
    }

    private int GetValueForQualityForIndexOfCurrentValue() {
      return _valuesFromDiceRollData[_indexOfCurrentValue];
    }

    private void SetTextForQuality(TMP_Text quality, int diceRollValue) {
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        if (IsTargetQualityText(quality, i)) {
          SetIndexOfCurrentValueAtIndexOfCurrentValueInQualityTextArrayForIndex(i);
          break;
        }
      }

      AssignDiceRollValueToStringForQualityText(quality, diceRollValue);
    }

    private bool IsTargetQualityText(TMP_Text quality, int index) {
      return _valuesForQualityText[index] == quality;
    }

    private void SetIndexOfCurrentValueAtIndexOfCurrentValueInQualityTextArrayForIndex(int index) {
      _indexOfCurrentValueInQualityText[index] = _indexOfCurrentValue;
    }

    private bool CanUseCurrentValueAtIndexOfCurrentValue() {
      return !_isValueNotSelected[_indexOfCurrentValue];
    }

    private void SetZeroForIndexOfCurrentValue() {
      _indexOfCurrentValue = 0;
    }

    private void SetLengthMinusOneForIndexOfCurrentValue(int[] array) {
      _indexOfCurrentValue = array.Length - 1;
    }

    private bool GreaterThanLastIndexOfArray(int index, int[] array) {
      bool greater = index >= array.Length;
      return greater;
    }

    private bool LessThanFistIndexOfArray(int index) {
      return index < 0;
    }

    private bool ThereIsOnlyOneInteractiveButtonLockLeft() {
      var count = 0;
      foreach (Button button in _buttonAccept) {
        if (IsButtonInteractable(button)) {
          count = IncreaseNumberOfInteractiveButtons(count);
        }
      }

      if (IsOneInteractableButton(count)) {
        return true;
      }

      return false;
    }

    private int GetLastButtonLockIndex() {
      for (var i = 0; i < _buttonAccept.Length; i++) {
        if (IsButtonInteractable(_buttonAccept[i])) {
          return i;
        }
      }

      return -1;
    }

    private void GetLastIndexOfCurrentValue() {
      for (var i = 0; i < _isValueNotSelected.Length; i++) {
        if (IsValueNotSelectedArrayAtIndexEqualFalse(i)) {
          _indexOfCurrentValue = i;
          return;
        }
      }
    }

    private bool IsValueNotSelectedArrayAtIndexEqualFalse(int index) {
      return _isValueNotSelected[index] == false;
    }

    private void DisableAllArrows() {
      DisableButtonsInteractable(_nextPreviousStrength);
      DisableButtonsInteractable(_nextPreviousAgility);
      DisableButtonsInteractable(_nextPreviousConstitution);
      DisableButtonsInteractable(_nextPreviousWisdom);
      DisableButtonsInteractable(_nextPreviousCourage);
    }

    private bool AllValuesIsSelect() {
      for (var i = 0; i < _isValueNotSelected.Length; i++) {
        if (IsValueNotSelectedArrayAtIndexEqualFalse(i)) {
          return false;
        }
      }

      AllSelectedLog();
      AllValuesSelected?.Invoke();
      SetAndSaveQualitiesAfterDistributing();
      return true;
    }

    private void AllSelectedLog() {
      Debug.Log("All selected");
      Debug.Log($"{_qualities[0]} \t" + $"{_qualities[1]} \t" + $"{_qualities[2]} \t" + $"{_qualities[3]} \t" + $"{_qualities[4]} \t");
    }

    private void SaveSelectionValue(Button buttonLock) {
      int indexOfCurrentValue = -1;
      for (var i = 0; i < _buttonAccept.Length; i++) {
        if (_buttonAccept[i] == buttonLock) {
          indexOfCurrentValue = _indexOfCurrentValueInQualityText[i];
          break;
        }
      }

      _qualities[_indexOfQuality] = _valuesFromDiceRollData[indexOfCurrentValue];
    }

    private void SetAndSaveQualitiesAfterDistributing() {
      if (_useGameSave) {
        var index = 0;
        _qualityPoints.SetQualityPoints(QualityType.Strength, _qualities[index++]);
        _qualityPoints.SetQualityPoints(QualityType.Agility, _qualities[index++]);
        _qualityPoints.SetQualityPoints(QualityType.Constitution, _qualities[index++]);
        _qualityPoints.SetQualityPoints(QualityType.Wisdom, _qualities[index++]);
        _qualityPoints.SetQualityPoints(QualityType.Courage, _qualities[index]);
      } else {
        InitQualitiesAfterDistributingValuesArray();
        for (var i = 0; i < _qualities.Length; i++) {
          _qualitiesAfterDistributing.Values[i] = _qualities[i];
        }
      }

      SaveQualitiesAfterDistributing();
      SaveQualities?.Invoke();
    }

    private void InitQualitiesAfterDistributingValuesArray() {
      _qualitiesAfterDistributing.Values = new int[_qualities.Length];
    }

    private void SaveQualitiesAfterDistributing() {
      if (_useGameSave) {
        // GSSSingleton.Instance.SaveInGame();
      }
    }

    private void EnableButtonsInteractable(Button[] buttons) {
      foreach (Button button in buttons) {
        button.interactable = true;
      }
    }

    private void DisableButtonsInteractable(Button[] buttons) {
      foreach (Button button in buttons) {
        button.interactable = false;
      }
    }

    private void DisableButtonInteractable(Button button) {
      button.interactable = false;
    }

    private int GetIndexOfQualityText(TMP_Text quality) {
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        if (IsTargetQualityText(quality, i)) {
          return i;
        }
      }

      return -1;
    }

    // ReSharper disable once UnusedMember.Local
    private int GetIndexOfButtonLock(Button button) {
      for (var i = 0; i < _buttonAccept.Length; i++) {
        if (_buttonAccept[i] == button) {
          return i;
        }
      }

      return -1;
    }

    // ReSharper disable once UnusedMember.Local
    private bool ThereIsTwoInteractiveButtonLockLeft() {
      var count = 0;
      foreach (Button button in _buttonAccept) {
        if (button.interactable) {
          count++;
        }
      }

      if (count == 2) {
        return true;
      }

      return false;
    }

    // ReSharper disable once UnusedMember.Local
    private void SetLastValueForQuality() {
      if (ThereIsOnlyOneInteractiveButtonLockLeft()) {
        int lastButtonLockIndex = GetLastButtonLockIndex();
        if (lastButtonLockIndex == -1) {
          Debug.LogError("Last Index -1");
          return;
        }

        GetLastIndexOfCurrentValue();
        int diceRollValue = GetValueForQualityForIndexOfCurrentValue();
        SetTextForQuality(_valuesForQualityText[lastButtonLockIndex], diceRollValue);
        DisableButtonInteractable(_buttonAccept[lastButtonLockIndex]);
        DisableAllArrows();
        AllValuesIsSelect();
      }
    }

    // ReSharper disable once UnusedMember.Local
    private void Check() {
      _indexOfCurrentValue++;
      if (GreaterThanLastIndexOfArray(_indexOfCurrentValue, _qualities)) {
        SetZeroForIndexOfCurrentValue();
      }

      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        return;
      }

      if (AllValuesIsSelect()) {
        return;
      }

      Check();
    }

    // ReSharper disable once UnusedMember.Local
    private void SetIndexOfText(TMP_Text quality) {
      for (var i = 0; i < _valuesForQualityText.Count; i++) {
        if (_valuesForQualityText[i] == quality) {
          _indexOfQualityText = i;
          break;
        }
      }
    }

    // ReSharper disable once UnusedMember.Local
    private void SetIndexOfCurrentValue(int index) {
      _indexOfCurrentValue = index;
    }
  }

  [Serializable]
  public class QualitiesAfterDistributing {
    [FormerlySerializedAs("values")]
    public int[] Values;
  }
}