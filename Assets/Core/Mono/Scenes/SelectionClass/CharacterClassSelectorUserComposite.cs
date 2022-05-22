using UnityEngine;

namespace Core.Mono.Scenes.SelectionClass {
  public class CharacterClassSelectorUserComposite : MonoBehaviour, ICharacterClassSelectorUser {
    [SerializeField]
    private GoToKronScene _goToKronScene;
    [SerializeField]
    private GoToWizardScene _goToWizardScene;
    [SerializeField]
    private GoToShopScene _goToShopScene;

    public void SetSelector(CharacterClassSelector characterClassSelector) {
      ClassSelector = characterClassSelector;
      _goToKronScene.SetSelector(characterClassSelector);
      _goToWizardScene.SetSelector(characterClassSelector);
      _goToShopScene.SetSelector(characterClassSelector);
    }

    public CharacterClassSelector ClassSelector { get; private set; }
  }
}