using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToWizardScene : GoToScene, ICharacterClassSelectorUser {
    private void OnDestroy() {
      ClassSelector.WizardSelected -= OnWizardSelected;
    }

    public void SetSelector(CharacterClassSelector characterClassSelector) {
      ClassSelector = characterClassSelector;
      ClassSelector.WizardSelected += OnWizardSelected;
    }

    private void OnWizardSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }

    public CharacterClassSelector ClassSelector { get; private set; }
  }
}