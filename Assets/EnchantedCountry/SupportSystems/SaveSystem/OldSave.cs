﻿using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public class OldSave {
    public DiceRollScribe DiceRollScribe;
    public ClassOfCharacterData classOfCharacterData;
    public WalletData walletData;
    public RiskPointsData riskPointsData;
    public EquipmentUsedDataSave EquipmentUsedDataSave;
    public EquipmentsScribe EquipmentsScribe;
    public GamePointsData gamePointsData;
    public QualitiesData qualitiesData;
    private List<ResetSave> _resetSaves;
    public OldSave() {
      DiceRollScribe = new DiceRollScribe();
      classOfCharacterData = new ClassOfCharacterData();
      walletData = new WalletData();
      riskPointsData = new RiskPointsData();
      EquipmentUsedDataSave = new EquipmentUsedDataSave();
      gamePointsData = new GamePointsData();
      qualitiesData = new QualitiesData();
      EquipmentsScribe = new EquipmentsScribe();
      _resetSaves = new List<ResetSave>() {
         classOfCharacterData, walletData,
        riskPointsData, gamePointsData,
        qualitiesData
      };
    }
		
    public void Reset() {
      foreach (ResetSave saveData in _resetSaves) {
        saveData.Reset();
      }
    }
  }
}