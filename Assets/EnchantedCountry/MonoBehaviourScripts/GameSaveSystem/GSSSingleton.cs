using System;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem {
  [Serializable]
  public class GSSSingleton {
    private static readonly Lazy<GSSSingleton> Lazy = new Lazy<GSSSingleton>(() => new GSSSingleton());

    public static GSSSingleton Instance {
      get {
        return Lazy.Value;
      }
    }

    private readonly SupportSystems.SaveSystem.GameSaveSystem _gameSaveSystem;
    private bool _doNotСheckNewGame;
    private bool _resetSave;
    private bool _dontLoadInAwake;
    private bool _dontSaveInDestroy;

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

    public SupportSystems.SaveSystem.GameSaveSystem CreateGameSaveSystem() {
      return new SupportSystems.SaveSystem.GameSaveSystem();
    }

    public DiceRollScribe GetDiceRollDataWithLoad(int numberOfSave = 0) {
      Load();
      return _gameSaveSystem.gameSaves[numberOfSave].DiceRollScribe;
    }

    public void LoadData() {
      Load();
      //TODO Invoke(nameof(AfterLoad), 0.2f);
    }

    public void SaveInGame() {
      Save();
    }

    public void SetIsNewGameFalse() {
      _gameSaveSystem.isNewGame = 1;
    }

    private void AfterLoad() {
      ResetGameSave();
      GetIsNewGame();
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

    public bool IsNewGame { get; private set; }
  }
}