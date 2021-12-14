using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public class OldSave {
    public DiceRollData diceRollData;
    public ClassOfCharacterData classOfCharacterData;
    public WalletData walletData;
    public RiskPointsData riskPointsData;
    public EquipmentUsedData equipmentUsedData;
    public EquipmentsOfCharacterData equipmentsOfCharacterData;
    public GamePointsData gamePointsData;
    public QualitiesData qualitiesData;
    private List<ResetSave> _resetSaves;
    public OldSave() {
      diceRollData = new DiceRollData();
      classOfCharacterData = new ClassOfCharacterData();
      walletData = new WalletData();
      riskPointsData = new RiskPointsData();
      equipmentUsedData = new EquipmentUsedData();
      gamePointsData = new GamePointsData();
      qualitiesData = new QualitiesData();
      equipmentsOfCharacterData = new EquipmentsOfCharacterData();
      _resetSaves = new List<ResetSave>() {
        diceRollData, classOfCharacterData, walletData,
        riskPointsData, equipmentUsedData, gamePointsData,
        qualitiesData, equipmentsOfCharacterData
      };
    }
		
    public void Reset() {
      foreach (ResetSave saveData in _resetSaves) {
        saveData.Reset();
      }
    }
  }
}