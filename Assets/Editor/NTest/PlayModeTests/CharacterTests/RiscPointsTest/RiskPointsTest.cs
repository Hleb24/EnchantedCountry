using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.CharacterTests.RiscPointsTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("RiskPoints")]
  public class RiskPointsTest {
    #region Preparation_for_Tests
    private RiskPoints _riskPoints;

    [SetUp]
    public void InitFields() {
      _riskPoints = new RiskPoints();
    }

    [TearDown]
    public void DeleteFields() {
      _riskPoints = null;
    }
    #endregion

    #region Tests
    [Test, Description("Risk point at least zero"), Repeat(1)]
    public void RiskPointAtLeastZero([Values(0, 1.25f, 2.50f)] float points) {
      _riskPoints.Points = points;
      Assert.That(_riskPoints.Points, Is.AtLeast(0));
    }

    [Test, Description("Is dead"), Repeat(1)]
    public void RiskPoinsZeroOrLessAndIsDead() {
      _riskPoints.Points = 0;
      Assert.That(_riskPoints.isDead, Is.True);
      _riskPoints.Points = -1;
      Assert.That(_riskPoints.isDead, Is.True);
    }
    #endregion
  }
}