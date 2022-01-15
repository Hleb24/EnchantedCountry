using System;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class CharacterGamePoints : MonoBehaviour {
    [SerializeField]
    private TMP_Text _gamePointsText;
    [SerializeField]
    private int _testPoints;
    [SerializeField]
    private bool _useTestPoints;
    [SerializeField]
    private bool _useGameSave;
    private IDealer _dealer;
    private IGamePoints _gamePoints;
    public event Action<int> LoadGamePoints;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }
    
    private void Start() {
      if (_useTestPoints) {
        SetTestPoints();
        SetGamePointsText();
      } else {
        LoadGamePointsDataWithInvoke();
      }
    }

    private void OnDestroy() {
      SaveGamePointsData();
    }
    private void SaveGamePointsData() {
      // if (_useGameSave) {
      //   GSSSingleton.Instance.SaveInGame();
      // } else {
      //   SaveSystem.Save(_gamePointsScribe, SaveSystem.Constants.GamePoints);
      // }
    }

    private void LoadGamePointsDataWithInvoke() {
      if (_useGameSave) {
        _gamePoints = _dealer.Peek<IGamePoints>();
        Invoke(nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      } else {
        // SaveSystem.LoadWithInvoke(_gamePointsScribe, SaveSystem.Constants.GamePoints
        // , (nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      }
    }
    private void SetTestPoints() {
      _gamePoints = _dealer.Peek<IGamePoints>();
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
    public IGamePoints GamePoints {
      get {
        return _gamePoints;
      }
    }
  }
}