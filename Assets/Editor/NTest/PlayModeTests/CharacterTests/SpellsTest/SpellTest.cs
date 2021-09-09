using Core.EnchantedCountry.CoreEnchantedCountry.Character.Spell;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.CharacterTests.SpellsTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("Spell")]
  public class SpellTest {
    [Test, Description("Test Spell Construct"), Repeat(1)]
    public void TestSpellConstruct() {
      Assert.That(_spell.SpellName, Is.EqualTo(string.Empty));
      Assert.That(_spell.Level, Is.EqualTo(1));
      Assert.That(_spell.NumberOfUses, Is.AtLeast(0));
      Assert.That(_spell.LuckRoll, Is.InRange(0, 18));
    }

    #region Preparation_for_Tests
    private Spell _spell;

    [SetUp]
    public void InitFields() {
      _spell = new Spell();
    }

    [TearDown]
    public void DeleteFields() {
      _spell = null;
    }
    #endregion
  }
}