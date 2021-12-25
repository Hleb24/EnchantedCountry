using Core.Mono.MainManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class OpenCharacterList : MonoBehaviour {
    [Header("Set in Inspector"), SerializeField]
    private Button _openCharacterList;
    [SerializeField]
    private GameObject _diceRollForRiskPointsCanvas;
    [SerializeField]
    private GameObject _characterListCanvas;
    [Inject]
    private IStartGame _startGame;

    private void Start() {
      WhichCanvasToOpenFirst();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListener();
    }

    private void AddListeners() {
      _openCharacterList.onClick.AddListener(OpenCharacterListCanvas);
    }

    private void RemoveListener() {
      _openCharacterList.onClick.RemoveListener(OpenCharacterListCanvas);
    }

    private void WhichCanvasToOpenFirst() {
      if (_startGame.StartNewGame()) {
        OpenDiceRollForRiskPointsCanvas();
      } else {
        OpenCharacterListCanvas();
      }
    }

    private void OpenDiceRollForRiskPointsCanvas() {
      ToggleForCanvas(_diceRollForRiskPointsCanvas, true);
      ToggleForCanvas(_characterListCanvas, false);
      // GSSSingleton.Instance.SetIsNewGameFalse();
    }

    private void OpenCharacterListCanvas() {
      ToggleForCanvas(_characterListCanvas, true);
      ToggleForCanvas(_diceRollForRiskPointsCanvas, false);
    }

    private void ToggleForCanvas(GameObject gObject, bool isActive) {
      gObject.SetActive(isActive);
    }
  }
}