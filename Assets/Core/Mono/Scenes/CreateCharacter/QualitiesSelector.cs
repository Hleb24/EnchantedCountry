using System;
using System.Collections.Generic;
using Core.Mono.Scenes.SelectionClass;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.CreateCharacter {
  public class QualitiesSelector : MonoBehaviour {
    private static int SetIndexOfQualityAtIndexFromButtonLockArray(int index) {
      return index;
    }

    private static void AssignDiceRollValueToStringForQualityText(TMP_Text quality, int diceRollValue) {
      quality.text = diceRollValue.ToString();
    }

    public static event Action AllValuesSelected;
    public static event Action SaveQualities;
    public static event Action DistributeValues;
    [SerializeField]
    private List<QualityVisual> _qualityVisuals;
    [SerializeField]
    private Button _buttonDistribute;
    private IDiceRoll _diceRollData;
    private IQualityPoints _qualityPoints;
    private QualitiesVisualContainer _visualContainer;

    private int[] _qualities;
    private bool[] _isValueNotSelected;
    private int[] _valuesFromDiceRollData;
    private int[] _indexOfCurrentValueInQualityText;
    private int _indexOfCurrentValue = -1;
    private int _indexOfQuality;
    private bool? _isNext;
    private int _indexOfQualityText = -1;

    private void Awake() {
      _visualContainer = new QualitiesVisualContainer(_qualityVisuals);
    }

    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _diceRollData = ScribeDealer.Peek<DiceRollScribe>();
      _isValueNotSelected = new bool[_diceRollData.GetDiceRollValues().Length];
      _qualities = new int[_diceRollData.GetDiceRollValues().Length];
      _indexOfCurrentValueInQualityText = new int[_diceRollData.GetDiceRollValues().Length];
      DisableButtons();
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
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        int index = i;
        _visualContainer[(QualityType)index].AddNextPreviousListener(() => NextAction(index), () => PreviousAction(index));
      }
    }

    private void AddListenerForButtonAccept() {
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        int index = i;

        _visualContainer[(QualityType)i].AddAcceptListener(() => {
                                                             SetBooleanForIsValueSelectedArray(_visualContainer[(QualityType)index].GetAcceptButton());
                                                             FindIndexForQuality(_visualContainer[(QualityType)index].GetAcceptButton());
                                                             SaveSelectionValue(_visualContainer[(QualityType)index].GetAcceptButton());
                                                             DisableButtonInteractable(_visualContainer[(QualityType)index].GetAcceptButton());
                                                             SetValueForAllText();
                                                           });
      }
    }

    private void AddListenerForButtonDistribute() {
      _buttonDistribute.onClick.AddListener(() => {
                                              _visualContainer.EnableButtons();
                                              InitLengthAndCopyValuesForValuesFromDiceRollDataArray();
                                              SetFalseForAllIsValueNotSelectedArray();
                                              SetFirstTimeDiceRollValuesForQualityText();
                                              DistributeValues?.Invoke();
                                            });
    }

    private void RemoveListeners() {
      RemoveListenersForVisualContainer();
      RemoveAllListenerForButtonDistribute();
      DiceRoll.AllDiceRollCompleteOrLoad -= OnAllDiceRollCompleteOrLoad;
      DiceRoll.ResetDiceRoll -= OnResetDiceRoll;
    }

    private void RemoveListenersForVisualContainer() {
      _visualContainer.RemoveListeners();
    }

    private void RemoveAllListenerForButtonDistribute() {
      _buttonDistribute.onClick.RemoveAllListeners();
    }

    private void SetBooleanForIsValueSelectedArray(Button buttonLock) {
      int indexOfCurrentValue = -1;
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (IsTargetButtonLock(buttonLock, i)) {
          indexOfCurrentValue = GetIndexOfCurrentValueInQualityText(i);
          break;
        }
      }

      SetTrueForIsValueNotSelectedArrayAtIndexOfCurrentValueInQualityText(indexOfCurrentValue);
    }

    private bool IsTargetButtonLock(Button targetButtonLock, int index) {
      return _visualContainer[(QualityType)index].GetAcceptButton() == targetButtonLock;
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
      DisableButtons();
    }

    private void DisableButtons() {
      _buttonDistribute.interactable = false;
      _visualContainer.DisableButtons();
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
        SetFirstTimeTextForQuality(_visualContainer[(QualityType)i].GetQualityValueText(), _valuesFromDiceRollData[i]);
      }
    }

    private void SetFirstTimeTextForQuality(TMP_Text quality, int diceRollValue) {
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
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

    private void NextAction(int index) {
      NextValue(_visualContainer[(QualityType)index].GetQualityValueText());
    }

    private void PreviousAction(int index) {
      PreviousValue(_visualContainer[(QualityType)index].GetQualityValueText());
    }

    private void FindIndexForQuality(Button button) {
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
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
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (IsButtonNotInteractable(i)) {
          continue;
        }

        SetTextForQuality(_visualContainer[(QualityType)i].GetQualityValueText(), value);
      }
    }

    private bool IsButtonNotInteractable(int index) {
      return _visualContainer[(QualityType)index].IsAcceptEnable() == false;
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
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (IsTargetQualityText(quality, i)) {
          SetIndexOfCurrentValueAtIndexOfCurrentValueInQualityTextArrayForIndex(i);
          break;
        }
      }

      AssignDiceRollValueToStringForQualityText(quality, diceRollValue);
    }

    private bool IsTargetQualityText(TMP_Text quality, int index) {
      return _visualContainer[(QualityType)index].GetQualityValueText() == quality;
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

    private bool IsValueNotSelectedArrayAtIndexEqualFalse(int index) {
      return _isValueNotSelected[index] == false;
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

    private void SaveSelectionValue(Button acceptButton) {
      int indexOfCurrentValue = -1;
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (_visualContainer[(QualityType)i].GetAcceptButton() == acceptButton) {
          indexOfCurrentValue = _indexOfCurrentValueInQualityText[i];
          break;
        }
      }

      _qualities[_indexOfQuality] = _valuesFromDiceRollData[indexOfCurrentValue];
    }

    private void SetAndSaveQualitiesAfterDistributing() {
      var index = 0;
      _qualityPoints.SetQualityPoints(QualityType.Strength, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Agility, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Constitution, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Wisdom, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Courage, _qualities[index]);

      SaveQualities?.Invoke();
    }

    private void DisableButtonInteractable(Button button) {
      button.interactable = false;
    }

    private int GetIndexOfQualityText(TMP_Text quality) {
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (IsTargetQualityText(quality, i)) {
          return i;
        }
      }

      return -1;
    }
  }
}