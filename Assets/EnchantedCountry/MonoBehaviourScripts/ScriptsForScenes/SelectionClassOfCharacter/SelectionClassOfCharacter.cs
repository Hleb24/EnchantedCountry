using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.SelectionClassOfCharacter {
	public class SelectionClassOfCharacter : MonoBehaviour {
		#region FIELDS
		// ReSharper disable once Unity.RedundantSerializeFieldAttribute
		[SerializeField]
		private Qualities _qualities;
		[SerializeField]
		private Button _warriorButton;
		[SerializeField]
		private Button _elfButton;
		[SerializeField]
		private Button _wizardButton;
		[SerializeField]
		private Button _kronButton;
		[SerializeField]
		private Button _gnomButton;
		[SerializeField]
		private bool _useGameSave;
		// [Inject]
		[SerializeField]
		private QualitiesData _qualitiesData;
		private ClassOfCharacterData _classOfCharacterData;
		private CharacterType _characterType;
		private bool _isCanBeWarrior;
		private bool _isCanBeElf;
		private bool _isCanBeWizard;
		private bool _isCanBeKron;
		private bool _isCanBeGnom;
		private int _lowerLimitQualityValueForClass = 9;
		public static event Action WizardSelected;
		public static event Action KronSelected;
		public static event Action ElseCharacterTypeSelected;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			_qualitiesData = GSSSingleton.Singleton;
			_qualities = new Qualities(_qualitiesData);
			_classOfCharacterData = GSSSingleton.Singleton;
			Invoke(nameof(SetAllowedClassesAndEnableInteractableForButtonsIfAllowedByCondition), 0.03f);
		}

		private void OnEnable() {
			AddListener();
		}

		private void OnDisable() {
			RemoveListener();
		}
		#endregion
		#region LISTENERS
		private void AddListener() {
			_warriorButton.onClick.AddListener(SelectWarrior);
			_elfButton.onClick.AddListener(SelectElf);
			_wizardButton.onClick.AddListener(SelectWizard);
			_kronButton.onClick.AddListener(SelectKron);
			_gnomButton.onClick.AddListener(SelectGnom);
		}

		private void RemoveListener() {
			_warriorButton.onClick.RemoveListener(SelectWarrior);
			_elfButton.onClick.RemoveListener(SelectElf);
			_wizardButton.onClick.RemoveListener(SelectWizard);
			_kronButton.onClick.RemoveListener(SelectKron);
			_gnomButton.onClick.RemoveListener(SelectGnom);
		}
		#endregion
		#region ALLOWED_CLASSES
		private void SetAllowedClassesAndEnableInteractableForButtonsIfAllowedByCondition() {
			SetAllowedClasses();
			EnableInteractableForButtonsIfAllowedByCondition();
		}

		private void SetAllowedClasses() {
			IsCanBeWarrior();
			IsCanBeElf();
			IsCanBeWizard();
			IsCanBeKron();
			IsCanBeGnom();
		}

		private void EnableInteractableForButtonsIfAllowedByCondition() {
			EnableInteractableForButtonAtCondition(_warriorButton, _isCanBeWarrior);
			EnableInteractableForButtonAtCondition(_elfButton, _isCanBeElf);
			EnableInteractableForButtonAtCondition(_wizardButton, _isCanBeWizard);
			EnableInteractableForButtonAtCondition(_kronButton, _isCanBeKron);
			EnableInteractableForButtonAtCondition(_gnomButton, _isCanBeGnom);

		}
		#endregion
		#region INTERACTABLE_FOR_BUTTON
		// ReSharper disable once UnusedMember.Local
		private void EnableInteractableForButton(Button button) {
			button.interactable = true;
		}
		private void EnableInteractableForButtonAtCondition(Button button, bool allowed) {
			button.interactable = allowed;
		}
		private void DisableInteractableForButton(Button button) {
			button.interactable = false;
		}
		#endregion
		#region IS_CAN_BE
		private void IsCanBeWarrior() {
			if (_useGameSave) {
				_isCanBeWarrior = _qualitiesData.strength >= _lowerLimitQualityValueForClass;
			} else {
				_isCanBeWarrior = _qualities[Quality.QualityType.Strength].ValueOfQuality >= _lowerLimitQualityValueForClass;
			}
		}

		private void IsCanBeElf() {
			if (_useGameSave) {
				_isCanBeElf = _qualitiesData.strength >= _lowerLimitQualityValueForClass
				              && _qualitiesData.courage >= _lowerLimitQualityValueForClass;
			} else {
				_isCanBeElf = _qualities[Quality.QualityType.Strength].ValueOfQuality >= _lowerLimitQualityValueForClass
				              && _qualities[Quality.QualityType.Courage].ValueOfQuality >= _lowerLimitQualityValueForClass;
			}
		}

		private void IsCanBeWizard() {
			if (_useGameSave) {
				_isCanBeWizard = _qualitiesData.wisdom >= _lowerLimitQualityValueForClass;
			} else {
				_isCanBeWizard = _qualities[Quality.QualityType.Wisdom].ValueOfQuality >= _lowerLimitQualityValueForClass;
			}
		}

		private void IsCanBeKron() {
			if (_useGameSave) {
				_isCanBeKron = _qualitiesData.agility >= _lowerLimitQualityValueForClass
				               && _qualitiesData.wisdom >= _lowerLimitQualityValueForClass;
			} else {
				_isCanBeKron = _qualities[Quality.QualityType.Agility].ValueOfQuality >= _lowerLimitQualityValueForClass
				               && _qualities[Quality.QualityType.Wisdom].ValueOfQuality >= _lowerLimitQualityValueForClass;
			}
		}

		private void IsCanBeGnom() {
			if (_useGameSave) {
				_isCanBeGnom = _qualitiesData.agility >= _lowerLimitQualityValueForClass
				               && _qualitiesData.constitution >= _lowerLimitQualityValueForClass;
			} else {
				_isCanBeGnom = _qualities[Quality.QualityType.Agility].ValueOfQuality >= _lowerLimitQualityValueForClass
				               && _qualities[Quality.QualityType.Constitution].ValueOfQuality >= _lowerLimitQualityValueForClass;
			}
		}
		#endregion
		#region SELECT_CLASS_OF_CHARACTER
		private void SelectWarrior() {
			_characterType = CharacterType.Warrior;
			SaveClassOfCharacter();
		}

		private void SelectElf() {
			_characterType = CharacterType.Elf;
			SaveClassOfCharacter();
		}

		private void SelectWizard() {
			_characterType = CharacterType.Wizard;
			SaveClassOfCharacter();
		}

		private void SelectKron() {
			_characterType = CharacterType.Kron;
			SaveClassOfCharacter();
		}

		private void SelectGnom() {
			_characterType = CharacterType.Gnom;
			SaveClassOfCharacter();
		}
		#endregion
		#region SAVE_CLASS_OF_CHARACTER
		private void SaveClassOfCharacter() {
			_classOfCharacterData.nameOfClass = _characterType.ToString();
			if (_useGameSave) {
				GSSSingleton.Singleton.SaveInGame();
			} else {
				SaveSystem.Save(_classOfCharacterData, SaveSystem.Constants.CLASS_OF_CHARACTER);
			}
			InvokeCharacterTypeEvent();
		}

		private void InvokeCharacterTypeEvent() {
			switch (_characterType) {
				case CharacterType.Warrior:
				case CharacterType.Gnom:
				case CharacterType.Elf:
					InvokeElseCharacterTypeSelectedEvent();
					break;
				case CharacterType.Wizard:
					InvokeWizardSelectedEvent();
					break;
				case CharacterType.Kron:
					InvokeKronSelcetedEvent();
					break;
			}
		}

		private void InvokeWizardSelectedEvent() {
			WizardSelected?.Invoke();
		}

		private void InvokeKronSelcetedEvent() {
			KronSelected?.Invoke();
		}

		private void InvokeElseCharacterTypeSelectedEvent() {
			ElseCharacterTypeSelected?.Invoke();
		}
		#endregion
	}
}
