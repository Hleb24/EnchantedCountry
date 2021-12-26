using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToShopScene : GoToScene {
    [SerializeField]
    private CharacterClassSelector _selector;

    private void Start() {
      _selector.ElseCharacterTypeSelected += OnElseCharacterTypeSelected;
    }

    private void OnDestroy() {
      _selector.ElseCharacterTypeSelected -= OnElseCharacterTypeSelected;
    }

    private void OnElseCharacterTypeSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }
  }
}