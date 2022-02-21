using System.Collections.Generic;
using Core.Support.Data;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Character {
  public class Qualities {
    private readonly Dictionary<QualityType, Quality> _qualities;

    public Qualities([NotNull] IQualityPoints qualityPoints) {
      Assert.IsNotNull(qualityPoints, nameof(qualityPoints));
      _qualities = new Dictionary<QualityType, Quality> {
        [QualityType.Strength] = new Strength(qualityPoints),
        [QualityType.Agility] = new Agility(qualityPoints),
        [QualityType.Constitution] = new Constitution(qualityPoints),
        [QualityType.Wisdom] = new Wisdom(qualityPoints),
        [QualityType.Courage] = new Courage(qualityPoints)
      };
    }

    public int GetModifierOf(QualityType qualityType) {
      return _qualities[qualityType].GetModifier();
    }

    public int GetPointsOf(QualityType qualityType) {
      return _qualities[qualityType].GetQualityPoints();
    }

    
  }
}