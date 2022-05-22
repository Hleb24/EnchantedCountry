using System.Collections.Generic;
using System.IO;
using Aberrance.UnityEngine.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.WeaponObjects {
  [CreateAssetMenu(menuName = "Character/WeaponSet", fileName = "CharacterWeaponSet")]
  public class CharacterWeaponSet : ScriptableObject {
    [SerializeField]
    private List<WeaponSO> _weaponSet;
    [SerializeField]
    private List<WeaponSO> _oneHandedSet;
    [SerializeField]
    private List<WeaponSO> _twoHandedSet;
    [SerializeField]
    private List<WeaponSO> _rangeSet;
    [FormerlySerializedAs("_projectliesSet"), SerializeField]
    private List<WeaponSO> _projectilesSet;

    #region SAVE_TO_JSON
    [Button]
    public void SaveNpcToJson() {
      var saver = new JsonSaver();
      string pathToFolder = Path.Combine(Application.persistentDataPath, "CharacterWeapon");
      for (var i = 0; i < _weaponSet.Count; i++) {
        string pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "CharacterWeapon"), $"{_weaponSet[i].GetWeaponName()}.json");
        saver.Save(_weaponSet[i], pathToFolder, pathToFile);
      }
    }
    #endregion
  }
}