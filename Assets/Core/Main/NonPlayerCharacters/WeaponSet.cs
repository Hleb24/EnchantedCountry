using System.Collections.Generic;
using Core.Main.GameRule;

namespace Core.Main.NonPlayerCharacters {
  public class WeaponSet {
    private Weapon _meleeWeapon;
    private Weapon _rangeWeapon;
    private Weapon _projectiles;
    private readonly List<Weapon> _weapons;
    private readonly bool _hasWeapon;
    private readonly int _numberOfWeapon;

    public WeaponSet(List<Weapon> weapons) {
      _weapons = weapons;
      if (_weapons.Count <= 0) {
        return;
      }

      _hasWeapon = true;
      _numberOfWeapon = _weapons.Count;
    }

    public bool HasWeapon() {
      return _hasWeapon;
    }

    public int GetNumberOfWeapon() {
      return _numberOfWeapon;
    }

    public float ToDamage(int weaponIndex) {
      var damage = 0.0f;
      if (WeaponsNotExists(weaponIndex)) {
        return damage;
      }

      damage = _weapons[weaponIndex].ToDamage();
      return damage;
    }

    public int GetAccuracy(int weaponIndex = 0) {
      var accuracy = 0;
      if (WeaponsNotExists(weaponIndex)) {
        return accuracy;
      }

      accuracy = _weapons[GetIndexOfWeapon(weaponIndex)].Attack.Accuracy;

      return accuracy;
    }

    private int GetIndexOfWeapon(int weaponIndex = 0) {
      return weaponIndex;
    }

    private bool WeaponsNotExists(int weaponIndex) {
      return _weapons is null || _weapons.Count < 0 || _weapons.Count <= weaponIndex;
    }
  }
}