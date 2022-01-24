using System;
using System.Collections.Generic;
using Core.Mono.MainManagers;
using Core.SO.Mock;
using Core.Support.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class Improvement : MonoBehaviour {
    public event Action SummarizeCompleted;

    [SerializeField]
    private List<TMP_Text> _listOfDiceRollValues;
    [SerializeField]
    protected Button _diceRoll;
    [SerializeField]
    protected QualitiesTexts _finallyQualitiesTexts;
    protected QualityIncrease QualityIncrease;
    private IStartGame _startGame;
    private IQualityPoints _qualityPoints;
    private IQualityPoints _mockQualitiesPoints;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    [Inject]
    public void Constructor(IStartGame startGame, IQualityPoints qualityPoints, QualityIncrease qualityIncrease) {
      _startGame = startGame;
      _qualityPoints = qualityPoints;
      QualityIncrease = qualityIncrease;
    }

    protected virtual void DiceRollsQualityIncrease() { }

    private void GetSum() {
      if (_startGame.UseGameSave()) {
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