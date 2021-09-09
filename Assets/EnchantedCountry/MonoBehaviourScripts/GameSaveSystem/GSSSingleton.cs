using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem {
  public class GSSSingleton : MonoBehaviour {
    #region FIELDS
    public static GSSSingleton Singleton;
    [SerializeField]
    private SupportSystems.SaveSystem.GameSaveSystem _gameSaveSystem;
    [SerializeField]
    private bool _isNewGame;
    [SerializeField]
    private bool _doNotСheckNewGame;
    [SerializeField]
    private bool _resetSave;
    [SerializeField]
    private bool _dontLoadInAwake;
    [SerializeField]
    private bool _dontSaveInDestroy;
    #endregion
    #region MONBEHAVIOUR_METHODS
    private void Awake() {
      if (Singleton == null) {
        Singleton = this;
        _gameSaveSystem = CreateGameSaveSystem();
        DontDestroyOnLoad(this);
      } else {
        return;
      }

      if (_dontLoadInAwake) {
        return;
      }

      LoadData();
    }

    private void OnDestroy() {
      if (_dontSaveInDestroy || Singleton == null) {
        return;
      }

      Save();
    }
    #endregion

    #region CREATE_GAME_SAVE_SYSTEM
    public SupportSystems.SaveSystem.GameSaveSystem CreateGameSaveSystem() {
      return new SupportSystems.SaveSystem.GameSaveSystem();
    }
    #endregion
    #region SAVE_AND_LOAD
    private void LoadData() {
      Load();
      Invoke(nameof(AfterLoad), 0.2f);
    }

    private void AfterLoad() {
      ResetGameSave();
      GetIsNewGame();
    }

    public void SaveInGame() {
      Save();
    }

    private void Save() {
      SaveSystem.Save(_gameSaveSystem, SaveSystem.Constants.GAME_SAVE);
    }

    private void Load() {
      SaveSystem.Load(_gameSaveSystem, SaveSystem.Constants.GAME_SAVE);
    }

    private void ResetGameSave() {
      if (_resetSave) {
        for (int i = _gameSaveSystem.gameSaves.Count - 1; i >= 1; i--) {
          _gameSaveSystem.gameSaves.RemoveAt(i);
        }

        Debug.Log("Count " + _gameSaveSystem.gameSaves.Count);
        _gameSaveSystem.gameSaves[0].Reset();
        _gameSaveSystem.isNewGame = 0;
        _isNewGame = true;
      }
    }
    #endregion
    #region GET_DATA
    public DiceRollData GetDiceRollData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves[numberOfSave].diceRollData;
    }
    public ClassOfCharacterData GetClassOfCharacterData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new ClassOfCharacterData() : _gameSaveSystem.gameSaves[numberOfSave].classOfCharacterData;
    }
    public WalletData GetWalletData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new WalletData() : _gameSaveSystem.gameSaves[numberOfSave].walletData;
    }
    public RiskPointsData GetRiskPointsData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new RiskPointsData() : _gameSaveSystem.gameSaves[numberOfSave].riskPointsData;
    }
    public GamePointsData GetGamePointsData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new GamePointsData() : _gameSaveSystem.gameSaves[numberOfSave].gamePointsData;
    }
    public EquipmentUsedData GetEquipmentUsedData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new EquipmentUsedData() : _gameSaveSystem.gameSaves[numberOfSave].equipmentUsedData;
    }
    public QualitiesData GetQualitiesData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave ? new QualitiesData() : _gameSaveSystem.gameSaves[numberOfSave].qualitiesData;
    }
    public EquipmentsOfCharacterData GetEquipmentsOfCharacterData(int numberOfSave = 0) {
      return _gameSaveSystem.gameSaves.Count == numberOfSave
        ? new EquipmentsOfCharacterData(new List<EquipmentCard>())
        : _gameSaveSystem.gameSaves[numberOfSave].equipmentsOfCharacterData;
    }
    #endregion
    #region GET_DAT_WITH_LOAD
    public DiceRollData GetDiceRollDataWithLoad(int numberOfSave = 0) {
      Load();
      return _gameSaveSystem.gameSaves[numberOfSave].diceRollData;
    }
    #endregion
    #region IS_NEW_GAME
    private void GetIsNewGame() {
      if (_doNotСheckNewGame) {
        _isNewGame = false;
        return;
      }

      if (_gameSaveSystem.isNewGame.Equals(0)) {
        _isNewGame = true;
        ResetGameSave();
      } else {
        _isNewGame = false;
      }
    }

    public void SetIsNewGameFalse() {
      _gameSaveSystem.isNewGame = 1;
    }
    #endregion
    #region OPERATORS
    public static implicit operator DiceRollData(GSSSingleton value) {
      return Singleton.GetDiceRollData();
    }
    public static implicit operator ClassOfCharacterData(GSSSingleton value) {
      return Singleton.GetClassOfCharacterData();
    }
    public static implicit operator WalletData(GSSSingleton value) {
      return Singleton.GetWalletData();
    }
    public static implicit operator RiskPointsData(GSSSingleton value) {
      return Singleton.GetRiskPointsData();
    }
    public static implicit operator GamePointsData(GSSSingleton value) {
      return Singleton.GetGamePointsData();
    }
    public static implicit operator EquipmentUsedData(GSSSingleton value) {
      return Singleton.GetEquipmentUsedData();
    }
    public static implicit operator QualitiesData(GSSSingleton value) {
      return Singleton.GetQualitiesData();
    }
    public static implicit operator EquipmentsOfCharacterData(GSSSingleton value) {
      return Singleton.GetEquipmentsOfCharacterData();
    }
    #endregion
    #region PROPERTIES
    public bool IsNewGame {
      get {
        return _isNewGame;
      }
    }
    #endregion
  public class Factory : PlaceholderFactory<SupportSystems.SaveSystem.GameSaveSystem,GSSSingleton> {
    
  }
  }

}