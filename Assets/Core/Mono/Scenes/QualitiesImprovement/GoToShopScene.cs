using Core.Mono.BaseClass;
using Core.Mono.Scenes.SelectionClass;

namespace Core.Mono.Scenes.QualitiesImprovement {
	public class GoToShopScene : GoToScene {
		#region MONBEHAVIOUR_METHODS
		private void Start() {
			Wizard.SetSummarizeValuesForQualitiesTexts += OnSetSummarizeValuesForQualitiesTexts;
			CharacterClassSelector.ElseCharacterTypeSelected += OnElseCharacterTypeSelected;
		}
		private void OnDestroy() {
			Wizard.SetSummarizeValuesForQualitiesTexts -= OnSetSummarizeValuesForQualitiesTexts;
			CharacterClassSelector.ElseCharacterTypeSelected -= OnElseCharacterTypeSelected;

		}

		#endregion
		#region HANDLERS
		private void OnElseCharacterTypeSelected() {
			RemoveAllListener();
			AddListener();
			EnableInteractableForButton();
		}

		private void OnSetSummarizeValuesForQualitiesTexts() {
			EnableInteractableForButton();
		}
		#endregion
	}
}
