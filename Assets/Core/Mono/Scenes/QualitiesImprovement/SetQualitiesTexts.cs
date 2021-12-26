using System.Collections.Generic;
using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class SetQualitiesTexts : MonoBehaviour {
    #region FIELDS
    private Qualities _qualities;
    [SerializeField]
    private List<TMP_Text> _listOfValues;
    [SerializeField]
    private bool _setQualitiesTextsOnStart;
    [SerializeField]
    private bool _useGameSave;
    private IQualityPoints _qualityPoints;
    #endregion
    #region MONOBEHAVIOUR_METHODS
    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _qualities = new Qualities(_qualityPoints);
      IfSetQualitiesTextsOnStart();
    }
    #endregion
    #region SET_VALUE_OF_QUALITIES_FOR_TEXT
    private void IfSetQualitiesTextsOnStart() {
      if (_setQualitiesTextsOnStart) {
        Invoke(nameof(SetValueOfQualitiesForText), 0.1f);
      }
    }

    public void SetValueOfQualitiesForText() {
      if (_useGameSave) {
        _listOfValues[0].text = _qualityPoints.GetQualityPoints(QualityType.Strength).ToString();
        _listOfValues[1].text = _qualityPoints.GetQualityPoints(QualityType.Agility).ToString();
        _listOfValues[2].text =_qualityPoints.GetQualityPoints(QualityType.Constitution).ToString();
        _listOfValues[3].text = _qualityPoints.GetQualityPoints(QualityType.Wisdom).ToString();
        _listOfValues[4].text = _qualityPoints.GetQualityPoints(QualityType.Courage).ToString();
      } else {
        _listOfValues[0].text = _qualities[QualityType.Strength].GetQualityValue().ToString();
        _listOfValues[1].text = _qualities[QualityType.Agility].GetQualityValue().ToString();
        _listOfValues[2].text = _qualities[QualityType.Constitution].GetQualityValue().ToString();
        _listOfValues[3].text = _qualities[QualityType.Wisdom].GetQualityValue().ToString();
        _listOfValues[4].text = _qualities[QualityType.Courage].GetQualityValue().ToString();
      }
    }
    #endregion
    #region PROPERTIES
    public Qualities Qualities {
      get {
        return _qualities;
      }
    }
    #endregion
  }
}