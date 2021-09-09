using System.Collections.Generic;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities {
  public class CharacterQualities {
    #region FIELDS
    private  Quality _strength;
    private  Quality _agility;
    private  Quality _constitution;
    private  Quality _wisdom;
    private  Quality _courage;
    private Dictionary<Quality.QualityType, Quality> _dictionaryOfQualities;
    #endregion
    #region CONTSTRUCTORS
    public CharacterQualities(Quality.QualityType strength, int strengthValue,
      Quality.QualityType agility, int agilityValue,
      Quality.QualityType constitution, int constitutionValue,
      Quality.QualityType wisdom, int wisdomValue,
      Quality.QualityType courage, int courageValue) {
      _strength = new Quality(strength, strengthValue);
      _agility = new Quality(agility, agilityValue);
      _constitution = new Quality(constitution, constitutionValue);
      _wisdom = new Quality(wisdom, wisdomValue);
      _courage = new Quality(courage, courageValue);
      InitializationDictionaryOfQualities();
    }
    #endregion
    #region INDEXERS
    public Quality this[Quality.QualityType type] {
      get {
        return _dictionaryOfQualities[type];
      }
      set {
        _dictionaryOfQualities[type] = value;
      }
    }
    #endregion
    #region METHODS
    private void InitializationDictionaryOfQualities() {
      _dictionaryOfQualities = new Dictionary<Quality.QualityType, Quality> {
        [Quality.QualityType.Strength] = _strength,
        [Quality.QualityType.Agility] = _agility,
        [Quality.QualityType.Constitution] = _constitution,
        [Quality.QualityType.Wisdom] = _wisdom,
        [Quality.QualityType.Courage] = _courage
      };
    }
    #endregion
  }
}