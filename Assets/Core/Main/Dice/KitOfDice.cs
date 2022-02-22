using System.Collections.Generic;

namespace Core.Main.Dice {
  public static class KitOfDice {
    private static readonly Dice[] OneThreeSidedAndOneSixSidedDice = { new ThreeSidedDice(), new SixSidedDice() };
    private static readonly Dice[] OneSixSidedDice = { new SixSidedDice() };
    private static readonly Dice[] OneTwelveSidedAndOneSixSidedDice = { new TwelveSidedDice(), new SixSidedDice() };
    private static readonly Dice[] FourSixSidedDice = { new SixSidedDice(), new SixSidedDice(), new SixSidedDice(), new SixSidedDice() };

    public static readonly Dictionary<string, DiceBox> DicesKit = new() {
      [SET_WITH_ONE_THREE_SIDED_AND_ONE_SIX_SIDED_DICE] = new DiceBox(OneThreeSidedAndOneSixSidedDice),
      [SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE] = new DiceBox(OneTwelveSidedAndOneSixSidedDice),
      [SET_WITH_FOUR_SIX_SIDED_DICE] = new DiceBox(FourSixSidedDice),
      [SET_WITH_ONE_SIX_SIDED_DICE] = new DiceBox(OneSixSidedDice)
    };

    public const string SET_WITH_ONE_THREE_SIDED_AND_ONE_SIX_SIDED_DICE = "SetWithOneThreeSidedAndOneSixSidedDice";
    public const string SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE = "SetWithOneTwelveSidedAndOneSixSidedDice";
    public const string SET_WITH_FOUR_SIX_SIDED_DICE = "SetWithFourSixSidedDice";
    public const string SET_WITH_ONE_SIX_SIDED_DICE = "SetWithOnSixSidedDice";
  }
}