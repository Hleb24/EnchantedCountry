using System;
using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.Mono.MainManagers;
using Core.SO.Mock;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  /// <summary>
  ///   The class is responsible for choosing a character class.
  /// </summary>
  public class CharacterClassSelector : MonoBehaviour {
    public event Action WizardSelected;
    public event Action KronSelected;
    public event Action ElseCharacterTypeSelected;
    private IQualityPoints _mockQualitiesPoints;
    private IClassType _classType;
    private ClassType _classTypeEnum;
    private AvailableCharacterClass _availableCharacterClass;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    [Inject]
    public void Constructor(IQualityPoints qualityPoints, IClassType classType, IStartGame startGame) {
      _classType = classType;
      IQualityPoints tempQualityPoints = startGame.UseGameSave() ? qualityPoints : _mockQualitiesPoints;
      _availableCharacterClass = new AvailableCharacterClass(tempQualityPoints);
    }

    public void SelectClassType(ClassType classType) {
      _classTypeEnum = classType;
      SaveClassOfCharacter();
    }

    public bool CanBe(ClassType classType) {
      return _availableCharacterClass.CanBe(classType);
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