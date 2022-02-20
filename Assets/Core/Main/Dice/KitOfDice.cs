using System.Collections.Generic;

namespace Core.Main.Dice {
  public static class KitOfDice {
    private static readonly Dice[] OneSixSidedDice = { new SixSidedDice() };
    private static readonly Dice[] OneThreeSidedAndOneSixSidedDice = { new ThreeSidedDice(), new SixSidedDice() };
    private static readonly Dice[] OneTwelveSidedAndOneSixSidedDice = { new TwelveSidedDice(), new SixSidedDice() };
    private static readonly Dice[] FourSixSidedDice = { new SixSidedDice(), new SixSidedDice(), new SixSidedDice(), new SixSidedDice() };

    public static readonly Dictionary<string, DiceBox> DiceKit = new Dictionary<string, DiceBox> {
      [SetWithOneSixSidedDice] = new DiceBox(OneSixSidedDice),
      [SetWithOneThreeSidedAndOneSixSidedDice] = new DiceBox(OneThreeSidedAndOneSixSidedDice),
      [SetWithOneTwelveSidedAndOneSixSidedDice] = new DiceBox(OneTwelveSidedAndOneSixSidedDice),
      [SetWithFourSixSidedDice] = new DiceBox(FourSixSidedDice)
    };

    public const string SetWithOneSixSidedDice = "SetWithOneSixSidedDice";
    public const string SetWithOneThreeSidedAndOneSixSidedDice = "SetWithOneThreeSidedAndOneSixSidedDice";
    public const string SetWithOneTwelveSidedAndOneSixSidedDice = "SetWithOneTwelveSidedAndOneSixSidedDice";
    public const string SetWithFourSixSidedDice = "SetWithFourSixSidedDice";
  }
}