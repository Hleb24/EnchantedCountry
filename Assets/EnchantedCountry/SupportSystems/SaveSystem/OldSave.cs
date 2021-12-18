using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public class OldSave {
    public DiceRollScribe DiceRollScribe;
    public ClassOfCharacterData classOfCharacterData;
    public WalletDataSave WalletDataSave;
    public RiskPointsData riskPointsData;
    public EquipmentUsedDataSave EquipmentUsedDataSave;
    public EquipmentsScribe EquipmentsScribe;
    public GamePointsData gamePointsData;
    public QualitiesData qualitiesData;
    private List<ResetSave> _resetSaves;
    public OldSave() {
      DiceRollScribe = new DiceRollScribe();
      classOfCharacterData = new ClassOfCharacterData();
      WalletDataSave = new WalletDataSave();
      riskPointsData = new RiskPointsData();
      EquipmentUsedDataSave = new EquipmentUsedDataSave();
      gamePointsData = new GamePointsData();
      qualitiesData = new QualitiesData();
      EquipmentsScribe = new EquipmentsScribe();
      _resetSaves = new List<ResetSave>() {
         classOfCharacterData,
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