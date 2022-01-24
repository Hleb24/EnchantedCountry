using Core.Main.GameRule;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.GameRuleTests.ArmorClassTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("ArmorClass")]
  public class ArmorClassTest {
    #region Preparation_for_Tests
    private ArmorClass _armorClass;

    [SetUp]
    public void InitFields() {
      _armorClass = new ArmorClass();
    }

    [TearDown]
    public void DeleteFields() {
      _armorClass = null;
    }
    #endregion

    #region Tests
    [Test, Description("Check borders"), Repeat(1)]
    public void ArmorClassAtLeastMinusThreeAndAtMostEight([Values(-3, 8)] int arClass) {
      _armorClass.ClassOfArmor = arClass;
      Assert.That(_armorClass.ClassOfArmor, Is.AtLeast(-3).And.AtMost(8));
    }

    [Test, Description("Is kill only spell"), Repeat(1)]
    public void IsKillOnlySpellIfArmorClassMinusThree([Values(-2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8)] int arClass) {
      _armorClass.ClassOfArmor = -3;
      Assert.That(_armorClass.isKillOnlySpell, Is.True);
      _armorClass.ClassOfArmor = arClass;
      Assert.That(_armorClass.isKillOnlySpell, Is.False);
    }

    [Test, Description("Chek Number of Points To Hit"), Repeat(1), Sequential]
    public void NumberOfPointsToHitAtLeastNineAndAtMostEighteenAndEqualToArmorClass([Values(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 18)] int pointsToHit,
      [Values(16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2)]
      int arClass) {
      _armorClass.ClassOfArmor = arClass;
      Assert.That(_armorClass.NumberOfPointsToHit, Is.EqualTo(pointsToHit));
      _armorClass.ClassOfArmor = -3;
      Assert.That(_armorClass.isKillOnlySpell, Is.True);
    }
    #endregion
  }
}