using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter {
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
