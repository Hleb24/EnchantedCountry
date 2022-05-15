using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.GameRule.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Main.NonPlayerCharacters.Item {
  public class WeaponSet {
    private readonly List<Weapon> _weapons;
    private readonly bool _hasWeapon;
    private readonly int _numberOfWeapon;
    private Weapon _meleeWeapon;
    private Weapon _rangeWeapon;
    private Weapon _projectiles;

    public WeaponSet([NotNull, ItemNotNull] List<Weapon> weapons) {
      _weapons = weapons;
      Debug.LogWarning("Weapons count " + weapons.Count);
      if (_weapons.IsEmpty()) {
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

      accuracy = _weapons[GetIndexOfWeapon(weaponIndex)].GetAccuracy();
      return accuracy;
    }

    private int GetIndexOfWeapon(int weaponIndex = 0) {
      return weaponIndex;
    }

    private bool WeaponsNotExists(int weaponIndex) {
      return _weapons == null || _weapons.Count <= weaponIndex;
    }
  }
}