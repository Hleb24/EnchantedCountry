using Core.Main.Dice;
using Core.Support.Data.DiceRoll;
using FluentAssertions;
using NUnit.Framework;
using Zenject;

namespace Editor.Tests.EditorTests {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("QualityDiceRoll"), TestOf(typeof(Dice))]
  public class DiceRollTests : ZenjectUnitTestFixture {
    [SetUp]
    public void ContainerSetup() {
      Container.Bind<DiceRollDataScroll>().FromInstance(new DiceRollDataScroll(DiceRollScribe.StartRollValues));
      Container.Bind<IDiceRoll>().To<DiceRollScribe>().AsSingle();
    }

    [Test, TestCase(16, 2, 18), TestCase(8, -3, 5)]
    public void Change_first_roll_value_after_dice_roll(int start, int increment, int end) {
      var sut = Container.Resolve<IDiceRoll>();
      sut.SetStatsRoll(QualityRolls.First, start);

      sut.ChangeStatsRoll(QualityRolls.First, increment);

      sut.GetQualitiesRoll(QualityRolls.First).Should().Be(end);
    }

    [Test]
    public void Dice_roll_not_in_range_for_get() {
      var sut = Container.Resolve<DiceRollDataScroll>();

      int value = sut.GetDiceRollValue(DiceRollScribe.StartRollValues.Length);

      value.Should().Be(-1);
    }
  }
}