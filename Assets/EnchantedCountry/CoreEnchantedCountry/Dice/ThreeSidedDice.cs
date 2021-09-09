namespace Core.EnchantedCountry.CoreEnchantedCountry.Dice {
    public class ThreeSidedDice : Dices {
        #region Fields
        private DiceType _diceType = DiceType.ThreeEdges;
        #endregion
        #region Constructors
        public ThreeSidedDice(DiceType diceType) : base(diceType) { }
        #endregion
        #region Properties
        public override DiceType DiceType {
            get {
                return _diceType;
            }
        }
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
            return _edges[randomIndex];
            #endregion
        }
    }
}
