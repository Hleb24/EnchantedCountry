using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
	public class BaseDiceRollQualityIncreaseMonoBehaviourClass : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		protected Qualities qualities;
		[FormerlySerializedAs("listOfDiceRollValues"),SerializeField]
		protected List<Text> _listOfDiceRollValues;
		[FormerlySerializedAs("diceRoll"),SerializeField]
		protected Button _diceRoll;
		[FormerlySerializedAs("setQualitiesTexts"),SerializeField]
		protected SetQualitiesTexts _setQualitiesTexts;
		protected DiceRollQualityIncrease diceRollQualityIncrease;
		public static event Action SetSummarizeValuesForQualitiesTexts;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		protected void Start() {
			qualities = new Qualities(GSSSingleton.Singleton);
			diceRollQualityIncrease = new DiceRollQualityIncrease();
			diceRollQualityIncrease.Initialization();
		}

		protected void OnEnable() {
			AddListener();
		}

		protected void OnDestroy() {
			RemoveListener();
		}
		#endregion
		#region HADNLERS
		protected void AddListener() {
			_diceRoll.onClick.AddListener(DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts);
		}

		protected void RemoveListener() {
			_diceRoll.onClick.RemoveListener(DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts);
		}
		protected void DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts() {
			DiceRollForQualityIncrease();
			SummarizeDiceRollsAndQualityValues(qualities, diceRollQualityIncrease);
			SetValueOfQualitiesForText();
			SetSummarizeValuesForQualitiesTexts?.Invoke();
		}

		#endregion
		#region DICE_ROLL_FOR_QUALITY_INCREASE
		protected void DiceRollForQualityIncrease() {
			DiceRollsQualityIncrease();
			for (int i = 0; i < _listOfDiceRollValues.Count; i++) {
				_listOfDiceRollValues[i].text = diceRollQualityIncrease[i].ToString();
			}
		}

		protected virtual void DiceRollsQualityIncrease() { }
		#endregion
		#region SUMMARIZE_DICE_ROLLS_AND_QUALITY_VALUES
		// ReSharper disable once MemberCanBePrivate.Global
		protected void SummarizeDiceRollsAndQualityValues(Qualities aQualities, DiceRollQualityIncrease diceRollQualityIncreaseForWizard) {
			aQualities[Quality.QualityType.Strength].ValueOfQuality += diceRollQualityIncreaseForWizard[0];
			aQualities[Quality.QualityType.Agility].ValueOfQuality += diceRollQualityIncreaseForWizard[1];
			aQualities[Quality.QualityType.Constitution].ValueOfQuality += diceRollQualityIncreaseForWizard[2];
			aQualities[Quality.QualityType.Wisdom].ValueOfQuality += diceRollQualityIncreaseForWizard[3];
			aQualities[Quality.QualityType.Courage].ValueOfQuality += diceRollQualityIncreaseForWizard[4];
		}
		#endregion
		#region SET_VALUE_OF_QUALITIES_FOR_TEXTS
		protected void SetValueOfQualitiesForText() {
			_setQualitiesTexts.SetValueOfQualitiesForText();
		}
		#endregion
	}
}
