using System.Collections.Generic;
using System.IO;
using Aberrance.UnityEngine.Attributes;
using Aberrance.UnityEngine.Saver;
using Core.Main.GameRule.Item;
using Core.Main.NonPlayerCharacters.Item;
using Core.SO.WeaponObjects;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.SO.NpcObjects.NpcSet {
  [CreateAssetMenu(menuName = "NpcWeaponSet", fileName = "NpcWeaponSet")]
  public class NpcWeaponSetSO : ScriptableObject, INpcWeaponSet {
    [SerializeField]
    private List<WeaponSO> _weaponSet;

    [CanBeNull]
    public Weapon GetNpcWeapon(int weaponId) {
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
      string pathToFolder = Path.Combine(Application.persistentDataPath, "NpcWeapon");
      for (var i = 0; i < _weaponSet.Count; i++) {
        string pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "NpcWeapon"), $"{_weaponSet[i].GetWeaponName()}.json");
        saver.Save(_weaponSet[i], pathToFolder, pathToFile);
      }
    }
  }
}