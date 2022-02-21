using System;
using System.Collections.Generic;
using Aberrance.Extensions;
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


    private void SetDamageList() {
      if (damageList.Empty()) {
        
      }
    }

    public Weapon GetWeapon() {
      return new Weapon(maxDamage, weaponType, weaponName, minDamage, accuracy, effectName, id);
    }
  }
}