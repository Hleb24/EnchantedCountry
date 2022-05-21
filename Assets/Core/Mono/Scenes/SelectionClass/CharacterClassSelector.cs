using System;
using Core.Main.Character.Class;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  /// <summary>
  ///   The class is responsible for choosing a character class.
  /// </summary>
  public class CharacterClassSelector {
    private readonly IClassType _classType;
    private readonly AvailableCharacterClass _availableCharacterClass;
    public event Action WizardSelected;
    public event Action KronSelected;
    public event Action ElseCharacterTypeSelected;
    private ClassType _classTypeEnum;

    [Inject]
    public CharacterClassSelector(AvailableCharacterClass availableCharacterClass, IClassType classType) {
      _classType = classType;
      _availableCharacterClass = availableCharacterClass;
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