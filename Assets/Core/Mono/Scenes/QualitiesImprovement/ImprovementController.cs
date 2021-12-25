using System;
using System.Collections.Generic;
using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.QualitiesImprovement {
	public class IncreaseController : MonoBehaviour {
		[SerializeField]
		private Qualities _qualities;
		[FormerlySerializedAs("listOfDiceRollValues"),SerializeField]
		protected List<Text> _listOfDiceRollValues;
		[FormerlySerializedAs("diceRoll"),SerializeField]
		protected Button _diceRoll;
		[FormerlySerializedAs("setQualitiesTexts"),SerializeField]
		protected SetQualitiesTexts _setQualitiesTexts;
		private QualityIncrease _qualityIncrease;
		public static event Action SetSummarizeValuesForQualitiesTexts;
		protected void Start() {
			_qualities = new Qualities(ScribeDealer.Peek<QualityPointsScribe>());
			_qualityIncrease = new QualityIncrease();
			_qualityIncrease.Initialization();
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
			SummarizeDiceRollsAndQualityValues(_qualities, _qualityIncrease);
			SetValueOfQualitiesForText();
			SetSummarizeValuesForQualitiesTexts?.Invoke();
		}

		private void DiceRollForQualityIncrease() {
			DiceRollsQualityIncrease();
			for (int i = 0; i < _listOfDiceRollValues.Count; i++) {
				_listOfDiceRollValues[i].text = _qualityIncrease[i].ToString();
			}
		}

		protected virtual void DiceRollsQualityIncrease() { }
		// ReSharper disable once MemberCanBePrivate.Global
		protected void SummarizeDiceRollsAndQualityValues(Qualities aQualities, QualityIncrease qualityIncreaseForWizard) {
			aQualities[QualityType.Strength].ValueOfQuality += qualityIncreaseForWizard[0];
			aQualities[QualityType.Agility].ValueOfQuality += qualityIncreaseForWizard[1];
			aQualities[QualityType.Constitution].ValueOfQuality += qualityIncreaseForWizard[2];
			aQualities[QualityType.Wisdom].ValueOfQuality += qualityIncreaseForWizard[3];
			aQualities[QualityType.Courage].ValueOfQuality += qualityIncreaseForWizard[4];
		}
		private void SetValueOfQualitiesForText() {
			_setQualitiesTexts.SetValueOfQualitiesForText();
		}
	}
}
