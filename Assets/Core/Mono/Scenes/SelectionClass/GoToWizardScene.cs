using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
	public class GoToWizardScene: GoToScene {
		private void Start() {
			SelectionClassOfCharacter.WizardSelected += OnWizardSelected;
		}

		private void OnDestroy() {
			SelectionClassOfCharacter.WizardSelected -= OnWizardSelected;
		}

		private void OnWizardSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}
	}
}
