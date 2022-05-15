using System.Collections.Generic;
using System.IO;
using Aberrance.UnityEngine.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.SO.ArmorObjects {
  [CreateAssetMenu(menuName = "Character/ArmorSet", fileName = "CharacterArmorSet")]
  public class CharacterArmorSet : ScriptableObject {
    [SerializeField]
    private List<ArmorSO> _armorSet;

    #region SAVE_TO_JSON
    [Button]
    public void SaveNpcToJson() {
      var saver = new JsonSaver();
      string _pathToFolder = Path.Combine(Application.persistentDataPath, "CharacterArmor");
      string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterArmor"), "Save.json");
      for (var i = 0; i < _armorSet.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterArmor"), $"{_armorSet[i].GetArmorName()}.json");
        saver.Save(_armorSet[i], _pathToFolder, _pathToFile);
      }
    }
    #endregion
  }
}