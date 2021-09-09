namespace Core.EnchantedCountry.CoreEnchantedCountry.Dice {
    public class SixSidedDice : Dices {
        #region Fields
        private DiceType _diceType = DiceType.SixEdges;
        #endregion
        #region Constructors
        public SixSidedDice(DiceType diceType) : base(diceType) { }
        public SixSidedDice(int range, params int[] values) : base(range, values){}
        #endregion
        #region Indexers
        public override int this[int index] => _edges[index];
        #endregion
        #region Methods
        public override int RollOfDice() {
            int randomIndex = UnityEngine.Random.Range(0, _edges.Length);
            return _edges[randomIndex];
        }

        public override int RollOfDice(int edges) {
            int randomIndex = UnityEngine.Random.Range(0, _edges.Length);
            if (edges == 2) {
                if (randomIndex <= 2) {
                    return 1;
                }

                return 2;
            }
            if (edges == 3) {
                if (randomIndex <= 1) {
                    return 1;
                }

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (randomIndex > 1 && randomIndex <= 3) {
                    return 2;
                }

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (randomIndex > 3 && randomIndex <= 5) {
                    return 3;
                }
            }
            if (edges == 4) {
                if (randomIndex <= 3) {
                    return ++randomIndex;
                }

                return 4;
            }
            if (edges == 5) {
                if (randomIndex <= 4) {
                    return ++randomIndex;
                }

                return 5;
            }
            if (edges == 6) {
                return ++randomIndex;
            }
            return 0;
            #endregion
        }
        #region Properties
        public override DiceType DiceType {
            get {
                return _diceType;
            }
        }
        #endregion
    }
}
