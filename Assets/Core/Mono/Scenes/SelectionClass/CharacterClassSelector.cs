using System;
using Core.Rule.Character;
using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.SelectionClass {
  public class CharacterClassSelector : MonoBehaviour {
    private readonly int _lowerLimitQualityValueForClass = 9;
    public static event Action WizardSelected;
    public static event Action KronSelected;
    public static event Action ElseCharacterTypeSelected;
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
    private IQualityPoints _qualityPoints;
    private IClassType _type;
    private ClassType _classType;
    private bool _isCanBeWarrior;
    private bool _isCanBeElf;
    private bool _isCanBeWizard;
    private bool _isCanBeKron;
    private bool _isCanBeGnom;

    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _qualities = new Qualities(_qualityPoints);
      _type = ScribeDealer.Peek<ClassTypeScribe>();
      Invoke(nameof(SetAllowedClassesAndEnableInteractableForButtonsIfAllowedByCondition), 0.03f);
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

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

    private void IsCanBeWarrior() {
      if (_useGameSave) {
        _isCanBeWarrior = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeWarrior = _qualities[QualityType.Strength].GetQualityValue() >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeElf() {
      if (_useGameSave) {
        _isCanBeElf = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass &&
                      _qualityPoints.GetQualityPoints(QualityType.Courage) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeElf = _qualities[QualityType.Strength].GetQualityValue() >= _lowerLimitQualityValueForClass &&
                      _qualities[QualityType.Courage].GetQualityValue() >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeWizard() {
      if (_useGameSave) {
        _isCanBeWizard = _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeWizard = _qualities[QualityType.Wisdom].GetQualityValue() >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeKron() {
      if (_useGameSave) {
        _isCanBeKron = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeKron = _qualities[QualityType.Agility].GetQualityValue() >= _lowerLimitQualityValueForClass &&
                       _qualities[QualityType.Wisdom].GetQualityValue() >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeGnom() {
      if (_useGameSave) {
        _isCanBeGnom = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _qualityPoints.GetQualityPoints(QualityType.Constitution) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeGnom = _qualities[QualityType.Agility].GetQualityValue() >= _lowerLimitQualityValueForClass &&
                       _qualities[QualityType.Constitution].GetQualityValue() >= _lowerLimitQualityValueForClass;
      }
    }

    private void SelectWarrior() {
      _classType = ClassType.Warrior;
      SaveClassOfCharacter();
    }

    private void SelectElf() {
      _classType = ClassType.Elf;
      SaveClassOfCharacter();
    }

    private void SelectWizard() {
      _classType = ClassType.Wizard;
      SaveClassOfCharacter();
    }

    private void SelectKron() {
      _classType = ClassType.Kron;
      SaveClassOfCharacter();
    }

    private void SelectGnom() {
      _classType = ClassType.Gnom;
      SaveClassOfCharacter();
    }

    private void SaveClassOfCharacter() {
      _type.SetClassType(_classType);
      if (_useGameSave) {
        // GSSSingleton.Instance.SaveInGame();
      }

      InvokeCharacterTypeEvent();
    }

    private void InvokeCharacterTypeEvent() {
      switch (_classType) {
        case ClassType.Warrior:
        case ClassType.Gnom:
        case ClassType.Elf:
          InvokeElseCharacterTypeSelectedEvent();
          break;
        case ClassType.Wizard:
          InvokeWizardSelectedEvent();
          break;
        case ClassType.Kron:
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
  }
}