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
    private ILauncher _launcher;

    private void Start() {
      WhichCanvasToOpenFirst();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListener();
    }

    [Inject]
    public void Constructor(ILauncher launcher) {
      _launcher = launcher;
    }

    private void AddListeners() {
      _openCharacterList.onClick.AddListener(OpenCharacterListCanvas);
    }

    private void RemoveListener() {
      _openCharacterList.onClick.RemoveListener(OpenCharacterListCanvas);
    }

    private void WhichCanvasToOpenFirst() {
      if (_launcher.StartNewGame()) {
        OpenDiceRollForRiskPointsCanvas();
      } else {
        OpenCharacterListCanvas();
      }
    }

    private void OpenDiceRollForRiskPointsCanvas() {
      ToggleForCanvas(_diceRollForRiskPointsCanvas, true);
      ToggleForCanvas(_characterListCanvas, false);
    }

    private void OpenCharacterListCanvas() {
      ToggleForCanvas(_characterListCanvas, true);
      ToggleForCanvas(_diceRollForRiskPointsCanvas, false);
    }

    private void ToggleForCanvas(GameObject canvas, bool isActive) {
      canvas.SetActive(isActive);
    }
  }
}