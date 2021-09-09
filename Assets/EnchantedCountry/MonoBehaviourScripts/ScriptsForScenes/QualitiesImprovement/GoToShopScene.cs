using Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses;
using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement.QualitiesImprovementForWizard;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
	public class GoToShopScene : GoToScene {
		#region MONBEHAVIOUR_METHODS
		private void Start() {
			DiceRollQualityIncreaseForWizard.SetSummarizeValuesForQualitiesTexts += OnSetSummarizeValuesForQualitiesTexts;
			SelectionClassOfCharacter.SelectionClassOfCharacter.ElseCharacterTypeSelected += OnElseCharacterTypeSelected;
		}
		private void OnDestroy() {
			DiceRollQualityIncreaseForWizard.SetSummarizeValuesForQualitiesTexts -= OnSetSummarizeValuesForQualitiesTexts;
			SelectionClassOfCharacter.SelectionClassOfCharacter.ElseCharacterTypeSelected -= OnElseCharacterTypeSelected;

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
