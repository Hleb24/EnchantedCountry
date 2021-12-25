using System;

namespace Core.Rule.Character.GamePoints {
    public class GamePoints {
        #region Fields
        private const int BOTTOM_BORDER = 0;
        private int _points;
        #endregion
        #region Constructors
        public GamePoints() { }
        public GamePoints(int startGamePoints) {
            Points = startGamePoints;
        }
        #endregion
        #region Properties
        public int Points {
            get {
                return _points;
            }
            set {
                if (WithinInBorder(value)) {
                    _points = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }
        #endregion
        #region Methods
        private static bool WithinInBorder(int value) {
            return value >= BOTTOM_BORDER;
        }
        #endregion
    }
}