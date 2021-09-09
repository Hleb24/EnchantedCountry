using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses {
	public class GoToScene : MonoBehaviour {
		#region FIELDS
		[SerializeField]
		protected string _nameOfScene;
		[SerializeField]
		protected Button _goToScene;
		#endregion
		#region MONOBEHAVIOUR_METHODS
		private void OnEnable() {
			AddListener();
		}

		private void OnDisable() {
			RemoveListener();
		}
		#endregion
		#region HANDLERS
		protected void AddListener() {
			_goToScene.onClick.AddListener(() => SceneManager.LoadScene(_nameOfScene));
		}

		private void RemoveListener() {
			_goToScene.onClick.RemoveListener(() => SceneManager.LoadScene(_nameOfScene));
		}

		protected void RemoveAllListener() {
			_goToScene.onClick.RemoveAllListeners();
		}
		#endregion
		#region TOGGLE_BUTTON_INTERACTIVITY
		protected void EnableInteractableForButton() {
			_goToScene.interactable = true;
		}

		protected void DisableInteractableForButton() {
			_goToScene.interactable = false;
		}
		#endregion
	}

	public struct SceneNameConstants {
		public const string SceneDiceRollsForQualities = "Scene_DiceRollsForQualities";
		public const string SceneCharacterList = "Scene_CharacterList";
	}
}