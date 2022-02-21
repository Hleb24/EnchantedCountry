using System.Collections.Generic;

namespace Core.Main.Dice {
  public static class KitOfDice {
    private static readonly Dice[] OneThreeSidedAndOneSixSidedDice = { new ThreeSidedDice(), new SixSidedDice() };
    private static readonly Dice[] OneSixSidedDice = { new SixSidedDice() };
    private static readonly Dice[] OneTwelveSidedAndOneSixSidedDice = { new TwelveSidedDice(), new SixSidedDice() };
    private static readonly Dice[] FourSixSidedDice = { new SixSidedDice(), new SixSidedDice(), new SixSidedDice(), new SixSidedDice() };

    public static readonly Dictionary<string, DiceBox> DicesKit = new Dictionary<string, DiceBox> {
      [SetWithOneThreeSidedAndOneSixSidedDice] = new DiceBox(OneThreeSidedAndOneSixSidedDice),
      [SetWithOneTwelveSidedAndOneSixSidedDice] = new DiceBox(OneTwelveSidedAndOneSixSidedDice),
      [SetWithFourSixSidedDice] = new DiceBox(FourSixSidedDice),
      [SetWithOneSixSidedDice] = new DiceBox(OneSixSidedDice)
    };

    public const string SetWithOneThreeSidedAndOneSixSidedDice = "SetWithOneThreeSidedAndOneSixSidedDice";
    public const string SetWithOneTwelveSidedAndOneSixSidedDice = "SetWithOneTwelveSidedAndOneSixSidedDice";
    public const string SetWithFourSixSidedDice = "SetWithFourSixSidedDice";
    public const string SetWithOneSixSidedDice = "SetWithOnSixSidedDice";
  }
}