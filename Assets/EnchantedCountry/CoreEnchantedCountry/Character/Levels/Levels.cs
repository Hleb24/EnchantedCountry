using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels {
  public class Levels {
    #region Fields
    private const int BOTTOM_BORDER = 0;
    private const int TOP_BORDER = 40;
    private int _level;
    #endregion
    #region Constructors
    public Levels() { }

    public Levels(int startLevel) {
      Level = startLevel;
    }
    #endregion
    #region Properties
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
    #endregion
    #region Methods
    private static bool WithinInBorders(int value) {
      return value >= BOTTOM_BORDER && value <= TOP_BORDER;
    }
    #endregion
  }
}