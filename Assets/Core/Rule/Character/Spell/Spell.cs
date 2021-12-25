using System;

namespace Core.Rule.Character.Spell {
    public class Spell {
        private const int BOTTOM_BORDER = 0;
        private const int NUMBER_OF_USED_TOP_BORDER = 10;
        private const int LEVEL_TOP_BORDER = 12;
        private const int LUCK_ROLL_TOP_BORDER = 18;
        private string _spellName;
        private int _numberOfUses;
        private int _level;
        private int _luckRoll;
        public Spell(string name = "", int uses = 0, int lvl = 1, int luck = 0) {
            SpellName = name;
            NumberOfUses = uses;
            Level = lvl;
            LuckRoll = luck;
        }
        public string SpellName {
            get {
                return _spellName;
            }
            set {
                if(value != null) {
                    _spellName = value;
                }else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        public int NumberOfUses {
            get {
                return _numberOfUses;
            }
            set {
                if (IsWithinInNumberOfUsesBorders(value)) {
                    _numberOfUses = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        public int Level {
            get {
                return _level;
            }
            set {
                if (IsWithinInLevelBorders(value)) {
                    _level = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        public int LuckRoll {
            get {
                return _luckRoll;
            }
            set {
                if (IsWithinInLuckRollBorders(value)) {
                    _luckRoll = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        private static bool IsWithinInNumberOfUsesBorders(int value) {
            return value >= BOTTOM_BORDER && value <= NUMBER_OF_USED_TOP_BORDER;
        }

        private static bool IsWithinInLevelBorders(int value) {
            return value > BOTTOM_BORDER && value <= LEVEL_TOP_BORDER;
        }

        private static bool IsWithinInLuckRollBorders(int value) {
            return value >= BOTTOM_BORDER && value <= LUCK_ROLL_TOP_BORDER;
        }
    }
}