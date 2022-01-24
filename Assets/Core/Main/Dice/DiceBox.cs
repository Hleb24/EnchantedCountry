using System.Collections.Generic;

namespace Core.Main.Dice {
    public class DiceBox {
        private List<Dices> _setOfDice;
        public DiceBox(params Dices[] dices) {
            _setOfDice = new List<Dices>();
            for (int i = 0; i < dices.Length; i++) {
                _setOfDice.Add(dices[i]);
            }
        }
        public Dices this[int index] {
            get {
                return _setOfDice[index];
            }
        }
        public int GetCountSetOfDice() {
            return _setOfDice.Count;
        }

        public int SumRollsOfDice() {
            int sum = 0;
            foreach (var dice in _setOfDice) {
                sum += dice.RollOfDice();
            }
            return sum;
        }
        public int SumRollsOfDice(int edges) {
            int sum = 0;
            foreach (var dice in _setOfDice) {
                sum += dice.RollOfDice(edges);
            }
            return sum;
        }

        public void AddDiceInSetOfDict(Dices dices) {
            _setOfDice.Add(dices);
        }

        public void RemoveDiceFromSetOfDice(DiceType diceTypeForRemove) {
            for (int i = 0; i < _setOfDice.Count; i++) {
                if (_setOfDice[i].DiceType == diceTypeForRemove) {
                    _setOfDice.RemoveAt(i);
                    return;
                }
            }
        }
    }
}