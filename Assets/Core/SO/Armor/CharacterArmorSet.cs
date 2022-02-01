using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Core.SO.Armor;
using Core.SO.Npc;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/ArmorSet", fileName = "CharacterArmorSet")]
public class CharacterArmorSet : ScriptableObject {
  public List<ArmorSO> _armorSet;
  
  [Button]
  public void SaveNpcToJson() {
    var saver = new JsonSaver();
    string _pathToFolder = Path.Combine(Application.persistentDataPath, "CharacterArmor");
    string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterArmor"), "Save.json");
    for (var i = 0; i < _armorSet.Count; i++) {
      _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterArmor"), $"{_armorSet[i].armorName}.json");
      saver.Save(_armorSet[i], _pathToFolder, _pathToFile);
    }
  }
}
