using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.Mono.Scenes.SelectionClass {
  public class GoToKronScene : GoToScene {
    [SerializeField]
    private CharacterClassSelector _selector;

    private void Start() {
      _selector.KronSelected += OnKronSelected;
    }

    private void OnDestroy() {
      _selector.KronSelected -= OnKronSelected;
    }

    private void OnKronSelected() {
      RemoveAllListener();
      AddListener();
      EnableInteractableForButton();
    }
  }
}