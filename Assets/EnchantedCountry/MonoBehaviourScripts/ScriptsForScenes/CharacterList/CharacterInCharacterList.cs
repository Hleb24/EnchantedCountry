using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop;
using TMPro;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
	public class CharacterInCharacterList : CharacterIn {
		#region FIELDS
		[SerializeField]
		private TMP_Text _characterTypeText;
		#endregion
		#region MONOBEHAVIOUT_METHODS
		private void OnEnable() {
			GetCharacterType += OnGetCharacterType;
		}

		private void OnDisable() {
			GetCharacterType -= OnGetCharacterType;
		}
		#endregion
		#region SET_TEXT_OF_CHARACTER_TYPE
		private void OnGetCharacterType() {
			_characterTypeText.text = _characterType.ToString();
		}
		#endregion
	}
}