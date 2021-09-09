using System;
using Core.EnchantedCountry.SupportSystems.SaveSystem;

namespace Core.EnchantedCountry.SupportSystems.Data {
  [Serializable]
  public class ClassOfCharacterData: ResetSave {
    public string nameOfClass;
    public void Reset() {
      nameOfClass = default;
    }
  }
}