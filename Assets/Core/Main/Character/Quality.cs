using Core.Support.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Main.Character {
  public abstract class Quality {
    private readonly IQualityPoints _qualityPoints;

    protected Quality([NotNull] IQualityPoints qualityPoints) {
      Assert.IsNotNull(qualityPoints, nameof(qualityPoints));
      _qualityPoints = qualityPoints;
    }

    [MustUseReturnValue]
    public int GetQualityPoints() {
      return _qualityPoints.GetQualityPoints(QualityType);
    }

    [Pure]
    public virtual int GetModifier() {
      switch (GetQualityPoints()) {
        case 0:
        case 1:
        case 2:
        case 3:
          return -3;
        case 4:
        case 5:
          return -2;
        case 6:
        case 7:
        case 8:
          return -1;
        case 9:
        case 10:
        case 11:
        case 12:
          return 0;
        case 13:
        case 14:
        case 15:
          return 1;
        case 16:
        case 17:
          return 2;
        case 18:
        case 19:
        case 20:
        case 21:
        case 22:
        case 23:
        case 24:
          return 3;
        default:
          Debug.LogWarning($"Значение {QualityType} в не диапазоне модификаторов");
          return 0;
      }
    }

    protected abstract QualityType QualityType { get; }
  }
}