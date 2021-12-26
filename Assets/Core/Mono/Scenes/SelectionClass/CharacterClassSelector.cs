using System;
using Core.Rule.Character;
using Core.ScriptableObject.Mock;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.SelectionClass {
  /// <summary>
  ///   Класс отвечает за выбор класса персонажа.
  /// </summary>
  public class CharacterClassSelector : MonoBehaviour {
    private readonly int _lowerLimitQualityValueForClass = 9;
    public event Action WizardSelected;
    public event Action KronSelected;
    public event Action ElseCharacterTypeSelected;
    // ReSharper disable once Unity.RedundantSerializeFieldAttribute
    [SerializeField]
    private IQualityPoints _mockQualitiesPoints;
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
    private IQualityPoints _qualityPoints;
    private IClassType _type;
    private ClassType _classType;
    private bool _isCanBeWarrior;
    private bool _isCanBeElf;
    private bool _isCanBeWizard;
    private bool _isCanBeKron;
    private bool _isCanBeGnom;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    private void Start() {
      Init();
      CheckAllowedClasses();
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    private void Init() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _type = ScribeDealer.Peek<ClassTypeScribe>();
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

    private void CheckAllowedClasses() {
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
      EnableUIAtCondition(_warriorButton, _isCanBeWarrior);
      EnableUIAtCondition(_elfButton, _isCanBeElf);
      EnableUIAtCondition(_wizardButton, _isCanBeWizard);
      EnableUIAtCondition(_kronButton, _isCanBeKron);
      EnableUIAtCondition(_gnomButton, _isCanBeGnom);
    }

    private void EnableUIAtCondition(Button button, bool allowed) {
      button.interactable = allowed;
    }

    private void IsCanBeWarrior() {
      if (_useGameSave) {
        _isCanBeWarrior = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeWarrior = _mockQualitiesPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeElf() {
      if (_useGameSave) {
        _isCanBeElf = _qualityPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass &&
                      _qualityPoints.GetQualityPoints(QualityType.Courage) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeElf = _mockQualitiesPoints.GetQualityPoints(QualityType.Strength) >= _lowerLimitQualityValueForClass &&
                      _mockQualitiesPoints.GetQualityPoints(QualityType.Courage) >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeWizard() {
      if (_useGameSave) {
        _isCanBeWizard = _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeWizard = _mockQualitiesPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeKron() {
      if (_useGameSave) {
        _isCanBeKron = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _qualityPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeKron = _mockQualitiesPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _mockQualitiesPoints.GetQualityPoints(QualityType.Wisdom) >= _lowerLimitQualityValueForClass;
      }
    }

    private void IsCanBeGnom() {
      if (_useGameSave) {
        _isCanBeGnom = _qualityPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _qualityPoints.GetQualityPoints(QualityType.Constitution) >= _lowerLimitQualityValueForClass;
      } else {
        _isCanBeGnom = _mockQualitiesPoints.GetQualityPoints(QualityType.Agility) >= _lowerLimitQualityValueForClass &&
                       _mockQualitiesPoints.GetQualityPoints(QualityType.Constitution) >= _lowerLimitQualityValueForClass;
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
      InvokeCharacterTypeEvent();
    }

    private void InvokeCharacterTypeEvent() {
      switch (_classType) {
        case ClassType.Human:
        case ClassType.Warrior:
        case ClassType.Gnom:
        case ClassType.Elf:
          ElseCharacterTypeSelected?.Invoke();
          break;
        case ClassType.Wizard:
          WizardSelected?.Invoke();
          break;
        case ClassType.Kron:
          KronSelected?.Invoke();
          break;
      }
    }
  }
}