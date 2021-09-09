using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Editor.NTest.PlayModeTests.DicesTests.DiceTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Dices")]
  public class KitOfDiceTest {
    #region Preparation_for_Tests
    private readonly ReusableConstraint _isNotNull = Is.Not.Null;
    #endregion

    #region Tests
    [Test, Description("Dictionary for fight, and characteristics in diceBox is not null"), TestOf("KitOfDice"), Repeat(10)]
    public void KitOfDiceDictionaryIsNotNull() {
      Assert.That(KitOfDice.diceKit, _isNotNull);
    }

    [Test, Description("Dictionary for fight and characteristics has correctly counts"), TestOf("KitOfDice"), Repeat(10)]
    public void SetFightAndCharacteristicsHasCorrectlyCount() {
      Assert.That(KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice].GetCountSetOfDice(), Is.EqualTo(4));
      Assert.That(KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetCountSetOfDice(), Is.EqualTo(2));
    }

    [Test, Description("Check sum of kits in range"), TestOf("KitOfDice"), Repeat(1000)]
    public void CheckSumSetForFightAndCharacteristictsInRange() {
      Assert.That(KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice].SumRollsOfDice(), Is.InRange(4, 24));
      Assert.That(KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice(), Is.InRange(1, 18));
      Assert.That(KitOfDice.diceKit[KitOfDice.SetWithOneThreeSidedAndOneSixSidedDice].SumRollsOfDice(), Is.InRange(1, 9));
    }
    #endregion
  }
}