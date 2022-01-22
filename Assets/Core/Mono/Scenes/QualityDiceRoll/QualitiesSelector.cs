using System;
using System.Threading.Tasks;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс отвечает за распределине значений бросков костей для качеств персонажа.
  /// </summary>
  public class QualitiesSelector : MonoBehaviour {
    private static bool IsLastRemainingValue(int numberOfSelectedValues) {
      return numberOfSelectedValues == QualityTypeHandler.NUMBER_OF_QUALITY - 1;
    }

    private const int StartValueForQuality = 0;
    public event Action AllValuesSelected;
    public event Action DistributeValues;
    [SerializeField]
    private QualitiesVisualContainer _visualContainer;
    [SerializeField]
    private Button _buttonDistribute;
    private IDiceRoll _diceRollData;
    private IQualityPoints _qualityPoints;

    private int[] _qualities;
    private bool[] _isValueSelected;
    private int[] _valuesFromDiceRollData;
    private int[] _indexOfCurrentValueInQualityText;
    private int _indexOfCurrentValue = -1;
    private bool? _isNext;
    private int _indexOfQualityText = -1;
    private bool _progress;

    
    [Inject]
    public void Constructor(IDiceRoll diceRollData, IQualityPoints qualityPoints) {
      _diceRollData = diceRollData;
      _qualityPoints = qualityPoints;
    }

    private void Start() {
      Init();
      DisableButtons();
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    public void EnableDistribute() {
      EnableButtonDistribute();
    }

    public void DisableUI() {
      DisableButtons();
    }

    private void Init() {
      _isValueSelected = new bool[QualityTypeHandler.NUMBER_OF_QUALITY];
      _qualities = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
      _indexOfCurrentValueInQualityText = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
    }

    private void AddListener() {
      AddListenersForVisualContainer();
      AddListenerForButtonDistribute();
    }

    private void AddListenersForVisualContainer() {
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        int index = i;
        _visualContainer[(QualityType)index].AddNextPreviousListener(() => NextAction(index), () => PreviousAction(index));
      }

      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        int index = i;

        _visualContainer[(QualityType)i].AddAcceptListener(() => {
                                                             ValuesNotSelected(index);
                                                             SaveSelectionValue(index);
                                                             DisableAcceptButton(index);
                                                             SetValueForAllText();
                                                             LastQualityValue();
                                                           });
      }
    }

    private void AddListenerForButtonDistribute() {
      _buttonDistribute.onClick.AddListener(() => {
                                              _visualContainer.EnableButtons();
                                              InitValuesFromDiceRollData();
                                              ValuesSelected();
                                              InitQualityTexts();
                                              ResetAfterDistribute();
                                              DistributeValues?.Invoke();
                                            });
    }

    private void RemoveListeners() {
      RemoveListenersForVisualContainer();
      RemoveAllListenerForButtonDistribute();
    }

    private void RemoveListenersForVisualContainer() {
      _visualContainer.RemoveListeners();
    }

    private void RemoveAllListenerForButtonDistribute() {
      _buttonDistribute.onClick.RemoveAllListeners();
    }

    private void ResetAfterDistribute() {
      _indexOfCurrentValue = 0;
      _indexOfQualityText = -1;
      _qualities = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
    }

    private void LastQualityValue() {
      var numberOfSelectedValues = 0;
      for (var i = 0; i < _isValueSelected.Length; i++) {
        if (_isValueSelected[i]) {
          numberOfSelectedValues++;
        }
      }

      if (AllValuesIsSelect()) {
        return;
      }

      if (IsLastRemainingValue(numberOfSelectedValues)) {
        SetLastQuality();
      }
    }

    private async void SetLastQuality() {
      while (_progress) {
        await Task.Yield();
      }

      int index = _qualities.IndexOf(StartValueForQuality);
      ValuesNotSelected(index);
      SaveSelectionValue(index);
      DisableAcceptButton(index);
      DisableNextPrevious(index);
      SetValueForAllText();
    }

    private void ValuesNotSelected(int index) {
      _progress = true;
      int indexInQualityText = _indexOfCurrentValueInQualityText[index];
      _isValueSelected[indexInQualityText] = true;
    }

    private void EnableButtonDistribute() {
      _buttonDistribute.interactable = true;
    }

    private void DisableButtons() {
      _buttonDistribute.interactable = false;
      _visualContainer.DisableButtons();
    }

    private void ValuesSelected() {
      for (var i = 0; i < _isValueSelected.Length; i++) {
        _isValueSelected[i] = false;
      }
    }

    private void InitValuesFromDiceRollData() {
      _valuesFromDiceRollData = new int[QualityTypeHandler.NUMBER_OF_QUALITY];
      _diceRollData.GetDiceRollValues().CopyTo(_valuesFromDiceRollData, 0);
    }

    private void InitQualityTexts() {
      for (var i = 0; i < _valuesFromDiceRollData.Length; i++) {
        InitTextForQuality(i, _valuesFromDiceRollData[i]);
      }
    }

    private void InitTextForQuality(int index, int diceRollValue) {
      InitIndexOfCurrentValueAt(index);

      SetQualityText(index, diceRollValue);
    }

    private void SetQualityText(int index, int diceRollValue) {
      _visualContainer[(QualityType)index].SetQualityText(diceRollValue);
    }

    private void InitIndexOfCurrentValueAt(int index) {
      _indexOfCurrentValueInQualityText[index] = index;
      _indexOfCurrentValue = 0;
    }

    private void NextAction(int index) {
      NextValue(index);
    }

    private void PreviousAction(int index) {
      PreviousValue(index);
    }

    private void SetValueForAllText() {
      if (AllValuesIsSelect()) {
        return;
      }

      MovePointerForward(out int value);
      for (var i = 0; i < QualityTypeHandler.NUMBER_OF_QUALITY; i++) {
        if (IsButtonNotInteractable(i)) {
          continue;
        }

        SetTextForQuality(i, value);
      }

      _progress = false;
    }

    private bool IsButtonNotInteractable(int index) {
      return _visualContainer[(QualityType)index].IsAcceptEnable() == false;
    }

    private void MovePointerForward(out int diceRollValue) {
      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        diceRollValue = GetValueForQuality();
      } else {
        MovePointerForward();
        MovePointerForward(out diceRollValue);
      }
    }

    private void NextValue(int index) {
      if (IsAnotherDirection(true, index)) {
        _indexOfCurrentValue = GetIndexOfCurrentValueInQualityText(index);
      }

      MovePointerForward();
      if (IsNextNull()) {
        IsNext();
      }

      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        int diceRollValue = GetValueForQuality();
        SetTextForQuality(index, diceRollValue);
        IsNext();
        SetIndexOfQualityText(index);
      } else {
        IsNext();
        NextValue(index);
      }
    }

    private void PreviousValue(int index) {
      if (IsAnotherDirection(false, index)) {
        _indexOfCurrentValue = GetIndexOfCurrentValueInQualityText(index);
      }

      MovePointerToBack();
      if (IsNextNull()) {
        IsPrevious();
      }

      if (CanUseCurrentValueAtIndexOfCurrentValue()) {
        int diceRollValue = GetValueForQuality();
        SetTextForQuality(index, diceRollValue);
        IsPrevious();
        SetIndexOfQualityText(index);
      } else {
        IsPrevious();
        PreviousValue(index);
      }
    }

    private int GetIndexOfCurrentValueInQualityText(int index) {
      return _indexOfCurrentValueInQualityText[index];
    }

    private void SetIndexOfQualityText(int index) {
      _indexOfQualityText = index;
    }

    private bool IsAnotherDirection(bool aIsNext, int index) {
      return _isNext != aIsNext && index != _indexOfQualityText;
    }

    private bool IsNextNull() {
      bool isNull = _isNext == null;
      return isNull;
    }

    private void IsNext() {
      _isNext = true;
    }

    private void IsPrevious() {
      _isNext = false;
    }

    private void MovePointerForward() {
      _indexOfCurrentValue++;
      if (GreaterThanLastIndexOfArray(_indexOfCurrentValue, QualityTypeHandler.NUMBER_OF_QUALITY)) {
        SetZeroForIndexOfCurrentValue();
      }
    }

    private void MovePointerToBack() {
      _indexOfCurrentValue--;
      if (LessThanFistIndexOfArray(_indexOfCurrentValue)) {
        SetNewIndexOfCurrentValue(QualityTypeHandler.NUMBER_OF_QUALITY);
      }
    }

    private int GetValueForQuality() {
      return _valuesFromDiceRollData[_indexOfCurrentValue];
    }

    private void SetTextForQuality(int index, int diceRollValue) {
      SetIndexForQualitiesTexts(index);
      SetQualityText(index, diceRollValue);
    }

    private void SetIndexForQualitiesTexts(int index) {
      _indexOfCurrentValueInQualityText[index] = _indexOfCurrentValue;
    }

    private bool CanUseCurrentValueAtIndexOfCurrentValue() {
      return !_isValueSelected[_indexOfCurrentValue];
    }

    private void SetZeroForIndexOfCurrentValue() {
      _indexOfCurrentValue = 0;
    }

    private void SetNewIndexOfCurrentValue(int numberOfQuality) {
      _indexOfCurrentValue = numberOfQuality - 1;
    }

    private bool GreaterThanLastIndexOfArray(int index, int maxIndex) {
      bool greater = index >= maxIndex;
      return greater;
    }

    private bool LessThanFistIndexOfArray(int index) {
      return index < 0;
    }

    private bool IsValueNotSelected(int index) {
      return _isValueSelected[index] == false;
    }

    private bool AllValuesIsSelect() {
      for (var i = 0; i < _isValueSelected.Length; i++) {
        if (IsValueNotSelected(i)) {
          return false;
        }
      }

      AllValuesSelected?.Invoke();
      SaveQualities();
      return true;
    }

    private void SaveSelectionValue(int index) {
      int indexOfCurrentValue = _indexOfCurrentValueInQualityText[index];
      _qualities[index] = _valuesFromDiceRollData[indexOfCurrentValue];
    }

    private void SaveQualities() {
      var index = 0;
      _qualityPoints.SetQualityPoints(QualityType.Strength, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Agility, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Constitution, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Wisdom, _qualities[index++]);
      _qualityPoints.SetQualityPoints(QualityType.Courage, _qualities[index]);
    }

    private void DisableAcceptButton(int index) {
      _visualContainer[(QualityType)index].DisableAcceptButton();
    }

    private void DisableNextPrevious(int index) {
      _visualContainer[(QualityType)index].DisableButtons();
    }
  }
}