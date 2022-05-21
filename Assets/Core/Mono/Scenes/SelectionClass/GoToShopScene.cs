using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToShopScene : GoToScene, ICharacterClassSelectorUser {
    private void OnDestroy() {
      ClassSelector.ElseCharacterTypeSelected -= OnElseCharacterTypeSelected;
    }

    public void SetSelector(CharacterClassSelector characterClassSelector) {
      ClassSelector = characterClassSelector;
      ClassSelector.ElseCharacterTypeSelected += OnElseCharacterTypeSelected;
    }

    private void OnElseCharacterTypeSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }

    public CharacterClassSelector ClassSelector { get; private set; }
  }
}