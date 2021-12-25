using Random = UnityEngine.Random;

namespace Core.Rule.Dice {
    public class TwelveSidedDice : Dices {
        private DiceType _diceType = DiceType.TwelveEdges;
        public TwelveSidedDice(DiceType diceType) : base(diceType) { }
        public override DiceType DiceType {
            get {
                return _diceType;
            }
        }
        public override int this[int index] => _edges[index];
        public override int RollOfDice() {
            int randomIndex = Random.Range(0, _edges.Length);
            return _edges[randomIndex];
        }

        public override int RollOfDice(int edges) {
            int randomIndex = Random.Range(0, _edges.Length);
            return _edges[randomIndex];
        }
    }
}
