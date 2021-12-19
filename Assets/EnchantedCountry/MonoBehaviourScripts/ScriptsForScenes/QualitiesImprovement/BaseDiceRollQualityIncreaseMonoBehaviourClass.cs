using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
	public class BaseDiceRollQualityIncreaseMonoBehaviourClass : MonoBehaviour {
		[SerializeField]
		private Qualities _qualities;
		[FormerlySerializedAs("listOfDiceRollValues"),SerializeField]
		protected List<Text> _listOfDiceRollValues;
		[FormerlySerializedAs("diceRoll"),SerializeField]
		protected Button _diceRoll;
		[FormerlySerializedAs("setQualitiesTexts"),SerializeField]
		protected SetQualitiesTexts _setQualitiesTexts;
		private DiceRollQualityIncrease _diceRollQualityIncrease;
		public static event Action SetSummarizeValuesForQualitiesTexts;
		protected void Start() {
			_qualities = new Qualities(ScribeDealer.Peek<QualityPointsScribe>());
			_diceRollQualityIncrease = new DiceRollQualityIncrease();
			_diceRollQualityIncrease.Initialization();
		}

		protected void OnEnable() {
			AddListener();
		}

		private void OnDestroy() {
			RemoveListener();
		}
		private void AddListener() {
			_diceRoll.onClick.AddListener(DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts);
		}

		private void RemoveListener() {
			_diceRoll.onClick.RemoveListener(DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts);
		}
		private void DiceRollForQualityIncreaseSummarizeValuesAndSetQualitiesTexts() {
			DiceRollForQualityIncrease();
			SummarizeDiceRollsAndQualityValues(_qualities, _diceRollQualityIncrease);
			SetValueOfQualitiesForText();
			SetSummarizeValuesForQualitiesTexts?.Invoke();
		}

		private void DiceRollForQualityIncrease() {
			DiceRollsQualityIncrease();
			for (int i = 0; i < _listOfDiceRollValues.Count; i++) {
				_listOfDiceRollValues[i].text = _diceRollQualityIncrease[i].ToString();
			}
		}

		protected virtual void DiceRollsQualityIncrease() { }
		// ReSharper disable once MemberCanBePrivate.Global
		protected void SummarizeDiceRollsAndQualityValues(Qualities aQualities, DiceRollQualityIncrease diceRollQualityIncreaseForWizard) {
			aQualities[QualityType.Strength].ValueOfQuality += diceRollQualityIncreaseForWizard[0];
			aQualities[QualityType.Agility].ValueOfQuality += diceRollQualityIncreaseForWizard[1];
			aQualities[QualityType.Constitution].ValueOfQuality += diceRollQualityIncreaseForWizard[2];
			aQualities[QualityType.Wisdom].ValueOfQuality += diceRollQualityIncreaseForWizard[3];
			aQualities[QualityType.Courage].ValueOfQuality += diceRollQualityIncreaseForWizard[4];
		}
		private void SetValueOfQualitiesForText() {
			_setQualitiesTexts.SetValueOfQualitiesForText();
		}
	}
}
