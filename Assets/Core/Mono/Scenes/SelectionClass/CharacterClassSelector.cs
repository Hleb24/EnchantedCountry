using System;
using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.SO.Mock;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  /// <summary>
  ///   The class is responsible for choosing a character class.
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
      _warriorButton.onClick.AddListener(() => SelectClassType(ClassType.Warrior));
      _elfButton.onClick.AddListener(() => SelectClassType(ClassType.Elf));
      _wizardButton.onClick.AddListener(() => SelectClassType(ClassType.Wizard));
      _kronButton.onClick.AddListener(() => SelectClassType(ClassType.Kron));
      _gnomButton.onClick.AddListener(() => SelectClassType(ClassType.Gnom));
    }

    private void RemoveListener() {
      _warriorButton.onClick.RemoveListener(() => SelectClassType(ClassType.Warrior));
      _elfButton.onClick.RemoveListener(() => SelectClassType(ClassType.Elf));
      _wizardButton.onClick.RemoveListener(() => SelectClassType(ClassType.Wizard));
      _kronButton.onClick.RemoveListener(() => SelectClassType(ClassType.Kron));
      _gnomButton.onClick.RemoveListener(() => SelectClassType(ClassType.Gnom));
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

    private void SelectClassType(ClassType classType) {
      _classTypeEnum = classType;
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