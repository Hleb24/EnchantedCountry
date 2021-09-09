using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class QualitiesData: ResetSave {
    public int strength;
    public int agility;
    public int constitution;
    public int wisdom;
    public int courage;
    public void Reset() {
      strength = default;
      agility = default;
      constitution = default;
      wisdom = default;
      courage = default;
    }
  }
}