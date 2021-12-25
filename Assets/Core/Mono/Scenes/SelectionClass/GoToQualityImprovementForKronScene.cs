using Core.Mono.BaseClass;

namespace Core.Mono.Scenes.SelectionClass {
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
