using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.CreateCharacter {
	public class GoToSelectCharacterClassScene : GoToScene {
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			ValuesSelectionForQualities.AllValuesSelected += OnAllValuesSelected;
			ValuesSelectionForQualities.DistributeValues += OnDistributeValues;
		}

		private void OnDestroy() {
			ValuesSelectionForQualities.AllValuesSelected -= OnAllValuesSelected;
			ValuesSelectionForQualities.DistributeValues -= OnDistributeValues;
		}
		#endregion
		#region HANDLERS
		private void OnAllValuesSelected() {
			EnableInteractableForButton();
		}

		private void OnDistributeValues() {
			DisableInteractableForButton();
		}
		#endregion
	}
}
