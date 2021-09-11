using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class OpenCharacterList : MonoBehaviour {
    #region FIELDS
    [Header("Set in Inspector"), SerializeField]
    private Button _openCharacterList;
    [SerializeField]
    private GameObject _diceRollForRiskPointsCanvas;
    [SerializeField]
    private GameObject _characterListCanvas;
    #endregion

    #region MONOBEHAVIOUR_METHODS
    private void Start() {
      WhichCanvasToOpenFirst();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListener();
    }
    #endregion

    #region HANDLERS
    private void AddListeners() {
      _openCharacterList.onClick.AddListener(OpenCharacterListCanvas);
    }

    private void RemoveListener() {
      _openCharacterList.onClick.RemoveListener(OpenCharacterListCanvas);
    }
    #endregion

    #region WHICH_CANVAS_TO_OPEN_FIRST
    private void WhichCanvasToOpenFirst() {
      if (GSSSingleton.Instance.IsNewGame) {
        OpenDiceRollForRiskPointsCanvas();
      } else {
        OpenCharacterListCanvas();
      }
    }
    #endregion
    #region OPEN_CANVAS;
    private void OpenDiceRollForRiskPointsCanvas() {
      ToggleForCanvas(_diceRollForRiskPointsCanvas, true);
      ToggleForCanvas(_characterListCanvas, false);
      GSSSingleton.Instance.SetIsNewGameFalse();
    }

    private void OpenCharacterListCanvas() {
      ToggleForCanvas(_characterListCanvas, true);
      ToggleForCanvas(_diceRollForRiskPointsCanvas, false);
    }
    
    private void ToggleForCanvas(GameObject gObject, bool isActive) {
      gObject.SetActive(isActive);
    }
    #endregion
  }
}