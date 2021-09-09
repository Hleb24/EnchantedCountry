using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class RiskPointsData: ResetSave {
    public float riskPoints;
    public void Reset() {
      riskPoints = default;
    }
  }
}