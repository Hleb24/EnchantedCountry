using System;
using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.SO.Mock;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  /// <summary>
  ///   Класс отвечает за выбор класса персонажа.
  /// </summary>
  public class CharacterClassSelector : MonoBehaviour {
    public event Action WizardSelected;
    public event Action KronSelected;
    public event Action ElseCharacterTypeSelected;
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
    private IQualityPoints _mockQualitiesPoints;
    private IClassType _classType;
    private ClassType _classTypeEnum;
    private AvailableCharacterClass _availableCharacterClass;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    private void Start() {
      CheckAllowedClasses();
    }

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    [Inject]
    public void Constructor(IQualityPoints qualityPoints, IClassType classType) {
      _classType = classType;
      IQualityPoints tempQualityPoints = _useGameSave ? qualityPoints : _mockQualitiesPoints;
      _availableCharacterClass = new AvailableCharacterClass(tempQualityPoints);
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
      EnableInteractableForButtonsIfAllowedByCondition();
    }

    private void EnableInteractableForButtonsIfAllowedByCondition() {
      EnableUIAtCondition(_warriorButton, _availableCharacterClass.CanBe(ClassType.Warrior));
      EnableUIAtCondition(_elfButton, _availableCharacterClass.CanBe(ClassType.Elf));
      EnableUIAtCondition(_wizardButton, _availableCharacterClass.CanBe(ClassType.Wizard));
      EnableUIAtCondition(_kronButton, _availableCharacterClass.CanBe(ClassType.Kron));
      EnableUIAtCondition(_gnomButton, _availableCharacterClass.CanBe(ClassType.Gnom));
    }

    private void EnableUIAtCondition(Button button, bool allowed) {
      button.interactable = allowed;
    }

    private void SelectWarrior() {
      _classTypeEnum = ClassType.Warrior;
      SaveClassOfCharacter();
    }

    private void SelectElf() {
      _classTypeEnum = ClassType.Elf;
      SaveClassOfCharacter();
    }

    private void SelectWizard() {
      _classTypeEnum = ClassType.Wizard;
      SaveClassOfCharacter();
    }

    private void SelectKron() {
      _classTypeEnum = ClassType.Kron;
      SaveClassOfCharacter();
    }

    private void SelectGnom() {
      _classTypeEnum = ClassType.Gnom;
      SaveClassOfCharacter();
    }

    private void SaveClassOfCharacter() {
      _classType.SetClassType(_classTypeEnum);
      InvokeCharacterTypeEvent();
    }

    private void InvokeCharacterTypeEvent() {
      switch (_classTypeEnum) {
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