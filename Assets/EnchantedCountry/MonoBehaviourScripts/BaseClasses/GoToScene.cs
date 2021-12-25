using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.BaseClasses {
  public class GoToScene : MonoBehaviour {
    [SerializeField]
    protected string _nameOfScene;
    [SerializeField]
    protected Button _goToScene;

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    protected void AddListener() {
      _goToScene.onClick.AddListener(() => SceneManager.LoadScene(_nameOfScene));
    }

    protected void RemoveAllListener() {
      _goToScene.onClick.RemoveAllListeners();
    }

    protected void EnableInteractableForButton() {
      _goToScene.interactable = true;
    }

    protected void DisableInteractableForButton() {
      _goToScene.interactable = false;
    }

    private void RemoveListener() {
      _goToScene.onClick.RemoveListener(() => SceneManager.LoadScene(_nameOfScene));
    }
  }

  public class SceneNamesConstants {
    public const string SCENE_DICE_ROLLS_FOR_QUALITIES = "Scene_DiceRollsForQualities";
    public const string SCENE_SELECT_CHARACTER_CLASS = "Scene_SelectCharacterClass";
    public const string SCENE_QUALITY_IMPROVEMENT_FOR_WIZARD = "Scene_QualityImprovementForWizard";
    public const string SCENE_QUALITY_IMPROVEMENT_FOR_KRON = "Scene_QualityImprovementForKron";
    public const string SCENE_TRURLS_SHOP = "Scene_TrurlsShop";
    public const string SCENE_CHARACTER_LIST = "Scene_CharacterList";
    public const string SCENE_FIGHT = "Scene_Fight";
  }
}