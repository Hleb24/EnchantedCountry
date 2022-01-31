using System.Collections.Generic;
using Core.SO.Weapon;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.SO.NpcSet {
  [CreateAssetMenu(menuName = "NpcWeaponSet", fileName = "NpcWeaponSet")]
  public class NpcWeaponSet : ScriptableObject {
    [SerializeField]
    private List<WeaponSO> _weaponSet;

    [CanBeNull]
    public Main.GameRule.Weapon GetWeapon(int weaponId) {
      for (var i = 0; i < _weaponSet.Count; i++) {
        if (_weaponSet[i].GetId() == weaponId) {
          return _weaponSet[i].GetWeapon();
        }
      }

      Debug.LogWarning("Оружие не найдено в наборе!");
      return null;
    }
  }
}