using System;

namespace Core.Rule.Character.Levels {
  public class Levels {
    private const int BOTTOM_BORDER = 0;
    private const int TOP_BORDER = 40;
    private int _level;
    public Levels() { }

    public Levels(int startLevel) {
      Level = startLevel;
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
      return value >= BOTTOM_BORDER && value <= TOP_BORDER;
    }
  }
}