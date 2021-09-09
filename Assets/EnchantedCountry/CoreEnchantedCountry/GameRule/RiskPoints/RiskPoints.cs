using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints {
    public class RiskPoints {
        #region Fields
        private const float BOTTOM_BORDER = 0f;
        public bool isDead;
        private float _points;
        #endregion
        #region Constructors
        public RiskPoints() { }
        public RiskPoints(float startPoints) {
            _points = startPoints;
        }
        #endregion
        #region Properties
        public float Points {
            get {
                return _points;
            }
            set {
                if (IsWithinBorders(value)) {
                    _points = value;

                } else if (IsBecomeDead(value)) {
                    _points = BOTTOM_BORDER;
                    isDead = true;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }
        #endregion
        #region Methods
        private static bool IsBecomeDead(float value) {
            return value <= BOTTOM_BORDER;
        }

        private bool IsWithinBorders(float value) {
            return value > BOTTOM_BORDER && !isDead;
        }
        #endregion
    }
  
}