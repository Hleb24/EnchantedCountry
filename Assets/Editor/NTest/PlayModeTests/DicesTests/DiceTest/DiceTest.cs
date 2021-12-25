using Core.Rule.Dice;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Editor.NTest.PlayModeTests.DicesTests.DiceTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Dices"), TestOf("Dices")]
  public class DiceTest {
    #region Preparation_for_Tests
    private Dices _twelveSidedDice;
    private Dices _sixSidedDice;
    private Dices _threeSidedDice;

    [SetUp]
    public void InitDices() {
      _twelveSidedDice = new TwelveSidedDice(DiceType.TwelveEdges);
      _sixSidedDice = new SixSidedDice(DiceType.SixEdges);
      _threeSidedDice = new ThreeSidedDice(DiceType.ThreeEdges);
    }

    [TearDown]
    public void DeleteDices() {
      _sixSidedDice = null;
      _twelveSidedDice = null;
      _threeSidedDice = null;
    }

    private readonly ReusableConstraint _isNotNull = Is.Not.Null;
    #endregion

    #region Tests
    [Test, Order(1), Description("Array _edge is not Empty"), TestOf("Dices")]
    public void ArrayEdgeNotEmpty() {
      Assert.That(_sixSidedDice[0], _isNotNull);
      Assert.That(_threeSidedDice[0], _isNotNull);
      Assert.That(_twelveSidedDice[0], _isNotNull);
    }

    #region ThreeSidedDiceTest
    [Test, Description("ThreeSidedDice roll is 0 or positive"), Repeat(100), MaxTime(30), TestOf("ThreeSidedDice"), Property("First Test", ">=0")]
    public void RollOfThreeSidedDiceGreaterOrEqualThanZero() {
      int rollValue = _threeSidedDice.RollOfDice();
      Assert.That(rollValue, Is.AtLeast(0));
    }

    [Test, Description("ThreeSidedDice roll with values: 0, 6, 12"), Repeat(100), MaxTime(35), TestOf("ThreeSidedDice")]
    public void RollThreeSidedDiceAndGetZeroSixOrTwelve() {
      int rollValue = _threeSidedDice.RollOfDice();
      Assert.That(rollValue, Is.EqualTo(0).Or.EqualTo(2).Or.EqualTo(3));
    }
    #endregion

    #region SixSidedDiceTest
    [Test, Description("SixSidedDice roll is 0 or positive"), Repeat(100), TestOf("SixSidedDice")]
    public void RollOfWhiteDiceGreaterOrEqualThanZero() {
      int rollValue = _sixSidedDice.RollOfDice();
      Assert.That(rollValue, Is.AtLeast(0));
    }

    [Test, Description("SixSidedDice roll with value: 1,2,3,4,5,6"), Repeat(100), TestOf("SixSidedDice")]
    public void RollSixSidedDiceAndGetOneTwoThreeFourFiveOrSix() {
      int rollValue = _sixSidedDice.RollOfDice();
      Assert.That(rollValue, Is.InRange(1, 6));
    }

    [Test, Description("Test SixSidedDice get damage"), Repeat(100), TestOf("SixSidedDice"), Sequential]
    public void TestSixSidedDiceGetDamage([Values(2, 3, 4, 5, 6)] int edges, [Values(2, 3, 4, 5, 6)] int topBorder) {
      Assert.That(_sixSidedDice.RollOfDice(edges), Is.InRange(1, topBorder));
    }
    #endregion

    #region TwelveSidedDiceTest
    [Test, Description("TwelveSidedDice roll is 0 or positive"), Repeat(100), MaxTime(30), TestOf("TwelveSidedDice"), Property("First Test", ">=0")]
    public void RollOfBlackDiceGreaterOrEqualThanZero() {
      int rollValue = _twelveSidedDice.RollOfDice();
      Assert.That(rollValue, Is.AtLeast(0));
    }

    [Test, Description("TwelveSidedDice roll with values: 0, 6, 12"), Repeat(100), MaxTime(35), TestOf("TwelveSidedDice")]
    public void RollTwelveSidedDiceAndGetZeroSixOrTwelve() {
      int rollValue = _twelveSidedDice.RollOfDice();
      Assert.That(rollValue, Is.EqualTo(0).Or.EqualTo(6).Or.EqualTo(12));
    }

    [Test, Description("TwelveSidedDice roll in range: 0-12"), Repeat(100), TestOf("TwelveSidedDice")]
    public void RollTwelveSidedDiceAndValuesInRangeFromZeroToTwelve() {
      int rollValue = _twelveSidedDice.RollOfDice();
      Assert.That(rollValue, Is.InRange(0, 12));
    }
    #endregion
    #endregion
  }
}