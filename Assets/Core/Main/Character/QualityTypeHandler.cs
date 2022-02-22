namespace Core.Main.Character {
  public class QualityTypeHandler {
    public static bool IsStrength(int index) {
      return (int)QualityType.Strength == index;
    }

    public static bool IsAgility(int index) {
      return (int)QualityType.Agility == index;
    }

    public static bool IsConstitution(int index) {
      return (int)QualityType.Constitution == index;
    }

    public static bool IsWisdom(int index) {
      return (int)QualityType.Wisdom == index;
    }

    public static bool IsCourage(int index) {
      return (int)QualityType.Courage == index;
    }

    public const int NUMBER_OF_QUALITY = 5;

    public bool IsStrength(QualityType qualityType) {
      return QualityType.Strength == qualityType;
    }

    public bool IsAgility(QualityType qualityType) {
      return QualityType.Agility == qualityType;
    }

    public bool IsConstitution(QualityType qualityType) {
      return QualityType.Constitution == qualityType;
    }

    public bool IsWisdom(QualityType qualityType) {
      return QualityType.Wisdom == qualityType;
    }

    public bool IsCourage(QualityType qualityType) {
      return QualityType.Courage == qualityType;
    }
  }
}