using System;
using System.Collections.Generic;
using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class Improvement : MonoBehaviour {
    private void GetSum() {
      if (_useGameSave) {
        _qualityPoints.ChangeQualityPoints(QualityType.Strength, QualityIncrease[0]);
        _qualityPoints.ChangeQualityPoints(QualityType.Agility, QualityIncrease[1]);
        _qualityPoints.ChangeQualityPoints(QualityType.Constitution, QualityIncrease[2]);
        _qualityPoints.ChangeQualityPoints(QualityType.Wisdom, QualityIncrease[3]);
        _qualityPoints.ChangeQualityPoints(QualityType.Courage, QualityIncrease[4]);
      } else {
        _qualities[QualityType.Strength].ValueOfQuality += QualityIncrease[0];
        _qualities[QualityType.Agility].ValueOfQuality += QualityIncrease[1];
        _qualities[QualityType.Constitution].ValueOfQuality += QualityIncrease[2];
        _qualities[QualityType.Wisdom].ValueOfQuality += QualityIncrease[3];
        _qualities[QualityType.Courage].ValueOfQuality += QualityIncrease[4];
      }
    }

    // ReSharper disable once Unity.RedundantSerializeFieldAttribute
    [SerializeField]
    private Qualities _qualities;
    [FormerlySerializedAs("listOfDiceRollValues"), SerializeField]
    private List<TMP_Text> _listOfDiceRollValues;
    [FormerlySerializedAs("diceRoll"), SerializeField]
    protected Button _diceRoll;
    [FormerlySerializedAs("setQualitiesTexts"), SerializeField]
    protected SetQualitiesTexts _setQualitiesTexts;
    [SerializeField]
    protected bool _useGameSave;
    private IQualityPoints _qualityPoints;
    protected QualityIncrease QualityIncrease;
    public static event Action SetSummarizeValuesForQualitiesTexts;

    private void Start() {
      if (_useGameSave) {
        _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
        _qualities = new Qualities(_qualityPoints);
      }

      _qualities = new Qualities(_qualityPoints, new[] { 0, 0, 0, 0, 0 });
      QualityIncrease = new QualityIncrease();
      QualityIncrease.Initialization();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
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
      _setQualitiesTexts.SetValueOfQualitiesForText();
      SetSummarizeValuesForQualitiesTexts?.Invoke();
    }

    protected virtual void DiceRollsQualityIncrease() { }
  }
}