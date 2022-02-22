using Aberrance.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Main.Character.Level {
  public class BaseLevel {
    private static bool IsLevelInBorders(int level) {
      const int bottomBorder = 0;
      const int topBorder = 40;
      return level >= bottomBorder && level <= topBorder;
    }

    private int _currentLevel;

    public BaseLevel(int currentLevel) {
      Assert.IsTrue(IsLevelInBorders(currentLevel), nameof(currentLevel));
      _currentLevel = currentLevel;
    }

    [MustUseReturnValue]
    public int GetCurrentLevel() {
      return _currentLevel;
    }

    public void SetCurrentLevel(int newLevel) {
      if (IsLevelInBorders(newLevel).False()) {
        Debug.LogWarning($"Новый уровень {newLevel} не входит в диапазон доступных уровней!");
        return;
      }

      _currentLevel = newLevel;
    }
  }
}