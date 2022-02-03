using System;
using System.Collections.Generic;
using System.IO;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.Fight;
using Core.SO.Npc;
using Core.Support.Attributes;
using Core.Support.SaveSystem.Saver;
using UnityEngine;

namespace Core.SO.NpcSet {
  [CreateAssetMenu(menuName = "SetOfNpcSO", fileName = "SetOfNpcSO", order = 61)]
  public class NpcSetSO : ScriptableObject, INpcModelSet {
    public List<NpcSO> _npcSoList;

    public INpcModel GetNpcModel(int id) {
      foreach (NpcSO npcSO in _npcSoList) {
        if (npcSO.GetId() == id) {
          return npcSO;
        }
      }

      Debug.LogWarning("Модель Npc не найдена!");
      return null;
    }

   

    
  }
}