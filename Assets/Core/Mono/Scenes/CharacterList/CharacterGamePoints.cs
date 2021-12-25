using System;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.CharacterList {
  public class CharacterGamePoints : MonoBehaviour {
    #region FIELDS
    [SerializeField]
    private TMP_Text _gamePointsText;
    [SerializeField]
    private int _testPoints;
    [SerializeField]
    private bool _useTestPoints;
    [SerializeField]
    private bool _useGameSave;
    
    private IGamePoints _gamePoints;
    public event Action<int> LoadGamePoints;
    #endregion
    #region MONOBEHAVIOUR_METHODS
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
    #endregion
    #region SAVE_AND_LOAD
    private void SaveGamePointsData() {
      // if (_useGameSave) {
      //   GSSSingleton.Instance.SaveInGame();
      // } else {
      //   SaveSystem.Save(_gamePointsScribe, SaveSystem.Constants.GamePoints);
      // }
    }

    private void LoadGamePointsDataWithInvoke() {
      if (_useGameSave) {
        _gamePoints = ScribeDealer.Peek<GamePointsScribe>();
        Invoke(nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      } else {
        // SaveSystem.LoadWithInvoke(_gamePointsScribe, SaveSystem.Constants.GamePoints
        // , (nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      }
    }
    #endregion
    #region SET_GAME_POINTS
    private void SetTestPoints() {
      _gamePoints = ScribeDealer.Peek<GamePointsScribe>();
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
    #endregion
    #region PROOPERTIES
    public IGamePoints GamePoints {
      get {
        return _gamePoints;
      }
    }
    #endregion
  }
}