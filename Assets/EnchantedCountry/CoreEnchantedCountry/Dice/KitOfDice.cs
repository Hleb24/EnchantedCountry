using System.Collections.Generic;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Dice {
    public static class KitOfDice {
        #region Fields
        public const string SetWithOneSixSidedDice = "Set With One Six-Sided Dice";
        public const string SetWithFourSixSidedDice = "Set With Four Six-Sided Dice";
        public const string SetWithOneThreeSidedAndOneSixSidedDice = "Set With One Three-Sided and One Six-Sided Dice";
        public const string SetWithOneTwelveSidedAndOneSixSidedDice = "Set With One Twelve-Sided and One Six-Sided  Dice";
        public static Dictionary<string, DiceBox> diceKit = new Dictionary<string, DiceBox> {
            [SetWithOneSixSidedDice] = new DiceBox(new SixSidedDice(DiceType.SixEdges)),
            [SetWithOneThreeSidedAndOneSixSidedDice] = new DiceBox(new ThreeSidedDice(DiceType.ThreeEdges), new SixSidedDice(DiceType.SixEdges)),
            [SetWithFourSixSidedDice] = new DiceBox(new SixSidedDice(DiceType.SixEdges), new SixSidedDice(DiceType.SixEdges), new SixSidedDice(DiceType.SixEdges),
                new SixSidedDice(DiceType.SixEdges)),
            [SetWithOneTwelveSidedAndOneSixSidedDice] = new DiceBox(new TwelveSidedDice(DiceType.TwelveEdges), new SixSidedDice(DiceType.SixEdges))
        };
        #endregion
    }
}
