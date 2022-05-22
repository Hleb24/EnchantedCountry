using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  public class SelectionHolder : SerializedMonoBehaviour {
    [SerializeField]
    private CharacterClassSelectionButton[] _characterClassSelectionButtons;
    [SerializeField]
    private CharacterClassSelectorUserComposite _classSelectorUserComposite;
    private CharacterClassSelector _characterClassSelector;

    private void Start() {
      SendSelector();
    }

    [Inject]
    public void Constructor(CharacterClassSelector characterClassSelector) {
      _characterClassSelector = characterClassSelector;
    }

    private void SendSelector() {
      for (var i = 0; i < _characterClassSelectionButtons.Length; i++) {
        _characterClassSelectionButtons[i].SetCharacterClassSelector(_characterClassSelector);
      }

      _classSelectorUserComposite.SetSelector(_characterClassSelector);
    }
  }
}