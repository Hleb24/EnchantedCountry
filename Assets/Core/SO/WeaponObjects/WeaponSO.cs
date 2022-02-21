using System;
using System.Collections.Generic;
using Core.Main.GameRule;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.WeaponObjects {
  [Serializable, CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
  public class WeaponSO : ScriptableObject {
    [FormerlySerializedAs("accurancy")]
    public int accuracy;
    public WeaponType weaponType = WeaponType.None;
    public string weaponName = "";
    public float minDamage;
    public float maxDamage;
    public List<float> damageList;
    public string effectName = "";
    public int id;

    public int GetId() {
      return id;
    }

    public Weapon GetWeapon() {
      var attack = new Attack(damageList, accuracy);
      return new Weapon(attack, weaponType, weaponName, effectName, id);
    }
  }
}