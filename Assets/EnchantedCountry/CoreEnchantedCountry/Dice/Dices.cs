using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Dice {
    public enum DiceType {
        TwoEdges = 2,
        ThreeEdges = 3,
        FourEdges = 4,
        FiveEdges = 5,
        SixEdges = 6,
        TwelveEdges = 12
    }
    public abstract class Dices : IComparable<Dices> {
        #region Fields
        protected int[] _edges;
        #endregion
        #region Constructors
        protected Dices() { }
        protected Dices(int range, params int[] values) {
            _edges = new int[range];
            for (int i = 0; i < _edges.Length; i++) {
                _edges[i] = values[i];
            }
        }
        protected Dices(DiceType diceTypeForConstructor) {
            // ReSharper disable once VirtualMemberCallInConstructor
            if (diceTypeForConstructor == DiceType.ThreeEdges && diceTypeForConstructor == DiceType) {
                _edges = new[] { 0, 0, 2, 2, 3, 3 };
                return;
            }
            // ReSharper disable once VirtualMemberCallInConstructor
            if (diceTypeForConstructor == DiceType.SixEdges && diceTypeForConstructor == DiceType) {
                _edges = new[] { 1, 2, 3, 4, 5, 6 };
                return;
            }
            // ReSharper disable once VirtualMemberCallInConstructor
            if (diceTypeForConstructor == DiceType.TwelveEdges && diceTypeForConstructor == DiceType) {
                _edges = new[] { 0, 0, 6, 6, 12, 12 };
                return;
            }
            throw new InvalidOperationException("Dice type is  invalid");
        }
        #endregion
        #region Properties
        public abstract DiceType DiceType { get; }
        #endregion
        #region Indexers
        public abstract int this[int index] { get; }
        #endregion
        #region Methods
        public abstract int RollOfDice();
        public abstract int RollOfDice(int edges);

        public int CompareTo(Dices other) {
            int result = 0;
            if (other != null) {
                if (this._edges.Length > other._edges.Length) {
                    result = 1;
                    return result;
                }
                if (this._edges.Length < other._edges.Length) {
                    result = -1;
                    return result;
                }
                if (this._edges.Length == other._edges.Length) {
                    for (int i = 0; i < _edges.Length; i++) {
                        if (this._edges[i] > other._edges[i]) {
                            result = 1;
                            return result;
                        }
                        if (this._edges[i] < other._edges[i]) {
                            result = -1;
                            return result;
                        }
                    }
                    return result;
                }
            } else {
                throw new ArgumentException("Parameter in not Dices");
            }
            return result;
        }
        #endregion
    }
}