using System.Collections.Generic;
using Core.Support.Data;

namespace Core.Rule.Character.Qualities {
  public class CharacterQualities {
    #region FIELDS
    private  Quality _strength;
    private  Quality _agility;
    private  Quality _constitution;
    private  Quality _wisdom;
    private  Quality _courage;
    private Dictionary<QualityType, Quality> _dictionaryOfQualities;
    #endregion
    #region CONTSTRUCTORS
    public CharacterQualities(QualityType strength, int strengthValue,
      QualityType agility, int agilityValue,
      QualityType constitution, int constitutionValue,
      QualityType wisdom, int wisdomValue,
      QualityType courage, int courageValue) {
      _strength = new Quality(strength, strengthValue);
      _agility = new Quality(agility, agilityValue);
      _constitution = new Quality(constitution, constitutionValue);
      _wisdom = new Quality(wisdom, wisdomValue);
      _courage = new Quality(courage, courageValue);
      InitializationDictionaryOfQualities();
    }
    #endregion
    #region INDEXERS
    public Quality this[QualityType type] {
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
      _dictionaryOfQualities = new Dictionary<QualityType, Quality> {
        [QualityType.Strength] = _strength,
        [QualityType.Agility] = _agility,
        [QualityType.Constitution] = _constitution,
        [QualityType.Wisdom] = _wisdom,
        [QualityType.Courage] = _courage
      };
    }
    #endregion
  }
}