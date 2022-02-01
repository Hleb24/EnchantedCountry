using System;
using System.Collections.Generic;
using System.IO;
using Core.SO.Npc;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.SO.Weapon {
  [CreateAssetMenu(menuName = "Character/WeaponSet", fileName = "CharacterWeaponSet")]
  public class CharacterWeaponSet : ScriptableObject {
    public List<WeaponSO> _weaponSet;
    public List<WeaponSO> _oneHandedSet;
    public List<WeaponSO> _twoHandedSet;
    public List<WeaponSO> _rangeSet;
    public List<WeaponSO> _projectliesSet;
    
    [Button]
    public void SaveNpcToJson() {
      var saver = new JsonSaver(); 
      string _pathToFolder = Path.Combine(Application.persistentDataPath, "CharacterWeapon");
      string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Weapon"), "Save.json");
      Type type = typeof(NpcSO);
      for (var i = 0; i < _weaponSet.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterWeapon"), $"{_weaponSet[i].weaponName}.json");
        saver.Save(_weaponSet[i], _pathToFolder, _pathToFile);
      }
    }
  }
}
