using Core.Support.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Main.Character {
  public class Wisdom : Quality {
    public Wisdom([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    public override int GetModifier() {
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
          return 4;
        default:
          Debug.LogWarning($"Значение {QualityType} в не диапазоне модификаторов");
          return 0;
      }
    }

    protected override QualityType QualityType {
      get {
        return QualityType.Wisdom;
      }
    }
  }
}