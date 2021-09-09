using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.GameRuleTests.WeapontTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("Weapon")]
  public class WeaponTest {
    #region Preparation_for_Tests
    private const string WEAPON_NAME = "Sword";
    private const string EFFECT_NORMAL_NAME = "";
    private const string DIVINITY_NAME = "divinity";
    private readonly Weapon.WeaponType _weaponType = Weapon.WeaponType.None;
    private Weapon _weapon;

    [SetUp]
    public void InitFields() {
      _weapon = new Weapon(1f, _weaponType, WEAPON_NAME);
    }

    [TearDown]
    public void DeleteFields() {
      _weapon = null;
    }
    #endregion

    #region Tests
    [Test, Description("Test weapon constructor"), Repeat(1)]
    public void TestWeaponConstructor() {
      Assert.That(_weapon.Attack.MinDamage, Is.AtLeast(0));
      Assert.That(_weapon.Attack.MaxDamage, Is.EqualTo(1f));
      Assert.That(_weapon.Attack.Accuracy, Is.EqualTo(0));
      Assert.That(_weapon.NameOfWeapon, Is.EqualTo(WEAPON_NAME));
      Assert.That(_weapon.EffectName, Is.EqualTo(EFFECT_NORMAL_NAME));
    }

    [Test, Description("Test add effect weapon"), Repeat(1)]
    public void TestAddEffectWeapon([Values(DIVINITY_NAME)] string n, [Values(7f)] float m, [Values(1)] int a) {
      _weapon.AddEffectOnWeapon(n, m, a);
      Assert.That(_weapon.Attack.MaxDamage, Is.EqualTo(7f));
      Assert.That(_weapon.Attack.Accuracy, Is.EqualTo(1));
      Assert.That(_weapon.EffectName, Is.EqualTo(DIVINITY_NAME));
    }

    [Test, Description("Test to damage"), Repeat(1)]
    public void ToDamageAtLeastZero() {
      Assert.That(_weapon.ToDamage(), Is.AtLeast(0));
    }

    [Test, Description("Test to damage from edge"), Repeat(100)]
    public void TestToDamageWithEdge() {
      _weapon = new Weapon(0, _weaponType, WEAPON_NAME);
      Assert.That(_weapon.ToDamage(), Is.EqualTo(0));
      _weapon = new Weapon(2.5f, _weaponType, WEAPON_NAME, 0.5f);
      Assert.That(_weapon.ToDamage(), Is.InRange(0.5, 2.5));
      _weapon = new Weapon(1.5f, _weaponType, WEAPON_NAME, 0.5f);
      Assert.That(_weapon.ToDamage(), Is.InRange(0.5, 1.5));
      _weapon = new Weapon(3f, _weaponType, WEAPON_NAME, 1f);
      Assert.That(_weapon.ToDamage(), Is.InRange(1, 3));
      _weapon = new Weapon(5f, _weaponType, WEAPON_NAME, 1f);
      Assert.That(_weapon.ToDamage(), Is.InRange(1, 5));
      _weapon = new Weapon(6, _weaponType, WEAPON_NAME, 1);
      Assert.That(_weapon.ToDamage(), Is.InRange(1, 6));
      _weapon = new Weapon(9f, _weaponType, WEAPON_NAME, 1f);
      Assert.That(_weapon.ToDamage(), Is.InRange(1, 9));
      _weapon = new Weapon(18f, _weaponType, WEAPON_NAME, 1f);
      Assert.That(_weapon.ToDamage(), Is.InRange(1, 18));
    }

    [Test, Description("Test type of weapon")]
    public void TestTypeOfWeapon() {
      _weapon.weaponType = Weapon.WeaponType.WarriorWeaponKit;
      Assert.That((_weapon.weaponType & Weapon.WeaponType.Catapult) != 0, Is.True);
      Assert.That((_weapon.weaponType & Weapon.WeaponType.CrossbowArrows) != 0, Is.True);
      _weapon.weaponType = Weapon.WeaponType.ElfWeaponKit;
      Assert.That((_weapon.weaponType & Weapon.WeaponType.TwoHandedSword) != 0, Is.False);
      Assert.That((_weapon.weaponType & Weapon.WeaponType.Dart) != 0, Is.True);
      _weapon.weaponType = Weapon.WeaponType.WizardWeaponKit;
      Assert.That((_weapon.weaponType & Weapon.WeaponType.Dart) != 0, Is.False);
      Assert.That((_weapon.weaponType & Weapon.WeaponType.GoldDagger) != 0, Is.True);
      _weapon.weaponType = Weapon.WeaponType.KronWeaponKit;
      Assert.That((_weapon.weaponType & Weapon.WeaponType.BattleAxe) != 0, Is.False);
      Assert.That((_weapon.weaponType & Weapon.WeaponType.ShortSword) != 0, Is.True);
      _weapon.weaponType = Weapon.WeaponType.GnomWeaponKit;
      Assert.That((_weapon.weaponType & Weapon.WeaponType.LongSword) != 0, Is.False);
      Assert.That((_weapon.weaponType & Weapon.WeaponType.Dart) != 0, Is.True);
    }
    #endregion
  }
}