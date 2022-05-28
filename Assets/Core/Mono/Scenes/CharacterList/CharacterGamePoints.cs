using System;
using Core.Main.GameRule.Point;
using Core.Mono.MainManagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class CharacterGamePoints : MonoBehaviour {
    public event Action<int> LoadGamePoints;
    [SerializeField]
    private TMP_Text _gamePointsText;
    [SerializeField]
    private int _testPoints;
    [SerializeField]
    private bool _useTestPoints;
    private ILauncher _launcher;
    private IGamePoints _gamePoints;

    private void Start() {
      if (_useTestPoints) {
        SetTestPoints();
        SetGamePointsText();
      } else {
        LoadGamePointsDataWithInvoke();
      }
    }

    [Inject]
    public void Constructor(ILauncher launcher, IGamePoints gamePoints) {
      _launcher = launcher;
      _gamePoints = gamePoints;
    }

    private void LoadGamePointsDataWithInvoke() {
      if (_launcher.UseGameSave()) {
        SetGamePointsAndGamePointsTextAfterLoad();
      }
    }

    private void SetTestPoints() {
      _gamePoints.SetPoints(_testPoints);
      LoadGamePoints?.Invoke(_gamePoints.GetPoints());
    }

    private void SetGamePointsAndGamePointsTextAfterLoad() {
      SetGamePointsWithData();
      SetGamePointsText();
      LoadGamePoints?.Invoke(_gamePoints.GetPoints());
    }

    private void SetGamePointsWithData() {
      _gamePoints.GetPoints();
    }

    private void SetGamePointsText() {
      _gamePointsText.text = _gamePoints.GetPoints().ToString();
    }
  }
}