using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.SelectionClassOfCharacter {
	public class GoToQualityImprovementForKronScene : GoToScene{
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			SelectionClassOfCharacter.KronSelected += OnKronSelected;
		}

		private void OnDestroy() {
			SelectionClassOfCharacter.KronSelected -= OnKronSelected;
		}

		#endregion
		#region HANDLERS
		private void OnKronSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}
		#endregion
	}
}
