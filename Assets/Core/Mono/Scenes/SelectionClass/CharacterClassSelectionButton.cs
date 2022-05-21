using Core.Main.Character.Class;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Mono.Scenes.SelectionClass {
  public class CharacterClassSelectionButton : MonoBehaviour {
    [SerializeField]
    protected Button _characterButton;
    [SerializeField]
    protected ClassType _classType;
    private CharacterClassSelector _characterClassSelector;

    private void OnDisable() {
      RemoveListeners();
    }

    public void SetCharacterClassSelector([NotNull] CharacterClassSelector characterClassSelector) {
      _characterClassSelector = characterClassSelector;
      AddListeners();
      EnableInteractableForButtonsIfAllowedByCondition();
    }

    private void AddListeners() {
      _characterButton.onClick.AddListener(() => _characterClassSelector.SelectClassType(_classType));
    }

    private void RemoveListeners() {
      _characterButton.onClick.RemoveListener(() => _characterClassSelector.SelectClassType(_classType));
    }

    private void EnableInteractableForButtonsIfAllowedByCondition() {
      _characterButton.interactable = _characterClassSelector.CanBe(_classType);
    }
  }
}