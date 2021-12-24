using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
  public class BaseQualitiesImprovement : MonoBehaviour {
    private void GetSum() {
      if (_useGameSave) {
        _qualityPoints.ChangeQualityPoints(QualityType.Strength, diceRollQualityIncrease[0]);
        _qualityPoints.ChangeQualityPoints(QualityType.Agility, diceRollQualityIncrease[1]);
        _qualityPoints.ChangeQualityPoints(QualityType.Constitution, diceRollQualityIncrease[2]);
        _qualityPoints.ChangeQualityPoints(QualityType.Wisdom, diceRollQualityIncrease[3]);
        _qualityPoints.ChangeQualityPoints(QualityType.Courage, diceRollQualityIncrease[4]);
      } else {
        _qualities[QualityType.Strength].ValueOfQuality += diceRollQualityIncrease[0];
        _qualities[QualityType.Agility].ValueOfQuality += diceRollQualityIncrease[1];
        _qualities[QualityType.Constitution].ValueOfQuality += diceRollQualityIncrease[2];
        _qualities[QualityType.Wisdom].ValueOfQuality += diceRollQualityIncrease[3];
        _qualities[QualityType.Courage].ValueOfQuality += diceRollQualityIncrease[4];
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
    protected DiceRollQualityIncrease diceRollQualityIncrease;
    public static event Action SetSummarizeValuesForQualitiesTexts;

    private void Start() {
      if (_useGameSave) {
        _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
        _qualities = new Qualities(_qualityPoints);
      }

      _qualities = new Qualities(_qualityPoints, new[] { 0, 0, 0, 0, 0 });
      diceRollQualityIncrease = new DiceRollQualityIncrease();
      diceRollQualityIncrease.Initialization();
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
        _listOfDiceRollValues[i].text = diceRollQualityIncrease[i].ToString();
      }

      GetSum();
      _setQualitiesTexts.SetValueOfQualitiesForText();
      SetSummarizeValuesForQualitiesTexts?.Invoke();
    }

    protected virtual void DiceRollsQualityIncrease() { }
  }
}