using Core.Main.GameRule;
using Core.SO.Armor;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.ArmorObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("ArmorObject")]
  public class ArmorObjectTest {
    #region Preparation_for_Tests
    private ArmorSO _armorSo;
    private readonly Armor.ArmorType _armorType = Armor.ArmorType.No;
    private readonly string _armorName = "No";
    private readonly int _classOfArmor = 8;
    private readonly string _effectName = "";

    [SetUp]
    public void InitFields() {
      _armorSo = ScriptableObject.CreateInstance<ArmorSO>();
    }

    [TearDown]
    public void DeleteFields() {
      _armorSo = null;
    }
    #endregion

    #region Tests
    [Test, Description("Test Armor Object On Enable"), Repeat(1)]
    public void TestArmorObjectOnEnable() {
      _armorSo.armorName = _armorName;
      _armorSo.armorType = _armorType;
      _armorSo.classOfArmor = _classOfArmor;
      _armorSo.effectName = _effectName;
      _armorSo.OnEnable();
      Assert.That(_armorSo.armor.ArmorName, Is.EqualTo(_armorName));
      Assert.That(_armorSo.armor.armorType, Is.EqualTo(_armorType));
      Assert.That(_armorSo.armor.ArmorClass.ClassOfArmor, Is.EqualTo(_classOfArmor));
      Assert.That(_armorSo.armor.EffectName, Is.EqualTo(_effectName));
    }
  }
  #endregion
}