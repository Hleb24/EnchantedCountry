using UnityEngine;

namespace Core.Mono.Scenes.SelectionClass {
  public class SelectionButtonsHolder : MonoBehaviour {
    [SerializeField]
    private CharacterClassSelector _characterClassSelector;
    [SerializeField]
    private CharacterClassSelectionButton[] _characterClassSelectionButtons;

    private void Awake() {
      for (var i = 0; i < _characterClassSelectionButtons.Length; i++) {
        _characterClassSelectionButtons[i].SetCharacterClassSelector(_characterClassSelector);
      }
    }
  }
}