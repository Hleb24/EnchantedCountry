using System;

namespace Core.Main.Character.Levels {
  public class Levels {
    private const int BottomBorder = 0;
    private const int TopBorder = 40;
    private int _level;
    public Levels() { }

    public Levels(int startLevel) {
      Level = startLevel;
    }

    public int GetLevel() {
      return _level;
    }
    public int Level {
      get {
        return _level;
      }
      set {
        if (WithinInBorders(value)) {
          _level = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }
    private static bool WithinInBorders(int value) {
      return value >= BottomBorder && value <= TopBorder;
    }
  }
}