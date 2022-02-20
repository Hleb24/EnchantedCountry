using System.Collections;
using Core.Main.Dice;
using FluentAssertions;
using NUnit.Framework;
using Zenject;

namespace Editor.Tests.EditorTests {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Dice"), TestOf(typeof(Dice))]
  public class DiceTests : ZenjectUnitTestFixture {
    private static IEnumerable EdgeMinMaxValues() {
      yield return new[] { 2, 1, 2 };
      yield return new[] { 3, 1, 3 };
      yield return new[] { 4, 1, 4 };
      yield return new[] { 5, 1, 5 };
      yield return new[] { 6, 1, 6 };
    }

    private const int NumberOfRepeat = 10;

    [SetUp]
    public void OneTimeSetup() {
      Container.Bind<ThreeSidedDice>().AsSingle();
      Container.Bind<SixSidedDice>().AsSingle();
      Container.Bind<TwelveSidedDice>().AsSingle();
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

    [Test, Repeat(NumberOfRepeat), TestCaseSource(nameof(EdgeMinMaxValues))]
    public void Rolls_according_to_edges(int edge, int min, int max) {
      var sut = Container.Resolve<SixSidedDice>();

      int diceRollValue = sut.GetDiceRollAccordingToEdges(edge);

      diceRollValue.Should().BeInRange(min, max);
    }
  }
}