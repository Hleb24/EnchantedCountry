using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Attack;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.GameRuleTests.AttackTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("Attack")]
  public class AttackTest {
    #region Preparation_for_Tests
    private Attack _attack;

    [SetUp]
    public void InitFields() {
      _attack = new Attack();
    }

    [TearDown]
    public void DeleteFields() {
      _attack = null;
    }
    #endregion

    #region Tests
    [Test, Description("Accuracy at least zero"), Repeat(1)]
    public void AccuracyAtLeastZero([Values(0, 1, 2, 17)] int accuracy) {
      _attack.Accuracy = accuracy;
      Assert.That(_attack.Accuracy, Is.AtLeast(0));
    }

    [Test, Description("Min damage at least zero"), Repeat(1)]
    public void MinDamageAtLeastZero([Values(0, 1, 2, 18)] float damage) {
      _attack.MinDamage = damage;
      Assert.That(_attack.MinDamage, Is.AtLeast(0));
    }

    [Test, Description("Max damage at least zero"), Repeat(1)]
    public void MaxDamageAtLeastZero([Values(0.25f, 1, 2, 18)] float damage) {
      _attack.MaxDamage = damage;
      Assert.That(_attack.MaxDamage, Is.AtLeast(0));
    }

    [Test, Description("Edges at least Zero"), Repeat(1), Sequential]
    public void EdgesAtLeastZero([Values(0, 0.5f, 1, 1, 1, 1, 1)] float minDamage, [Values(1, 2.5f, 3, 5, 6, 9, 18)] float maxDamage) {
      _attack.MinDamage = minDamage;
      _attack.MaxDamage = maxDamage;
      Assert.That(_attack.DiceEdges, Is.AtLeast(0));
    }
    #endregion
  }
}