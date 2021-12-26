using Core.Mono.BaseClass;
using UnityEngine;

namespace Core.Mono.Scenes.SelectionClass {
	public class GoToWizardScene: GoToScene {
		[SerializeField]
		private CharacterClassSelector _selector;
		
		private void Start() {
			_selector.WizardSelected += OnWizardSelected;
		}

		private void OnDestroy() {
			_selector.WizardSelected -= OnWizardSelected;
		}

		private void OnWizardSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}
	}
}
