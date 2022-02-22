using System;
using System.Collections.Generic;
using Core.Main.GameRule.Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.WeaponObjects {
  [Serializable, CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
  public class WeaponSO : ScriptableObject {
    [FormerlySerializedAs("accuracy"), FormerlySerializedAs("accurancy"), SerializeField]
    private int _accuracy;
    [FormerlySerializedAs("weaponType"), SerializeField]
    private WeaponType _weaponType;
    [FormerlySerializedAs("weaponName"), SerializeField]
    private string _weaponName;
    [FormerlySerializedAs("minDamage"), SerializeField]
    private float _minDamage;
    [FormerlySerializedAs("maxDamage"), SerializeField]
    private float _maxDamage;
    [FormerlySerializedAs("damageList"), SerializeField]
    private List<float> _damageList;
    [FormerlySerializedAs("effectName"), SerializeField]
    private string _effectName;
    [FormerlySerializedAs("id"), SerializeField]
    private int _id;

    public int GetId() {
      return _id;
    }

    public Weapon GetWeapon() {
      var attack = new Attack(_damageList, _accuracy);
      return new Weapon(attack, _weaponType, _weaponName, _effectName, _id);
    }

    public WeaponType GetWeaponType() {
      return _weaponType;
    }

    public string GetWeaponName() {
      return _weaponName;
    }

    public string GetEffectName() {
      return _effectName;
    }

    public float GetMinDamage() {
      return _minDamage;
    }

    public float GetMaxDamage() {
      return _maxDamage;
    }
  }
}