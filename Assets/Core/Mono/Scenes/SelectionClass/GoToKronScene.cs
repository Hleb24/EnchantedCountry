using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToKronScene : GoToScene, ICharacterClassSelectorUser {
    private void OnDestroy() {
      ClassSelector.KronSelected -= OnKronSelected;
    }

    public void SetSelector(CharacterClassSelector characterClassSelector) {
      ClassSelector = characterClassSelector;
      ClassSelector.KronSelected += OnKronSelected;
    }

    private void OnKronSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }

    public CharacterClassSelector ClassSelector { get; private set; }
  }
}