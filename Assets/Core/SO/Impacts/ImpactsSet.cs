using System;
using System.Collections.Generic;
using System.IO;
using Core.Main.GameRule.Impact;
using Core.SO.Npc;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.SO.Impacts {
  [CreateAssetMenu(menuName = "ImpactsSO/ImpactsSet", fileName = "ImpactsSet")]
  public class ImpactsSet : ScriptableObject {
    [SerializeField]
    private List<ImpactsSO> _impactsSet;

    [CanBeNull]
    public Impact<ImpactOnRiskPoints> GetImpactOnRiskPoints(int impactId) {
      for (var i = 0; i < _impactsSet.Count; i++) {
        if (_impactsSet[i].GetId() == impactId) {
          return _impactsSet[i].GetImpact();
        }
      }

      Debug.LogWarning("Импакт не найден!");
      return null;
    }
    
    private void SaveNpcToJson() {
      var saver = new JsonSaver();
      string _pathToFolder = Path.Combine(Application.persistentDataPath, "Ipmacts");
      string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Ipmacts"), "Save.json");
      Type type = typeof(NpcSO);
      for (var i = 0; i < _impactsSet.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Ipmacts"), $"{_impactsSet[i].impactName}.json");
        saver.Save(_impactsSet[i], _pathToFolder, _pathToFile);
      }
    }
  }
}