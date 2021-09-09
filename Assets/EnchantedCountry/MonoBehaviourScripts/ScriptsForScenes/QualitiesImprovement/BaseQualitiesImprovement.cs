using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
  public class BaseQualitiesImprovement : MonoBehaviour{
		#region FIELDS
		// ReSharper disable once Unity.RedundantSerializeFieldAttribute
		[SerializeField]
    private Qualities _qualities;
    [FormerlySerializedAs("listOfDiceRollValues"),SerializeField]
    private List<TMP_Text> _listOfDiceRollValues;
    [FormerlySerializedAs("diceRoll"),SerializeField]
    protected Button _diceRoll;
    [FormerlySerializedAs("setQualitiesTexts"),SerializeField]
    protected SetQualitiesTexts _setQualitiesTexts;
    [SerializeField]
    protected bool _useGameSave;
    protected QualitiesData _qualitiesData;
    protected DiceRollQualityIncrease diceRollQualityIncrease;
    public static event Action SetSummarizeValuesForQualitiesTexts;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			if (_useGameSave) {
				_qualitiesData = GSSSingleton.Singleton;
				_qualities = new Qualities(_qualitiesData);
			}
			_qualities = new Qualities();
			diceRollQualityIncrease = new DiceRollQualityIncrease();
			diceRollQualityIncrease.Initialization();

		}
		private void OnEnable() {
			AddListeners();
		}

		private void OnDisable() {
			RemoveListeners();
		}

		#endregion
		#region HANDLERS
		private void AddListeners() {
			_diceRoll.onClick.AddListener(GetValueIncreaseAndSetTexts);
		}

		private void RemoveListeners() {
			_diceRoll.onClick.RemoveListener(GetValueIncreaseAndSetTexts);
		}
		#endregion
		#region GET_VALUE_INCREASE_AND_SET_TEXTS
		protected void GetValueIncreaseAndSetTexts() {
			DiceRollsQualityIncrease();
			for (int i = 0; i < _listOfDiceRollValues.Count; i++) {
				_listOfDiceRollValues[i].text = diceRollQualityIncrease[i].ToString();
			}
			GetSum();
			_setQualitiesTexts.SetValueOfQualitiesForText();
			SetSummarizeValuesForQualitiesTexts?.Invoke();
		}

		protected virtual void DiceRollsQualityIncrease() { }
		#endregion
		#region GET_SUM
		protected void GetSum() {
			if (_useGameSave) {
				_qualitiesData.strength += diceRollQualityIncrease[0];
				_qualitiesData.agility += diceRollQualityIncrease[1];
				_qualitiesData.constitution += diceRollQualityIncrease[2];
				_qualitiesData.wisdom += diceRollQualityIncrease[3];
				_qualitiesData.courage += diceRollQualityIncrease[4];
				GSSSingleton.Singleton.SaveInGame();
			} else {
				_qualities[Quality.QualityType.Strength].ValueOfQuality += diceRollQualityIncrease[0];
				_qualities[Quality.QualityType.Agility].ValueOfQuality += diceRollQualityIncrease[1];
				_qualities[Quality.QualityType.Constitution].ValueOfQuality += diceRollQualityIncrease[2];
				_qualities[Quality.QualityType.Wisdom].ValueOfQuality += diceRollQualityIncrease[3];
				_qualities[Quality.QualityType.Courage].ValueOfQuality += diceRollQualityIncrease[4];
			}
		}
		#endregion
	}
}
