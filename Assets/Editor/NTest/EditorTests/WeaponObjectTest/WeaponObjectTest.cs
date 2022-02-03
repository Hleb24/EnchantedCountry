using Core.Main.GameRule;
using Core.SO.WeaponObjects;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.WeaponObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("WeaponsObject")]
  public class WeaponObjectTest {
    #region Tests
    [Test, Description("Test Weapons Object On Enable"), Repeat(1)]
    public void TestWeaponsObjectOnEnable() {
      _weaponSO.weaponName = weaponName;
      _weaponSO.minDamage = _min;
      _weaponSO.maxDamage = _max;
      _weaponSO.accuracy = _accuracy;
      _weaponSO.weaponType = weaponType;
      _weaponSO.effectName = _effectName;
      _weaponSO.OnEnable();
      Assert.That(_weaponSO.weapon.NameOfWeapon, Is.EqualTo(weaponName));
      Assert.That(_weaponSO.weapon.Attack.MinDamage, Is.EqualTo(_min));
      Assert.That(_weaponSO.weapon.Attack.MaxDamage, Is.EqualTo(_max));
      Assert.That(_weaponSO.weapon.Attack.Accuracy, Is.EqualTo(_accuracy));
      Assert.That(_weaponSO.weapon.weaponType, Is.EqualTo(weaponType));
      Assert.That(_weaponSO.weapon.EffectName, Is.EqualTo(_effectName));
    }
    #endregion

    #region Preparation_for_Tests
    private WeaponSO _weaponSO;
    private readonly string weaponName = "Long Sword";
    private readonly int _min = 2;
    private readonly int _max = 7;
    private readonly int _accuracy = 0;
    private readonly string _effectName = "Normal";
    private readonly Weapon.WeaponType weaponType = Weapon.WeaponType.LongSword;

    [SetUp]
    public void InitFields() {
      _weaponSO = ScriptableObject.CreateInstance<WeaponSO>();
    }

    [TearDown]
    public void DeleteFields() {
      _weaponSO = null;
    }
    #endregion
  }
}