using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.GameRule.Item {
  public class ArmorClass {
    private static bool IsWithinBorders(int value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER;
    }

    private const int TOP_BORDER = 16;
    private const int BOTTOM_BORDER = -3;
    private const int ARMOR_CLASS_WITH_HIGH_NUMBER_OF_POINTS_TO_HIT = -2;
    private const int HIGH_NUMBER_OF_POINTS_TO_HIT = 18;
    private const int BASE_NUMBER_OF_POINTS_TO_HIT_FOR_EXPRESSION = 17;
    private const int NUMBER_OF_POINTS_TO_HIT_FOR_KILL_ONLY_SPELL = 0;
    public bool isKillOnlySpell;
    private int _classOfArmor;

    public ArmorClass(int armorClass) {
      Assert.IsTrue(IsWithinBorders(armorClass), nameof(armorClass));
      _classOfArmor = armorClass;
      SetKillOnlySpell();
    }

    [MustUseReturnValue]
    public int GetArmorClass() {
      return _classOfArmor;
    }

    public void ChangeArmorClass(int value) {
      _classOfArmor += value;
      if (_classOfArmor > TOP_BORDER) {
        _classOfArmor = TOP_BORDER;
      } else if (_classOfArmor < BOTTOM_BORDER) {
        _classOfArmor = BOTTOM_BORDER;
      }

      SetKillOnlySpell();
    }

    [Pure]
    public bool IsHit(int hit) {
      return hit >= GetNumberOfPointsToHit();
    }

    private void SetKillOnlySpell() {
      isKillOnlySpell = _classOfArmor <= BOTTOM_BORDER;
    }

    private int GetNumberOfPointsToHit() {
      if (isKillOnlySpell) {
        return NUMBER_OF_POINTS_TO_HIT_FOR_KILL_ONLY_SPELL;
      }

      if (_classOfArmor == ARMOR_CLASS_WITH_HIGH_NUMBER_OF_POINTS_TO_HIT) {
        return HIGH_NUMBER_OF_POINTS_TO_HIT;
      }

      return BASE_NUMBER_OF_POINTS_TO_HIT_FOR_EXPRESSION - _classOfArmor;
    }
  }
}