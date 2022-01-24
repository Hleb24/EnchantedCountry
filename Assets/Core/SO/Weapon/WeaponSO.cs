using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.SO.Weapon {
  [Serializable, CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
  public class WeaponSO : ScriptableObject {
    #region Fields
    public Main.GameRule.Weapon.WeaponType weaponType = Main.GameRule.Weapon.WeaponType.None;
    public string weaponName = "";
    public float minDamage;
    public float maxDamage;
    public List<float> damageList;
    public int accurancy;
    public string effectName = "";
    public Main.GameRule.Weapon weapon;
    public int id;
    #endregion

    #region Methods
    public void OnEnable() {
      InitWeapon();
    }

    public void OnValidate() {
      if (damageList.Count != 0 || damageList != null) {
        InitWeaponWithDamageList();
      } else {
        InitWeapon();
      }

      weaponName = name;
    }

    public Main.GameRule.Weapon InitWeapon() {
      if (weapon == null) {
        weapon = new Main.GameRule.Weapon(maxDamage, weaponType, weaponName, minDamage, accurancy, effectName, id);
      } else {
        weapon.Init(maxDamage, weaponType, weaponName, minDamage, accurancy, effectName, id);
      }

      return weapon;
    }

    public Main.GameRule.Weapon InitWeaponWithDamageList() {
      if (weapon == null) {
        weapon = new Main.GameRule.Weapon(maxDamage, weaponType, weaponName, minDamage, accurancy, effectName, id);
      } else {
        weapon.Init(damageList, weaponType, weaponName, accurancy, effectName, id);
      }

      return weapon;
    }
    #endregion
  }
}