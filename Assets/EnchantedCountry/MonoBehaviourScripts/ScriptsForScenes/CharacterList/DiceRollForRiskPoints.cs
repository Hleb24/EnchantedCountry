using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {

	public class DiceRollForRiskPoints : MonoBehaviour {
		#region FIELDS
		private ClassOfCharacterData _classOfCharacterData;
		private RiskPointsData _riskPointsData;
		[SerializeField]
		private TMP_Text _numberOfRiskPointsText;
		[SerializeField]
		private Button _diceRollForRiskPointsButton;
		[SerializeField]
		private CharacterType _characterType;
		[SerializeField]
		private bool _useCharacterTypeForTest;
		[SerializeField]
		private bool _useGameSave;
		private Dices _dice;
		private Qualities _qualities;
		private int _numberOfRiskPoints;
		public static event Action DiceRollForRiskPointsCompleted;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			LoadData();
		}
		private void OnEnable() {
			_diceRollForRiskPointsButton.onClick.AddListener(OnDiceRollForRiskPointsButtonClicked);
		}
		private void OnDisable() {
			_diceRollForRiskPointsButton.onClick.RemoveListener(OnDiceRollForRiskPointsButtonClicked);
		}
		#endregion
		#region HANDLERS
		private void OnDiceRollForRiskPointsButtonClicked() {
			PlayerPrefsTools.WritteInPlayerPrefs(PlayerPrefsConstans.DiceRollForRiskPoints, PlayerPrefsConstans.Completed);
			_numberOfRiskPoints = GetDiceRollValueForCharacterType();
			_numberOfRiskPoints += _qualities[Quality.QualityType.Constitution].Modifier;
			SetRiskPointsData(_numberOfRiskPoints);
			SetNumberOfRiskPointsText(_numberOfRiskPointsText, _numberOfRiskPoints);
			DisableDiceRollButton();
			SaveData();
			DiceRollForRiskPointsCompleted?.Invoke();
		}

		private void DisableDiceRollButton() {
			_diceRollForRiskPointsButton.gameObject.SetActive(false);
		}
		#endregion
		#region LOAD_AND_SAVE_DATA
		private void LoadData() {
			if (_useGameSave) {
				_classOfCharacterData = GSSSingleton.Instance;
				_riskPointsData = GSSSingleton.Instance;
				_qualities = new Qualities(GSSSingleton.Instance);
				Invoke(nameof(SetCharacterType), 0.3f);
			} else {
				SaveSystem.LoadWithInvoke(_classOfCharacterData, SaveSystem.Constants.ClassOfCharacter, 
				(nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetCharacterType), 0.3f);
			}
		}

		private void SaveData() {
			if (_useGameSave) {
				GSSSingleton.Instance.SaveInGame();
			} else {
				SaveSystem.Save(_riskPointsData, SaveSystem.Constants.RiskPoints);
			}
		}
		#endregion
		#region SET_CHARACTER_TYPE
		private void SetCharacterType() {
			if (_useCharacterTypeForTest)
				return;
			if(Enum.TryParse(_classOfCharacterData.nameOfClass, out CharacterType characterType)) {
				_characterType = characterType;
			} else {
				Debug.LogError("Can not parse CharacterType!");
			}
		}
		#endregion
		#region SET_RISK_POINTS
		private void SetRiskPointsData(int riskPoints) {
			_riskPointsData.riskPoints = riskPoints;
		}

		private void SetNumberOfRiskPointsText(TMP_Text riskPointsText, int riskPoints) {
			riskPointsText.text = riskPoints.ToString();
		}
		#endregion
		#region GET_DICE_ROLL_VALUE_FOR_CHARACTER_TYPE
		private int GetDiceRollValueForCharacterType() {
			_dice = new SixSidedDice(DiceType.SixEdges);
			int riskPoints = 0;
			switch (_characterType) {
				case CharacterType.Warrior:
					riskPoints = _dice.RollOfDice(2) + 6;
					break;
				case CharacterType.Elf:
					riskPoints = _dice.RollOfDice() + 3;
					break;
				case CharacterType.Wizard:
					riskPoints = _dice.RollOfDice() + 4;
					break;
				case CharacterType.Kron:
					riskPoints = _dice.RollOfDice(3) + 4;
					break;
				case CharacterType.Gnom:
					riskPoints = _dice.RollOfDice(3) + 5;
					break;
			}
			return riskPoints;
		}
		#endregion
	}
}
