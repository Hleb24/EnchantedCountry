using System.Collections.Generic;
using Core.ScriptableObject.Npc;
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
  }
}