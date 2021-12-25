using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToKronScene : GoToScene {
    private void Start() {
      SelectionClassOfCharacter.KronSelected += OnKronSelected;
    }

    private void OnDestroy() {
      SelectionClassOfCharacter.KronSelected -= OnKronSelected;
    }

    private void OnKronSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }
  }
}