using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
	public class GoToWizardScene: GoToScene {
		private void Start() {
			CharacterClassSelector.WizardSelected += OnWizardSelected;
		}

		private void OnDestroy() {
			CharacterClassSelector.WizardSelected -= OnWizardSelected;
		}

		private void OnWizardSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}
	}
}
