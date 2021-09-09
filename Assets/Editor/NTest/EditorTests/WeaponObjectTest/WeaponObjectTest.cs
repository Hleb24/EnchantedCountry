using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.ScriptableObject.WeaponObjects;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.WeaponObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("WeaponsObject")]
  public class WeaponObjectTest {
    #region Tests
    [Test, Description("Test Weapons Object On Enable"), Repeat(1)]
    public void TestWeaponsObjectOnEnable() {
      _weaponObject.weaponName = weaponName;
      _weaponObject.minDamage = _min;
      _weaponObject.maxDamage = _max;
      _weaponObject.accurancy = _accuracy;
      _weaponObject.weaponType = weaponType;
      _weaponObject.effectName = _effectName;
      _weaponObject.OnEnable();
      Assert.That(_weaponObject.weapon.NameOfWeapon, Is.EqualTo(weaponName));
      Assert.That(_weaponObject.weapon.Attack.MinDamage, Is.EqualTo(_min));
      Assert.That(_weaponObject.weapon.Attack.MaxDamage, Is.EqualTo(_max));
      Assert.That(_weaponObject.weapon.Attack.Accuracy, Is.EqualTo(_accuracy));
      Assert.That(_weaponObject.weapon.weaponType, Is.EqualTo(weaponType));
      Assert.That(_weaponObject.weapon.EffectName, Is.EqualTo(_effectName));
    }
    #endregion

    #region Preparation_for_Tests
    private WeaponObject _weaponObject;
    private readonly string weaponName = "Long Sword";
    private readonly int _min = 2;
    private readonly int _max = 7;
    private readonly int _accuracy = 0;
    private readonly string _effectName = "Normal";
    private readonly Weapon.WeaponType weaponType = Weapon.WeaponType.LongSword;

    [SetUp]
    public void InitFields() {
      _weaponObject = ScriptableObject.CreateInstance<WeaponObject>();
    }

    [TearDown]
    public void DeleteFields() {
      _weaponObject = null;
    }
    #endregion
  }
}