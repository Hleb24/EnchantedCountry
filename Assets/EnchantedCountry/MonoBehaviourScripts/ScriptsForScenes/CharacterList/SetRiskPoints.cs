using System.Globalization;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using TMPro;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
	public class SetRiskPoints : MonoBehaviour {
		#region FIELDS
		private RiskPointsData _riskPointsData;
		[SerializeField]
		private TMP_Text _numberOfRiskPointsText;
		[SerializeField]
		private bool _useRiskPointsDataForTest;
		[SerializeField]
		private bool _useGameSave;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			Invoke(nameof(LoadRiskPointsData), 0.1f);
		}

		private void OnEnable() {
			DiceRollForRiskPoints.DiceRollForRiskPointsCompleted += OnDiceRollForRiskPointsCompleted;
		}

		private void OnDisable() {
			DiceRollForRiskPoints.DiceRollForRiskPointsCompleted -= OnDiceRollForRiskPointsCompleted;

		}

		#endregion
		#region HANDLERS
		private void OnDiceRollForRiskPointsCompleted() {
			LoadData();
		}

		// ReSharper disable once UnusedMember.Local
		private static bool IsDiceThrownForRiskPoints(int diceRollForRiskPointsPrefs) {
			return diceRollForRiskPointsPrefs == PlayerPrefsConstans.Completed;
		}
		#endregion
		#region LOAD_RISK_POINTS_DATA
		private void LoadRiskPointsData() {
			PlayerPrefsTools.ReadFromPlayerPrefs(PlayerPrefsConstans.DiceRollForRiskPoints, out int diceRollForRiskPointsPrefs);
			if (IsDiceNotThrownForRiskPoints(diceRollForRiskPointsPrefs) && !_useRiskPointsDataForTest)
				return;
			LoadData();
		}

		private static bool IsDiceNotThrownForRiskPoints(int diceRollForRiskPointsPrefs) {
			return diceRollForRiskPointsPrefs == PlayerPrefsConstans.Initial;
		}
		private void LoadData() {
			if (_useGameSave) {
				_riskPointsData = GSSSingleton.Instance;
				Invoke(nameof(SetNumberOfRiskPointsText), 0.2f);
			} else {
				SaveSystem.LoadWithInvoke(_riskPointsData, SaveSystem.Constants.RiskPoints,
				(nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetNumberOfRiskPointsText), 0.1f);
			}
		}
		#endregion
		#region SET_RISK_POINTS_TEXT
		private void SetNumberOfRiskPointsText() {
			float riskPoints = _riskPointsData.riskPoints;
			_numberOfRiskPointsText.text = riskPoints.ToString(CultureInfo.InvariantCulture);
		}
		#endregion
	}
}
