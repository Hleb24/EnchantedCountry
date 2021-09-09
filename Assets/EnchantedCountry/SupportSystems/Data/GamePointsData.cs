using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class GamePointsData: ResetSave {
    public int Points;
    public void Reset() {
      Points = default;
    }
  }
}