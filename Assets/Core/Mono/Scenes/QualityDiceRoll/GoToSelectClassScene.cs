using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.Mono.Scenes.CreateCharacter {
  public class GoToSelectClassScene : GoToScene {
    [SerializeField]
    private QualitiesSelector _qualitiesSelector;

    private void Start() {
      DisableInteractableForButton();
      _qualitiesSelector.AllValuesSelected += OnAllValuesSelected;
      _qualitiesSelector.DistributeValues += OnDistributeValues;
    }

    private void OnDestroy() {
      _qualitiesSelector.AllValuesSelected -= OnAllValuesSelected;
      _qualitiesSelector.DistributeValues -= OnDistributeValues;
    }

    private void OnAllValuesSelected() {
      EnableInteractableForButton();
    }

    private void OnDistributeValues() {
      DisableInteractableForButton();
    }
  }
}