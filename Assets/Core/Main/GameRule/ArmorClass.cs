using System;

namespace Core.Main.GameRule {
  public class ArmorClass {
    #region Fields
    private const int TOP_BORDER = 16;
    private const int BOTTOM_BORDER = -3;
    private const int ARMOR_CLASS_WITH_HIGH_NUMBER_OF_POINTS_TO_HIT = -2;
    private const int HIGH_NUMBER_OF_POINTS_TO_HIT = 18;
    private const int BASE_NUMBER_OF_POINTS_TO_HIT_FOR_EXPRESSION = 17;
    private const int NUMBER_OF_POINTS_TO_HIT_FOR_KILL_ONLY_SPELL = 0;
    public bool isKillOnlySpell;
    private int _classOfArmor;
    #endregion

    #region Constructors
    public ArmorClass() { }

    public ArmorClass(int startArmorClass) {
      ClassOfArmor = startArmorClass;
    }
    #endregion

    #region Properties
    public int ClassOfArmor {
      get {
        return _classOfArmor;
      }
      set {
        if (IsWithinBorders(value)) {
          _classOfArmor = value;
          KillOnlySpell(value);
          GetNumberOfPointsToHit();
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public int NumberOfPointsToHit { get; private set; }
    #endregion

    #region Methods
    private static bool IsWithinBorders(int value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER;
    }

    private void KillOnlySpell(int value) {
      if (value == BOTTOM_BORDER) {
        isKillOnlySpell = true;
      } else {
        isKillOnlySpell = false;
      }
    }

    private void GetNumberOfPointsToHit() {
      if (isKillOnlySpell) {
        NumberOfPointsToHit = NUMBER_OF_POINTS_TO_HIT_FOR_KILL_ONLY_SPELL;
      } else {
        if (_classOfArmor == ARMOR_CLASS_WITH_HIGH_NUMBER_OF_POINTS_TO_HIT) {
          NumberOfPointsToHit = HIGH_NUMBER_OF_POINTS_TO_HIT;
        } else {
          NumberOfPointsToHit = BASE_NUMBER_OF_POINTS_TO_HIT_FOR_EXPRESSION - ClassOfArmor;
        }
      }
    }

    public bool IsHit(int hit) {
      if (hit < NumberOfPointsToHit) {
        return false;
      }

      return true;
    }
    #endregion
  }
}