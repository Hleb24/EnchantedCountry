using System;
using System.Collections.Generic;
using System.IO;
using Core.SO.Npc;
using Core.SO.Weapon;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
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
    
    [Button]
    public void SaveNpcToJson() {
      var saver = new JsonSaver();
      string _pathToFolder = Path.Combine(Application.persistentDataPath, "NpcWeapon");
      string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), "Save.json");
      Type type = typeof(NpcSO);
      for (var i = 0; i < _weaponSet.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), $"{_weaponSet[i].weaponName}.json");
        saver.Save(_weaponSet[i], _pathToFolder, _pathToFile);
      }
    }
  }
}