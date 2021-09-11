using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class GamePointsInCharacterList : MonoBehaviour {
    #region FIELDS
    [SerializeField]
    private TMP_Text _gamePointsText;
    [SerializeField]
    private int _testPoints;
    [SerializeField]
    private bool _useTestPoints;
    [SerializeField]
    private bool _useGameSave;
    [Inject]
    private GamePoints _gamePoints;
    private GamePointsData _gamePointsData;
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
      if (_useGameSave) {
        GSSSingleton.Instance.SaveInGame();
      } else {
        SaveSystem.Save(_gamePointsData, SaveSystem.Constants.GamePoints);
      }
    }

    private void LoadGamePointsDataWithInvoke() {
      if (_useGameSave) {
        _gamePointsData = GSSSingleton.Instance;
        Invoke(nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      } else {
        SaveSystem.LoadWithInvoke(_gamePointsData, SaveSystem.Constants.GamePoints
        , (nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetGamePointsAndGamePointsTextAfterLoad), 0.3f);
      }
    }
    #endregion
    #region SET_GAME_POINTS
    private void SetTestPoints() {
      _gamePoints.Points = _testPoints;
      LoadGamePoints?.Invoke(_gamePoints.Points);
    }

    private void SetGamePointsAndGamePointsTextAfterLoad() {
      SetGamePointsWithData();
      SetGamePointsText();
      LoadGamePoints?.Invoke(_gamePoints.Points);
    }

    private void SetGamePointsWithData() {
      _gamePoints.Points = _gamePointsData.Points;
    }

    private void SetGamePointsText() {
      _gamePointsText.text = _gamePoints.Points.ToString();
    }
    #endregion
    #region PROOPERTIES
    public GamePoints GamePoints {
      get {
        return _gamePoints;
      }
    }
    #endregion
  }
}