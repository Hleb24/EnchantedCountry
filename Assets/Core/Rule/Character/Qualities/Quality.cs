using System;
using Core.SupportSystems.Data;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Rule.Character.Qualities {
  [Serializable]
  public class Quality {
    private static bool IsWithinBorders(int value) {
      return value >= BottomBorder && value <= TopBorder;
    }

    private const int BottomBorder = 0;
    private const int TopBorder = 24;
    private int _valueOfQuality;

    public Quality(QualityType qualityType) {
      this.qualityType = qualityType;
    }

    public Quality(QualityType qualityType, int valueOfQuality) {
      this.qualityType = qualityType;
      SetQualityValue(valueOfQuality);
    }

    public int GetQualityValue() {
      return _valueOfQuality;
    }

    public void SetQualityValue(int value) {
      Assert.IsTrue(IsWithinBorders(value));
      value = value < BottomBorder ? BottomBorder : value;
      value = value > TopBorder ? TopBorder : value;
      _valueOfQuality = value;
      SetModifier();
    }

    private void SetModifier() {
      if (qualityType == QualityType.Wisdom) {
        SetWisdomModifier();
      } else if (qualityType == QualityType.Courage) {
        SetCourageModifier();
      } else {
        SetModifierForElse();
      }
    }

    private void SetModifierForElse() {
      switch (_valueOfQuality) {
        case 0:
        case 1:
        case 2:
        case 3:
          Modifier = -3;
          break;
        case 4:
        case 5:
          Modifier = -2;
          break;
        case 6:
        case 7:
        case 8:
          Modifier = -1;
          break;
        case 9:
        case 10:
        case 11:
        case 12:
          Modifier = 0;
          break;
        case 13:
        case 14:
        case 15:
          Modifier = 1;
          break;
        case 16:
        case 17:
          Modifier = 2;
          break;
        case 18:
        case 19:
        case 20:
        case 21:
        case 22:
        case 23:
        case 24:
          Modifier = 3;
          break;
        default:
          Debug.LogWarning("Значение качества в не диапазоне модификаторов");
          Modifier = 0;
          break;
      }
    }

    private void SetCourageModifier() {
      Modifier = 0;
    }

    private void SetWisdomModifier() {
      switch (_valueOfQuality) {
        case 0:
        case 1:
        case 2:
        case 3:
          Modifier = -3;
          break;
        case 4:
        case 5:
          Modifier = -2;
          break;
        case 6:
        case 7:
        case 8:
          Modifier = -1;
          break;
        case 9:
        case 10:
        case 11:
        case 12:
          Modifier = 0;
          break;
        case 13:
        case 14:
        case 15:
          Modifier = 1;
          break;
        case 16:
        case 17:
          Modifier = 2;
          break;
        case 18:
        case 19:
        case 20:
        case 21:
        case 22:
        case 23:
        case 24:
          Modifier = 4;
          break;
        default:
          Debug.LogWarning("Значение качества в не диапазоне модификаторов");
          Modifier = 0;
          break;
      }
    }

    public QualityType qualityType { get; private set; }

    public int Modifier { get; private set; }

    // public int ValueOfQuality {
    //   get {
    //     return _valueOfQuality;
    //   }
    //   set {
    //     if (IsWithinBorders(value)) {
    //       _valueOfQuality = value;
    //       SetModifier();
    //     } else {
    //       throw new InvalidOperationException("Value is invalid");
    //     }
    //   }
    // }
  }
}