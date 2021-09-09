using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class DiceRollData: ResetSave {
    public int[] values;

    public DiceRollData() {
      values = new int[5];
    }

    public void Reset() {
      values = new int[5];
    }
  }
}