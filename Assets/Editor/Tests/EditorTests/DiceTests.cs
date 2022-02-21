using System.Collections;
using System.Collections.Generic;
using Core.Main.Dice;
using FluentAssertions;
using NUnit.Framework;
using Zenject;

namespace Editor.Tests.EditorTests {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Dice"), TestOf(typeof(Dice))]
  public class DiceTests : ZenjectUnitTestFixture {
    private static IEnumerable GetEdgeAndMinMaxDiceRollValues() {
      yield return new[] { 2, 1, 2 };
      yield return new[] { 3, 1, 3 };
      yield return new[] { 4, 1, 4 };
      yield return new[] { 5, 1, 5 };
      yield return new[] { 6, 1, 6 };
    }

    private static IEnumerable<(Dice[], int)> GetDicesInBoxAndHisAmount() {
      yield return (new Dice[] { new ThreeSidedDice(), new SixSidedDice() }, 2);
      yield return (new Dice[] { new TwelveSidedDice() }, 1);
      yield return (new Dice[] { new SixSidedDice(), new SixSidedDice(), new SixSidedDice(), new SixSidedDice() }, 4);
    }

    private static IEnumerable<(Dice[], (int, int))> GetDicesInBoxAndHisMinAndMaxSum() {
      yield return (new Dice[] { new ThreeSidedDice(), new SixSidedDice() }, (1, 9));
      yield return (new Dice[] { new TwelveSidedDice(), new SixSidedDice() }, (1, 18));
      yield return (new Dice[] { new SixSidedDice(), new SixSidedDice(), new SixSidedDice(), new SixSidedDice() }, (4, 24));
    }

    private const int NumberOfRepeat = 10;
    private const int NumberOfRepeatForBoxDicesRoll = 36;

    [SetUp]
    public void OneTimeSetup() {
      Container.Bind<ThreeSidedDice>().AsSingle();
      Container.Bind<SixSidedDice>().AsSingle();
      Container.Bind<TwelveSidedDice>().AsSingle();
      Container.Bind<DiceRollCalculator>().AsSingle();
    }

    [Test, Repeat(NumberOfRepeat)]
    public void Rolls_of_three_sided_dice() {
      Dice sut = Container.Resolve<ThreeSidedDice>();

      int diceRollValue = sut.GetDiceRoll();

      diceRollValue.Should().BeOneOf(0, 2, 3);
    }

    [Test, Repeat(NumberOfRepeat)]
    public void Rolls_of_six_sided_dice() {
      Dice sut = Container.Resolve<SixSidedDice>();

      int diceRollValue = sut.GetDiceRoll();

      diceRollValue.Should().BeInRange(1, 6);
    }

    [Test, Repeat(NumberOfRepeat)]
    public void Rolls_of_twelve_sided_dice() {
      Dice sut = Container.Resolve<TwelveSidedDice>();

      int diceRollValue = sut.GetDiceRoll();

      diceRollValue.Should().BeOneOf(0, 6, 12);
    }

    [Test, Repeat(NumberOfRepeat), TestCaseSource(nameof(GetEdgeAndMinMaxDiceRollValues))]
    public void Rolls_according_to_edges(int edge, int min, int max) {
      var sut = Container.Resolve<SixSidedDice>();

      int diceRollValue = sut.GetDiceRollAccordingToEdges(edge);

      diceRollValue.Should().BeInRange(min, max);
    }

    [Test, TestCaseSource(nameof(GetDicesInBoxAndHisAmount))]
    public void Check_number_of_dices_in_dice_box((Dice[] dices, int numberOfDices) tuple) {
      var sut = new DiceBox(tuple.dices);

      int numberOfDicesInBox = sut.GetNumberOfDicesInBox();

      numberOfDicesInBox.Should().Be(tuple.numberOfDices);
    }

    [Test, Repeat(NumberOfRepeatForBoxDicesRoll), TestCaseSource(nameof(GetDicesInBoxAndHisMinAndMaxSum))]
    public void Sum_of_roll_box_dices((Dice[] dices, (int min, int max) minMaxSum ) box) {
      var sut = new DiceBox(box.dices);

      int numberOfDicesInBox = sut.GetSumRollOfBoxDices();

      numberOfDicesInBox.Should().BeInRange(box.minMaxSum.min, box.minMaxSum.max);
    }

    [Test, Repeat(NumberOfRepeatForBoxDicesRoll)]
    public void Roll_dices_for_qualities() {
      var sut = Container.Resolve<DiceRollCalculator>();

      int rollForQuality = sut.GetSumDiceRollForQuality();

      rollForQuality.Should().BeInRange(3, 18);
    }

    [Test, Repeat(NumberOfRepeatForBoxDicesRoll)]
    public void Roll_dices_for_coins() {
      var sut = Container.Resolve<DiceRollCalculator>();

      int rollForCoins = sut.GetSumDiceRollForCoins();

      rollForCoins.Should().BeInRange(30, 180);
    }
  }
}