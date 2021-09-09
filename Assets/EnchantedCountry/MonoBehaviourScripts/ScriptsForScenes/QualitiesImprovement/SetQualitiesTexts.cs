using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using TMPro;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
  public class SetQualitiesTexts : MonoBehaviour {
    #region FIELDS
    private Qualities _qualities;
    [SerializeField]
    private List<TMP_Text> _listOfValues;
    [SerializeField]
    private bool _setQualitiesTextsOnStart;
    [SerializeField]
    private bool _useGameSave;
    private QualitiesData _qualitiesData;
    #endregion
    #region MONOBEHAVIOUR_METHODS
    private void Start() {
      _qualitiesData = GSSSingleton.Singleton;
      _qualities = new Qualities(GSSSingleton.Singleton);
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
        _listOfValues[0].text = _qualitiesData.strength.ToString();
        _listOfValues[1].text = _qualitiesData.agility.ToString();
        _listOfValues[2].text = _qualitiesData.constitution.ToString();
        _listOfValues[3].text = _qualitiesData.wisdom.ToString();
        _listOfValues[4].text = _qualitiesData.courage.ToString();
      } else {
        _listOfValues[0].text = _qualities[Quality.QualityType.Strength].ValueOfQuality.ToString();
        _listOfValues[1].text = _qualities[Quality.QualityType.Agility].ValueOfQuality.ToString();
        _listOfValues[2].text = _qualities[Quality.QualityType.Constitution].ValueOfQuality.ToString();
        _listOfValues[3].text = _qualities[Quality.QualityType.Wisdom].ValueOfQuality.ToString();
        _listOfValues[4].text = _qualities[Quality.QualityType.Courage].ValueOfQuality.ToString();
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