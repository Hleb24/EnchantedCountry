using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;
using Zenject;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public class OldSave {
    public DiceRollScribe DiceRollScribe;
    public ClassTypeDataScroll ClassTypeDataScroll;
    public WalletDataScroll WalletDataScroll;
    public RiskPointsScribe RiskPointsScribe;
    public EquipmentUsedDataScroll EquipmentUsedDataScroll;
    public EquipmentsScribe EquipmentsScribe;
    public GamePointsScribe GamePointsScribe;
    public QualityPointsDataScroll QualityPointsDataScroll;
    private List<ResetSave> _resetSaves;
    public OldSave() {
      DiceRollScribe = new DiceRollScribe();
      ClassTypeDataScroll = new ClassTypeDataScroll();
      WalletDataScroll = new WalletDataScroll();
      RiskPointsScribe = new RiskPointsScribe();
      EquipmentUsedDataScroll = new EquipmentUsedDataScroll();
      GamePointsScribe = new GamePointsScribe();
      QualityPointsDataScroll = new QualityPointsDataScroll();
      EquipmentsScribe = new EquipmentsScribe();
      _resetSaves = new List<ResetSave>() {
      };
    }
		
    public void Reset() {
      foreach (ResetSave saveData in _resetSaves) {
        saveData.Reset();
      }
    }
  }
}