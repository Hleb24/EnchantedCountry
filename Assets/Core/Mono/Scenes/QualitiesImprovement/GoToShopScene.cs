using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class GoToShopScene : GoToScene {
    [SerializeField]
    private Improvement _improvement;

    private void Start() {
      _improvement.SummarizeCompleted += OnSummarizeCompleted;
    }

    private void OnDestroy() {
      _improvement.SummarizeCompleted -= OnSummarizeCompleted;
    }

    private void OnSummarizeCompleted() {
      EnableInteractableForButton();
    }
  }
}