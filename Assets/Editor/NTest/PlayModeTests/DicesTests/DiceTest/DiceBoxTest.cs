using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.DicesTests.DiceTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Dices")]
  public class DiceBoxTest {
    #region Preparation_for_tests
    private DiceBox _diceBox;

    [SetUp]
    public void InitFields() {
      _diceBox = new DiceBox(new SixSidedDice(DiceType.SixEdges), new TwelveSidedDice(DiceType.TwelveEdges));
    }

    [TearDown]
    public void DeleteFields() {
      _diceBox = null;
    }
    #endregion

    #region Tests
    [Test, Description("DiceBox has a set of dice"), TestOf("DiceBox")]
    public void DiceBoxHasNotNullSetOfDice() {
      for (var i = 0; i < _diceBox.GetCountSetOfDice(); i++) {
        Assert.That(_diceBox[i], Is.Not.Null);
      }
    }

    [Test, Description("Get rolls of dice value from set of dice and it at least 0"), TestOf("DiceBox"), Repeat(100)]
    public void GetRollsOfDiceValueFromSetOfDiceAndItAtLeastZero() {
      for (var i = 0; i < _diceBox.GetCountSetOfDice(); i++) {
        int rollValue = _diceBox[i].RollOfDice();
        Assert.That(rollValue, Is.AtLeast(0));
      }
    }

    [Test, Description("Sum rolls value from set of dice "), TestOf("DiceBox"), Repeat(100)]
    public void SumRollsOfDiceValueFromSetOfDice() {
      int sumOfRollsValue = _diceBox.SumRollsOfDice();
      Assert.That(sumOfRollsValue, Is.InRange(1, 18));
    }

    [Test, Description("Added dices in set of dice"), TestOf("DiceBox"), Repeat(100)]
    public void AddedDiceInSetOfDiceCheckCountAndCheckSum() {
      int preCount = _diceBox.GetCountSetOfDice();
      preCount++;
      _diceBox.AddDiceInSetOfDict(new SixSidedDice(DiceType.SixEdges));
      Assert.That(_diceBox.GetCountSetOfDice(), Is.EqualTo(preCount));
      Assert.That(_diceBox.SumRollsOfDice(), Is.InRange(1, 24));
    }

    [Test, Description("Remove dices from set of dice by DiceType"), TestOf("DiceBox"), Repeat(10)]
    public void RemoveDiceFromSetOfDiceAndCheckCount() {
      int preCount = _diceBox.GetCountSetOfDice();
      preCount--;
      _diceBox.RemoveDiceFromSetOfDice(DiceType.TwelveEdges);
      Assert.That(_diceBox.GetCountSetOfDice(), Is.EqualTo(preCount));
    }
    #endregion
  }
}