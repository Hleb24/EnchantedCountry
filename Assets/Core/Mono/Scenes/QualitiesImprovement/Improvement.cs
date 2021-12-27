using System;
using System.Collections.Generic;
using Core.ScriptableObject.Mock;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class Improvement : MonoBehaviour {
    public event Action SummarizeCompleted;

    [SerializeField]
    private List<TMP_Text> _listOfDiceRollValues;
    [SerializeField]
    protected Button _diceRoll;
    [SerializeField]
    protected QualitiesTexts _finallyQualitiesTexts;
    [SerializeField]
    protected bool _useGameSave;
    protected QualityIncrease QualityIncrease;
    private IQualityPoints _qualityPoints;
    private IQualityPoints _mockQualitiesPoints;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      QualityIncrease = new QualityIncrease();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    protected virtual void DiceRollsQualityIncrease() { }

    private void GetSum() {
      if (_useGameSave) {
        _qualityPoints.ChangeQualityPoints(QualityType.Strength, QualityIncrease[0]);
        _qualityPoints.ChangeQualityPoints(QualityType.Agility, QualityIncrease[1]);
        _qualityPoints.ChangeQualityPoints(QualityType.Constitution, QualityIncrease[2]);
        _qualityPoints.ChangeQualityPoints(QualityType.Wisdom, QualityIncrease[3]);
        _qualityPoints.ChangeQualityPoints(QualityType.Courage, QualityIncrease[4]);
      } else {
        _mockQualitiesPoints.ChangeQualityPoints(QualityType.Strength, QualityIncrease[0]);
        _mockQualitiesPoints.ChangeQualityPoints(QualityType.Agility, QualityIncrease[1]);
        _mockQualitiesPoints.ChangeQualityPoints(QualityType.Constitution, QualityIncrease[2]);
        _mockQualitiesPoints.ChangeQualityPoints(QualityType.Wisdom, QualityIncrease[3]);
        _mockQualitiesPoints.ChangeQualityPoints(QualityType.Courage, QualityIncrease[4]);
      }
    }

    private void AddListeners() {
      _diceRoll.onClick.AddListener(GetValueIncreaseAndSetTexts);
    }

    private void RemoveListeners() {
      _diceRoll.onClick.RemoveListener(GetValueIncreaseAndSetTexts);
    }

    private void GetValueIncreaseAndSetTexts() {
      DiceRollsQualityIncrease();
      for (var i = 0; i < _listOfDiceRollValues.Count; i++) {
        _listOfDiceRollValues[i].text = QualityIncrease[i].ToString();
      }

      GetSum();
      _finallyQualitiesTexts.SetQualitiesText();
      SummarizeCompleted?.Invoke();
    }
  }
}