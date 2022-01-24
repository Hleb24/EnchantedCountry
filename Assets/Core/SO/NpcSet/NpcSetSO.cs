using System;
using System.Collections.Generic;
using System.IO;
using Core.SO.Npc;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.SO.NpcSet {
  [CreateAssetMenu(menuName = "SetOfNpcSO", fileName = "SetOfNpcSO", order = 61)]
  public class NpcSetSO : UnityEngine.ScriptableObject {
    public List<NpcSO> _npcSoList;

    public NpcSO GetNpcSOFromList(int id) {
      foreach (NpcSO npcSO in _npcSoList) {
        if (npcSO.Id == id) {
          return npcSO;
        }
      }

      Debug.LogWarning("NpcSO not found!");
      return null;
    }

    public Main.NonPlayerCharacters.NonPlayerCharacter GetNpcFromList(int id) {
      foreach (NpcSO npc in _npcSoList) {
        if (npc.Id == id) {
          return npc.GetNpc();
        }
      }

      Debug.LogWarning("NpcSO not found!");
      return null;
    }

    private void SaveNpcToJson() {
      var saver = new JsonSaver();
      string _pathToFolder = Path.Combine(Application.persistentDataPath, "Npc");
      string _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Npc"), "Save.json");
      Type type = typeof(NpcSO);
      for (var i = 0; i < _npcSoList.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Npc"), $"{_npcSoList[i].Name}.json");
        saver.Save(_npcSoList[i], _pathToFolder, _pathToFile);
      }
    }
  }
}