using System;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem {
  [Serializable]
  public class GSSSingleton {
    #region CREATE_GAME_SAVE_SYSTEM
    public SupportSystems.SaveSystem.GameSaveSystem CreateGameSaveSystem() {
      return new SupportSystems.SaveSystem.GameSaveSystem();
    }
    #endregion

    #region GET_DAT_WITH_LOAD
    public DiceRollScribe GetDiceRollDataWithLoad(int numberOfSave = 0) {
      Load();
      return _gameSaveSystem.gameSaves[numberOfSave].DiceRollScribe;
    }
    #endregion

    #region PROPERTIES
    public bool IsNewGame { get; private set; }
    #endregion

    #region FIELDS
    private static readonly Lazy<GSSSingleton> Lazy = new Lazy<GSSSingleton>(() => new GSSSingleton());
    private readonly SupportSystems.SaveSystem.GameSaveSystem _gameSaveSystem;
    private bool _doNotСheckNewGame;
    private bool _resetSave;
    private bool _dontLoadInAwake;
    private bool _dontSaveInDestroy;

    public static GSSSingleton Instance {
      get {
        return Lazy.Value;
      }
    }
    #endregion

    #region CONSTRUCTS
    private GSSSingleton() {
      _gameSaveSystem = CreateGameSaveSystem();
      LoadData();
    }

    private void OnDestroy() {
      if (_dontSaveInDestroy || Instance == null) {
        return;
      }

      Save();
    }
    #endregion

    #region SAVE_AND_LOAD
    public void LoadData() {
      Load();
      //TODO Invoke(nameof(AfterLoad), 0.2f);
    }

    private void AfterLoad() {
      ResetGameSave();
      GetIsNewGame();
    }

    public void SaveInGame() {
      Save();
    }

    private void Save() {
      SaveSystem.Save(_gameSaveSystem, SaveSystem.Constants.GameSave);
    }

    private void Load() {
      SaveSystem.Load(_gameSaveSystem, SaveSystem.Constants.GameSave);
    }

    private void ResetGameSave() {
      if (_resetSave) {
        for (int i = _gameSaveSystem.gameSaves.Count - 1; i >= 1; i--) {
          _gameSaveSystem.gameSaves.RemoveAt(i);
        }

        Debug.Log("Count " + _gameSaveSystem.gameSaves.Count);
        _gameSaveSystem.gameSaves[0].Reset();
        _gameSaveSystem.isNewGame = 0;
        IsNewGame = true;
      }
    }
    #endregion

    #region GET_DATA
    public ClassOfCharacterData GetClassOfCharacterData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new ClassOfCharacterData() : _gameSaveSystem.gameSaves[numberOfSave].classOfCharacterData;
    }

    public RiskPointsData GetRiskPointsData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new RiskPointsData() : _gameSaveSystem.gameSaves[numberOfSave].riskPointsData;
    }

    public GamePointsData GetGamePointsData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new GamePointsData() : _gameSaveSystem.gameSaves[numberOfSave].gamePointsData;
    }

    public QualitiesData GetQualitiesData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new QualitiesData() : _gameSaveSystem.gameSaves[numberOfSave].qualitiesData;
    }
    #endregion

    #region IS_NEW_GAME
    private void GetIsNewGame() {
      if (_doNotСheckNewGame) {
        IsNewGame = false;
        return;
      }

      if (_gameSaveSystem.isNewGame.Equals(0)) {
        IsNewGame = true;
        ResetGameSave();
      } else {
        IsNewGame = false;
      }
    }

    public void SetIsNewGameFalse() {
      _gameSaveSystem.isNewGame = 1;
    }
    #endregion

    #region OPERATORS
    public static implicit operator ClassOfCharacterData(GSSSingleton value) {
      return Instance.GetClassOfCharacterData();
    }

    public static implicit operator RiskPointsData(GSSSingleton value) {
      return Instance.GetRiskPointsData();
    }

    public static implicit operator GamePointsData(GSSSingleton value) {
      return Instance.GetGamePointsData();
    }

    public static implicit operator QualitiesData(GSSSingleton value) {
      return Instance.GetQualitiesData();
    }
    #endregion
  }
}