using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public class OldSave {
    public DiceRollScribe DiceRollScribe;
    public ClassOfCharacterData classOfCharacterData;
    public WalletDataScroll WalletDataScroll;
    public RiskPointsData riskPointsData;
    public EquipmentUsedDataScroll EquipmentUsedDataScroll;
    public EquipmentsScribe EquipmentsScribe;
    public GamePointsScribe GamePointsScribe;
    public QualityPointsDataScroll QualityPointsDataScroll;
    private List<ResetSave> _resetSaves;
    public OldSave() {
      DiceRollScribe = new DiceRollScribe();
      classOfCharacterData = new ClassOfCharacterData();
      WalletDataScroll = new WalletDataScroll();
      riskPointsData = new RiskPointsData();
      EquipmentUsedDataScroll = new EquipmentUsedDataScroll();
      GamePointsScribe = new GamePointsScribe();
      QualityPointsDataScroll = new QualityPointsDataScroll();
      EquipmentsScribe = new EquipmentsScribe();
      _resetSaves = new List<ResetSave>() {
         classOfCharacterData,
        riskPointsData,
      };
    }
		
    public void Reset() {
      foreach (ResetSave saveData in _resetSaves) {
        saveData.Reset();
      }
    }
  }
}