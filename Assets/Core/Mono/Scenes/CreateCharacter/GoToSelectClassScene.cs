using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.CreateCharacter {
	public class GoToSelectClassScene : GoToScene {
		private void Start() {
			QualitiesSelector.AllValuesSelected += OnAllValuesSelected;
			QualitiesSelector.DistributeValues += OnDistributeValues;
		}

		private void OnDestroy() {
			QualitiesSelector.AllValuesSelected -= OnAllValuesSelected;
			QualitiesSelector.DistributeValues -= OnDistributeValues;
		}
		private void OnAllValuesSelected() {
			EnableInteractableForButton();
		}

		private void OnDistributeValues() {
			DisableInteractableForButton();
		}
	}
}
