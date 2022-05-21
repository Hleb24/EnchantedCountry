using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.SelectionClass {
  public class SelectionHolder : MonoBehaviour {
    [SerializeField]
    private CharacterClassSelectionButton[] _characterClassSelectionButtons;
    [SerializeField]
    private GoToKronScene _goToKronScene;
    [SerializeField]
    private GoToWizardScene _goToWizardScene;
    [SerializeField]
    private GoToShopScene _goToShopScene;

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

      _goToKronScene.SetSelector(_characterClassSelector);
      _goToWizardScene.SetSelector(_characterClassSelector);
      _goToShopScene.SetSelector(_characterClassSelector);
    }
  }
}