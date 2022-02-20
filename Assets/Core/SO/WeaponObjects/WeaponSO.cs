using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.WeaponObjects {
  [Serializable, CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
  public class WeaponSO : ScriptableObject {
    public Main.GameRule.Weapon.WeaponType weaponType = Main.GameRule.Weapon.WeaponType.None;
    public string weaponName = "";
    public float minDamage;
    public float maxDamage;
    public List<float> damageList;
    [FormerlySerializedAs("accurancy")]
    public int accuracy;
    public string effectName = "";
    public Main.GameRule.Weapon weapon;
    public int id;
    public void OnEnable() {
      InitWeapon();
    }

    public void OnValidate() {
      if (damageList.NotNullAndEmpty()) {
        InitWeaponWithDamageList();
      } else {
        InitWeapon();
      }

      weaponName = name;
    }

    public Main.GameRule.Weapon InitWeapon() {
      if (weapon.Null()) {
        weapon = new Main.GameRule.Weapon(maxDamage, weaponType, weaponName, minDamage, accuracy, effectName, id);
      } else {
        weapon.Init(maxDamage, weaponType, weaponName, minDamage, accuracy, effectName, id);
      }

      return weapon;
    }

    public Main.GameRule.Weapon InitWeaponWithDamageList() {
      if (weapon == null) {
        weapon = new Main.GameRule.Weapon(maxDamage, weaponType, weaponName, minDamage, accuracy, effectName, id);
      } else {
        weapon.Init(damageList, weaponType, weaponName, accuracy, effectName, id);
      }

      return weapon;
    }

    public int GetId() {
      return id;
    }

    public Main.GameRule.Weapon GetWeapon() {
      return weapon;
    }
  }
}