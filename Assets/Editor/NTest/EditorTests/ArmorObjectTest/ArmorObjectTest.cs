using Core.Rule.GameRule.Armor;
using Core.ScriptableObject.Armor;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.ArmorObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("ArmorObject")]
  public class ArmorObjectTest {
    #region Preparation_for_Tests
    private ArmorObject _armorObject;
    private readonly Armor.ArmorType _armorType = Armor.ArmorType.No;
    private readonly string _armorName = "No";
    private readonly int _classOfArmor = 8;
    private readonly string _effectName = "";

    [SetUp]
    public void InitFields() {
      _armorObject = ScriptableObject.CreateInstance<ArmorObject>();
    }

    [TearDown]
    public void DeleteFields() {
      _armorObject = null;
    }
    #endregion

    #region Tests
    [Test, Description("Test Armor Object On Enable"), Repeat(1)]
    public void TestArmorObjectOnEnable() {
      _armorObject.armorName = _armorName;
      _armorObject.armorType = _armorType;
      _armorObject.classOfArmor = _classOfArmor;
      _armorObject.effectName = _effectName;
      _armorObject.OnEnable();
      Assert.That(_armorObject.armor.ArmorName, Is.EqualTo(_armorName));
      Assert.That(_armorObject.armor.armorType, Is.EqualTo(_armorType));
      Assert.That(_armorObject.armor.ArmorClass.ClassOfArmor, Is.EqualTo(_classOfArmor));
      Assert.That(_armorObject.armor.EffectName, Is.EqualTo(_effectName));
    }
  }
  #endregion
}