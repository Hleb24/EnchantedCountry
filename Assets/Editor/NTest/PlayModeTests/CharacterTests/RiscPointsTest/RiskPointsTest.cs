using Core.Rule.GameRule.RiskPoints;
using Core.SupportSystems.Data;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.CharacterTests.RiscPointsTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("RiskPoints")]
  public class RiskPointsTest {
    #region Preparation_for_Tests
    private RiskPoints _riskPoints;

    [SetUp]
    public void InitFields() {
      _riskPoints = new RiskPoints(new RiskPointsMock());
    }

    [TearDown]
    public void DeleteFields() {
      _riskPoints = null;
    }
    #endregion

    #region Tests
    [Test, Description("Risk point at least zero"), Repeat(1)]
    public void RiskPointAtLeastZero([Values(0, 1.25f, 2.50f)] float points) {
      _riskPoints.SetRiskPoints(points);
      Assert.That(_riskPoints.GetPoints, Is.AtLeast(0));
    }

    [Test, Description("Is dead"), Repeat(1)]
    public void RiskPoinsZeroOrLessAndIsDead() {
      _riskPoints.SetRiskPoints(0);
      Assert.That(_riskPoints.IsDead(), Is.True);
      _riskPoints.SetRiskPoints(-1);
      Assert.That(_riskPoints.IsDead(), Is.True);
    }
    #endregion

    internal class RiskPointsMock : IRiskPoints {
      private float _riskPoints;
      public float GetRiskPoints() {
        return _riskPoints;
      }

      public void SetRiskPoints(float riskPoints) {
         _riskPoints = riskPoints;
      }

      public void ChangeRiskPoints(float riskPoints) {
        _riskPoints += riskPoints;
      }

      public bool EnoughRiskPoints(float riskPoints) {
        return _riskPoints - riskPoints >= 0;
      }
    }
  }
}