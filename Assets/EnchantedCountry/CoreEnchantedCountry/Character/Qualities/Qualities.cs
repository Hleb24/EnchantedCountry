using System;
using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities {
  [Serializable]
  public class Qualities {
    private readonly Quality _strength;
    private readonly Quality _agility;
    private readonly Quality _constitution;
    private readonly Quality _wisdom;
    private readonly Quality _courage;
    private readonly Dictionary<QualityType, Quality> _listOfQualities;
    private readonly IQualityPoints _qualityPoints;

    public Qualities(IQualityPoints qualityPoints, int[] points) {
      _qualityPoints = qualityPoints;
      _strength = new Quality(QualityType.Strength, points[0]);
      _agility = new Quality(QualityType.Agility, points[1]);
      _constitution = new Quality(QualityType.Constitution, points[2]);
      _wisdom = new Quality(QualityType.Wisdom, points[3]);
      _courage = new Quality(QualityType.Courage, points[4]);
      _listOfQualities = new Dictionary<QualityType, Quality> {
        [QualityType.Strength] = _strength,
        [QualityType.Agility] = _agility,
        [QualityType.Constitution] = _constitution,
        [QualityType.Wisdom] = _wisdom,
        [QualityType.Courage] = _courage
      };
    }

    public Qualities(IQualityPoints qualityPoints) {
      _qualityPoints = qualityPoints;
      _strength = new Quality(QualityType.Strength, _qualityPoints.GetQualityPoints(QualityType.Strength));
      _agility = new Quality(QualityType.Agility, _qualityPoints.GetQualityPoints(QualityType.Agility));
      _constitution = new Quality(QualityType.Constitution, _qualityPoints.GetQualityPoints(QualityType.Constitution));
      _wisdom = new Quality(QualityType.Wisdom, _qualityPoints.GetQualityPoints(QualityType.Wisdom));
      _courage = new Quality(QualityType.Courage, _qualityPoints.GetQualityPoints(QualityType.Courage));
      _listOfQualities = new Dictionary<QualityType, Quality> {
        [QualityType.Strength] = _strength,
        [QualityType.Agility] = _agility,
        [QualityType.Constitution] = _constitution,
        [QualityType.Wisdom] = _wisdom,
        [QualityType.Courage] = _courage
      };
    }

    public Quality this[QualityType type] {
      get {
        return _listOfQualities[type];
      }
    }
  }
}