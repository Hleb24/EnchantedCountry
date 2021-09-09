using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.SetOfNpc {
  [CreateAssetMenu(menuName = "SetOfNpcSO", fileName = "SetOfNpcSO", order = 61)]
  public class SetOfNpcSO : UnityEngine.ScriptableObject {
    public List<NpcSO.NpcSO> _npcSoList;

    public NpcSO.NpcSO GetNpcSOFromList(int id) {
      foreach (NpcSO.NpcSO npcSO in _npcSoList) {
        if (npcSO.Id == id) {
          return npcSO;
        }
      }

      Debug.LogWarning("NpcSO not found!");
      return null;
    }
    
    public Npc GetNpcFromList(int id) {
      foreach (NpcSO.NpcSO npc in _npcSoList) {
        if (npc.Id == id) {
          return npc.GetNpc();
        }
      }

      Debug.LogWarning("NpcSO not found!");
      return null;
    }
  }
}