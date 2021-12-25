using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
	public class GoToQualityImprovementForWizardScene: GoToScene {
		#region MONOBEHAVIOUR_METHODS
		private void Start() {
			SelectionClassOfCharacter.WizardSelected += OnWizardSelected;
		}

		private void OnDestroy() {
			SelectionClassOfCharacter.WizardSelected -= OnWizardSelected;
		}

		#endregion
		#region HANDLERS
		private void OnWizardSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}
		#endregion
	}
}
