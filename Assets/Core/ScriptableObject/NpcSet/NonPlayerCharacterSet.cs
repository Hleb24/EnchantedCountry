using System;
using System.Collections.Generic;
using System.IO;
using Core.ScriptableObject.Npc;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.ScriptableObject.NpcSet {
  [CreateAssetMenu(menuName = "SetOfNpcSO", fileName = "SetOfNpcSO", order = 61)]
  public class NonPlayerCharacterSet : UnityEngine.ScriptableObject {
    public List<NonPlayerCharacter> _npcSoList;

    public NonPlayerCharacter GetNpcSOFromList(int id) {
      foreach (NonPlayerCharacter npcSO in _npcSoList) {
        if (npcSO.Id == id) {
          return npcSO;
        }
      }

      Debug.LogWarning("NpcSO not found!");
      return null;
    }

    public Rule.GameRule.NPC.Npc GetNpcFromList(int id) {
      foreach (NonPlayerCharacter npc in _npcSoList) {
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
      Type type = typeof(NonPlayerCharacter);
      for (var i = 0; i < _npcSoList.Count; i++) {
        _pathToFile = Path.Combine(Path.Combine(Application.persistentDataPath, "Npc"), $"{_npcSoList[i].Name}.json");
        saver.Save(_npcSoList[i], _pathToFolder, _pathToFile);
      }
    }
  }
}