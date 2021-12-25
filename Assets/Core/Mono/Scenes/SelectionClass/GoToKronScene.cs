using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToKronScene : GoToScene {
    private void Start() {
      CharacterClassSelector.KronSelected += OnKronSelected;
    }

    private void OnDestroy() {
      CharacterClassSelector.KronSelected -= OnKronSelected;
    }

    private void OnKronSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }
  }
}