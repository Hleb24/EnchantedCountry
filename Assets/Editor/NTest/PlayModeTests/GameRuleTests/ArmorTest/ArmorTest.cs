using Core.Main.GameRule;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.GameRuleTests.ArmorTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("Armor")]
  public class ArmorTest {
    #region Preparation_for_Tests
    private Armor _armor;
    private string _effectName = string.Empty;
    private readonly string _effectDivinty = "Divinity";

    [SetUp]
    public void InitFields() {
      _armor = new Armor();
    }

    [TearDown]
    public void DeleteFields() {
      _armor = null;
    }
    #endregion

    #region Tests
    [Test, Description("Test armor constructor"), Repeat(1)]
    public void TestArmorConstructor() {
      Assert.That(_armor.ArmorClass.ClassOfArmor, Is.EqualTo(8));
      Assert.That(_armor.ArmorName, Is.EqualTo(Armor.DEFAULT_NAME_FOR_ARMOR));
    }

    [Test, Description("Test effect on armor"), Repeat(1)]
    public void TestEffectOnArmor() {
      _armor.AddEffectOnArmor(_effectDivinty, -1);
      Assert.That(_armor.EffectName, Is.EqualTo(_effectDivinty));
      Assert.That(_armor.ArmorClass.ClassOfArmor, Is.EqualTo(7));
    }
    #endregion
  }
}