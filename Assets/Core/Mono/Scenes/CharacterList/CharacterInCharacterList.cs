using Core.Mono.Scenes.TrurlsShop;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.CharacterList {
  public class CharacterInCharacterList : CharacterIn {
    [SerializeField]
    private TMP_Text _characterTypeText;

    private void OnEnable() {
      GetCharacterType += OnGetCharacterType;
    }

    private void OnDisable() {
      GetCharacterType -= OnGetCharacterType;
    }

    private void OnGetCharacterType() {
      _characterTypeText.text = _classTypeEnum.ToString();
    }
  }
}